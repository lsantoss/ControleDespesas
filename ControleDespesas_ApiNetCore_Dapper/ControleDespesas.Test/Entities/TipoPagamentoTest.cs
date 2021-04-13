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
            TestContext.WriteLine(_tipoPagamento.FormatarJsonDeSaida());

            Assert.True(_tipoPagamento.Valido);
            Assert.AreEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_DescricaoInvalida(string descricao)
        {
            _tipoPagamento.DefinirDescricao(descricao);
            _tipoPagamento.Validar();

            TestContext.WriteLine(_tipoPagamento.FormatarJsonDeSaida());

            Assert.False(_tipoPagamento.Valido);
            Assert.AreNotEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void DefinirId(int id)
        {
            _tipoPagamento.DefinirId(id);

            TestContext.WriteLine(_tipoPagamento.FormatarJsonDeSaida());

            Assert.AreEqual(id, _tipoPagamento.Id);
            Assert.True(_tipoPagamento.Valido);
            Assert.AreEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase("Saneamento")]
        [TestCase("Telecomunicações")]
        public void DefinirNome(string descricao)
        {
            _tipoPagamento.DefinirDescricao(descricao);

            TestContext.WriteLine(_tipoPagamento.FormatarJsonDeSaida());

            Assert.AreEqual(descricao, _tipoPagamento.Descricao);
            Assert.True(_tipoPagamento.Valido);
            Assert.AreEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _tipoPagamento = null;
    }
}