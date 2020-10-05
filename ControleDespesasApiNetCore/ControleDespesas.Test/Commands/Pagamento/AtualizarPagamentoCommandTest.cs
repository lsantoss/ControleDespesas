﻿using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Test.AppConfigurations.Factory;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Pagamento
{
    public class AtualizarPagamentoCommandTest : BaseTest
    {
        private AtualizarPagamentoCommand _command;

        [SetUp]
        public void Setup() => _command = new AtualizarPagamentoCommandTest().MockSettingsTest.PagamentoAtualizarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdInvalido(int id)
        {
            _command.Id = id;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdTipoPagamentoInvalido(int idTipoPagamento)
        {
            _command.IdTipoPagamento = idTipoPagamento;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdEmpresaInvalido(int idEmpresa)
        {
            _command.IdEmpresa = idEmpresa;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdPessoaInvalido(int idPessoa)
        {
            _command.IdPessoa = idPessoa;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_DescricaoInvalido(string descricao)
        {
            _command.Descricao = descricao;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_ValorInvalido(double valor)
        {
            _command.Valor = valor;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}