using System.Threading.Channels;
using Common;
using DataEngine.API.Database;
using DataEngine.API.Endpoints;
using DataEngine.API.Extensions;
using DataEngine.API.Processors;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using DataEngine.API.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));

builder.Services.AddSingleton(Channel.CreateBounded<StorePersonCreate>(100));
builder.Services.AddHostedService<PersonCreatedProcessor>();
builder.Services.AddSingleton(Channel.CreateBounded<StorePersonUpdate>(100));
builder.Services.AddHostedService<PersonUpdatedProcessor>();
builder.Services.AddSingleton(Channel.CreateBounded<StorePersonDelete>(100));
builder.Services.AddHostedService<PersonDeletedProcessor>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });

        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();

app.MapPersonEndpoints();

app.Run();
