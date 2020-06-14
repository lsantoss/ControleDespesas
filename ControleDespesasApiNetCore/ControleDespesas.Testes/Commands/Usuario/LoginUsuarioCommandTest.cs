using ControleDespesas.Dominio.Commands.Usuario.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Usuario
{
    public class LoginUsuarioCommandTest
    {
        private readonly LoginUsuarioCommand _commandReadOnly;

        public LoginUsuarioCommandTest()
        {
            _commandReadOnly = new LoginUsuarioCommand()
            {
                Login = "lucas123",
                Senha = "123"
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            LoginUsuarioCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetes_True()
        {
            LoginUsuarioCommand command = _commandReadOnly;
            command.Login = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetes_False()
        {
            LoginUsuarioCommand command = _commandReadOnly;
            command.Login = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes_True()
        {
            LoginUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes_False()
        {
            LoginUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetes_True()
        {
            LoginUsuarioCommand command = _commandReadOnly;
            command.Senha = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetes_False()
        {
            LoginUsuarioCommand command = _commandReadOnly;
            command.Senha = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMaximoDeCaractetes_True()
        {
            LoginUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMaximoDeCaractetes_False()
        {
            LoginUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }
    }
}