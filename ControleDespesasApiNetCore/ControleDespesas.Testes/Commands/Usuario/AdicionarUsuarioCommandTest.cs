using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Enums;
using Xunit;

namespace ControleDespesas.Testes.Commands.Usuario
{
    public class AdicionarUsuarioCommandTest
    {
        private readonly AdicionarUsuarioCommand _commandReadOnly;

        public AdicionarUsuarioCommandTest()
        {
            _commandReadOnly = new AdicionarUsuarioCommand()
            {
                Login = "lucas123",
                Senha = "123",
                Privilegio = EPrivilegioUsuario.Admin
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetes_True()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            command.Login = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetes_False()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            command.Login = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes_True()
        {
            AdicionarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes_False()
        {
            AdicionarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetes_True()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            command.Senha = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetes_False()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            command.Senha = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMaximoDeCaractetes_True()
        {
            AdicionarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMaximoDeCaractetes_False()
        {
            AdicionarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_PrivilegioZerado()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            command.Privilegio = (EPrivilegioUsuario)0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_PrivilegioNegativo()
        {
            AdicionarUsuarioCommand command = _commandReadOnly;
            command.Privilegio = (EPrivilegioUsuario)(-1);
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}