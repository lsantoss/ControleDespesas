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
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly UsuarioRepositorio _repository;
        private readonly UsuarioHandler _handler;

        public UsuarioHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new UsuarioRepositorio(_mockOptions.Object);
            _handler = new UsuarioHandler(_repository);
        }

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

            var retorno = _handler.Handler(usuarioCommand);

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

            _repository.Salvar(usuario);

            var retorno = _handler.Handler(usuarioCommand);

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
            var usuario = new Usuario(0, new Texto("LoginUsuario", "Login", 50), new SenhaMedia("Senha123Usuario"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            var usuarioCommand = new ApagarUsuarioCommand() { Id = 1 };

            var retorno = _handler.Handler(usuarioCommand);
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

            _repository.Salvar(usuario);

            var retorno = _handler.Handler(usuarioCommand);

            var retornoDados = (UsuarioQueryResult)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Admin, retornoDados.Privilegio);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}