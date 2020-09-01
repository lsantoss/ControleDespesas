using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    class UsuarioHandlerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarUsuario()
        {
            var usuarioCommand = new AdicionarUsuarioCommand()
            {
                Login = "LoginUsuario",
                Senha = "Senha123Usuario",
                Privilegio = EPrivilegioUsuario.Admin
            };

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new UsuarioRepositorio(mockOptions.Object);
            var handler = new UsuarioHandler(repository);

            var retorno = handler.Handler(usuarioCommand);

            var retornoDados = (AdicionarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(usuarioCommand.Privilegio, retornoDados.Privilegio);
        }

        [Test]
        public void Handler_AtualizarUsuario()
        {
            var usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);

            var usuarioCommand = new AtualizarUsuarioCommand()
            {
                Id = 1,
                Login = "LoginUsuario - Editado",
                Senha = "Senha123Editado",
                Privilegio = EPrivilegioUsuario.ReadOnly
            };

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new UsuarioRepositorio(mockOptions.Object);
            var handler = new UsuarioHandler(repository);

            repository.Salvar(usuario);

            var retorno = handler.Handler(usuarioCommand);

            var retornoDados = (AtualizarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuarioCommand.Id, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(usuarioCommand.Privilegio, retornoDados.Privilegio);
        }

        [Test]
        public void Handler_ApagarUsuario()
        {
            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new UsuarioRepositorio(mockOptions.Object);

            var handler = new UsuarioHandler(repository);

            var usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);
            repository.Salvar(usuario);

            var usuarioCommand = new ApagarUsuarioCommand() { Id = 1 };

            var retorno = handler.Handler(usuarioCommand);
            var retornoDados = (ApagarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuarioCommand.Id, retornoDados.Id);
        }

        [Test]
        public void Handler_LoginUsuario()
        {
            var usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);

            var usuarioCommand = new LoginUsuarioCommand()
            {
                Login = "LoginUsuario",
                Senha = "Senha123Usuario"
            };

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new UsuarioRepositorio(mockOptions.Object);
            var handler = new UsuarioHandler(repository);

            repository.Salvar(usuario);

            var retorno = handler.Handler(usuarioCommand);

            var retornoDados = (UsuarioQueryResult)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Admin, retornoDados.Privilegio);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}