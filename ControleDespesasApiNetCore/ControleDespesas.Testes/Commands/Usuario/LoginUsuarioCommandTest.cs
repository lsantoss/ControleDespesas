using ControleDespesas.Dominio.Commands.Usuario.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Usuario
{
    public class LoginUsuarioCommandTest
    {
        private readonly LoginUsuarioCommand _command;

        public LoginUsuarioCommandTest()
        {
            _command = new LoginUsuarioCommand()
            {
                Login = "lucas123",
                Senha = "Senha123"
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            LoginUsuarioCommand command = _command;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetesNull()
        {
            _command.Login = null;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetesEmpty()
        {
            _command.Login = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes()
        {
            _command.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetesNull()
        {
            _command.Senha = null;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetesEmpty()
        {
            _command.Senha = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }
    }
}