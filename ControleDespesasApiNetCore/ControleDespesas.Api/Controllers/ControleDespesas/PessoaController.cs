using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("Pessoa")]
    [ApiController]
    [Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepositorio _repositorio;
        private readonly PessoaHandler _handler;
        private readonly string _ChaveAPI;

        public PessoaController(IPessoaRepositorio repositorio, PessoaHandler handler, IOptions<SettingsAPI> options)
        {
            _repositorio = repositorio;
            _handler = handler;
            _ChaveAPI = options.Value.ChaveAPI;
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
        public ActionResult<ApiResponse<string, Notificacao>> PessoaHealthCheck()
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string, Notificacao>("Sucesso", "API Controle de Despesas - Pessoa OK"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Pessoas
        /// </summary>                
        /// <remarks><h2><b>Lista todas as Pessoas.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pessoas")]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<PessoaQueryResult>, Notificacao>> Pessoas()
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

                var result = _repositorio.Listar();

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PessoaQueryResult>, Notificacao>("Lista de pessoas obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PessoaQueryResult>, Notificacao>("Nenhuma pessoa cadastrada atualmente", new List<PessoaQueryResult>()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Pessoa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Pessoa.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter pelo Id</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pessoa")]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PessoaQueryResult, Notificacao>> Pessoa([FromBody] ObterPessoaPorIdCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repositorio.Obter(command.Id);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PessoaQueryResult, Notificacao>("Pessoa obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PessoaQueryResult, Notificacao>("Pessoa não cadastrada", result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Incluir Pessoa 
        /// </summary>                
        /// <remarks><h2><b>Inclui nova Pessoa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/PessoaInserir")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarPessoaCommandOutput, Notificacao>> PessoaInserir([FromBody] AdicionarPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Alterar Pessoa
        /// </summary>        
        /// <remarks><h2><b>Altera Pessoa na base de dados.</b></h2></remarks>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/PessoaAlterar")]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarPessoaCommandOutput, Notificacao>> PessoaAlterar([FromBody] AtualizarPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Excluir Pessoa
        /// </summary>                
        /// <remarks><h2><b>Exclui Pessoa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Delete</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/PessoaExcluir")]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarPessoaCommandOutput, Notificacao>> PessoaExcluir([FromBody] ApagarPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }
    }
}