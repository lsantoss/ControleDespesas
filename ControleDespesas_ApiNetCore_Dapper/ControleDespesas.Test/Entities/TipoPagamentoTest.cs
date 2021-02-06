using ControleDespesas.Domain.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entities
{
    public class TipoPagamentoTest : BaseTest
    {
        private TipoPagamento _tipoPagamento;

        [SetUp]
        public void Setup() => _tipoPagamento = new SettingsTest().TipoPagamento1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_tipoPagamento));

            Assert.True(_tipoPagamento.Valido);
            Assert.AreEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_DescricaoInvalida(string descricao)
        {
            _tipoPagamento.Descricao = descricao;
            _tipoPagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_tipoPagamento));

            Assert.False(_tipoPagamento.Valido);
            Assert.AreNotEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _tipoPagamento = null;
    }
}