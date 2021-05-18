using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Commands.Output;
using ControleDespesas.Domain.Pessoas.Interfaces.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Query.Input;
using ControleDespesas.Domain.Pessoas.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [ApiController]
    public class PessoaController : BaseController
    {
        private readonly IPessoaRepository _repository;
        private readonly IPessoaHandler _handler;

        public PessoaController(IPessoaRepository repository, IPessoaHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        /// <summary>
        /// Pessoas
        /// </summary>                
        /// <remarks><h2><b>Lista Pessoas.</b></h2></remarks>
        /// <param name="query">Parâmetro requerido query de busca</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pessoas")]
        [Authorize(Roles = "Administrador, Escrita, SomenteLeitura")]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Pessoas([FromQuery] ObterPessoasQuery query)
        {
            if (query == null)
                return ResultInputNull();

            if (!query.ValidarQuery())
                return ResultNotifications(query.Notificacoes);

            return ResultGetList(_repository.Listar(query.IdUsuario));
        }

        /// <summary>
        /// Pessoa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Pessoa pelo Id.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/pessoas/{id}")]
        [Authorize(Roles = "Administrador, Escrita, SomenteLeitura")]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult Pessoa(int id)
        {
            return ResultGet(_repository.Obter(id));
        }

        /// <summary>
        /// Incluir Pessoa 
        /// </summary>                
        /// <remarks><h2><b>Inclui nova Pessoa na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/pessoas")]
        [Authorize(Roles = "Administrador, Escrita")]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PessoaInserir([FromBody] AdicionarPessoaCommand command)
        {
            return ResultHandler(_handler.Handler(command));
        }

        /// <summary>
        /// Alterar Pessoa
        /// </summary>        
        /// <remarks><h2><b>Altera Pessoa na base de dados.</b></h2></remarks> 
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>       
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/pessoas/{id}")]
        [Authorize(Roles = "Administrador, Escrita")]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PessoaAlterar(int id, [FromBody] AtualizarPessoaCommand command)
        {
            return ResultHandler(_handler.Handler(id, command));
        }

        /// <summary>
        /// Excluir Pessoa
        /// </summary>                
        /// <remarks><h2><b>Exclui Pessoa na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>       
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/pessoas/{id}")]
        [Authorize(Roles = "Administrador, Escrita")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PessoaExcluir(int id)
        {
            return ResultHandler(_handler.Handler(id));
        }
    }
}