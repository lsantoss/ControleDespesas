using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.TipoPagamento
{
    public class ApagarTipoPagamentoCommandTest
    {
        private readonly ApagarTipoPagamentoCommand _commandReadOnly;

        public ApagarTipoPagamentoCommandTest()
        {
            _commandReadOnly = new ApagarTipoPagamentoCommand()
            {
                Id = 1
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            ApagarTipoPagamentoCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            ApagarTipoPagamentoCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            ApagarTipoPagamentoCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}