using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Handlers
{
    public class PagamentoHandlerTest : DatabaseTest
    {
        private readonly TipoPagamentoRepositorio _repositoryTipoPagamento;
        private readonly EmpresaRepositorio _repositoryEmpresa;
        private readonly PessoaRepositorio _repositoryPessoa;
        private readonly PagamentoRepositorio _repositoryPagamento;
        private readonly PagamentoHandler _handler;

        public PagamentoHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repositoryTipoPagamento = new TipoPagamentoRepositorio(optionsInfraData);
            _repositoryEmpresa = new EmpresaRepositorio(optionsInfraData);
            _repositoryPessoa = new PessoaRepositorio(optionsInfraData);
            _repositoryPagamento = new PagamentoRepositorio(optionsInfraData);
            _handler = new PagamentoHandler(_repositoryPagamento, _repositoryEmpresa, _repositoryPessoa, _repositoryTipoPagamento);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPagamento()
        {
            var tipoPagamento = MockSettingsTest.Pagamento1.TipoPagamento;
            var empresa = MockSettingsTest.Pagamento1.Empresa;
            var pessoa = MockSettingsTest.Pagamento1.Pessoa;

            var pagamentoCommand = MockSettingsTest.PagamentoAdicionarCommand;

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _handler.Handler(pagamentoCommand);

            var retornoDados = (AdicionarPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(pagamentoCommand.IdTipoPagamento, retornoDados.IdTipoPagamento);
            Assert.AreEqual(pagamentoCommand.IdEmpresa, retornoDados.IdEmpresa);
            Assert.AreEqual(pagamentoCommand.IdPessoa, retornoDados.IdPessoa);
            Assert.AreEqual(pagamentoCommand.Descricao, retornoDados.Descricao);
            Assert.AreEqual(pagamentoCommand.Valor, retornoDados.Valor);
            Assert.AreEqual(pagamentoCommand.DataVencimento.Date, retornoDados.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamentoCommand.DataPagamento).Date, Convert.ToDateTime(retornoDados.DataPagamento).Date);
        }

        [Test]
        public void Handler_AtualizarPagamento()
        {
            var tipoPagamento = MockSettingsTest.Pagamento1.TipoPagamento;
            var empresa = MockSettingsTest.Pagamento1.Empresa;
            var pessoa = MockSettingsTest.Pagamento1.Pessoa;
            var pagamento = MockSettingsTest.Pagamento1;

            var pagamentoCommand = MockSettingsTest.PagamentoAtualizarCommand;

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _handler.Handler(pagamentoCommand);

            var retornoDados = (AtualizarPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pagamentoCommand.Id, retornoDados.Id);
            Assert.AreEqual(pagamentoCommand.IdTipoPagamento, retornoDados.IdTipoPagamento);
            Assert.AreEqual(pagamentoCommand.IdEmpresa, retornoDados.IdEmpresa);
            Assert.AreEqual(pagamentoCommand.IdPessoa, retornoDados.IdPessoa);
            Assert.AreEqual(pagamentoCommand.Descricao, retornoDados.Descricao);
            Assert.AreEqual(pagamentoCommand.Valor, retornoDados.Valor);
            Assert.AreEqual(pagamentoCommand.DataVencimento.Date, retornoDados.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamentoCommand.DataPagamento).Date, Convert.ToDateTime(retornoDados.DataPagamento).Date);
        }

        [Test]
        public void Handler_ApagarPagamento()
        {
            var tipoPagamento = MockSettingsTest.Pagamento1.TipoPagamento;
            var empresa = MockSettingsTest.Pagamento1.Empresa;
            var pessoa = MockSettingsTest.Pagamento1.Pessoa;
            var pagamento = MockSettingsTest.Pagamento1;

            var pagamentoCommand = MockSettingsTest.PagamentoApagarCommand;

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _handler.Handler(pagamentoCommand);

            var retornoDados = (ApagarPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pagamentoCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}