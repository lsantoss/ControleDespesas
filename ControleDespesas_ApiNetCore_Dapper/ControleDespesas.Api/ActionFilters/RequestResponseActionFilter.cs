using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ControleDespesas.Api.ActionFilters
{
    public class RequestResponseActionFilter : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;
        private DateTime _dataRequest;
        private readonly ILogRequestResponseRepository _logRequestResponseRepository;

        public RequestResponseActionFilter(ILogRequestResponseRepository logRequestResponseRepository)
        {
            _logRequestResponseRepository = logRequestResponseRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _dataRequest = DateTime.Now;
            _stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            var dataResponse = DateTime.Now;
            var tempoDuracao = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Stop();

            var request = FormatRequest(context.HttpContext.Request);
            var response = FormatarResponse(context);

            var logRequestResponse = new LogRequestResponse()
            {
                MachineName = Environment.MachineName,
                DataEnvio = _dataRequest,
                DataRecebimento = dataResponse,
                EndPoint = context.ActionDescriptor.DisplayName,
                Request = request,
                Response = JsonConvert.SerializeObject(response),
                TempoDuracao = tempoDuracao
            };

            _logRequestResponseRepository.Adicionar(logRequestResponse);
        }

        private string FormatRequest(HttpRequest request)
        {
            StringBuilder informacoesRequest = new StringBuilder();
            informacoesRequest.AppendLine($"Http Request Information: ");
            informacoesRequest.AppendLine($"Path: {request.Path} {Environment.NewLine}");
            informacoesRequest.AppendLine($"QueryString: {request.QueryString} {Environment.NewLine}");
            informacoesRequest.AppendLine($"Request Body: {ObterBodyRequest(request.Body)}");
            return informacoesRequest.ToString();
        }

        private object FormatarResponse(ActionExecutedContext context)
        {
            try
            {
                return ((ObjectResult)context.Result).Value;
            }
            catch
            {
                return "No Content";
            }
        }

        private string ObterBodyRequest(Stream streamRequestBody)
        {
            using (var stream = new MemoryStream())
            {
                if (streamRequestBody.CanSeek)
                {
                    streamRequestBody.Seek(0, SeekOrigin.Begin);
                    streamRequestBody.CopyTo(stream);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}