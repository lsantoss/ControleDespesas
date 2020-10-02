using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class TipoPagamentoHelperTest
    {
        private readonly SettingsTest _settingsTest;

        public TipoPagamentoHelperTest() => _settingsTest = new SettingsTest();

        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarTipoPagamentoCommand()
        {
            var command = _settingsTest.TipoPagamentoAdicionarCommand;

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarTipoPagamentoCommand()
        {
            var command = _settingsTest.TipoPagamentoAtualizarCommand;

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = _settingsTest.TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Descricao.ToString(), command.Descricao);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = _settingsTest.TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Descricao.ToString(), command.Descricao);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = _settingsTest.TipoPagamento1;
            var command = TipoPagamentoHelper.GerarDadosRetornoDelete(entidade.Id);
            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}