using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Test.AppConfigurations.Factory;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Usuario
{
    public class LoginUsuarioCommandTest : BaseTest
    {
        private LoginUsuarioCommand _command;

        [SetUp]
        public void Setup() => _command = new LoginUsuarioCommandTest().MockSettingsTest.UsuarioLoginCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            LoginUsuarioCommand command = _command;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_LoginInvalido(string login)
        {
            _command.Login = login;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ValidarCommand_SenhaInvalido(string senha)
        {
            _command.Senha = senha;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}