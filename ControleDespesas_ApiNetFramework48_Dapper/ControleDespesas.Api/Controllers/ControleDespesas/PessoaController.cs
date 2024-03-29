﻿using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Factory;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query;
using LSCode.Facilitador.Api.InterfacesCommand;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    //[RequireHttps]
    [RoutePrefix("Pessoa")]
    public class PessoaController : ApiController
    {
        private readonly PessoaHandler _handler;

        public PessoaController()
        {
            _handler = new PessoaHandler(DbFactory.Instance.PessoaRepositorio);
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
        public String PessoaHealthCheck()
        {
            return "DISPONÍVEL!";
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
            return DbFactory.Instance.PessoaRepositorio.ListarPessoas();
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
            return DbFactory.Instance.PessoaRepositorio.ObterPessoa(Id);
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