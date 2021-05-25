using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace ControleDespesas.Api.Swagger
{
    public static class AddSwaggerGen
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.DescribeAllEnumsAsStrings();
                swagger.DescribeAllParametersInCamelCase();
                swagger.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\Swagger.xml");
                swagger.OperationFilter<SwaggerSecurityFilter>();
                swagger.OperationFilter<SwaggerNonBodyParameterFilter>();
                swagger.OperationFilter<SwaggerClassTypeIgnoreFilter<Notificadora>>();
                swagger.OperationFilter<SwaggerJsonIgnoreFilter>();
                swagger.SwaggerDoc("v1", new Info
                {
                    Title = "Controle de Despesas",
                    Version = "v1",
                    Description = "WebApi do Projeto Controle de Despesas",
                    Contact = new Contact
                    {
                        Name = "Lucas Santos",
                        Email = "l_santos@hotmail.com.br",
                        Url = "https://github.com/lsantoss"
                    },
                    License = new License
                    {
                        Name = "MIT License",
                        Url = "https://github.com/lsantoss/ControleDespesas/blob/master/LICENSE"
                    }
                });

                swagger.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Para autenticar use a palavra 'Bearer' + (um espaço entre a palavra Bearer e o Token) + 'Token'",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                //swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", new string[] { }},
                //});
            });

            return services;
        }
    }
}