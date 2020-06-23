using System;
using System.Collections.Generic;
using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Pagamento;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    [Route("Pagamento")]
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoRepositorio _repositorio;
        private readonly PagamentoHandler _handler;

        public PagamentoController(IPagamentoRepositorio repositorio, PagamentoHandler handler)
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
        public ICommandResult PagamentoHealthCheck()
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
        /// Pagamentos
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Pagamentos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pagamentos")]
        public IEnumerable<PagamentoQueryResult> Pagamentos()
        {
            return _repositorio.Listar();
        }

        /// <summary>
        /// Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o Pagamento.</b></h2></remarks>
        /// <param name="Id">Parâmetro requerido Id do Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Pagamento/{Id:int}")]
        public PagamentoQueryResult Pagamento(int Id)
        {
            return _repositorio.Obter(Id);
        }

        /// <summary>
        /// Incluir Pagamento 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/PagamentoNovo")]
        public ICommandResult PagamentoNovo([FromBody] AdicionarPagamentoCommand command)
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
        /// Alterar Pagamento
        /// </summary>        
        /// <remarks><h2><b>Altera Pagamento na base de dados.</b></h2></remarks>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/PagamentoAlterar")]
        public ICommandResult PagamentoAlterar([FromBody] AtualizarPagamentoCommand command)
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
        /// Excluir Pagamento
        /// </summary>                
        /// <remarks><h2><b>Exclui Pagamento na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Delete</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/PagamentoExcluir")]
        public ICommandResult PagamentoExcluir([FromBody] ApagarPagamentoCommand command)
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