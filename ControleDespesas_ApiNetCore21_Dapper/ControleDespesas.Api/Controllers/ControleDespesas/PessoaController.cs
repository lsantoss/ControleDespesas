using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Commands.Output;
using ControleDespesas.Domain.Pessoas.Interfaces.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Query.Parameters;
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
        /// <param name="usuarioId">Parâmetro requerido Id do Usuário</param>
        /// <param name="query">Parâmetro requerido query de busca</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>   
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios/{usuarioId}/pessoas")]
        [Authorize(Roles = "Administrador, Escrita, SomenteLeitura")]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<List<PessoaQueryResult>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Pessoas(long usuarioId, [FromQuery] ObterPessoasQuery query)
        {
            if (query == null)
                return ResponseInputNull();

            query.IdUsuario = usuarioId;

            if (!query.ValidarQuery())
                return ResponseNotifications(query.Notificacoes);

            return !query.RegistrosFilhos
                ? ResponseGetList(_repository.Listar(query.IdUsuario))
                : ResponseGetList(_repository.ListarContendoRegistrosFilhos(query.IdUsuario));
        }

        /// <summary>
        /// Pessoa
        /// </summary>                
        /// <remarks><h2><b>Consulta a Pessoa pelo Id.</b></h2></remarks>
        /// <param name="usuarioId">Parâmetro requerido Id do Usuário</param>
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios/{usuarioId}/pessoas/{id}")]
        [Authorize(Roles = "Administrador, Escrita, SomenteLeitura")]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<PessoaQueryResult>), StatusCodes.Status500InternalServerError)]
        public IActionResult Pessoa(long usuarioId, long id, [FromQuery] ObterPessoasQuery query)
        {
            if (query == null)
                return ResponseInputNull();

            return !query.RegistrosFilhos
                ? ResponseGet(_repository.Obter(id, usuarioId))
                : ResponseGet(_repository.ObterContendoRegistrosFilhos(id, usuarioId));
        }

        /// <summary>
        /// Incluir Pessoa 
        /// </summary>                
        /// <remarks><h2><b>Inclui nova Pessoa na base de dados.</b></h2></remarks>
        /// <param name="usuarioId">Parâmetro requerido Id do Usuário</param>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/usuarios/{usuarioId}/pessoas")]
        [Authorize(Roles = "Administrador, Escrita")]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PessoaInserir(long usuarioId, [FromBody] AdicionarPessoaCommand command)
        {            
            return ResponseHandler(_handler.Handler(usuarioId, command));
        }

        /// <summary>
        /// Alterar Pessoa
        /// </summary>        
        /// <remarks><h2><b>Altera Pessoa na base de dados.</b></h2></remarks>
        /// <param name="usuarioId">Parâmetro requerido Id do Usuário</param> 
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>       
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/usuarios/{usuarioId}/pessoas/{id}")]
        [Authorize(Roles = "Administrador, Escrita")]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<PessoaCommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PessoaAlterar(long usuarioId, long id, [FromBody] AtualizarPessoaCommand command)
        {
            return ResponseHandler(_handler.Handler(id, usuarioId, command));
        }

        /// <summary>
        /// Excluir Pessoa
        /// </summary>                
        /// <remarks><h2><b>Exclui Pessoa na base de dados.</b></h2></remarks>
        /// <param name="usuarioId">Parâmetro requerido Id do Usuário</param> 
        /// <param name="id">Parâmetro requerido Id da Pessoa</param>       
        /// <response code="200">OK Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/usuarios/{usuarioId}/pessoas/{id}")]
        [Authorize(Roles = "Administrador, Escrita")]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<CommandOutput>), StatusCodes.Status500InternalServerError)]
        public IActionResult PessoaExcluir(long usuarioId, long id)
        {
            return ResponseHandler(_handler.Handler(id, usuarioId));
        }
    }
}