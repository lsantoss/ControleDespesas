using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ControleDespesas.Api.Swagger
{
    public class SwaggerSecurityFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            context.ApiDescription.TryGetMethodInfo(out var methodInfo);

            if (methodInfo == null)
                return;

            if (methodInfo.MemberType == MemberTypes.Method)
            {
                var allowAnonymous = methodInfo.CustomAttributes.Any(x => x.AttributeType.Name == "AllowAnonymousAttribute");

                if (!allowAnonymous)
                {
                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>()
                    {
                        new Dictionary<string, IEnumerable<string>>
                        {
                            { "Bearer", new string[] { } },
                        }
                    };
                }
            }
        }
    }
}