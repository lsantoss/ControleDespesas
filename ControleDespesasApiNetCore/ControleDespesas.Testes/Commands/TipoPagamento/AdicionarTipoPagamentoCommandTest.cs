using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.TipoPagamento
{
    public class AdicionarTipoPagamentoCommandTest
    {
        private AdicionarTipoPagamentoCommand _command;

        public AdicionarTipoPagamentoCommandTest() => _command = new AdicionarTipoPagamentoCommand() { Descricao = "Saneamento" };

        [Fact]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.Equal(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetesNull()
        {
            _command.Descricao = null;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetesEmpty()
        {
            _command.Descricao = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_False()
        {
            _command.Descricao = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }
    }
}