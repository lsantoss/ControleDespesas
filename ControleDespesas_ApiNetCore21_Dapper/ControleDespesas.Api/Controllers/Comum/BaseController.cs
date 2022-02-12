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
    [TypeFilter(typeof(ChaveApiActionFilterAttribute))]
    [TypeFilter(typeof(RequestResponseActionFilterAttribute))]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult ResponseGetList<T>(IList<T> lista)
        {
            if (lista.Count > 0)
            {
                var response = new ApiResponse<IList<T>>("Lista obtida com sucesso", lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [NonAction]
        protected IActionResult ResponseGet<T>(T registro)
        {
            if (registro != null)
            {
                var response = new ApiResponse<object>("Registro obtido com sucesso", registro);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [NonAction]
        protected IActionResult ResponseHandler(ICommandResult<Notificacao> result)
        {
            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object>(result.Mensagem, result.Erros));
        }

        [NonAction]
        protected IActionResult ResponseInputNull()
        {
            var mensagem = "Parâmentros inválidos";
            var notificacao = new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos");
            var erros = new List<Notificacao>() { notificacao };
            var response = new ApiResponse<object>(mensagem, erros);
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }

        [NonAction]
        protected IActionResult ResponseNotifications(IReadOnlyCollection<Notificacao> notificacoes)
        {
            var mensagem = "Parâmentros inválidos";
            var response = new ApiResponse<object>(mensagem, notificacoes);
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response);
        }
    }
}