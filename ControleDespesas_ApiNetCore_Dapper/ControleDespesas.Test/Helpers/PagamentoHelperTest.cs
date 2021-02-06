using ControleDespesas.Domain.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class PagamentoHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarPagamentoCommand()
        {
            var command = new SettingsTest().PagamentoAdicionarCommand;

            var entidade = PagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(entidade));

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.IdTipoPagamento, entidade.TipoPagamento.Id);
            Assert.AreEqual(command.IdEmpresa, entidade.Empresa.Id);
            Assert.AreEqual(command.IdPessoa, entidade.Pessoa.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao);
            Assert.AreEqual(command.Valor, entidade.Valor);
            Assert.AreEqual(command.DataVencimento, entidade.DataVencimento);
            Assert.AreEqual(command.DataPagamento, entidade.DataPagamento);
            Assert.AreEqual(command.ArquivoPagamento, entidade.ArquivoPagamento);
            Assert.AreEqual(command.ArquivoComprovante, entidade.ArquivoComprovante);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarPagamentoCommand()
        {
            var command = new SettingsTest().PagamentoAtualizarCommand;

            var entidade = PagamentoHelper.GerarEntidade(command);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(entidade));

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.IdTipoPagamento, entidade.TipoPagamento.Id);
            Assert.AreEqual(command.IdEmpresa, entidade.Empresa.Id);
            Assert.AreEqual(command.IdPessoa, entidade.Pessoa.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao);
            Assert.AreEqual(command.Valor, entidade.Valor);
            Assert.AreEqual(command.DataVencimento, entidade.DataVencimento);
            Assert.AreEqual(command.DataPagamento, entidade.DataPagamento);
            Assert.AreEqual(command.ArquivoPagamento, entidade.ArquivoPagamento);
            Assert.AreEqual(command.ArquivoComprovante, entidade.ArquivoComprovante);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new SettingsTest().Pagamento1;

            var command = PagamentoHelper.GerarDadosRetornoInsert(entidade);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(command));

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.TipoPagamento.Id, command.IdTipoPagamento);
            Assert.AreEqual(entidade.Empresa.Id, command.IdEmpresa);
            Assert.AreEqual(entidade.Pessoa.Id, command.IdPessoa);
            Assert.AreEqual(entidade.Descricao, command.Descricao);
            Assert.AreEqual(entidade.Valor, command.Valor);
            Assert.AreEqual(entidade.DataVencimento, command.DataVencimento);
            Assert.AreEqual(entidade.DataPagamento, command.DataPagamento);
            Assert.AreEqual(entidade.ArquivoPagamento, command.ArquivoPagamento);
            Assert.AreEqual(entidade.ArquivoComprovante, command.ArquivoComprovante);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new SettingsTest().Pagamento1;

            var command = PagamentoHelper.GerarDadosRetornoUpdate(entidade);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(command));

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.TipoPagamento.Id, command.IdTipoPagamento);
            Assert.AreEqual(entidade.Empresa.Id, command.IdEmpresa);
            Assert.AreEqual(entidade.Pessoa.Id, command.IdPessoa);
            Assert.AreEqual(entidade.Descricao, command.Descricao);
            Assert.AreEqual(entidade.Valor, command.Valor);
            Assert.AreEqual(entidade.DataVencimento, command.DataVencimento);
            Assert.AreEqual(entidade.DataPagamento, command.DataPagamento);
            Assert.AreEqual(entidade.ArquivoPagamento, command.ArquivoPagamento);
            Assert.AreEqual(entidade.ArquivoComprovante, command.ArquivoComprovante);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = new SettingsTest().Pagamento1;

            var command = PagamentoHelper.GerarDadosRetornoDelete(entidade.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(command));

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}