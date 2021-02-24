using ControleDespesas.Api.Settings;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Api.Services
{
    public class ChaveAPIMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly SettingsAPI _settings;

		public ChaveAPIMiddleware(RequestDelegate next, SettingsAPI settings)
		{
			_next = next;
			_settings = settings;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			var path = httpContext.Request.Path.Value;
			var chaveRecebida = httpContext.Request.Headers["ChaveAPI"].ToString();

			var ignore = new string[] {
				"/",
				"/docs",
				"/swagger", 
				"/swagger/index.html", 
				"/swagger/v1/swagger.json", 
				"/swagger/swagger-ui-bundle.js",
				"/swagger/swagger-ui.css",
				"/swagger/swagger-ui-standalone-preset.js",
				"/swagger/favicon-32x32.png"
			};

			var ignorado = ignore.Any(x => path.Equals(x));

			var autenticado = ignorado ? true : !string.IsNullOrEmpty(chaveRecebida) && chaveRecebida == _settings.ChaveAPI;

            if (!autenticado)
			{
				var mensagem = "Acesso negado";
				var notificacao = new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada");
				var erros = new List<Notificacao>() { notificacao };

				var retorno = new ApiResponse<object, Notificacao>(mensagem, erros);

				var jsonRetorno = JsonConvert.SerializeObject(retorno);

				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await httpContext.Response.WriteAsync(jsonRetorno);

				return;
			}

			await _next(httpContext);
		}
	}

	public static class ChaveAPIMiddlerwareExtensions
	{
		public static IApplicationBuilder UseAPITokenMiddlerware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ChaveAPIMiddleware>();
			return app;
		}
	}
}