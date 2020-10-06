﻿using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Pessoa
{
    public class ApagarPessoaCommandTest : BaseTest
    {
        private ApagarPessoaCommand _command;

        [SetUp]
        public void Setup() => _command = new ApagarPessoaCommandTest().MockSettingsTest.PessoaApagarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdInvalido(int id)
        {
            _command.Id = id;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}