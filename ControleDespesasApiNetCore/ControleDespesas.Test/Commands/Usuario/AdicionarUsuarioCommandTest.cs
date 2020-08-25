using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Enums;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Usuario
{
    public class AdicionarUsuarioCommandTest
    {
        private AdicionarUsuarioCommand _command;

        [SetUp]
        public void Setup()
        {
            _command = new AdicionarUsuarioCommand()
            {
                Login = "lucas@123",
                Senha = "Senha123",
                Privilegio = EPrivilegioUsuario.Admin
            };
        }

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
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

        [Test]
        public void ValidarCommand_SenhaMaximoDeCaractetes()
        {
            _command.Senha = "1Aaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_SenhaContemLetrasMaiusculas()
        {
            _command.Senha = "aaaaa1";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_SenhaContemLetrasMinusculas()
        {
            _command.Senha = "AAAAA1";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_SenhaContemLetrasNumeros()
        {
            _command.Senha = "AAAAAa";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_PrivilegioZerado()
        {
            _command.Privilegio = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_PrivilegioNegativo()
        {
            _command.Privilegio = (EPrivilegioUsuario)(-1);
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}