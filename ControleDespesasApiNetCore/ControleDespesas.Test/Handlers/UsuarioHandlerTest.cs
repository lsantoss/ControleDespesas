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
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IUsuarioRepositorio IUsuarioRepos = new UsuarioRepositorio(mockOptions.Object);

            UsuarioHandler handler = new UsuarioHandler(IUsuarioRepos);

            AdicionarUsuarioCommand usuarioCommand = new AdicionarUsuarioCommand()
            {
                Login = "LoginUsuario",
                Senha = "Senha123Usuario",
                Privilegio = EPrivilegioUsuario.Admin
            };

            ICommandResult<Notificacao> retorno = handler.Handler(usuarioCommand);
            AdicionarUsuarioCommandOutput retornoDados = (AdicionarUsuarioCommandOutput)retorno.Dados;

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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Usuario usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);
            new UsuarioRepositorio(mockOptions.Object).Salvar(usuario);

            IUsuarioRepositorio IUsuarioRepos = new UsuarioRepositorio(mockOptions.Object);
            UsuarioHandler handler = new UsuarioHandler(IUsuarioRepos);

            AtualizarUsuarioCommand usuarioCommand = new AtualizarUsuarioCommand()
            {
                Id = 1,
                Login = "LoginUsuario - Editado",
                Senha = "Senha123Editado",
                Privilegio = EPrivilegioUsuario.ReadOnly
            };

            ICommandResult<Notificacao> retorno = handler.Handler(usuarioCommand);
            AtualizarUsuarioCommandOutput retornoDados = (AtualizarUsuarioCommandOutput)retorno.Dados;

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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Usuario usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);
            new UsuarioRepositorio(mockOptions.Object).Salvar(usuario);

            IUsuarioRepositorio IUsuarioRepos = new UsuarioRepositorio(mockOptions.Object);
            UsuarioHandler handler = new UsuarioHandler(IUsuarioRepos);

            ApagarUsuarioCommand usuarioCommand = new ApagarUsuarioCommand() { Id = 1 };

            ICommandResult<Notificacao> retorno = handler.Handler(usuarioCommand);
            ApagarUsuarioCommandOutput retornoDados = (ApagarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuarioCommand.Id, retornoDados.Id);
        }

        [Test]
        public void Handler_LoginUsuario()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Usuario usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);
            new UsuarioRepositorio(mockOptions.Object).Salvar(usuario);

            IUsuarioRepositorio IUsuarioRepos = new UsuarioRepositorio(mockOptions.Object);
            UsuarioHandler handler = new UsuarioHandler(IUsuarioRepos);

            LoginUsuarioCommand usuarioCommand = new LoginUsuarioCommand()
            {
                Login = "LoginUsuario",
                Senha = "Senha123Usuario"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(usuarioCommand);
            UsuarioQueryResult retornoDados = (UsuarioQueryResult)retorno.Dados;

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