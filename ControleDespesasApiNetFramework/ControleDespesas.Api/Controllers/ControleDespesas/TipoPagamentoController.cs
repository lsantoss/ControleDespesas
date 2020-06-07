using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
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
    [RoutePrefix("TipoPagamento")]
    public class TipoPagamentoController : ApiController
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
        public String TipoPagamentoHealthCheck()
        {
            return "DISPONÍVEL!";
        }

        /// <summary>
        /// Tipos de Pagamentos
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Tipos de Pagamentos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/TipoPagamentos")]
        public IEnumerable<TipoPagamentoQueryResult> TipoPagamentos()
        {
            return DbFactory.Instance.TipoPagamentoRepositorio.ListarTipoPagamentos();
        }

        /// <summary>
        /// Tipo de Pagamento
        /// </summary>                
        /// <remarks><h2><b>Consulta o Tipo de Pagamento.</b></h2></remarks>
        /// <param name="Id">Parâmetro requerido Id do Tipo de Pagamento</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/TipoPagamento/{Id:int}")]
        public TipoPagamentoQueryResult TipoPagamento(int Id)
        {
            return DbFactory.Instance.TipoPagamentoRepositorio.ObterTipoPagamento(Id);
        }

        ///// <summary>
        ///// Incluir Tipo de Pagamento 
        ///// </summary>                
        ///// <remarks><h2><b>Inclui novo Tipo de Pagamento na base de dados.</b></h2></remarks>
        ///// <param name="command">Parâmetro requerido command de Insert</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpPost]
        //[Route("v1/TipoPagamentoNovo")]
        //public ICommandResult TipoPagamentoNovo([FromBody] AdicionarTipoPagamentoCommand command)
        //{
        //    var result = (AdicionarTipoPagamentoCommandResult)_handler.Handle(command);
        //    return result;
        //}

        ///// <summary>
        ///// Alterar Tipo de Pagamento
        ///// </summary>        
        ///// <remarks><h2><b>Altera Tipo de Pagamento na base de dados.</b></h2></remarks>        
        ///// <param name="command">Parâmetro requerido command de Update</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpPut]
        //[Route("v1/TipoPagamentoAlterar")]
        //public ICommandResult TipoPagamentoAlterar([FromBody] AtualizarTipoPagamentoCommand command)
        //{
        //    var result = (AtualizarTipoPagamentoCommandResult)_handler.Handle(command);
        //    return result;
        //}

        ///// <summary>
        ///// Excluir Tipo de Pagamento
        ///// </summary>                
        ///// <remarks><h2><b>Exclui Tipo de Pagamento na base de dados.</b></h2></remarks>
        ///// <param name="command">Parâmetro requerido command de Delete</param>
        ///// <response code="200">OK Request</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpDelete]
        //[Route("v1/TipoPagamentoExcluir")]
        //public ICommandResult TipoPagamentoExcluir([FromBody] ApagarTipoPagamentoCommand command)
        //{
        //    var result = (ApagarTipoPagamentoCommandResult)_handler.Handle(command);
        //    return result;
        //}
    }
}