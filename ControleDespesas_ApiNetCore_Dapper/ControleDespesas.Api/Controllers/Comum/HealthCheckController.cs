using ControleDespesas.Infra.Settings;
using ElmahCore;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.Comum
{
    [Authorize]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class HealthCheckController : ControllerBase
    {
        private readonly string _ChaveAPI;

        public HealthCheckController(SettingsAPI settings)
        {
            _ChaveAPI = settings.ChaveAPI;
        }

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
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string, Notificacao>("Sucesso", "API Controle de Despesas - OK"));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }        
    }
}