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
    public class RequestResponseApiAttribute : ActionFilterAttribute
    {
        private readonly ILogRequestResponseRepository _logRequestResponseRepository;

        public RequestResponseApiAttribute(ILogRequestResponseRepository logRequestResponseRepository)
        {
            _logRequestResponseRepository = logRequestResponseRepository;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (context.HttpContext.Request.Path.HasValue && context.HttpContext.Request.Path.Value.Contains("hangfire"))
                return;

            var request = FormatRequest(context.HttpContext);

            object response;
            try
            {
                response = ((ObjectResult)context.Result).Value;
            }
            catch
            {
                response = "No Content";
            }

            long elapsedTime = 0;

            Stopwatch stopWatch = null;
            DateTime dataEnvio = DateTime.Now;

            if (context.HttpContext.Items.ContainsKey(GetType().FullName))
            {
                stopWatch = context.HttpContext.Items[GetType().FullName] as Stopwatch;
                stopWatch.Stop();
            }

            if (context.HttpContext.Items.ContainsKey("DataEnvio"))
                dataEnvio = (DateTime)context.HttpContext.Items["DataEnvio"];

            if (stopWatch != null) elapsedTime = stopWatch.ElapsedMilliseconds;

            var logRequestResponse = new LogRequestResponse()
            {
                MachineName = Environment.MachineName,
                DataEnvio = dataEnvio,
                DataRecebimento = DateTime.Now,
                EndPoint = context.ActionDescriptor.DisplayName,
                Request = request,
                Response = JsonConvert.SerializeObject(response),
                TempoDuracao = elapsedTime
            };

            _logRequestResponseRepository.Adicionar(logRequestResponse);
        }

        private string FormatRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            StringBuilder informacoesRequest = new StringBuilder();
            informacoesRequest.AppendLine($"Http Request Information: ");
            informacoesRequest.AppendLine($"Path: {request.Path} {Environment.NewLine}");
            informacoesRequest.AppendLine($"QueryString: {request.QueryString} {Environment.NewLine}");
            informacoesRequest.AppendLine($"Request Body: {ObterBodyRequest(request.Body)}");
            return informacoesRequest.ToString();
        }

        private static string ObterBodyRequest(Stream streamRequestBody)
        {
            var bodyStr = "";

            using (var stream = new MemoryStream())
            {
                if (streamRequestBody.CanSeek)
                {
                    streamRequestBody.Seek(0, SeekOrigin.Begin);
                    streamRequestBody.CopyTo(stream);
                    bodyStr = Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            return bodyStr;
        }
    }
}