using System;
using System.Collections.Generic;
using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Empresa;
using LSCode.Facilitador.Api.Exceptions;
using LSCode.Facilitador.Api.InterfacesCommand;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Route("Empresa")]
    [ApiController]
    public class EmpresaController : ApiController
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
        public string EmpresaHealthCheck()
        {
            return "DISPONÍVEL!";
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
        public IEnumerable<EmpresaQueryResult> Empresas()
        {
            return _repositorio.Listar();
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
                return (AdicionarEmpresaCommandResult)_handler.Handle(command);
            }
            catch (RepositoryException e)
            {
                return new AdicionarEmpresaCommandResult(false, e.Message, null);
            }
            catch (HandlerException e)
            {
                return new AdicionarEmpresaCommandResult(false, e.Message, null);
            }
            catch (Exception e)
            {
                return new AdicionarEmpresaCommandResult(false, "ControllerException: " + e.Message, null);
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
                return (AtualizarEmpresaCommandResult)_handler.Handle(command);
            }
            catch (RepositoryException e)
            {
                return new AtualizarEmpresaCommandResult(false, e.Message, null);
            }
            catch (HandlerException e)
            {
                return new AtualizarEmpresaCommandResult(false, e.Message, null);
            }
            catch (Exception e)
            {
                return new AtualizarEmpresaCommandResult(false, "ControllerException: " + e.Message, null);
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
                return (ApagarEmpresaCommandResult)_handler.Handle(command);
            }
            catch (RepositoryException e)
            {
                return new ApagarEmpresaCommandResult(false, e.Message, null);
            }
            catch (HandlerException e)
            {
                return new ApagarEmpresaCommandResult(false, e.Message, null);
            }
            catch (Exception e)
            {
                return new ApagarEmpresaCommandResult(false, "ControllerException: " + e.Message, null);
            }
        }
    }
}