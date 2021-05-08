using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;


namespace ControleDespesas.Api.Swagger
{
    public class SwaggerJsonIgnoreFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //Newtonsoft.Json.JsonIgnoreAttribute
            JsonIgnoreNewtonsoftJson(operation, context);

            //System.Text.Json.Serialization.JsonIgnoreAttribute
            JsonIgnoreSystemTextJsonSerialization(operation, context);
        }

        private void JsonIgnoreNewtonsoftJson(Operation operation, OperationFilterContext context)
        {
            var propriedadesIgnoradas = context.MethodInfo.GetParameters()
                .SelectMany(param => param.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<Newtonsoft.Json.JsonIgnoreAttribute>() != null))
                .ToList();

            if (!propriedadesIgnoradas.Any()) return;

            foreach (var prop in propriedadesIgnoradas)
                operation.Parameters = operation.Parameters.Where(param => !param.Name.Equals(prop.Name, StringComparison.InvariantCulture)).ToList();
        }

        private void JsonIgnoreSystemTextJsonSerialization(Operation operation, OperationFilterContext context)
        {
            var propriedadesIgnoradas = context.MethodInfo.GetParameters()
                .SelectMany(param => param.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>() != null))
                .ToList();

            if (!propriedadesIgnoradas.Any()) return;

            foreach (var prop in propriedadesIgnoradas)
                operation.Parameters = operation.Parameters.Where(p => !p.Name.Equals(prop.Name, StringComparison.InvariantCulture)).ToList();
        }
    }
}