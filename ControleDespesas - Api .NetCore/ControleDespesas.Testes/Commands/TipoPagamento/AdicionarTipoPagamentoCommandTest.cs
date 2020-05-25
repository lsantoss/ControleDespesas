using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.TipoPagamento
{
    public class AdicionarTipoPagamentoCommandTest
    {
        private readonly AdicionarTipoPagamentoCommand _commandReadOnly;

        public AdicionarTipoPagamentoCommandTest()
        {
            _commandReadOnly = new AdicionarTipoPagamentoCommand()
            {
                Descricao = "Saneamento"
            };
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_True()
        {
            AdicionarTipoPagamentoCommand command = _commandReadOnly;
            command.Descricao = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_False()
        {
            AdicionarTipoPagamentoCommand command = _commandReadOnly;
            command.Descricao = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_True()
        {
            AdicionarTipoPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_False()
        {
            AdicionarTipoPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }
    }
}