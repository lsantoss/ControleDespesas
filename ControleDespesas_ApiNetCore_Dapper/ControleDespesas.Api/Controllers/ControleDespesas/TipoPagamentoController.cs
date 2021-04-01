using ControleDespesas.Domain.Commands.TipoPagamento.Input;
using ControleDespesas.Domain.Commands.TipoPagamento.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using ControleDespesas.Infra.Commands;
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
    public class TipoPagamentoController : ControllerBase
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
        [ProducesResponseType(typeof(ApiResponse<List<TipoPagamentoQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<TipoPagamentoQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<TipoPagamentoQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<TipoPagamentoQueryResult>, Notificacao>> TipoPagamentos()
        {
            var result = _repository.Listar();
            var mensagem = result.Count > 0 ? "Lista de tipos de pagamento obtida com sucesso" : "Nenhum tipo de pagamento cadastrado atualmente";
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<TipoPagamentoQueryResult>, Notificacao>(mensagem, result));
        }

        /// <summary>
        /// Tipo de Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o Tipo de Pagamento.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Tipo de Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/tipos-pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<TipoPagamentoQueryResult, Notificacao>> TipoPagamento(int id)
        {
            var result = _repository.Obter(id);
            var mensagem = result != null ? "Tipo de pagameto obtido com sucesso" : "Tipo de pagamento não cadastrado";
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<TipoPagamentoQueryResult, Notificacao>(mensagem, result));
        }

        /// <summary>
        /// Incluir Tipo de Pagamento 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Tipo de Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/tipos-pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<TipoPagamentoCommandOutput, Notificacao>> TipoPagamentoInserir([FromBody] AdicionarTipoPagamentoCommand command)
        {
            var result = _handler.Handler(command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
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
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/tipos-pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TipoPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<TipoPagamentoCommandOutput, Notificacao>> TipoPagamentoAlterar(int id, [FromBody] AtualizarTipoPagamentoCommand command)
        {
            var result = _handler.Handler(id, command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }

        /// <summary>
        /// Excluir Tipo de Pagamento
        /// </summary>                
        /// <remarks><h2><b>Exclui Tipo de Pagamento na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Tipo de Pagamento</param>  
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/tipos-pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<CommandOutput, Notificacao>> TipoPagamentoExcluir(int id)
        {
            var result = _handler.Handler(id);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }
    }
}