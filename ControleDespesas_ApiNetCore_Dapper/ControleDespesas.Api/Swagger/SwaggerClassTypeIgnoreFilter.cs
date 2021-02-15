using LSCode.Validador.ValidacoesNotificacoes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ControleDespesas.Api.Swagger
{
    public class SwaggerClassTypeIgnoreFilter<T> : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo.GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.DeclaringType == typeof(T)))
                .ToList();

            if (!ignoredProperties.Any()) return;

            foreach (var property in ignoredProperties)
            {
                operation.Parameters = operation.Parameters
                    .Where(p => !p.Name.ToLower().Equals(property.Name.ToLower(), StringComparison.InvariantCulture))
                    .ToList();
            }
        }
    }
}