using ControleDespesas.Api.Settings;
using ElmahCore;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Api.Controllers.Comum
{
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    //[TypeFilter(typeof(RequestResponseApiAttribute))]
    public class BaseController : ControllerBase
    {
        protected readonly string _ChaveAPI;

        public BaseController(SettingsAPI settings)
        {
            _ChaveAPI = settings.ChaveAPI;
        }

        protected ActionResult<ApiResponse<string, Notificacao>> ResultOK()
        {
            var statusCode = StatusCodes.Status200OK;
            var mensagem = "Sucesso";
            var dados = "API Controle de Despesas - OK";
            var result = new ApiResponse<string, Notificacao>(mensagem, dados);
            return StatusCode(statusCode, result);
        }

        protected ActionResult<ApiResponse<string, Notificacao>> ResultUnauthorized()
        {
            var statusCode = StatusCodes.Status401Unauthorized;
            var mensagem = "Acesso negado";
            var notificacao = new Notificacao("Chave da API", "ChaveAPI não corresponde com a chave esperada");
            var erros = new List<Notificacao>() { notificacao };
            var result = new ApiResponse<object, Notificacao>(mensagem, erros);
            return StatusCode(statusCode, result);          
        }

        protected ActionResult<ApiResponse<string, Notificacao>> ResultInternalServerError(Exception e)
        {
            HttpContext.RiseError(e);
            var statusCode = StatusCodes.Status500InternalServerError;
            var mensagem = "Erro";
            var notificacao = new Notificacao("Erro", e.Message);
            var erros = new List<Notificacao>() { notificacao };
            var result = new ApiResponse<object, Notificacao>(mensagem, erros);
            return StatusCode(statusCode, result);
        }
    }
}