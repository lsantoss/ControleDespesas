using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Test.AppConfigurations.Factory;
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
            var command = MockSettingsTest.PagamentoAdicionarCommand;

            var entidade = PagamentoHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.IdTipoPagamento, entidade.TipoPagamento.Id);
            Assert.AreEqual(command.IdEmpresa, entidade.Empresa.Id);
            Assert.AreEqual(command.IdPessoa, entidade.Pessoa.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao.ToString());
            Assert.AreEqual(command.Valor, entidade.Valor);
            Assert.AreEqual(command.DataVencimento, entidade.DataVencimento);
            Assert.AreEqual(command.DataPagamento, entidade.DataPagamento);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Descricao.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Descricao.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarPagamentoCommand()
        {
            var command = MockSettingsTest.PagamentoAtualizarCommand;

            var entidade = PagamentoHelper.GerarEntidade(command);

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.IdTipoPagamento, entidade.TipoPagamento.Id);
            Assert.AreEqual(command.IdEmpresa, entidade.Empresa.Id);
            Assert.AreEqual(command.IdPessoa, entidade.Pessoa.Id);
            Assert.AreEqual(command.Descricao, entidade.Descricao.ToString());
            Assert.AreEqual(command.Valor, entidade.Valor);
            Assert.AreEqual(command.DataVencimento, entidade.DataVencimento);
            Assert.AreEqual(command.DataPagamento, entidade.DataPagamento);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Descricao.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Descricao.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = MockSettingsTest.Pagamento1;

            var command = PagamentoHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.TipoPagamento.Id, command.IdTipoPagamento);
            Assert.AreEqual(entidade.Empresa.Id, command.IdEmpresa);
            Assert.AreEqual(entidade.Pessoa.Id, command.IdPessoa);
            Assert.AreEqual(entidade.Descricao.ToString(), command.Descricao);
            Assert.AreEqual(entidade.Valor, command.Valor);
            Assert.AreEqual(entidade.DataVencimento, command.DataVencimento);
            Assert.AreEqual(entidade.DataPagamento, command.DataPagamento);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = MockSettingsTest.Pagamento1;

            var command = PagamentoHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.TipoPagamento.Id, command.IdTipoPagamento);
            Assert.AreEqual(entidade.Empresa.Id, command.IdEmpresa);
            Assert.AreEqual(entidade.Pessoa.Id, command.IdPessoa);
            Assert.AreEqual(entidade.Descricao.ToString(), command.Descricao);
            Assert.AreEqual(entidade.Valor, command.Valor);
            Assert.AreEqual(entidade.DataVencimento, command.DataVencimento);
            Assert.AreEqual(entidade.DataPagamento, command.DataPagamento);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = MockSettingsTest.Pagamento1;
            var command = PagamentoHelper.GerarDadosRetornoDelete(entidade.Id);
            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}