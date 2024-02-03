using DataEngine.API.Database;
using DataEngine.API.Endpoints;
using DataEngine.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();

app.MapPersonEndpoints();

app.Run();
