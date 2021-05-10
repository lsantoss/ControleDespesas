using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Domain.Pagamentos.Commands.Input;
using ControleDespesas.Domain.Pagamentos.Commands.Output;
using ControleDespesas.Domain.Pagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Query.Input;
using ControleDespesas.Domain.Pagamentos.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [ApiController]
    public class PagamentoController : BaseController
    {
        private readonly IPagamentoRepository _repository;
        private readonly IPagamentoHandler _handler;

        public PagamentoController(IPagamentoRepository repository, IPagamentoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        /// <summary>
        /// Pagamentos
        /// </summary>                
        /// <remarks><h2><b>Lista Pagamentos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Pagamentos([FromQuery] PagamentoQuery query)
        {
            if (query == null)
                return ResultInputNull();

            if (!query.ValidarQuery())
                return ResultNotifications(query.Notificacoes);

            return ResultGetList(_repository.Listar(query.IdPessoa, query.Status));
        }

        /// <summary>
        /// Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o Pagamento pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult Pagamento(int id)
        {
            return ResultGet(_repository.Obter(id));
        }

        /// <summary>
        /// Obter Arquivo para Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o arquivo para pagamento através do Id do Pagamento.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/{id}/arquivo-pagamento")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult ObterArquivoPagamento(int id)
        {
            return ResultGet(_repository.ObterArquivoPagamento(id));
        }

        /// <summary>
        /// Obter Arquivo de Comprovante
        /// </summary>                
        /// <remarks><h2><b>Consulta o arquivo de comprovante através do Id do Pagamento.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/{id}/arquivo-comprovante")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult ObterArquivoComprovante(int id)
        {
            return ResultGet(_repository.ObterArquivoComprovante(id));
        }

        /// <summary>
        /// Total Gasto no Ano/Mês
        /// </summary>                
        /// <remarks><h2><b>Consulta o total gasto pela pessoa durante o ano/mês.</b></h2></remarks>
        /// <param name="query">Parâmetro requerido command de Obter Gastos Ano/Mes.</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/gastos")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult ObterGastos([FromQuery] PagamentoGastosQuery query)
        {
            if (query == null)
                return ResultInputNull();

            if (!query.ValidarQuery())
                return ResultNotifications(query.Notificacoes);

            return ResultGet(_repository.ObterGastos(query.IdPessoa, query.Ano, query.Mes));
        }

        /// <summary>
        /// Incluir Pagamento 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PagamentoInserir([FromBody] AdicionarPagamentoCommand command)
        {
            return ResultHandler(_handler.Handler(command));
        }

        /// <summary>
        /// Alterar Pagamento
        /// </summary>        
        /// <remarks><h2><b>Altera Pagamento na base de dados.</b></h2></remarks> 
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>       
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PagamentoAlterar(int id, [FromBody] AtualizarPagamentoCommand command)
        {
            return ResultHandler(_handler.Handler(id, command));
        }

        /// <summary>
        /// Excluir Pagamento
        /// </summary>                
        /// <remarks><h2><b>Exclui Pagamento na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PagamentoExcluir(int id)
        {
            return ResultHandler(_handler.Handler(id));
        }
    }
}