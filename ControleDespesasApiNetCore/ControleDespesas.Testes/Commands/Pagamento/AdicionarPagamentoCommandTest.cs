using ControleDespesas.Dominio.Commands.Pagamento.Input;
using System;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pagamento
{
    public class AdicionarPagamentoCommandTest
    {
        private readonly AdicionarPagamentoCommand _commandReadOnly;

        public AdicionarPagamentoCommandTest()
        {
            _commandReadOnly = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = 15,
                IdEmpresa = 4,
                IdPessoa = 2,
                Descricao = "Pagamento do mês de Maio de Luz Elétrica",
                Valor = 89.75,
                DataPagamento = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(1),
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdTipoPagamentoZerado()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.IdTipoPagamento = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdTipoPagamentoNegativo()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.IdTipoPagamento = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdEmpresaZerado()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.IdEmpresa = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdEmpresaNegativo()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.IdEmpresa = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdPessoaZerado()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.IdPessoa = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdPessoaNegativo()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.IdPessoa = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_True()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.Descricao = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_False()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.Descricao = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_True()
        {
            AdicionarPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_False()
        {
            AdicionarPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_ValorZerado()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.Valor = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_ValorNegativo()
        {
            AdicionarPagamentoCommand command = _commandReadOnly;
            command.Valor = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}