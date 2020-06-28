﻿using System;
using System.Collections.Generic;
using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using ControleDespesas.Dominio.Query.Empresa;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Route("Empresa")]
    [ApiController]
    public class EmpresaController
    {
        private readonly IEmpresaRepositorio _repositorio;
        private readonly EmpresaHandler _handler;

        public EmpresaController(IEmpresaRepositorio repositorio, EmpresaHandler handler)
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
        public ICommandResult EmpresaHealthCheck()
        {
            try
            {
                return new CommandResult(true, "Sucesso!", "Disponível");
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!", e.Message);
            }
        }

        /// <summary>
        /// Empresas
        /// </summary>                
        /// <remarks><h2><b>Lista todas as Empresas.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Empresas")]
        public ICommandResult2<List<EmpresaQueryResult>> Empresas()
        {
            try
            {
                List<EmpresaQueryResult> empresas = _repositorio.Listar();
                return new CommandResult2(true, "Empesas obtidas com sucesso!", empresas);
            }
            catch (Exception e)
            {
                return new CommandResult2(false, "Erro! " + e.Message, null);
            }
        }

        /// <summary>
        /// Empresa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Empresa.</b></h2></remarks>
        /// <param name="Id">Parâmetro requerido Id da Empresa</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Empresa/{Id:int}")]
        public EmpresaQueryResult Empresa(int Id)
        {
            return _repositorio.Obter(Id);
        }

        /// <summary>
        /// Incluir Empresa 
        /// </summary>                
        /// <remarks><h2><b>Inclui nova Empresa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/EmpresaNovo")]
        public ICommandResult EmpresaNovo([FromBody] AdicionarEmpresaCommand command)
        {
            try
            {
                if(command == null)
                    return new CommandResult(false, "Erro!", "Dados de entrada nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

                return (CommandResult)_handler.Handle(command);
            }
            catch (Exception e)
            {
                return new CommandResult(false, "Erro!" , e.Message);
            }
        }

        /// <summary>
        /// Alterar Empresa
        /// </summary>        
        /// <remarks><h2><b>Altera Empresa na base de dados.</b></h2></remarks>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/EmpresaAlterar")]
        public ICommandResult EmpresaAlterar([FromBody] AtualizarEmpresaCommand command)
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
        /// Excluir Empresa
        /// </summary>                
        /// <remarks><h2><b>Exclui Empresa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Delete</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/EmpresaExcluir")]
        public ICommandResult EmpresaExcluir([FromBody] ApagarEmpresaCommand command)
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