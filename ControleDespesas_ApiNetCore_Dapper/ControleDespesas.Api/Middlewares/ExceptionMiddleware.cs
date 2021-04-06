using ControleDespesas.Infra.Response;
using ElmahCore;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDespesas.Api.Middlewares
{
    public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				httpContext.RiseError(ex);

				var mensagem = "Erro";
				var notificacao = new Notificacao("Erro", ex.Message);
				var erros = new List<Notificacao>() { notificacao };
				var retorno = new ApiResponse<object>(mensagem, erros);
				var jsonRetorno = JsonConvert.SerializeObject(retorno);

				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
				await httpContext.Response.WriteAsync(jsonRetorno);

				return;
			}
		}
	}

	public static class ExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
			return app;
		}
	}
}