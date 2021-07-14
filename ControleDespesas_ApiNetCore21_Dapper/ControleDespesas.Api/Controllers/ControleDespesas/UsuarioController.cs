﻿using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Commands.Output;
using ControleDespesas.Domain.Usuarios.Interfaces.Handlers;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Query.Parameters;
using ControleDespesas.Domain.Usuarios.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [ApiController]
    public class UsuarioController : BaseController
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
        /// <param name="query">Parâmetro requerido query de busca</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<List<UsuarioQueryResult>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Usuarios([FromQuery] ObterUsuarioQuery query)
        {
            return !query.RegistrosFilhos
                ? ResultGetList(_repository.Listar())
                : ResultGetList(_repository.ListarContendoRegistrosFilhos());
        }

        /// <summary>
        /// Usuário
        /// </summary>                
        /// <remarks><h2><b>Consulta o Usuário pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Usuário</param>
        /// <param name="query">Parâmetro requerido query de busca</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult Usuario(long id, [FromQuery] ObterUsuarioQuery query)
        {
            return !query.RegistrosFilhos
                ? ResultGet(_repository.Obter(id)) 
                : ResultGet(_repository.ObterContendoRegistrosFilhos(id));
        }

        /// <summary>
        /// Incluir Usuário 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Usuário na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/usuarios")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult UsuarioInserir([FromBody] AdicionarUsuarioCommand command)
        {
            return ResultHandler(_handler.Handler(command));
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
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/usuarios/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult UsuarioAlterar(long id, [FromBody] AtualizarUsuarioCommand command)
        {
            return ResultHandler(_handler.Handler(id, command));
        }

        /// <summary>
        /// Excluir Usuário
        /// </summary>                
        /// <remarks><h2><b>Exclui Usuário na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id do Usuário</param>      
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/usuarios/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult UsuarioExcluir(long id)
        {
            return ResultHandler(_handler.Handler(id));
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
        [HttpPost]
        [Route("v1/usuarios/login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<TokenCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TokenCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TokenCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TokenCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<TokenCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult UsuarioLogin([FromBody] LoginUsuarioCommand command)
        {
            return ResultHandler(_handler.Handler(command));
        }
    }
}