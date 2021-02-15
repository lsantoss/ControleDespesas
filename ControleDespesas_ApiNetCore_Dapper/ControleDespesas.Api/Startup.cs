using ControleDespesas.Api.Services;
using ControleDespesas.Api.Settings;
using ControleDespesas.Api.Swagger;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Interfaces.Services;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Data.Settings;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using LSCode.ConexoesBD.DataContexts;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDespesas.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            #region AppSettings
            SettingsAPI settingsAPI = new SettingsAPI();
            Configuration.GetSection("SettingsAPI").Bind(settingsAPI);
            services.AddSingleton(settingsAPI);

            SettingsInfraData settingsInfraData = new SettingsInfraData();
            Configuration.GetSection("SettingsInfraData").Bind(settingsInfraData);
            services.AddSingleton(settingsInfraData);
            #endregion

            #region Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.DescribeAllEnumsAsStrings();
                swagger.DescribeAllParametersInCamelCase();
                swagger.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\Swagger.xml");
                swagger.OperationFilter<SwaggerNonBodyParameterFilter>();
                swagger.OperationFilter<SwaggerClassTypeIgnoreFilter<Notificadora>>();
                //swagger.OperationFilter<SwaggerJsonIgnoreFilter>();
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

                swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                });
            });
            #endregion

            #region Autenticação JWT
            var keyString = Configuration.GetSection("SettingsAPI:ChaveJWT").Get<string>();
            var key = Encoding.ASCII.GetBytes(keyString);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion

            #region Log Elmah
            //Log em Memória
            //services.AddElmah(options => { options.Path = @"elmah"; });

            ////Log salvando em XML
            //services.AddElmah<XmlFileErrorLog>(options => { options.LogPath = "~/log"; });

            //Log salvando no banco de dados
            services.AddElmah<SqlErrorLog>(options => { options.ConnectionString = Configuration["SettingsInfraData:ConnectionString"]; });
            #endregion

            #region DataContext
            services.AddScoped<DataContext>();
            #endregion

            #region Repositories
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<ITipoPagamentoRepository, TipoPagamentoRepository>();
            services.AddTransient<IPagamentoRepository, PagamentoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            #endregion

            #region Handler
            services.AddTransient<IPessoaHandler, PessoaHandler>();
            services.AddTransient<IEmpresaHandler, EmpresaHandler>();
            services.AddTransient<ITipoPagamentoHandler, TipoPagamentoHandler>();
            services.AddTransient<IPagamentoHandler, PagamentoHandler>();
            services.AddTransient<IUsuarioHandler, UsuarioHandler>();
            #endregion

            #region Services
            services.AddTransient<ITokenJWTService, TokenJWTService>();
            #endregion

            #region Indented Pretty Print Formatting JSON
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => {
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseElmah();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleDespesas"); });

            app.UseMvc();
        }
    }
}