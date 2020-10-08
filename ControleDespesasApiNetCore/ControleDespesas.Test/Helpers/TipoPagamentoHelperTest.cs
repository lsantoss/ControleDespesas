using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class TipoPagamentoHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarTipoPagamentoCommand()
        {
            var command = MockSettingsTest.TipoPagamentoAdicionarCommand;

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(entidade));

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarTipoPagamentoCommand()
        {
            var command = MockSettingsTest.TipoPagamentoAtualizarCommand;

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(entidade));

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = MockSettingsTest.TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetornoInsert(entidade);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(command));

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Descricao.ToString(), command.Descricao);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = MockSettingsTest.TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetornoUpdate(entidade);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(command));

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Descricao.ToString(), command.Descricao);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = MockSettingsTest.TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetornoDelete(entidade.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(command));

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}