using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.Comum
{
    [Authorize]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// Health Check
        /// </summary>        
        /// <remarks><h2><b>Afere a resposta deste contexto do serviço.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/healthCheck")]
        [ProducesResponseType(typeof(ApiResponse<string, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<string, Notificacao>> HealthCheck()
        {
            var retorno = new ApiResponse<string, Notificacao>("Sucesso", "API Controle de Despesas - OK");
            return StatusCode(StatusCodes.Status200OK, retorno);
        }        
    }
}