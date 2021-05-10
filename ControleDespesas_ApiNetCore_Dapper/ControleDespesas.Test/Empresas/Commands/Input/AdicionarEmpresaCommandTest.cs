using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Empresas.Commands.Input
{
    public class AdicionarEmpresaCommandTest : BaseTest
    {
        private AdicionarEmpresaCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().EmpresaAdicionarCommand;

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
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_NomeInvalido(string nome)
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
        public void ValidarCommand_LogoInvalido(string logo)
        {
            _command.Logo = logo;
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