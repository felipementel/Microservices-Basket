using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace SportStore.Microservice.Basket.Api.Settings
{
    public class CustomSwaggerDocumentAttribute : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Info = new OpenApiInfo
            {
                Title = "Sport Store - Basket",
                Version = "v1",
                Description = "Basket",
                TermsOfService = new Uri("https://www.infnet.edu.br"),
                Contact = new OpenApiContact
                {
                    Name = "Felipe Augusto",
                    Email = "felipementel@hotmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "BSD",
                    Url = new Uri("https://pt.wikipedia.org/wiki/Licen%C3%A7a_BSD"),
                }
            };
        }
    }
}