using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.Commands.Pessoa.Output;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Pessoa;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class PessoaControllerTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly PessoaHandler _handler;
        private readonly PessoaController _controller;

        public PessoaControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _handler = new PessoaHandler(_repositoryPessoa);
            _controller = new PessoaController(_repositoryPessoa, _handler, MockSettingsAPI);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void PessoaHealthCheck()
        {
            var response = _controller.PessoaHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Pessoa OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Pessoas()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            var pessoa2 = new SettingsTest().Pessoa2;
            var pessoa3 = new SettingsTest().Pessoa3;

            var command = new SettingsTest().PessoaObterPorIdUsuarioCommand;

            _repositoryPessoa.Salvar(pessoa1);
            _repositoryPessoa.Salvar(pessoa2);
            _repositoryPessoa.Salvar(pessoa3);

            var response = _controller.Pessoas(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PessoaQueryResult>, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pessoas obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pessoa1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pessoa1.Nome, responseObj.Value.Dados[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, responseObj.Value.Dados[0].ImagemPerfil);

            Assert.AreEqual(pessoa2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(pessoa2.Nome, responseObj.Value.Dados[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, responseObj.Value.Dados[1].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(pessoa3.Nome, responseObj.Value.Dados[2].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, responseObj.Value.Dados[2].ImagemPerfil);
        }

        [Test]
        public void Pessoa()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            var pessoa2 = new SettingsTest().Pessoa2;
            var pessoa3 = new SettingsTest().Pessoa3;

            var command = new SettingsTest().PessoaObterPorIdCommand;

            _repositoryPessoa.Salvar(pessoa1);
            _repositoryPessoa.Salvar(pessoa2);
            _repositoryPessoa.Salvar(pessoa3);

            var response = _controller.Pessoa(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PessoaQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pessoa2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(pessoa2.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void PessoaInserir()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var command = new SettingsTest().PessoaAdicionarCommand;

            var response = _controller.PessoaInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarPessoaCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa gravada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void PessoaAlterar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;

            var command = new SettingsTest().PessoaAtualizarCommand;

            _repositoryPessoa.Salvar(pessoa);

            var response = _controller.PessoaAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarPessoaCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa atualizada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.ImagemPerfil, responseObj.Value.Dados.ImagemPerfil);
        }

        [Test]
        public void PessoaExcluir()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;

            var command = new SettingsTest().PessoaApagarCommand;

            _repositoryPessoa.Salvar(pessoa);

            var response = _controller.PessoaExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarPessoaCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

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