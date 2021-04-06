using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.Commands.Usuario.Output;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Helpers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Usuario.Results;
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
    public class UsuarioControllerTest : DatabaseTest
    {
        private readonly ITokenJwtHelper _tokenJwtHelper;
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioHandler _handler;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _tokenJwtHelper = new TokenJwtHelper(MockSettingsAPI);
            _repository = new UsuarioRepository(MockSettingsInfraData);
            _handler = new UsuarioHandler(_repository, _tokenJwtHelper);
            _controller = new UsuarioController(_repository, _handler);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Usuarios()
        {
            var usuario1 = new SettingsTest().Usuario1;
            var usuario2 = new SettingsTest().Usuario2;
            var usuario3 = new SettingsTest().Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var response = _controller.Usuarios();

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<UsuarioQueryResult>>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
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

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var response = _controller.Usuario(usuario2.Id);

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioQueryResult>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
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

            var response = _controller.UsuarioInserir(command);

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(201, responseObj.StatusCode);

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

            var response = _controller.UsuarioAlterar(command.Id, command);

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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

            _repository.Salvar(usuario);

            var response = _controller.UsuarioExcluir(usuario.Id);

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario.Id, responseObj.Value.Dados.Id);
        }

        [Test]
        public void UsuarioLogin()
        {
            var usuario = new SettingsTest().Usuario1;

            var command = new SettingsTest().UsuarioLoginCommand;

            _repository.Salvar(usuario);

            var response = _controller.UsuarioLogin(command);

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioQueryResult>>>(responseJson);

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