using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Pessoa
{
    public class ApagarPessoaCommandTest
    {
        private ApagarPessoaCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().PessoaApagarCommand;

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