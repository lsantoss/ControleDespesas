using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query;
using ControleDespesas.Infra.Data.Factory;
using LSCode.Facilitador.Api.InterfacesCommand;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ControleDespesas.Api.Controllers.ControleDespesas
{
    //[RequireHttps]
    [RoutePrefix("Pagamento")]
    public class PagamentoController : ApiController
    {
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
        public String PagamentoHealthCheck()
        {
            return "DISPONÍVEL!";
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
            return DbFactory.Instance.PagamentoRepositorio.ListarPagamentos();
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
            return DbFactory.Instance.PagamentoRepositorio.ObterPagamento(Id);
        }

        ///// <summary>
        ///// Incluir Pagamento 
        ///// </summary>                
        ///// <remarks><h2><b>Inclui novo Pagamento na base de dados.</b></h2></remarks>
        ///// <param name="command">Parâmetro requerido command de Insert</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpPost]
        //[Route("v1/PagamentoNovo")]
        //public ICommandResult PagamentoNovo([FromBody] AdicionarPagamentoCommand command)
        //{
        //    return (AdicionarPagamentoCommandResult)_handler.Handle(command);
        //}

        ///// <summary>
        ///// Alterar Pagamento
        ///// </summary>        
        ///// <remarks><h2><b>Altera Pagamento na base de dados.</b></h2></remarks>        
        ///// <param name="command">Parâmetro requerido command de Update</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpPut]
        //[Route("v1/PagamentoAlterar")]
        //public ICommandResult PagamentoAlterar([FromBody] AtualizarPagamentoCommand command)
        //{
        //    return (AtualizarPagamentoCommandResult)_handler.Handle(command);
        //}

        ///// <summary>
        ///// Excluir Pagamento
        ///// </summary>                
        ///// <remarks><h2><b>Exclui Pagamento na base de dados.</b></h2></remarks>
        ///// <param name="command">Parâmetro requerido command de Delete</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpDelete]
        //[Route("v1/PagamentoExcluir")]
        //public ICommandResult PagamentoExcluir([FromBody] ApagarPagamentoCommand command)
        //{
        //    return (ApagarPagamentoCommandResult)_handler.Handle(command);
        //}
    }
}