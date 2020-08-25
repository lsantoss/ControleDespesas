using ControleDespesas.Dominio.Commands.Pagamento.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pagamento
{
    public class ApagarPagamentoCommandTest
    {
        private ApagarPagamentoCommand _command;

        public ApagarPagamentoCommandTest() => _command = new ApagarPagamentoCommand() { Id = 1 };

        [Fact]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.Equal(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            _command.Id = 0;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            _command.Id = -1;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }
    }
}