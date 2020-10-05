﻿using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Test.AppConfigurations.Factory;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.TipoPagamento
{
    public class AdicionarTipoPagamentoCommandTest : BaseTest
    {
        private AdicionarTipoPagamentoCommand _command;

        [SetUp]
        public void Setup() => _command = new AdicionarTipoPagamentoCommandTest().MockSettingsTest.TipoPagamentoAdicionarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
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

        [TearDown]
        public void TearDown() => _command = null;
    }
}