using ControleDespesas.Dominio.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entities
{
    public class EmpresaTest : BaseTest
    {
        private Empresa _empresa;

        [SetUp]
        public void Setup() => _empresa = new SettingsTest().Empresa1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_empresa));

            Assert.True(_empresa.Valido);
            Assert.AreEqual(0, _empresa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_NomeInvalido(string nome)
        {
            _empresa.Nome = nome;
            _empresa.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_empresa));

            Assert.False(_empresa.Valido);
            Assert.AreNotEqual(0, _empresa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ValidarEntidade_LogoInvalido(string logo)
        {
            _empresa.Logo = logo;
            _empresa.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_empresa));

            Assert.False(_empresa.Valido);
            Assert.AreNotEqual(0, _empresa.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _empresa = null;
    }
}