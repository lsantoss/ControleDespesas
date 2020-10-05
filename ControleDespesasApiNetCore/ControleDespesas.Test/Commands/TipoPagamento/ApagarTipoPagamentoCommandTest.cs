﻿using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Test.AppConfigurations.Factory;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.TipoPagamento
{
    public class ApagarTipoPagamentoCommandTest : BaseTest
    {
        private ApagarTipoPagamentoCommand _command;

        [SetUp]
        public void Setup() => _command = new ApagarTipoPagamentoCommandTest().MockSettingsTest.TipoPagamentoApagarCommand;

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

        [TearDown]
        public void TearDown() => _command = null;
    }
}