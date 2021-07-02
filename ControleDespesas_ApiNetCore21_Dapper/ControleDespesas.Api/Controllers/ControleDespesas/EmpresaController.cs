using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Commands.Output;
using ControleDespesas.Domain.Empresas.Interfaces.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Empresas.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [ApiController]
    public class EmpresaController : BaseController
    {
        private readonly IEmpresaRepository _repository;
        private readonly IEmpresaHandler _handler;

        public EmpresaController(IEmpresaRepository repository, IEmpresaHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        /// <summary>
        /// Empresas
        /// </summary>                
        /// <remarks><h2><b>Lista todas as Empresas.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/empresas")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Empresas()
        {
            return ResultGetList(_repository.Listar());
        }

        /// <summary>
        /// Empresa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Empresa pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Empresa</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/empresas/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult Empresa(long id)
        {
            return ResultGet(_repository.Obter(id));
        }

        /// <summary>
        /// Incluir Empresa 
        /// </summary>                
        /// <remarks><h2><b>Inclui nova Empresa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/empresas")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult EmpresaInserir([FromBody] AdicionarEmpresaCommand command)
        {
            return ResultHandler(_handler.Handler(command));
        }

        /// <summary>
        /// Alterar Empresa
        /// </summary>        
        /// <remarks><h2><b>Altera Empresa na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Empresa</param>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/empresas/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult EmpresaAlterar(long id, [FromBody] AtualizarEmpresaCommand command)
        {
            return ResultHandler(_handler.Handler(id, command));
        }

        /// <summary>
        /// Excluir Empresa
        /// </summary>                
        /// <remarks><h2><b>Exclui Empresa na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Empresa</param>        
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/empresas/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult EmpresaExcluir(long id)
        {
            return ResultHandler(_handler.Handler(id));
        }
    }
}