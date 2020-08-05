using System;
using System.Collections.Generic;
using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using ControleDespesas.Dominio.Query.Empresa;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Route("Empresa")]
    [ApiController]
    public class EmpresaController : ControllerBase
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
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/HealthCheck")]
        public ActionResult<ApiResponse<string>> EmpresaHealthCheck()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>("Sucesso", "API Controle de Despesas - Empresa OK"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>("Erro", new List<Erro>() { new Erro { Propriedade = "Erro", Mensagem = e.Message } }));
            }
        }

        /// <summary>
        /// Empresas
        /// </summary>                
        /// <remarks><h2><b>Lista todas as Empresas.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Empresas")]
        public ActionResult<ApiResponse<List<EmpresaQueryResult>>> Empresas()
        {
            try
            {
                var result = _repositorio.Listar();

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<EmpresaQueryResult>>("Lista de empresas obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<EmpresaQueryResult>>("Nenhuma empresa cadastrada atualmente", new List<EmpresaQueryResult>()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>("Erro", new List<Erro>() { new Erro { Propriedade = "Erro", Mensagem = e.Message } }));
            }
        }

        /// <summary>
        /// Empresa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Empresa.</b></h2></remarks>
        /// <param name="Id">Parâmetro requerido Id da Empresa</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Empresa/{Id:int}")]
        public ActionResult<ApiResponse<EmpresaQueryResult>> Empresa(int Id)
        {
            try
            {
                var result = _repositorio.Obter(Id);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<EmpresaQueryResult>("Empresa obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<EmpresaQueryResult>("Empresa não cadastrada.", result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>("Erro", new List<Erro>() { new Erro { Propriedade = "Erro", Mensagem = e.Message } }));
            }
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
        public ActionResult<ApiResponse<ICommandResult>> EmpresaNovo([FromBody] AdicionarEmpresaCommand command)
        {
            try
            {
                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object>("Parâmentros inválidos", new List<Erro>() { new Erro { Propriedade = "Parâmetros de entrada", Mensagem = "Parâmetros de entrada estão nulos" } }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object>("Parâmentros inválidos", (IReadOnlyCollection<Erro>) command.Notificacoes));

                var result = _handler.Handler(command);

                if (result.Sucesso)
                    return StatusCode(StatusCodes.Status200OK, result);
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object>(result.Mensagem, (IReadOnlyCollection<Erro>) result.Dados));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>("Erro", new List<Erro>() { new Erro { Propriedade = "Erro", Mensagem = e.Message } }));
            }
        }

        ///// <summary>
        ///// Alterar Empresa
        ///// </summary>        
        ///// <remarks><h2><b>Altera Empresa na base de dados.</b></h2></remarks>        
        ///// <param name="command">Parâmetro requerido command de Update</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpPut]
        //[Route("v1/EmpresaAlterar")]
        //public ICommandResult EmpresaAlterar([FromBody] AtualizarEmpresaCommand command)
        //{
        //    try
        //    {
        //        if (command == null)
        //            return new CommandResult(false, "Erro!", "Dados de entrada nulos");

        //        if (!command.ValidarCommand())
        //            return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

        //        return _handler.Handler(command);
        //    }
        //    catch (Exception e)
        //    {
        //        return new CommandResult(false, "Erro!", e.Message);
        //    }
        //}

        ///// <summary>
        ///// Excluir Empresa
        ///// </summary>                
        ///// <remarks><h2><b>Exclui Empresa na base de dados.</b></h2></remarks>
        ///// <param name="command">Parâmetro requerido command de Delete</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpDelete]
        //[Route("v1/EmpresaExcluir")]
        //public ICommandResult EmpresaExcluir([FromBody] ApagarEmpresaCommand command)
        //{
        //    try
        //    {
        //        if (command == null)
        //            return new CommandResult(false, "Erro!", "Dados de entrada nulos");

        //        if (!command.ValidarCommand())
        //            return new CommandResult(false, "Erro! Dados de entrada incorretos", command.Notificacoes);

        //        return _handler.Handler(command);
        //    }
        //    catch (Exception e)
        //    {
        //        return new CommandResult(false, "Erro!", e.Message);
        //    }
        //}
    }
}