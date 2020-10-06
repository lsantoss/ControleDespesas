using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class PessoaTest : BaseTest
    {
        private Pessoa _pessoa;

        [SetUp]
        public void Setup() => _pessoa = new PessoaTest().MockSettingsTest.Pessoa1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pessoa));

            Assert.True(_pessoa.Valido);
            Assert.True(_pessoa.Nome.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
            Assert.AreEqual(0, _pessoa.Nome.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_NomeInvalido()
        {
            _pessoa.Nome = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Nome", 100);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pessoa));

            Assert.False(_pessoa.Nome.Valido);
            Assert.AreNotEqual(0, _pessoa.Nome.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pessoa = null;
    }
}