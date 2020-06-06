using ControleDespesas.Dominio.Commands.Pagamento.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pagamento
{
    public class ApagarPagamentoCommandTest
    {
        private readonly ApagarPagamentoCommand _commandReadOnly;

        public ApagarPagamentoCommandTest()
        {
            _commandReadOnly = new ApagarPagamentoCommand()
            {
                Id = 1
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            ApagarPagamentoCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            ApagarPagamentoCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            ApagarPagamentoCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}