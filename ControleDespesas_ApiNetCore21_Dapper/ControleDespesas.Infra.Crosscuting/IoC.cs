using ControleDespesas.Domain.Empresas.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Handlers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Handlers;
using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Handlers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Settings;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ControleDespesas.Infra.Crosscuting
{
    public static class IoC
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            SettingsApi settingsAPI = new SettingsApi();
            configuration.GetSection("SettingsAPI").Bind(settingsAPI);
            services.AddSingleton(settingsAPI);

            SettingsInfraData settingsInfraData = new SettingsInfraData();
            configuration.GetSection("SettingsInfraData").Bind(settingsInfraData);
            services.AddSingleton(settingsInfraData);

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["SettingsAPI:ChaveJWT"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<ITipoPagamentoRepository, TipoPagamentoRepository>();
            services.AddTransient<IPagamentoRepository, PagamentoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ILogRequestResponseRepository, LogRequestResponseRepository>();
            services.AddTransient<IHealthCheckRepository, HealthCheckRepository>();
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<IPessoaHandler, PessoaHandler>();
            services.AddTransient<IEmpresaHandler, EmpresaHandler>();
            services.AddTransient<ITipoPagamentoHandler, TipoPagamentoHandler>();
            services.AddTransient<IPagamentoHandler, PagamentoHandler>();
            services.AddTransient<IUsuarioHandler, UsuarioHandler>();
            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<ITokenJwtHelper, TokenJwtHelper>();
            return services;
        }

        public static IServiceCollection AddElmahCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmah<SqlErrorLog>(options => 
            {
                options.ConnectionString = configuration["SettingsInfraData:ConnectionString"]; 
            });

            return services;
        }
    }
}