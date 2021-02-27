using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Commands.Empresa.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Empresa.Results;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Authorize]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EmpresaController : ControllerBase
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
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/empresas")]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<EmpresaQueryResult>, Notificacao>> Empresas()
        {
            var result = _repository.Listar();
            var mensagem = result.Count > 0 ? "Lista de empresas obtida com sucesso" : "Nenhuma empresa cadastrada atualmente";
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<EmpresaQueryResult>, Notificacao>(mensagem, result));
        }

        /// <summary>
        /// Empresa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Empresa pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Empresa</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/empresas/{id}")]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<EmpresaQueryResult, Notificacao>> Empresa(int id)
        {
            var result = _repository.Obter(id);
            var mensagem = result != null ? "Empresa obtida com sucesso" : "Empresa não cadastrada";
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<EmpresaQueryResult, Notificacao>(mensagem, result));
        }

        /// <summary>
        /// Incluir Empresa 
        /// </summary>                
        /// <remarks><h2><b>Inclui nova Empresa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/empresas")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>> EmpresaInserir([FromBody] AdicionarEmpresaCommand command)
        {               
            var result = _handler.Handler(command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
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
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/empresas/{id}")]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>> EmpresaAlterar(int id, [FromBody] AtualizarEmpresaCommand command)
        {
            var result = _handler.Handler(id, command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }

        /// <summary>
        /// Excluir Empresa
        /// </summary>                
        /// <remarks><h2><b>Exclui Empresa na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Empresa</param>        
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/empresas/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarEmpresaCommandOutput, Notificacao>> EmpresaExcluir(int id)
        {
            var result = _handler.Handler(id);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }
    }
}