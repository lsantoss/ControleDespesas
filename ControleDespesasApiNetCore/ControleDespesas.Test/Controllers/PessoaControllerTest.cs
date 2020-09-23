using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class PessoaControllerTest : DatabaseFactory
    {
        private readonly SettingsTest _settingsTest;
        private readonly Mock<IOptions<SettingsAPI>> _mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
        private readonly PessoaRepositorio _repository;
        private readonly PessoaHandler _handler;
        private readonly PessoaController _controller;

        public PessoaControllerTest()
        {
            CriarBaseDeDadosETabelas();
            _settingsTest = new SettingsTest();
            _mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);
            _mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new PessoaRepositorio(_mockOptionsInfra.Object);
            _handler = new PessoaHandler(_repository);
            _controller = new PessoaController(_repository, _handler, _mockOptionsAPI.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void PessoaHealthCheck()
        {
            var response = _controller.PessoaHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Pessoa OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Pessoas()
        {
            var pessoa1 = _settingsTest.Pessoa1;
            var pessoa2 = _settingsTest.Pessoa2;
            var pessoa3 = _settingsTest.Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            var response = _controller.Pessoas().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PessoaQueryResult>, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pessoas obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pessoa1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pessoa1.Nome.ToString(), responseObj.Value.Dados[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, responseObj.Value.Dados[0].ImagemPerfil);

            Assert.AreEqual(pessoa2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(pessoa2.Nome.ToString(), responseObj.Value.Dados[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, responseObj.Value.Dados[1].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(pessoa3.Nome.ToString(), responseObj.Value.Dados[2].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, responseObj.Value.Dados[2].ImagemPerfil);
        }

        [Test]
        public void Empresa()
        {
            var pessoa1 = _settingsTest.Pessoa1;
            var pessoa2 = _settingsTest.Pessoa2;
            var pessoa3 = _settingsTest.Pessoa3;

            var command = _settingsTest.PessoaObterPorIdCommand;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            var response = _controller.Pessoa(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PessoaQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pessoa2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(pessoa2.Nome.ToString(), responseObj.Value.Dados.Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void EmpresaInserir()
        {
            var command = _settingsTest.PessoaAdicionarCommand;

            var response = _controller.PessoaInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarPessoaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa gravada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void EmpresaAlterar()
        {
            var pessoa = _settingsTest.Pessoa1;

            var command = _settingsTest.PessoaAtualizarCommand;

            _repository.Salvar(pessoa);

            var response = _controller.PessoaAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarPessoaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa atualizada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void EmpresaExcluir()
        {
            var pessoa = _settingsTest.Pessoa1;

            var command = _settingsTest.PessoaApagarCommand;

            _repository.Salvar(pessoa);

            var response = _controller.PessoaExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarPessoaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}