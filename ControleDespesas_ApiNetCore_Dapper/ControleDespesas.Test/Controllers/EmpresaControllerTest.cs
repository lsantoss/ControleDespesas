using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.Commands.Empresa.Output;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Empresa.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Response;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class EmpresaControllerTest : DatabaseTest
    {
        private readonly IEmpresaRepository _repository;
        private readonly IEmpresaHandler _handler;
        private readonly EmpresaController _controller;

        public EmpresaControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new EmpresaRepository(MockSettingsInfraData);
            _handler = new EmpresaHandler(_repository);
            _controller = new EmpresaController(_repository, _handler);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Empresas()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            var response = _controller.Empresas();
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<EmpresaQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
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
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            var response = _controller.Empresa(empresa2.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(empresa2.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(empresa2.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaInserir()
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;
            var response = _controller.EmpresaInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(201, responseObj.StatusCode);
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
            _repository.Salvar(empresa);

            var command = new SettingsTest().EmpresaAtualizarCommand;
            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

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
            _repository.Salvar(empresa);

            var response = _controller.EmpresaExcluir(empresa.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}