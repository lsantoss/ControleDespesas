using ControleDespesas.Api.Settings;
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

			var ignore = new string[] { "/swagger/index.html", "/swagger/v1/swagger.json", "/docs", "/" };

			var ignorado = ignore.Any(x => path.Equals(x));

			var autenticado = ignorado ? true : !string.IsNullOrEmpty(chaveRecebida) && chaveRecebida == _settings.ChaveAPI;

            if (!autenticado)
			{
				var retorno = new
				{
					Sucesso = false,
					Mensagem = "Acesso negado",
					Erros = new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }
				};

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