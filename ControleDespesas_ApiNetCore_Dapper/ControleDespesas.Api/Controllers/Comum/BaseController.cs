using ControleDespesas.Api.ActionFilters;
using ControleDespesas.Infra.Response;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.Comum
{
    [Authorize]
    [TypeFilter(typeof(ChaveApiActionFilter))]
    [TypeFilter(typeof(RequestResponseActionFilter))]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult ResultGetList<T>(IList<T> lista)
        {
            string mensagem;

            if (lista.Count > 0)
                mensagem = "Lista obtida com sucesso";
            else
                mensagem = "Nenhum registro cadastrado atualmente";

            var response = new ApiResponse<IList<T>>(mensagem, lista);

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [NonAction]
        protected IActionResult ResultGet<T>(T registro)
        {
            string mensagem;

            if (registro != null)
                mensagem = "Registro obtido com sucesso";
            else
                mensagem = "Nenhum registro cadastrado atualmente";

            var response = new ApiResponse<object>(mensagem, registro);

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [NonAction]
        protected IActionResult ResultHandler(ICommandResult<Notificacao> result)
        {
            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object>(result.Mensagem, result.Erros));
        }

        [NonAction]
        protected IActionResult ResultInputNull()
        {
            var mensagem = "Parâmentros inválidos";
            var notificacao = new Notificacao("Parâmetros de entrada", "ChaveParâmetros de entrada estão nulos");
            var erros = new List<Notificacao>() { notificacao };
            var response = new ApiResponse<object>(mensagem, erros);
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }

        [NonAction]
        protected IActionResult ResultNotifications(IReadOnlyCollection<Notificacao> notificacoes)
        {
            var mensagem = "Parâmentros inválidos";
            var response = new ApiResponse<object>(mensagem, notificacoes);
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response);
        }
    }
}