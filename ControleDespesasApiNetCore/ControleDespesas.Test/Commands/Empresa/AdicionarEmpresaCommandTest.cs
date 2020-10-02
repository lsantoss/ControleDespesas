using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Empresa
{
    public class AdicionarEmpresaCommandTest
    {
        private AdicionarEmpresaCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().EmpresaAdicionarCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarCommand_NomeInvalido(string nome)
        {
            _command.Nome = nome;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ValidarCommand_LogoInvalido(string logo)
        {
            _command.Logo = logo;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}