using ControleDespesas.Dominio.Commands.Pagamento.Input;
using System;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pagamento
{
    public class AtualizarPagamentoCommandTest
    {
        private readonly AtualizarPagamentoCommand _commandReadOnly;

        public AtualizarPagamentoCommandTest()
        {
            _commandReadOnly = new AtualizarPagamentoCommand()
            {
                Id = 1,
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
            AtualizarPagamentoCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdTipoPagamentoZerado()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.IdTipoPagamento = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdTipoPagamentoNegativo()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.IdTipoPagamento = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdEmpresaZerado()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.IdEmpresa = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdEmpresaNegativo()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.IdEmpresa = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdPessoaZerado()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.IdPessoa = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdPessoaNegativo()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.IdPessoa = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_True()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.Descricao = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMinimoDeCaractetes_False()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.Descricao = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_True()
        {
            AtualizarPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_DescricaoMaximoDeCaractetes_False()
        {
            AtualizarPagamentoCommand commandTest = _commandReadOnly;
            commandTest.Descricao = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_ValorZerado()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.Valor = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_ValorNegativo()
        {
            AtualizarPagamentoCommand command = _commandReadOnly;
            command.Valor = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}