
using Elasticsearch.Net;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Nest;
using ProcessEngine.API.Consumers;
using ProcessEngine.API.Mapper;
using ProcessEngine.API.Search;

namespace ProcessEngine.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.AddConsumer<PersonCreatedConsumer>();
                busConfigurator.AddConsumer<PersonUpdatedConsumer>();
                busConfigurator.AddConsumer<PersonDeletedConsumer>();

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

            var credentials = builder.Configuration.GetSection("Elastic");

            if (credentials["url"] == null) throw new Exception("Elastic url is not set");
            if (credentials["username"] == null) throw new Exception("Elastic username is not set");
            if (credentials["password"] == null) throw new Exception("Elastic password is not set");

            var settings = new ConnectionSettings(new Uri(credentials["url"]!))
                .BasicAuthentication(credentials["username"], credentials["password"])
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
            //.DefaultIndex("product");

            builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));
            builder.Services.AddScoped(typeof(ISearchMutatorRepository<>), typeof(SearchMutatorRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
