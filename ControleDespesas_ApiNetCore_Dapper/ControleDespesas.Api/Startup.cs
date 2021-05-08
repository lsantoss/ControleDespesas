using ControleDespesas.Api.Middlewares;
using ControleDespesas.Api.Swagger;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Helpers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Settings;
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

            services.AddSwagger();

            #endregion

            #region Autenticação JWT

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settingsAPI.ChaveJWT)),
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
            services.AddTransient<ILogRequestResponseRepository, LogRequestResponseRepository>();
            services.AddTransient<IHealthCheckRepository, HealthCheckRepository>();

            #endregion

            #region Handler

            services.AddTransient<IPessoaHandler, PessoaHandler>();
            services.AddTransient<IEmpresaHandler, EmpresaHandler>();
            services.AddTransient<ITipoPagamentoHandler, TipoPagamentoHandler>();
            services.AddTransient<IPagamentoHandler, PagamentoHandler>();
            services.AddTransient<IUsuarioHandler, UsuarioHandler>();

            #endregion

            #region Helpers

            services.AddTransient<ITokenJwtHelper, TokenJwtHelper>();

            #endregion

            #region Indented Pretty Print Formatting JSON

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
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

            app.UseExceptionMiddleware();

            app.UseElmah();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleDespesas"); });

            app.UseMvc();
        }
    }
}