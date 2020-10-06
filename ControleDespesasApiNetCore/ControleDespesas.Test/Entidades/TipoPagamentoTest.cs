using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class TipoPagamentoTest : BaseTest
    {
        private TipoPagamento _tipoPagamento;

        [SetUp]
        public void Setup() => _tipoPagamento = new TipoPagamentoTest().MockSettingsTest.TipoPagamento1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_tipoPagamento));

            Assert.True(_tipoPagamento.Valido);
            Assert.True(_tipoPagamento.Descricao.Valido);
            Assert.AreEqual(0, _tipoPagamento.Notificacoes.Count);
            Assert.AreEqual(0, _tipoPagamento.Descricao.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_DescricaoInvalida()
        {
            _tipoPagamento.Descricao = new Texto(@"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Descrição", 250);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_tipoPagamento));

            Assert.False(_tipoPagamento.Descricao.Valido);
            Assert.AreNotEqual(0, _tipoPagamento.Descricao.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _tipoPagamento = null;
    }
}