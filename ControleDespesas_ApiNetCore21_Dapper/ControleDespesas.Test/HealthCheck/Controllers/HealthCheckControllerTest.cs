using ControleDespesas.Api.Controllers.Comum;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Response;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ControleDespesas.Test.HealthCheck.Controllers
{
    [TestFixture]
    public class HealthCheckControllerTest : DatabaseTest
    {
        private readonly IHealthCheckRepository _repository;
        private readonly HealthCheckController _controller;

        public HealthCheckControllerTest()
        {
            _repository = new HealthCheckRepository(MockSettingsInfraData);
            _controller = new HealthCheckController(_repository);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsApi.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void PagamentoHealthCheck()
        {
            var response = _controller.HealthCheck();
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<string>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}