using ControleDespesas.Dominio.Commands.Pagamento.Input;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Commands.Pagamento
{
    public class AdicionarPagamentoCommandTest
    {
        private AdicionarPagamentoCommand _command;

        [SetUp]
        public void Setup()
        {
            _command = new AdicionarPagamentoCommand()
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

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdTipoPagamentoZerado()
        {
            _command.IdTipoPagamento = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdTipoPagamentoNegativo()
        {
            _command.IdTipoPagamento = -1;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdEmpresaZerado()
        {
            _command.IdEmpresa = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdEmpresaNegativo()
        {
            _command.IdEmpresa = -1;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdPessoaZerado()
        {
            _command.IdPessoa = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdPessoaNegativo()
        {
            _command.IdPessoa = -1;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_DescricaoMinimoDeCaractetesNull()
        {
            _command.Descricao = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_DescricaoMinimoDeCaractetesEmpty()
        {
            _command.Descricao = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_DescricaoMaximoDeCaractetes()
        {
            _command.Descricao = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_ValorZerado()
        {
            _command.Valor = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_ValorNegativo()
        {
            _command.Valor = -1;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}