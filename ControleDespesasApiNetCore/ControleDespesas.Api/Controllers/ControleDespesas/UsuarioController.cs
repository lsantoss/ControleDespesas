using System;
using System.Collections.Generic;
using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Usuario;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Route("Usuario")]
    [ApiController]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly UsuarioHandler _handler;

        public UsuarioController(IUsuarioRepositorio repositorio, UsuarioHandler handler)
        {
            _repositorio = repositorio;
            _handler = handler;
        }

        /// <summary>
        /// Health Check
        /// </summary>        
        /// <remarks><h2><b>Afere a resposta deste contexto do serviço.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/HealthCheck")]
        public ICommandResult UsuarioHealthCheck()
        {
            try
            {
                return new CommandResult(true, "Disponível", null);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!", e.Message);
            }
        }

        /// <summary>
        /// Usuarios
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Usuários.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Usuarios")]
        public IEnumerable<UsuarioQueryResult> Usuarios()
        {
            return _repositorio.Listar();
        }

        /// <summary>
        /// Usuario
        /// </summary>                
        /// <remarks><h2><b>Consulta o Usuário.</b></h2></remarks>
        /// <param name="Id">Parâmetro requerido Id do Usuário</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Usuario/{Id:int}")]
        public UsuarioQueryResult Usuario(int Id)
        {
            return _repositorio.Obter(Id);
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
        [Route("v1/UsuarioNovo")]
        public ICommandResult UsuarioNovo([FromBody] AdicionarUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(false, "Erro!", "Dados de entrada nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

                return (CommandResult)_handler.Handle(command);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!", e.Message);
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
        public ICommandResult UsuarioAlterar([FromBody] AtualizarUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(false, "Erro!", "Dados de entrada nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

                return (CommandResult)_handler.Handle(command);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!", e.Message);
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
        public ICommandResult UsuarioExcluir([FromBody] ApagarUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(false, "Erro!", "Dados de entrada nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

                return (CommandResult)_handler.Handle(command);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!", e.Message);
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
        public ICommandResult UsuarioLogin([FromBody] LoginUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(false, "Erro!", "Dados de entrada nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

                return (CommandResult)_handler.Handle(command);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!", e.Message);
            }
        }
    }
}