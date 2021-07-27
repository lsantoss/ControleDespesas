using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Commands.Input
{
    [TestFixture]
    public class AdicionarUsuarioCommandTest : BaseTest
    {
        private AdicionarUsuarioCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().UsuarioAdicionarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.True(valido);
            Assert.AreEqual(0, notificacoes);
        }

        [Test]
        [TestCase(null, null, -1)]
        [TestCase("", "", 0)]
        [TestCase("", "aaaaa1", 0)]
        public void ValidarCommand_Invalido(string login, string senha, EPrivilegioUsuario privilegio)
        {
            _command.Login = login;
            _command.Senha = senha;
            _command.Privilegio = privilegio;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_Login_Invalido(string login)
        {
            _command.Login = login;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void ValidarCommand_Senha_Invalido(string senha)
        {
            _command.Senha = senha;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void ValidarCommand_PrivilegioInvalido(EPrivilegioUsuario privilegio)
        {
            _command.Privilegio = privilegio;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}