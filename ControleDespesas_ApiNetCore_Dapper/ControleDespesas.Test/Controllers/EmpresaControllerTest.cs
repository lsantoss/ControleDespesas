using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class EmpresaControllerTest : DatabaseTest
    {
        private readonly EmpresaRepositorio _repository;
        private readonly EmpresaHandler _handler;
        private readonly EmpresaController _controller;

        public EmpresaControllerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);
            var optionsAPI = Options.Create(MockSettingsAPI);

            _repository = new EmpresaRepositorio(optionsInfraData);
            _handler = new EmpresaHandler(_repository);
            _controller = new EmpresaController(_repository, _handler, optionsAPI);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void EmpresaHealthCheck()
        {
            var response = _controller.EmpresaHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Empresa OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Empresas()
        {
            var empresa1 = new SettingsTest().Empresa1;
            var empresa2 = new SettingsTest().Empresa2;
            var empresa3 = new SettingsTest().Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            var response = _controller.Empresas().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<EmpresaQueryResult>, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de empresas obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(empresa1.Nome, responseObj.Value.Dados[0].Nome);
            Assert.AreEqual(empresa1.Logo, responseObj.Value.Dados[0].Logo);

            Assert.AreEqual(empresa2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(empresa2.Nome, responseObj.Value.Dados[1].Nome);
            Assert.AreEqual(empresa2.Logo, responseObj.Value.Dados[1].Logo);

            Assert.AreEqual(empresa3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(empresa3.Nome, responseObj.Value.Dados[2].Nome);
            Assert.AreEqual(empresa3.Logo, responseObj.Value.Dados[2].Logo);
        }

        [Test]
        public void Empresa()
        {
            var empresa1 = new SettingsTest().Empresa1;
            var empresa2 = new SettingsTest().Empresa2;
            var empresa3 = new SettingsTest().Empresa3;

            var command = new SettingsTest().EmpresaObterPorIdCommand;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            var response = _controller.Empresa(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<EmpresaQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(empresa2.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(empresa2.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaInserir()
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;

            var response = _controller.EmpresaInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarEmpresaCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaAlterar()
        {
            var empresa = new SettingsTest().Empresa1;

            var command = new SettingsTest().EmpresaAtualizarCommand;

            _repository.Salvar(empresa);

            var response = _controller.EmpresaAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarEmpresaCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaExcluir()
        {
            var empresa = new SettingsTest().Empresa1;

            var command = new SettingsTest().EmpresaApagarCommand;

            _repository.Salvar(empresa);

            var response = _controller.EmpresaExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarEmpresaCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}