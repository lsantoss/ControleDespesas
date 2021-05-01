using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Logs;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Repositories
{
    public class LogRequestResponseRepositoryTest : DatabaseTest
    {
        private readonly ILogRequestResponseRepository _repository;

        public LogRequestResponseRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new LogRequestResponseRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var logRequestResponse = new SettingsTest().LogRequestResponse1;
            _repository.Salvar(logRequestResponse);

            var retorno = _repository.Obter(logRequestResponse.LogRequestResponseId);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(logRequestResponse.LogRequestResponseId, retorno.LogRequestResponseId);
            Assert.AreEqual(logRequestResponse.MachineName, retorno.MachineName);
            Assert.AreEqual(logRequestResponse.DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"), retorno.DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse.DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"), retorno.DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse.EndPoint, retorno.EndPoint);
            Assert.AreEqual(logRequestResponse.Request, retorno.Request);
            Assert.AreEqual(logRequestResponse.Response, retorno.Response);
            Assert.AreEqual(logRequestResponse.TempoDuracao, retorno.TempoDuracao);
        }

        [Test]
        public void Obter()
        {
            var logRequestResponse = new SettingsTest().LogRequestResponse1;
            _repository.Salvar(logRequestResponse);

            var retorno = _repository.Obter(logRequestResponse.LogRequestResponseId);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(logRequestResponse.LogRequestResponseId, retorno.LogRequestResponseId);
            Assert.AreEqual(logRequestResponse.MachineName, retorno.MachineName);
            Assert.AreEqual(logRequestResponse.DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"), retorno.DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse.DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"), retorno.DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse.EndPoint, retorno.EndPoint);
            Assert.AreEqual(logRequestResponse.Request, retorno.Request);
            Assert.AreEqual(logRequestResponse.Response, retorno.Response);
            Assert.AreEqual(logRequestResponse.TempoDuracao, retorno.TempoDuracao);
        }

        [Test]
        public void Listar()
        {
            var logRequestResponse1 = new SettingsTest().LogRequestResponse1;
            _repository.Salvar(logRequestResponse1);

            var logRequestResponse2 = new SettingsTest().LogRequestResponse2;
            _repository.Salvar(logRequestResponse2);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(logRequestResponse1.LogRequestResponseId, retorno[0].LogRequestResponseId);
            Assert.AreEqual(logRequestResponse1.MachineName, retorno[0].MachineName);
            Assert.AreEqual(logRequestResponse1.DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"), retorno[0].DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse1.DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"), retorno[0].DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse1.EndPoint, retorno[0].EndPoint);
            Assert.AreEqual(logRequestResponse1.Request, retorno[0].Request);
            Assert.AreEqual(logRequestResponse1.Response, retorno[0].Response);
            Assert.AreEqual(logRequestResponse1.TempoDuracao, retorno[0].TempoDuracao);

            Assert.AreEqual(logRequestResponse2.LogRequestResponseId, retorno[1].LogRequestResponseId);
            Assert.AreEqual(logRequestResponse2.MachineName, retorno[1].MachineName);
            Assert.AreEqual(logRequestResponse2.DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"), retorno[1].DataRequest.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse2.DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"), retorno[1].DataResponse.ToString("dd/MM/yyyyy hh:mm:ss"));
            Assert.AreEqual(logRequestResponse2.EndPoint, retorno[1].EndPoint);
            Assert.AreEqual(logRequestResponse2.Request, retorno[1].Request);
            Assert.AreEqual(logRequestResponse2.Response, retorno[1].Response);
            Assert.AreEqual(logRequestResponse2.TempoDuracao, retorno[1].TempoDuracao);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}