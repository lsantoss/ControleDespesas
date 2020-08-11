using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Usuario;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly UsuarioHandler _handler;
        private readonly string _ChaveAutenticacao;

        public UsuarioController(IUsuarioRepositorio repositorio, UsuarioHandler handler, IOptions<SettingsAPI> options)
        {
            _repositorio = repositorio;
            _handler = handler;
            _ChaveAutenticacao = options.Value.ChaveAutorizacao;
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
        public ActionResult<ApiResponse<string, Notificacao>> UsuarioHealthCheck()
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string, Notificacao>("Sucesso", "API Controle de Despesas - Usuário OK"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Usuarios
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Usuários.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Usuarios")]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<UsuarioQueryResult>, Notificacao>> Usuarios()
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

                var result = _repositorio.Listar();

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<UsuarioQueryResult>, Notificacao>("Lista de usuários obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<UsuarioQueryResult>, Notificacao>("Nenhum usuário cadastrado atualmente", new List<UsuarioQueryResult>()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Usuario
        /// </summary>                
        /// <remarks><h2><b>Consulta o Usuário.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter pelo Id</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Usuario")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<UsuarioQueryResult, Notificacao>> Usuario([FromBody] ObterUsuarioPorIdCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave de Validação", "Esta a chave não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repositorio.Obter(command.Id);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<UsuarioQueryResult, Notificacao>("Usuário obtido com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<UsuarioQueryResult, Notificacao>("Usuário não cadastrado", result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Incluir Usuario 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Usuário na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/UsuarioInserir")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarUsuarioCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarUsuarioCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarUsuarioCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarUsuarioCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarUsuarioCommandOutput, Notificacao>> UsuarioInserir([FromBody] AdicionarUsuarioCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
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
        /// Alterar Usuario
        /// </summary>        
        /// <remarks><h2><b>Altera Usuário na base de dados.</b></h2></remarks>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/UsuarioAlterar")]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarUsuarioCommandOutput, Notificacao>> UsuarioAlterar([FromBody] AtualizarUsuarioCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
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
        /// Excluir Usuario
        /// </summary>                
        /// <remarks><h2><b>Exclui Usuário na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Delete</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/UsuarioExcluir")]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarUsuarioCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarUsuarioCommandOutput, Notificacao>> UsuarioExcluir([FromBody] ApagarUsuarioCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
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
        /// Login Usuario
        /// </summary>                
        /// <remarks><h2><b>Loga Usuário com credências corretas.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Login</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/UsuarioLogin")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<UsuarioQueryResult, Notificacao>> UsuarioLogin([FromBody] LoginUsuarioCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAutenticacao"].ToString() != _ChaveAutenticacao)
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