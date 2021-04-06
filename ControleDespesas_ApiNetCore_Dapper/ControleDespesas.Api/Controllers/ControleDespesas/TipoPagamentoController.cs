using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Domain.Commands.TipoPagamento.Input;
using ControleDespesas.Domain.Commands.TipoPagamento.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [ApiController]
    public class TipoPagamentoController : BaseController
    {
        private readonly ITipoPagamentoRepository _repository;
        private readonly ITipoPagamentoHandler _handler;

        public TipoPagamentoController(ITipoPagamentoRepository repository, ITipoPagamentoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        /// <summary>
        /// Tipos de Pagamentos
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Tipos de Pagamentos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/tipos-pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<List<TipoPagamentoQueryResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<TipoPagamentoQueryResult>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<TipoPagamentoQueryResult>>), StatusCodes.Status500InternalServerError)]
        public IActionResult TipoPagamentos()
        {
            return ResultGetList(_repository.Listar());
        }

        /// <summary>
        /// Tipo de Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o Tipo de Pagamento pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Tipo de Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/tipos-pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult TipoPagamento(int id)
        {
            return ResultGet(_repository.Obter(id));
        }

        /// <summary>
        /// Incluir Tipo de Pagamento 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Tipo de Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/tipos-pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult TipoPagamentoInserir([FromBody] AdicionarTipoPagamentoCommand command)
        {
            return ResultHandler(_handler.Handler(command));
        }

        /// <summary>
        /// Alterar Tipo de Pagamento
        /// </summary>        
        /// <remarks><h2><b>Altera Tipo de Pagamento na base de dados.</b></h2></remarks>  
        /// <param name="id">Parâmetro requerido Id do Tipo de Pagamento</param>      
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/tipos-pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult TipoPagamentoAlterar(int id, [FromBody] AtualizarTipoPagamentoCommand command)
        {
            return ResultHandler(_handler.Handler(id, command));
        }

        /// <summary>
        /// Excluir Tipo de Pagamento
        /// </summary>                
        /// <remarks><h2><b>Exclui Tipo de Pagamento na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Tipo de Pagamento</param>  
        /// <response code="200">OK Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/tipos-pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult TipoPagamentoExcluir(int id)
        {
            return ResultHandler(_handler.Handler(id));
        }
    }
}