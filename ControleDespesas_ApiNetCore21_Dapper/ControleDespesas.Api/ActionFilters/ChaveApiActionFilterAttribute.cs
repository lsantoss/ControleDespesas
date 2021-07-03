using ControleDespesas.Infra.Response;
using ControleDespesas.Infra.Settings;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleDespesas.Api.ActionFilters
{
    public class ChaveApiActionFilterAttribute : ActionFilterAttribute
    {
        private readonly SettingsApi _settings;

        public ChaveApiActionFilterAttribute(SettingsApi settings)
        {
            _settings = settings;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IHeaderDictionary header = context.HttpContext.Request.Headers;

            if (!header.ContainsKey("ChaveAPI") || string.IsNullOrEmpty(header["ChaveAPI"]))
            {
                if (header["ChaveAPI"] != _settings.ChaveAPI)
                {
                    var mensagem = "Acesso negado";
                    var notificacao = new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada");
                    var erros = new List<Notificacao>() { notificacao };
                    var retorno = new ApiResponse<object>(mensagem, erros);
                    var jsonRetorno = JsonConvert.SerializeObject(retorno);

                    context.Result = new ContentResult()
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        ContentType = "application/json",
                        Content = jsonRetorno
                    };

                    base.OnActionExecuting(context);
                    return;
                }                
            }

            base.OnActionExecuting(context);
        }
    }
}