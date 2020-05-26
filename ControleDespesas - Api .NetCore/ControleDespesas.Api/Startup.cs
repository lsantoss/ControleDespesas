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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();

            services.Configure<SettingsInfraData>(options => Configuration.GetSection("SettingsInfraData").Bind(options));

            services.AddTransient<DbContext, DbContext>();

            services.AddTransient<IPessoaRepositorio, PessoaRepositorio>();
            services.AddTransient<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddTransient<ITipoPagamentoRepositorio, TipoPagamentoRepositorio>();
            services.AddTransient<IPagamentoRepositorio, PagamentoRepositorio>();

            services.AddTransient<PessoaHandler, PessoaHandler>();
            services.AddTransient<EmpresaHandler, EmpresaHandler>();
            services.AddTransient<TipoPagamentoHandler, TipoPagamentoHandler>();
            services.AddTransient<PagamentoHandler, PagamentoHandler>();

            //Configurando o serviço de documentação do Swagger
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleDespesas");
            });

            app.UseMvc();
        }

        // Documentação XML para Swagger e Redoc
        protected static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\Swagger.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}