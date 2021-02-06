using ControleDespesas.Dominio.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entities
{
    public class PessoaTest : BaseTest
    {
        private Pessoa _pessoa;

        [SetUp]
        public void Setup() => _pessoa = new SettingsTest().Pessoa1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pessoa));

            Assert.True(_pessoa.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarEntidade_IdUsuarioInvalido(int id)
        {
            _pessoa.Usuario.Id = id;
            _pessoa.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pessoa));

            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_NomeInvalido(string nome)
        {
            _pessoa.Nome = nome;
            _pessoa.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pessoa));

            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ValidarEntidade_ImagemPerfilInvalida(string imagemPerfil)
        {
            _pessoa.ImagemPerfil = imagemPerfil;
            _pessoa.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pessoa));

            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pessoa = null;
    }
}