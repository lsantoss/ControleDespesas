using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.Pessoas.Commands.Output;
using ControleDespesas.Domain.Pessoas.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Query.Results;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
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

namespace ControleDespesas.Test.Pessoas.Controllers
{
    [TestFixture]
    public class PessoaControllerTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly IPessoaHandler _handler;
        private readonly PessoaController _controller;

        public PessoaControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _handler = new PessoaHandler(_repositoryPessoa, _repositoryUsuario);
            _controller = new PessoaController(_repositoryPessoa, _handler);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsApi.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Pessoas()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            var query = new SettingsTest().PessoaObterQuery;

            var response = _controller.Pessoas(usuario.Id, query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<PessoaQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
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
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            var query = new SettingsTest().PessoaObterQuery;

            var response = _controller.Pessoa(usuario.Id, pessoa2.Id, query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<PessoaQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
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

            var response = _controller.PessoaInserir(usuario.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<PessoaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(201, responseObj.StatusCode);
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
            _repositoryPessoa.Salvar(pessoa);

            var command = new SettingsTest().PessoaAtualizarCommand;

            var response = _controller.PessoaAlterar(command.IdUsuario, command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<PessoaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

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
            _repositoryPessoa.Salvar(pessoa);

            var response = _controller.PessoaExcluir(usuario.Id, pessoa.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pessoa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pessoa.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}