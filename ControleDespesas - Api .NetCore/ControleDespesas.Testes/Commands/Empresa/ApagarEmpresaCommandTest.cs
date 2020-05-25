using ControleDespesas.Dominio.Commands.Empresa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Empresa
{
    public class ApagarEmpresaCommandTest
    {
        private readonly ApagarEmpresaCommand _commandReadOnly;

        public ApagarEmpresaCommandTest()
        {
            _commandReadOnly = new ApagarEmpresaCommand()
            {
                Id = 1
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            ApagarEmpresaCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            ApagarEmpresaCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            ApagarEmpresaCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}