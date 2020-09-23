using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Pessoa
{
    public class AtualizarPessoaCommandTest
    {
        private AtualizarPessoaCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().PessoaAtualizarCommand;

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

        [Test]
        public void ValidarCommand_NomeMinimoDeCaractetesNull()
        {
            _command.Nome = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_NomeMinimoDeCaractetesEmpty()
        {
            _command.Nome = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_NomeMaximoDeCaractetes()
        {
            _command.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_ImagemPerfilMinimoDeCaractetesNull()
        {
            _command.ImagemPerfil = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_ImagemPerfilMinimoDeCaractetesEmpty()
        {
            _command.ImagemPerfil = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}