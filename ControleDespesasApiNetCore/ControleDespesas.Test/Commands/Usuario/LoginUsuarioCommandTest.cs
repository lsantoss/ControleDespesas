using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Usuario
{
    public class LoginUsuarioCommandTest
    {
        private LoginUsuarioCommand _command;

        [SetUp]
        public void Setup()
        {
            _command = new SettingsTest().UsuarioLoginCommand;
        }

        [Test]
        public void ValidarCommand_Valido()
        {
            LoginUsuarioCommand command = _command;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Test]
        public void ValidarCommand_LoginMinimoDeCaractetesNull()
        {
            _command.Login = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_LoginMinimoDeCaractetesEmpty()
        {
            _command.Login = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_LoginMaximoDeCaractetes()
        {
            _command.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_SenhaMinimoDeCaractetesNull()
        {
            _command.Senha = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_SenhaMinimoDeCaractetesEmpty()
        {
            _command.Senha = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}