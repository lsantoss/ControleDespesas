using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("Empresa")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepositorio _repositorio;
        private readonly EmpresaHandler _handler;
        private readonly string _ChaveAPI;

        public EmpresaController(IEmpresaRepositorio repositorio, EmpresaHandler handler, IOptions<SettingsAPI> options)
        {
            _repositorio = repositorio;
            _handler = handler;
            _ChaveAPI = options.Value.ChaveAPI;
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
        public ActionResult<ApiResponse<string, Notificacao>> EmpresaHealthCheck()
        {
            try
            {
                if(Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string, Notificacao>("Sucesso", "API Controle de Despesas - Empresa OK"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
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
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<EmpresaQueryResult>, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<List<EmpresaQueryResult>, Notificacao>> Empresas()
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                var result = _repositorio.Listar();

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<EmpresaQueryResult>, Notificacao>("Lista de empresas obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<EmpresaQueryResult>, Notificacao>("Nenhuma empresa cadastrada atualmente", new List<EmpresaQueryResult>()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }

        /// <summary>
        /// Empresa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Empresa pelo Id.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Obter pelo Id</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Empresa")]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<EmpresaQueryResult, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<EmpresaQueryResult, Notificacao>> Empresa([FromBody] ObterEmpresaPorIdCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
                    return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<object, Notificacao>("Acesso negado", new List<Notificacao>() { new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada") }));

                if (command == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", new List<Notificacao>() { new Notificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos") }));

                if (!command.ValidarCommand())
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object, Notificacao>("Parâmentros inválidos", command.Notificacoes));

                var result = _repositorio.Obter(command.Id);

                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<EmpresaQueryResult, Notificacao>("Empresa obtida com sucesso", result));
                else
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<EmpresaQueryResult, Notificacao>("Empresa não cadastrada", result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
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
        [Route("v1/EmpresaInserir")]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AdicionarEmpresaCommandOutput, Notificacao>> EmpresaInserir([FromBody] AdicionarEmpresaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
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
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
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
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<AtualizarEmpresaCommandOutput, Notificacao>> EmpresaAlterar([FromBody] AtualizarEmpresaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
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
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
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
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<ApagarEmpresaCommandOutput, Notificacao>), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse<ApagarEmpresaCommandOutput, Notificacao>> EmpresaExcluir([FromBody] ApagarEmpresaCommand command)
        {
            try
            {
                if (Request.Headers["ChaveAPI"].ToString() != _ChaveAPI)
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
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object, Notificacao>("Erro", new List<Notificacao>() { new Notificacao("Erro", e.Message) }));
            }
        }
    }
}