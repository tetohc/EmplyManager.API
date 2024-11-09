using EmplyManager.Entities.Models;
using System.Reflection;

namespace EmplyManager.API.Services
{
    /// <summary>
    /// Provides methods to configure services for the API.
    /// </summary>
    public static class ApiConfigurationService
    {
        /// <summary>
        /// Configures the database connecion using the provided connection string.
        /// </summary>
        /// <param name="services">Service collection for configuration.</param>
        /// <param name="configuration">Configuration settings used to retrieve the connection string.</param>
        /// <returns>The service collection after the database connection string has been configured.</returns>
        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringModel>(configuration.GetSection("ConnectionStrings"));
            return services;
        }

        /// <summary>
        /// Configures Swagger for API documentation.
        /// </summary>
        /// <param name="services">Service collection for configuration.</param>
        /// <returns>Returns the service collection with the added configurations.</returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Emply Manager API",
                    Description = "APIs for employee management with CRUD operations.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "",
                        Email = "",
                        Url = new Uri("https://www.google.com")
                    }
                });

                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
            });
            return services;
        }
    }
}