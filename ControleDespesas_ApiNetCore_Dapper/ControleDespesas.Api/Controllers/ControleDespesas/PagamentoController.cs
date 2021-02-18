using ControleDespesas.Api.Settings;
using ControleDespesas.Domain.Commands.Pagamento.Input;
using ControleDespesas.Domain.Commands.Pagamento.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Pagamento.Input;
using ControleDespesas.Domain.Query.Pagamento.Results;
using ElmahCore;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Authorize]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoRepository _repository;
        private readonly IPagamentoHandler _handler;
        private readonly SettingsAPI _settings;

        public PagamentoController(IPagamentoRepository repository, 
                                   IPagamentoHandler handler, 
                                   SettingsAPI settings)
        {
            _repository = repository;
            _handler = handler;
            _settings = settings;
        }

        /// <summary>
        /// Pagamentos
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Pagamentos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<PagamentoQueryResult>, Notificacao>> Pagamentos([FromQuery] PagamentoQuery query)
        {
            try
            {
                if (query == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!query.ValidarQuery())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", query.Notificacoes));

                var result = _repository.Listar(query.IdPessoa, query.Status);

                if (result != null && result.Count > 0)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PagamentoQueryResult>, Notificacao>("Lista de pagamentos obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PagamentoQueryResult>, Notificacao>("Nenhum pagamento cadastrado atualmente", new List<PagamentoQueryResult>()));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o Pagamento.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoQueryResult, Notificacao>> Pagamento(int id)
        {
            try
            {
                var result = _repository.Obter(id);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoQueryResult, Notificacao>("Pagamento obtido com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoQueryResult, Notificacao>("Pagamento não cadastrado", result));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Obter Arquivo para Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o arquivo para pagamento através do id do pagamento.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/{id}/arquivo-pagamento")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoArquivoQueryResult, Notificacao>> ObterArquivoPagamento(int id)
        {
            try
            {
                var result = _repository.ObterArquivoPagamento(id);

                if (result?.Arquivo != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoQueryResult, Notificacao>("Arquivo encontrado com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoQueryResult, Notificacao>("Arquivo não encontrado", result));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Obter Arquivo de Comprovante
        /// </summary>                
        /// <remarks><h2><b>Consulta o arquivo de comprovante através do id do pagamento.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/{id}/arquivo-comprovante")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoArquivoQueryResult, Notificacao>> ObterArquivoComprovante(int id)
        {
            try
            {
                var result = _repository.ObterArquivoComprovante(id);

                if (result?.Arquivo != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoQueryResult, Notificacao>("Arquivo encontrado com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoQueryResult, Notificacao>("Arquivo não encontrado", result));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Total Gasto no Ano/Mês
        /// </summary>                
        /// <remarks><h2><b>Consulta o total gasto pela pessoa durante o ano/mês.</b></h2></remarks>
        /// <param name="query">Parâmetro requerido command de Obter Gastos Ano/Mes.</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pagamentos/gastos")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoGastosQueryResult, Notificacao>> ObterGastos([FromQuery] PagamentoGastosQuery query)
        {
            try
            {
                if (query == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!query.ValidarQuery())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", query.Notificacoes));

                var result = _repository.ObterGastos(query.IdPessoa, query.Ano, query.Mes);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoGastosQueryResult, Notificacao>("Cáculo obtido com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoGastosQueryResult, Notificacao>("Cáculo não obtido", result));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Incluir Pagamento 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>> PagamentoInserir([FromBody] AdicionarPagamentoCommand command)
        {
            try
            {
                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _handler.Handler(command);

                if (result.Sucesso)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
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
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>> PagamentoAlterar(int id, [FromBody] AtualizarPagamentoCommand command)
        {
            try
            {
                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                command.Id = id;

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _handler.Handler(command);

                if (result.Sucesso)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Excluir Pagamento
        /// </summary>                
        /// <remarks><h2><b>Exclui Pagamento na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/pagamentos/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarPagamentoCommandOutput, Notificacao>> PagamentoExcluir(int id)
        {
            try
            {
                var result = _handler.Handler(id);

                if (result.Sucesso)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }
    }
}