using ControleDespesas.Domain.TiposPagamentos.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.TiposPagamentos.Entities
{
    public class TipoPagamentoTest : BaseTest
    {
        private TipoPagamento _tipoPagamento;

        [SetUp]
        public void Setup() => _tipoPagamento = new SettingsTest().TipoPagamento1;

        [Test]
        [TestCase(1, "Descrição")]
        public void Construtores_Valido(long id, string descricao)
        {
            var _tipoPagamento1 = new TipoPagamento(id);
            var _tipoPagamento2 = new TipoPagamento(descricao);
            var _tipoPagamento3 = new TipoPagamento(id, descricao);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_tipoPagamento1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_tipoPagamento2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_tipoPagamento3.FormatarJsonDeSaida());

            Assert.True(_tipoPagamento1.Valido);
            Assert.AreEqual(0, _tipoPagamento1.Notificacoes.Count);

            Assert.True(_tipoPagamento2.Valido);
            Assert.AreEqual(0, _tipoPagamento2.Notificacoes.Count);

            Assert.True(_tipoPagamento3.Valido);
            Assert.AreEqual(0, _tipoPagamento3.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(0, "")]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Construtores_Invalido(long id, string descricao)
        {
            var _tipoPagamento1 = new TipoPagamento(id);
            var _tipoPagamento2 = new TipoPagamento(descricao);
            var _tipoPagamento3 = new TipoPagamento(id, descricao);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_tipoPagamento1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_tipoPagamento2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_tipoPagamento3.FormatarJsonDeSaida());

            Assert.False(_tipoPagamento1.Valido);
            Assert.AreNotEqual(0, _tipoPagamento1.Notificacoes.Count);

            Assert.False(_tipoPagamento2.Valido);
            Assert.AreNotEqual(0, _tipoPagamento2.Notificacoes.Count);

            Assert.False(_tipoPagamento3.Valido);
            Assert.AreNotEqual(0, _tipoPagamento3.Notificacoes.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void DefinirId_Invalido(long id)
        {
            _tipoPagamento.DefinirId(id);

            TestContext.WriteLine(_tipoPagamento.FormatarJsonDeSaida());

            Assert.AreEqual(id, _tipoPagamento.Id);
            Assert.False(_tipoPagamento.Valido);
            Assert.AreNotEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void DefinirDescricao_Invalido(string descricao)
        {
            _tipoPagamento.DefinirDescricao(descricao);

            TestContext.WriteLine(_tipoPagamento.FormatarJsonDeSaida());

            Assert.AreEqual(descricao, _tipoPagamento.Descricao);
            Assert.False(_tipoPagamento.Valido);
            Assert.AreNotEqual(0, _tipoPagamento.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _tipoPagamento = null;
    }
}