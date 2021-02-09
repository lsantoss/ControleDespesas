using ControleDespesas.Api.Settings;
using ControleDespesas.Domain.Commands.Pagamento.Input;
using ControleDespesas.Domain.Commands.Pagamento.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Pagamento;
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
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("Pagamento")]
    [ApiController]
    [Authorize]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoRepository _repository;
        private readonly IPagamentoHandler _handler;
        private readonly string _ChaveAPI;

        public PagamentoController(IPagamentoRepository repository, IPagamentoHandler handler, SettingsAPI settings)
        {
            _repository = repository;
            _handler = handler;
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
        [Route("v1/HealthCheck")]
        [ProducesResponseType(typeof(ApiResponse<string, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<string, Notificacao>> PagamentoHealthCheck()
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string, Notificacao>("Sucesso", "API Controle de Despesas - Pagamento OK"));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Pagamentos
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Pagamentos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pagamentos")]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<PagamentoQueryResult>, Notificacao>> Pagamentos([FromBody] ObterPagamentoPorIdPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.Listar(command.IdPessoa);

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
        /// <param name="command">Parâmetro requerido command de Obter pelo Id</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pagamento")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoQueryResult, Notificacao>> Pagamento([FromBody] ObterPagamentoPorIdCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.Obter(command.Id);

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
        /// Pagamentos Concluídos
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Pagamentos Concluídos.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter pelo Id da Pessoa</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/PagamentosConcluidos")]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<PagamentoQueryResult>, Notificacao>> PagamentoConcluido([FromBody] ObterPagamentoPorIdPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.ListarPagamentoConcluido(command.IdPessoa);

                if (result != null && result.Count > 0)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PagamentoQueryResult>, Notificacao>("Lista de pagamentos obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PagamentoQueryResult>, Notificacao>("Nenhum pagamento concluído foi encontrado", new List<PagamentoQueryResult>()));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Pagamentos Pendentes
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Pagamentos Pendentes.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter pelo Id da Pessoa</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/PagamentosPendentes")]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PagamentoQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<PagamentoQueryResult>, Notificacao>> PagamentoPendente([FromBody] ObterPagamentoPorIdPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.ListarPagamentoPendente(command.IdPessoa);

                if (result != null && result.Count > 0)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PagamentoQueryResult>, Notificacao>("Lista de pagamentos obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PagamentoQueryResult>, Notificacao>("Nenhum pagamento pendente foi encontrado", new List<PagamentoQueryResult>()));
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
        /// <param name="command">Parâmetro requerido command de Obter Arquivo Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/ObterArquivoPagamento")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>> ObterArquivoPagamento([FromBody] ObterArquivoPagamentoCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.ObterArquivoPagamento(command.IdPagamento);

                if (result?.ArquivoPagamento != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>("Arquivo encontrado com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoPagamentoQueryResult, Notificacao>("Arquivo não encontrado", result));
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
        /// <param name="command">Parâmetro requerido command de Obter Arquivo Comprovante</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/ObterArquivoComprovante")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>> ObterArquivoComprovante([FromBody] ObterArquivoComprovanteCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.ObterArquivoComprovante(command.IdPagamento);

                if (result?.ArquivoComprovante != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>("Arquivo encontrado com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PagamentoArquivoComprovanteQueryResult, Notificacao>("Arquivo não encontrado", result));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Total Gasto
        /// </summary>                
        /// <remarks><h2><b>Consulta o total gasto pela pessoa.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter Gastos</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/ObterGastos")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoGastosQueryResult, Notificacao>> ObterGastos([FromBody] ObterGastosCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.CalcularValorGastoTotal(command.IdPessoa);

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
        /// Total Gasto no Ano
        /// </summary>                
        /// <remarks><h2><b>Consulta o total gasto pela pessoa durante o ano.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter Gastos Ano</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/ObterGastosAno")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoGastosQueryResult, Notificacao>> ObterGastosAno([FromBody] ObterGastosAnoCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.CalcularValorGastoAno(command.IdPessoa, command.Ano);

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
        /// Total Gasto no Ano/Mês
        /// </summary>                
        /// <remarks><h2><b>Consulta o total gasto pela pessoa durante o ano/mês.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter Gastos Ano/Mes.</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/ObterGastosAnoMes")]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagamentoGastosQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PagamentoGastosQueryResult, Notificacao>> ObterGastosAnoMes([FromBody] ObterGastosAnoMesCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repository.CalcularValorGastoAnoMes(command.IdPessoa, command.Ano, command.Mes);

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
        [Route("v1/PagamentoInserir")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarPagamentoCommandOutput, Notificacao>> PagamentoInserir([FromBody] AdicionarPagamentoCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

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
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/PagamentoAlterar")]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarPagamentoCommandOutput, Notificacao>> PagamentoAlterar([FromBody] AtualizarPagamentoCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

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
        /// Excluir Pagamento
        /// </summary>                
        /// <remarks><h2><b>Exclui Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Delete</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/PagamentoExcluir")]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPagamentoCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarPagamentoCommandOutput, Notificacao>> PagamentoExcluir([FromBody] ApagarPagamentoCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

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
    }
}