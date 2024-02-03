using SearchEngine.Application.Interfaces;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace SearchEngine.Persistence
{
    public static class PersistenceInstallers
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var credentials = configuration.GetSection("Elastic");
            
            if (credentials["url"] == null) throw new Exception("Elastic url is not set");
            if (credentials["username"] == null) throw new Exception("Elastic username is not set");
            if (credentials["password"] == null) throw new Exception("Elastic password is not set");

            var settings = new ConnectionSettings(new Uri(credentials["url"]!))
                .BasicAuthentication(credentials["username"], credentials["password"])
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                //.DefaultIndex("product");

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericSearchRepo<>));
        }
    }
}
