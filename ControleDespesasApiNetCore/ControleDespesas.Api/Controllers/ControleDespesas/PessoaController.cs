using System;
using System.Collections.Generic;
using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Pessoa;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Route("Pessoa")]
    [ApiController]
    public class PessoaController : ApiController
    {
        private readonly IPessoaRepositorio _repositorio;
        private readonly PessoaHandler _handler;

        public PessoaController(IPessoaRepositorio repositorio, PessoaHandler handler)
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
        public ICommandResult PessoaHealthCheck()
        {
            try
            {
                return new CommandResult(true, "Disponível", null);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "ControllerException: " + e.Message, null);
            }
        }

        /// <summary>
        /// Pessoas
        /// </summary>                
        /// <remarks><h2><b>Lista todas as Pessoas.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pessoas")]
        public IEnumerable<PessoaQueryResult> Pessoas()
        {
            return _repositorio.Listar();
        }

        /// <summary>
        /// Pessoa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Pessoa.</b></h2></remarks>
        /// <param name="Id">Parâmetro requerido Id da Pessoa</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pessoa/{Id:int}")]
        public PessoaQueryResult Pessoa(int Id)
        {
            return _repositorio.Obter(Id);
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
        [Route("v1/PessoaNovo")]
        public ICommandResult PessoaNovo([FromBody] AdicionarPessoaCommand command)
        {
            return (AdicionarPessoaCommandResult)_handler.Handle(command);
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
        public ICommandResult PessoaAlterar([FromBody] AtualizarPessoaCommand command)
        {
            return (AtualizarPessoaCommandResult)_handler.Handle(command);
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
        public ICommandResult PessoaExcluir([FromBody] ApagarPessoaCommand command)
        {
            return (ApagarPessoaCommandResult)_handler.Handle(command);
        }
    }
}