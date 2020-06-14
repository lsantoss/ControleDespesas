using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Enums;
using Xunit;

namespace ControleDespesas.Testes.Commands.Usuario
{
    public class AtualizarUsuarioCommandTest
    {
        private readonly AtualizarUsuarioCommand _commandReadOnly;

        public AtualizarUsuarioCommandTest()
        {
            _commandReadOnly = new AtualizarUsuarioCommand()
            {
                Id = 1,
                Login = "lucas123",
                Senha = "123",
                Privilegio = EPrivilegioUsuario.Admin
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetes_True()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Login = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMinimoDeCaractetes_False()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Login = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes_True()
        {
            AtualizarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LoginMaximoDeCaractetes_False()
        {
            AtualizarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetes_True()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Senha = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMinimoDeCaractetes_False()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Senha = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMaximoDeCaractetes_True()
        {
            AtualizarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_SenhaMaximoDeCaractetes_False()
        {
            AtualizarUsuarioCommand commandTest = _commandReadOnly;
            commandTest.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_PrivilegioZerado()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Privilegio = (EPrivilegioUsuario)0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_PrivilegioNegativo()
        {
            AtualizarUsuarioCommand command = _commandReadOnly;
            command.Privilegio = (EPrivilegioUsuario)(-1);
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}