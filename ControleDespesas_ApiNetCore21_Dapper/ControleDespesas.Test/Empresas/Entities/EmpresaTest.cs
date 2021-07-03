using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Empresas.Entities
{
    [TestFixture]
    public class EmpresaTest : BaseTest
    {
        private Empresa _empresa;

        [SetUp]
        public void Setup() => _empresa = new SettingsTest().Empresa1;

        [Test]
        [TestCase(1, "Oi", "Logo")]
        public void Construtores_Valido(long id, string nome, string logo)
        {
            var _empresa1 = new Empresa(id);
            var _empresa2 = new Empresa(nome, logo);
            var _empresa3 = new Empresa(id, nome, logo);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_empresa1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_empresa2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_empresa3.FormatarJsonDeSaida());

            Assert.True(_empresa1.Valido);
            Assert.AreEqual(0, _empresa1.Notificacoes.Count);

            Assert.True(_empresa2.Valido);
            Assert.AreEqual(0, _empresa2.Notificacoes.Count);

            Assert.True(_empresa3.Valido);
            Assert.AreEqual(0, _empresa3.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(0, "", "")]
        public void Construtores_Invalido(long id, string nome, string logo)
        {
            var _empresa1 = new Empresa(id);
            var _empresa2 = new Empresa(nome, logo);
            var _empresa3 = new Empresa(id, nome, logo);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_empresa1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_empresa2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_empresa3.FormatarJsonDeSaida());

            Assert.False(_empresa1.Valido);
            Assert.AreNotEqual(0, _empresa1.Notificacoes.Count);

            Assert.False(_empresa2.Valido);
            Assert.AreNotEqual(0, _empresa2.Notificacoes.Count);

            Assert.False(_empresa3.Valido);
            Assert.AreNotEqual(0, _empresa3.Notificacoes.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void DefinirId_Invalido(long id)
        {
            _empresa.DefinirId(id);

            TestContext.WriteLine(_empresa.FormatarJsonDeSaida());

            Assert.AreEqual(id, _empresa.Id);
            Assert.False(_empresa.Valido);
            Assert.AreNotEqual(0, _empresa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void DefinirNome_Invalido(string nome)
        {
            _empresa.DefinirNome(nome);

            TestContext.WriteLine(_empresa.FormatarJsonDeSaida());

            Assert.AreEqual(nome, _empresa.Nome);
            Assert.False(_empresa.Valido);
            Assert.AreNotEqual(0, _empresa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void DefinirLogo_Invalido(string logo)
        {
            _empresa.DefinirLogo(logo);

            TestContext.WriteLine(_empresa.FormatarJsonDeSaida());

            Assert.AreEqual(logo, _empresa.Logo);
            Assert.False(_empresa.Valido);
            Assert.AreNotEqual(0, _empresa.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _empresa = null;
    }
}