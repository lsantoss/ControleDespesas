using ControleDespesas.Dominio.Commands.Usuario.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Usuario
{
    public class ApagarUsuarioCommandTest
    {
        private readonly ApagarUsuarioCommand _commandReadOnly;

        public ApagarUsuarioCommandTest()
        {
            _commandReadOnly = new ApagarUsuarioCommand() { Id = 1 };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            ApagarUsuarioCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            ApagarUsuarioCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            ApagarUsuarioCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}