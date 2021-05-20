using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace OnBoardingTracker.WebApi.Infrastructure.Swagger.OperationFilters
{
    public class AddDefaultVersionHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation).ToString());
            }

            var parameter = operation.Parameters.First(x => x.Name == "api-version");
            parameter.Schema = new OpenApiSchema
            {
                Type = "String",
                Default = new OpenApiString("1.0")
            };
        }
    }
}
