using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Services;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Models;
using LSCode.Facilitador.Api.Models.Results;
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
    public class UsuarioControllerTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsAPI>> _mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
        private readonly UsuarioRepositorio _repository;
        private readonly UsuarioHandler _handler;
        private readonly TokenJWTService _tokenJWTService;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);
            _mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new UsuarioRepositorio(_mockOptionsInfra.Object);
            _handler = new UsuarioHandler(_repository);
            _tokenJWTService = new TokenJWTService(_mockOptionsAPI.Object);
            _controller = new UsuarioController(_repository, _handler, _mockOptionsAPI.Object, _tokenJWTService);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void UsuarioHealthCheck()
        {
            var response = _controller.UsuarioHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Usuário OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Usuarios()
        {
            var usuario0 = new Usuario(0, new Texto("Login0", "Login", 100), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            var usuario1 = new Usuario(0, new Texto("Login1", "Login", 100), new SenhaMedia("Senha1231"), EPrivilegioUsuario.Admin);
            var usuario2 = new Usuario(0, new Texto("Login2", "Login", 100), new SenhaMedia("Senha1232"), EPrivilegioUsuario.ReadOnly);

            _repository.Salvar(usuario0);
            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);

            var response = _controller.Usuarios().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<UsuarioQueryResult>, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de usuários obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(usuario0.Login.ToString(), responseObj.Value.Dados[0].Login);
            Assert.AreEqual(usuario0.Senha.ToString(), responseObj.Value.Dados[0].Senha);
            Assert.AreEqual(usuario0.Privilegio, responseObj.Value.Dados[0].Privilegio);

            Assert.AreEqual(2, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(usuario1.Login.ToString(), responseObj.Value.Dados[1].Login);
            Assert.AreEqual(usuario1.Senha.ToString(), responseObj.Value.Dados[1].Senha);
            Assert.AreEqual(usuario0.Privilegio, responseObj.Value.Dados[0].Privilegio);

            Assert.AreEqual(3, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(usuario2.Login.ToString(), responseObj.Value.Dados[2].Login);
            Assert.AreEqual(usuario2.Senha.ToString(), responseObj.Value.Dados[2].Senha);
            Assert.AreEqual(usuario0.Privilegio, responseObj.Value.Dados[0].Privilegio);
        }

        [Test]
        public void Usuario()
        {
            var usuario0 = new Usuario(0, new Texto("Login0", "Login", 100), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            var usuario1 = new Usuario(0, new Texto("Login1", "Login", 100), new SenhaMedia("Senha1231"), EPrivilegioUsuario.Admin);
            var usuario2 = new Usuario(0, new Texto("Login2", "Login", 100), new SenhaMedia("Senha1232"), EPrivilegioUsuario.ReadOnly);

            var command = new ObterUsuarioPorIdCommand() { Id = 2 };

            _repository.Salvar(usuario0);
            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);

            var response = _controller.Usuario(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<UsuarioQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(2, responseObj.Value.Dados.Id);
            Assert.AreEqual(usuario1.Login.ToString(), responseObj.Value.Dados.Login);
            Assert.AreEqual(usuario1.Senha.ToString(), responseObj.Value.Dados.Senha);
            Assert.AreEqual(usuario1.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [Test]
        public void UsuarioInserir()
        {
            var command = new AdicionarUsuarioCommand()
            {
                Login = "Login",
                Senha = "Senha123",
                Privilegio = EPrivilegioUsuario.Admin
            };

            var response = _controller.UsuarioInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarUsuarioCommandOutput, Notificacao>>>(responseJson);

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
            var usuario = new Usuario(0, new Texto("Login", "Login", 100), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            var command = new AtualizarUsuarioCommand()
            {
                Id = 1,
                Login = "Login - Editado",
                Senha = "Senha123Edit",
                Privilegio = EPrivilegioUsuario.ReadOnly
            };

            _repository.Salvar(usuario);

            var response = _controller.UsuarioAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarUsuarioCommandOutput, Notificacao>>>(responseJson);

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
            var usuario = new Usuario(0, new Texto("Login", "Login", 100), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            var command = new ApagarUsuarioCommand() { Id = 1 };

            _repository.Salvar(usuario);

            var response = _controller.UsuarioExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarUsuarioCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [Test]
        public void UsuarioLogin()
        {
            var usuario = new Usuario(0, new Texto("Login", "Login", 100), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            var command = new LoginUsuarioCommand()
            {
                Login = "Login",
                Senha = "Senha123"
            };

            _repository.Salvar(usuario);

            var response = _controller.UsuarioLogin(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<UsuarioQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(usuario.Login.ToString(), responseObj.Value.Dados.Login);
            Assert.AreEqual(usuario.Senha.ToString(), responseObj.Value.Dados.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Admin, responseObj.Value.Dados.Privilegio);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}