using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Services;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Models;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class UsuarioControllerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void UsuarioHealthCheck()
        {
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var responseJson = controller.UsuarioHealthCheck().Result.ToJson();

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
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            Usuario usuario0 = new Usuario(0, new Texto("Login0", "Login", 100), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            Usuario usuario1 = new Usuario(0, new Texto("Login1", "Login", 100), new SenhaMedia("Senha1231"), EPrivilegioUsuario.Admin);
            Usuario usuario2 = new Usuario(0, new Texto("Login2", "Login", 100), new SenhaMedia("Senha1232"), EPrivilegioUsuario.ReadOnly);

            repository.Salvar(usuario0);
            repository.Salvar(usuario1);
            repository.Salvar(usuario2);

            var responseJson = controller.Usuarios().Result.ToJson().Replace("_id", "id");

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
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            Usuario usuario0 = new Usuario(0, new Texto("Login0", "Login", 100), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            Usuario usuario1 = new Usuario(0, new Texto("Login1", "Login", 100), new SenhaMedia("Senha1231"), EPrivilegioUsuario.Admin);
            Usuario usuario2 = new Usuario(0, new Texto("Login2", "Login", 100), new SenhaMedia("Senha1232"), EPrivilegioUsuario.ReadOnly);

            repository.Salvar(usuario0);
            repository.Salvar(usuario1);
            repository.Salvar(usuario2);

            var command = new ObterUsuarioPorIdCommand() { Id = 2 };

            var responseJson = controller.Usuario(command).Result.ToJson().Replace("_id", "id");

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
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var command = new AdicionarUsuarioCommand()
            {
                Login = "Login",
                Senha = "Senha123",
                Privilegio = EPrivilegioUsuario.Admin
            };

            var responseJson = controller.UsuarioInserir(command).Result.ToJson().Replace("_id", "id");

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
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            Usuario usuario = new Usuario(0, new Texto("Login", "Login", 100), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            repository.Salvar(usuario);

            var command = new AtualizarUsuarioCommand()
            {
                Id = 1,
                Login = "Login - Editado",
                Senha = "Senha123Edit",
                Privilegio = EPrivilegioUsuario.ReadOnly
            };

            var responseJson = controller.UsuarioAlterar(command).Result.ToJson().Replace("_id", "id");

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
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            Usuario usuario = new Usuario(0, new Texto("Login", "Login", 100), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            repository.Salvar(usuario);

            var command = new ApagarUsuarioCommand() { Id = 1 };

            var responseJson = controller.UsuarioExcluir(command).Result.ToJson().Replace("_id", "id");

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
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            UsuarioHandler handler = new UsuarioHandler(repository);

            TokenJWTService tokenJWTService = new TokenJWTService(mockOptionsAPI.Object);

            UsuarioController controller = new UsuarioController(repository, handler, mockOptionsAPI.Object, tokenJWTService);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            Usuario usuario = new Usuario(0, new Texto("Login", "Login", 100), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            repository.Salvar(usuario);

            var command = new LoginUsuarioCommand()
            {
                Login = "Login",
                Senha = "Senha123"
            };

            var responseJson = controller.UsuarioLogin(command).Result.ToJson().Replace("_id", "id");

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
        public void TearDown() => DroparBaseDeDados();
    }
}