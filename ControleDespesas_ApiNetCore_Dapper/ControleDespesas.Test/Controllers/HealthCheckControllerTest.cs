using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ControleDespesas.Test.Controllers
{
    public class HealthCheckControllerTest : DatabaseTest
    {
        private readonly HealthCheckController _controller;

        public HealthCheckControllerTest()
        {
            _controller = new HealthCheckController(MockSettingsAPI);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() { }

        [Test]
        public void PagamentoHealthCheck()
        {
            var response = _controller.HealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [TearDown]
        public void TearDown() { }
    }
}