using ControleDespesas.Api.ActionFilters;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.Comum
{
    [ApiController]
    [AllowAnonymous]
    [TypeFilter(typeof(ChaveApiActionFilterAttribute))]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckRepository _repository;

        public HealthCheckController(IHealthCheckRepository repository)
        {
            _repository = repository;
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
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult HealthCheck()
        {
            _repository.Verificar();

            return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>("Sucesso", "API Controle de Despesas - OK"));
        }        
    }
}