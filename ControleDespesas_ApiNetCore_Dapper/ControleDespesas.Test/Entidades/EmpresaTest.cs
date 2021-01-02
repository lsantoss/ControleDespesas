using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
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
        public void ValidarEntidade_NomeInvalido()
        {
            _empresa.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            _empresa.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_empresa));

            Assert.False(_empresa.Valido);
            Assert.AreNotEqual(0, _empresa.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _empresa = null;
    }
}