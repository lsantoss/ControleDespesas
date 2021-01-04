using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class PagamentoTest : BaseTest
    {
        private Pagamento _pagamento;

        [SetUp]
        public void Setup() => _pagamento = new SettingsTest().Pagamento1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.True(_pagamento.Valido);
            Assert.AreEqual(0, _pagamento.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_DescricaoInvalida()
        {
            _pagamento.Descricao = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                     aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                     aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            _pagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.False(_pagamento.Valido);
            Assert.AreNotEqual(0, _pagamento.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pagamento = null;
    }
}