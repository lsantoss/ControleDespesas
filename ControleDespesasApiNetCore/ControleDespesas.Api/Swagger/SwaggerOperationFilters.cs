using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ControleDespesas.Api.Swagger
{
    public class SwaggerOperationFilters : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "ChaveAutenticacao",
                Description = "Chave de Autenticação de acesso",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}