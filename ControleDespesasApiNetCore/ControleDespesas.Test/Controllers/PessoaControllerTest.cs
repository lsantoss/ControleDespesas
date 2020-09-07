using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Models;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
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
        private readonly Mock<IOptions<SettingsAPI>> _mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
        private readonly PessoaRepositorio _repository;
        private readonly PessoaHandler _handler;
        private readonly PessoaController _controller;

        public PessoaControllerTest()
        {
            CriarBaseDeDadosETabelas();
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
            var pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPessoa0");
            var pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPessoa1");
            var pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPessoa2");

            _repository.Salvar(pessoa0);
            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);

            var response = _controller.Pessoas().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PessoaQueryResult>, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pessoas obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pessoa0.Nome.ToString(), responseObj.Value.Dados[0].Nome);
            Assert.AreEqual(pessoa0.ImagemPerfil, responseObj.Value.Dados[0].ImagemPerfil);

            Assert.AreEqual(2, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(pessoa1.Nome.ToString(), responseObj.Value.Dados[1].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, responseObj.Value.Dados[1].ImagemPerfil);

            Assert.AreEqual(3, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(pessoa2.Nome.ToString(), responseObj.Value.Dados[2].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, responseObj.Value.Dados[2].ImagemPerfil);
        }

        [Test]
        public void Empresa()
        {
            var pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            var pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            var pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            var command = new ObterPessoaPorIdCommand() { Id = 2 };

            _repository.Salvar(pessoa0);
            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);

            var response = _controller.Pessoa(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PessoaQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(2, responseObj.Value.Dados.Id);
            Assert.AreEqual(pessoa1.Nome.ToString(), responseObj.Value.Dados.Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void EmpresaInserir()
        {
            var command = new AdicionarPessoaCommand()
            {
                Nome = "NomePessoa",
                ImagemPerfil = "ImagemPessoa"
            };

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
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");

            var command = new AtualizarPessoaCommand()
            {
                Id = 1,
                Nome = "NomePessoa - Editada",
                ImagemPerfil = "ImagemPessoa - Editada"
            };

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
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");

            var command = new ApagarPessoaCommand() { Id = 1 };

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