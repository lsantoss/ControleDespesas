using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Empresa
{
    public class AdicionarEmpresaCommandTest : BaseTest
    {
        private AdicionarEmpresaCommand _command;

        [SetUp]
        public void Setup() => _command = new AdicionarEmpresaCommandTest().MockSettingsTest.EmpresaAdicionarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

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

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

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

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}