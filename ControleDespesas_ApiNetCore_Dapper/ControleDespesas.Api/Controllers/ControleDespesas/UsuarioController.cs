﻿using ControleDespesas.Domain.Commands.Usuario.Input;
using ControleDespesas.Domain.Commands.Usuario.Output;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Usuario.Results;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioHandler _handler;

        public UsuarioController(IUsuarioRepository repository, IUsuarioHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        /// <summary>
        /// Usuários
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Usuários.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios")]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<UsuarioQueryResult>, Notificacao>> Usuarios()
        {
            var result = _repository.Listar();
            var mensagem = result.Count > 0 ? "Lista de usuários obtida com sucesso" : "Nenhum usuário cadastrado atualmente";
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<UsuarioQueryResult>, Notificacao>(mensagem, result));
        }

        /// <summary>
        /// Usuário
        /// </summary>                
        /// <remarks><h2><b>Consulta o Usuário pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Usuário</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios/{id}")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<UsuarioQueryResult, Notificacao>> Usuario(int id)
        {
            var result = _repository.Obter(id);
            var mensagem = result != null ? "Usuário obtido com sucesso" : "Usuário não cadastrado";
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<UsuarioQueryResult, Notificacao>(mensagem, result));
        }

        /// <summary>
        /// Incluir Usuário 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Usuário na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/usuarios")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<UsuarioCommandOutput, Notificacao>> UsuarioInserir([FromBody] AdicionarUsuarioCommand command)
        {
            var result = _handler.Handler(command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }

        /// <summary>
        /// Alterar Usuário
        /// </summary>        
        /// <remarks><h2><b>Altera Usuário na base de dados.</b></h2></remarks>  
        /// <param name="id">Parâmetro requerido Id do Usuário</param>      
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/usuarios/{id}")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<UsuarioCommandOutput, Notificacao>> UsuarioAlterar(int id, [FromBody] AtualizarUsuarioCommand command)
        {
            var result = _handler.Handler(id, command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }

        /// <summary>
        /// Excluir Usuário
        /// </summary>                
        /// <remarks><h2><b>Exclui Usuário na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Usuário</param>      
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/usuarios/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<CommandOutput, Notificacao>> UsuarioExcluir(int id)
        {
            var result = _handler.Handler(id);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }

        /// <summary>
        /// Login Usuário
        /// </summary>                
        /// <remarks><h2><b>Loga Usuário com credências corretas.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Login</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("v1/usuarios/login")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioTokenQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioTokenQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioTokenQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioTokenQueryResult, Notificacao>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioTokenQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<UsuarioTokenQueryResult, Notificacao>> UsuarioLogin([FromBody] LoginUsuarioCommand command)
        {
            var result = _handler.Handler(command);

            if (result.Sucesso)
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Dados));
            else
                return StatusCode(result.StatusCode, new ApiResponse<object, Notificacao>(result.Mensagem, result.Erros));
        }
    }
}