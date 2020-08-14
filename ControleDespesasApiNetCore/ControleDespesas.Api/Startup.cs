using ControleDespesas.Api.Services;
using ControleDespesas.Api.Settings;
using ControleDespesas.Api.Swagger;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using LSCode.ConexoesBD.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace ControleDespesas.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();

            #region AppSettings
            services.Configure<SettingsInfraData>(options => Configuration.GetSection("SettingsInfraData").Bind(options));
            services.Configure<SettingsAPI>(options => Configuration.GetSection("SettingsAPI").Bind(options));
            #endregion

            #region DataContext
            services.AddScoped<DbContext>();
            #endregion

            #region Repositorios
            services.AddTransient<IPessoaRepositorio, PessoaRepositorio>();
            services.AddTransient<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddTransient<ITipoPagamentoRepositorio, TipoPagamentoRepositorio>();
            services.AddTransient<IPagamentoRepositorio, PagamentoRepositorio>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            #endregion

            #region Handler
            services.AddTransient<PessoaHandler, PessoaHandler>();
            services.AddTransient<EmpresaHandler, EmpresaHandler>();
            services.AddTransient<TipoPagamentoHandler, TipoPagamentoHandler>();
            services.AddTransient<PagamentoHandler, PagamentoHandler>();
            services.AddTransient<UsuarioHandler, UsuarioHandler>();
            #endregion

            #region Services
            services.AddTransient<TokenJWTService, TokenJWTService>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                //c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.SwaggerDoc("v1", new Info { Title = "Controle de Despesas", Version = "v1", Description = "WebApi do Projeto Controle de Despesas", });
                c.OperationFilter<SwaggerOperationFilters>();
            });
            #endregion

            #region JWT
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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleDespesas"); });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }

        protected static string GetXmlCommentsPath() => String.Format(@"{0}\Swagger.xml", AppDomain.CurrentDomain.BaseDirectory);
    }
}