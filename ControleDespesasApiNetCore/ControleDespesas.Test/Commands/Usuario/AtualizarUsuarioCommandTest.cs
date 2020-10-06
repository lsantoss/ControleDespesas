﻿using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Usuario
{
    public class AtualizarUsuarioCommandTest : BaseTest
    {
        private AtualizarUsuarioCommand _command;

        [SetUp]
        public void Setup() => _command = new AtualizarUsuarioCommandTest().MockSettingsTest.UsuarioAtualizarCommand;

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

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_LoginInvalido(string login)
        {
            _command.Login = login;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void ValidarCommand_SenhaInvalida(string senha)
        {
            _command.Senha = senha;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_PrivilegioInvalido(int privilegio)
        {
            _command.Privilegio = (EPrivilegioUsuario)privilegio;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}