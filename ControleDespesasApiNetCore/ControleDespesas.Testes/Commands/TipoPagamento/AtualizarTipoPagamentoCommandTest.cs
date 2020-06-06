using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.TipoPagamento
{
    public class AtualizarTipoPagamentoCommandTest
    {
        private readonly AtualizarTipoPagamentoCommand _commandReadOnly;

        public AtualizarTipoPagamentoCommandTest()
        {
            _commandReadOnly = new AtualizarTipoPagamentoCommand()
            {
                Id = 1,
                Descricao = "Saneamento"
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            AtualizarTipoPagamentoCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            AtualizarTipoPagamentoCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            AtualizarTipoPagamentoCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_True()
        {
            AtualizarTipoPagamentoCommand command = _commandReadOnly;
            command.Descricao = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_False()
        {
            AtualizarTipoPagamentoCommand command = _commandReadOnly;
            command.Descricao = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_True()
        {
            AtualizarTipoPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_False()
        {
            AtualizarTipoPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }
    }
}