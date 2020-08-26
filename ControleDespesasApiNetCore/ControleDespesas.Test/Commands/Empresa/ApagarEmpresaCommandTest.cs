﻿using ControleDespesas.Dominio.Commands.Empresa.Input;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Empresa
{
    public class ApagarEmpresaCommandTest
    {
        private ApagarEmpresaCommand _command;

        [SetUp]
        public void Setup() => _command = new ApagarEmpresaCommand() { Id = 1 };

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdZerado()
        {
            _command.Id = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdNegativo()
        {
            _command.Id = -1;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}