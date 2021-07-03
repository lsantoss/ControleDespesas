using ControleDespesas.Domain.TiposPagamentos.Commands.Input;
using ControleDespesas.Domain.TiposPagamentos.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.TiposPagamentos.Helpers
{
    public class TipoPagamentoHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarTipoPagamentoCommand_Valido()
        {
            var command = new SettingsTest().TipoPagamentoAdicionarCommand;

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void GerarEntidade_AdcionarTipoPagamentoCommand_Invalido(string descricao)
        {
            var command = new AdicionarTipoPagamentoCommand
            {
                Descricao = descricao
            };

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarTipoPagamentoCommand_Valido()
        {
            var command = new SettingsTest().TipoPagamentoAtualizarCommand;

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(0, "")]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void GerarEntidade_AtualizarTipoPagamentoCommand_Invalido(long id, string descricao)
        {
            var command = new AtualizarTipoPagamentoCommand
            {
                Id = id,
                Descricao = descricao
            };

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetorno_TipoPagamento()
        {
            var entidade = new SettingsTest().TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetorno(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Descricao, command.Descricao);
        }

        [Test]
        public void GerarDadosRetorno_Id()
        {
            var entidade = new SettingsTest().TipoPagamento1;

            var command = TipoPagamentoHelper.GerarDadosRetornoDelete(entidade.Id);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}