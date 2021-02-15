using ControleDespesas.Api.Settings;
using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Commands.Pessoa.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Pessoa;
using ControleDespesas.Domain.Query.Pessoa.Input;
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
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _repository;
        private readonly IPessoaHandler _handler;
        private readonly SettingsAPI _settings;

        public PessoaController(IPessoaRepository repository, 
                                IPessoaHandler handler, 
                                SettingsAPI settings)
        {
            _repository = repository;
            _handler = handler;
            _settings = settings;
        }

        /// <summary>
        /// Pessoas
        /// </summary>                
        /// <remarks><h2><b>Lista todas as Pessoas.</b></h2></remarks>
        /// <param name="query">Parâmetro requerido query de busca</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pessoas")]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<PessoaQueryResult>, Notificacao>> Pessoas([FromQuery] ObterPessoasQuery query)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _settings.ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (query == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!query.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", query.Notificacoes));

                var result = _repository.Listar(query.IdUsuario);

                if (result != null && result.Count > 0)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PessoaQueryResult>, Notificacao>("Lista de pessoas obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<PessoaQueryResult>, Notificacao>("Nenhuma pessoa cadastrada atualmente", new List<PessoaQueryResult>()));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Pessoa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Pessoa.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pessoas/{id}")]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<PessoaQueryResult, Notificacao>> Pessoa(int id)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _settings.ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                var result = _repository.Obter(id);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PessoaQueryResult, Notificacao>("Pessoa obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<PessoaQueryResult, Notificacao>("Pessoa não cadastrada", result));
            }
            catch (Exception e)
            {
                HttpContext.RiseError(e);
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
        [Route("v1/pessoas")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarPessoaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarPessoaCommandOutput, Notificacao>> PessoaInserir([FromBody] AdicionarPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _settings.ChaveAPI)
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
        /// Alterar Pessoa
        /// </summary>        
        /// <remarks><h2><b>Altera Pessoa na base de dados.</b></h2></remarks> 
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>       
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/pessoas/{id}")]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarPessoaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarPessoaCommandOutput, Notificacao>> PessoaAlterar(int id, [FromBody] AtualizarPessoaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _settings.ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

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
        /// Excluir Pessoa
        /// </summary>                
        /// <remarks><h2><b>Exclui Pessoa na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>       
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/pessoas/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarPessoaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarPessoaCommandOutput, Notificacao>> PessoaExcluir(int id)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _settings.ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

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