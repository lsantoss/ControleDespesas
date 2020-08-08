using System;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Infra.Data;
using ControleDespesas.Infra.Data.Repositorio;
using LSCode.ConexoesBD.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ControleDespesas.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();

            #region AppSettings
            services.Configure<SettingsInfraData>(options => Configuration.GetSection("SettingsInfraData").Bind(options));
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
            //services.AddTransient<PagamentoHandler, PagamentoHandler>();
            //services.AddTransient<UsuarioHandler, UsuarioHandler>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Controle de Despesas",
                        Version = "v1",
                        Description = "WebApi do Projeto Controle de Despesas",
                    });
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleDespesas");
            });

            app.UseMvc();
        }

        protected static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\Swagger.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}