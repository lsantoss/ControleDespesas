using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Services;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Usuario;
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
    public class UsuarioControllerTest : DatabaseTest
    {
        private readonly UsuarioRepositorio _repository;
        private readonly UsuarioHandler _handler;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);
            var optionsAPI = Options.Create(MockSettingsAPI);
            var tokenJWTService = new TokenJWTService(optionsAPI);

            _repository = new UsuarioRepositorio(optionsInfraData);
            _handler = new UsuarioHandler(_repository);
            _controller = new UsuarioController(_repository, _handler, optionsAPI, tokenJWTService);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void UsuarioHealthCheck()
        {
            var response = _controller.UsuarioHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Usuário OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Usuarios()
        {
            var usuario1 = new SettingsTest().Usuario1;
            var usuario2 = new SettingsTest().Usuario2;
            var usuario3 = new SettingsTest().Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var response = _controller.Usuarios().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<UsuarioQueryResult>, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de usuários obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(usuario1.Login, responseObj.Value.Dados[0].Login);
            Assert.AreEqual(usuario1.Senha, responseObj.Value.Dados[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, responseObj.Value.Dados[0].Privilegio);

            Assert.AreEqual(usuario2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(usuario2.Login, responseObj.Value.Dados[1].Login);
            Assert.AreEqual(usuario2.Senha, responseObj.Value.Dados[1].Senha);
            Assert.AreEqual(usuario1.Privilegio, responseObj.Value.Dados[0].Privilegio);

            Assert.AreEqual(usuario3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(usuario3.Login, responseObj.Value.Dados[2].Login);
            Assert.AreEqual(usuario3.Senha, responseObj.Value.Dados[2].Senha);
            Assert.AreEqual(usuario1.Privilegio, responseObj.Value.Dados[0].Privilegio);
        }

        [Test]
        public void Usuario()
        {
            var usuario1 = new SettingsTest().Usuario1;
            var usuario2 = new SettingsTest().Usuario2;
            var usuario3 = new SettingsTest().Usuario3;

            var command = new SettingsTest().UsuarioObterPorIdCommand;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var response = _controller.Usuario(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<UsuarioQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(usuario2.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(usuario2.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(usuario2.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [Test]
        public void UsuarioInserir()
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;

            var response = _controller.UsuarioInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarUsuarioCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário gravado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(command.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(command.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [Test]
        public void UsuarioAlterar()
        {
            var usuario = new SettingsTest().Usuario1;

            var command = new SettingsTest().UsuarioAtualizarCommand;

            _repository.Salvar(usuario);

            var response = _controller.UsuarioAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarUsuarioCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário atualizado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(command.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(command.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [Test]
        public void UsuarioExcluir()
        {
            var usuario = new SettingsTest().Usuario1;

            var command = new SettingsTest().UsuarioApagarCommand;

            _repository.Salvar(usuario);

            var response = _controller.UsuarioExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarUsuarioCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [Test]
        public void UsuarioLogin()
        {
            var usuario = new SettingsTest().Usuario1;

            var command = new SettingsTest().UsuarioLoginCommand;

            _repository.Salvar(usuario);

            var response = _controller.UsuarioLogin(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<UsuarioQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(usuario.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(usuario.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(usuario.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}