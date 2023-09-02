using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CleanArchitecture.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwaggerPolicy(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Process Builder API",
                    Description = "An ASP.NET 7 Web API for Creating Dynamic Processes with Forms and Workflow Steps."
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
