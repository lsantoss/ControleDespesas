using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ControleDespesas.Api.Swagger
{
    public class SwaggerNonBodyParameterFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "ChaveAPI",
                Description = "Chave de Acesso da API",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}