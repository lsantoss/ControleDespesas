using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Pessoas.Commands.Inputs
{
    [TestFixture]
    public class AdicionarPessoaCommandTest : BaseTest
    {
        private AdicionarPessoaCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().PessoaAdicionarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.True(valido);
            Assert.AreEqual(0, notificacoes);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(-1, "", "")]
        [TestCase(0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase(0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void ValidarCommand_Invalido(long idUsuario, string nome, string imagemPerfil)
        {
            _command.IdUsuario = idUsuario;
            _command.Nome = nome;
            _command.ImagemPerfil = imagemPerfil;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdUsuario_Invalido(int idUsuario)
        {
            _command.IdUsuario = idUsuario;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_Nome_Invalido(string nome)
        {
            _command.Nome = nome;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ValidarCommand_ImagemPerfil_Invalido(string imagemPerfil)
        {
            _command.ImagemPerfil = imagemPerfil;

            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(_command.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}