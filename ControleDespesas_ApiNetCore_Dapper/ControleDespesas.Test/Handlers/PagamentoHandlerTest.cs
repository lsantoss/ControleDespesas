using ControleDespesas.Domain.Commands.Pagamento.Output;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Handlers
{
    public class PagamentoHandlerTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly ITipoPagamentoRepository _repositoryTipoPagamento;
        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly IPagamentoRepository _repositoryPagamento;
        private readonly PagamentoHandler _handler;

        public PagamentoHandlerTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryTipoPagamento = new TipoPagamentoRepository(MockSettingsInfraData);
            _repositoryEmpresa = new EmpresaRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _repositoryPagamento = new PagamentoRepository(MockSettingsInfraData);
            _handler = new PagamentoHandler(_repositoryPagamento, _repositoryEmpresa, _repositoryPessoa, _repositoryTipoPagamento);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPagamento()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var tipoPagamento = new SettingsTest().Pagamento1.TipoPagamento;
            var empresa = new SettingsTest().Pagamento1.Empresa;
            var pessoa = new SettingsTest().Pagamento1.Pessoa;

            var pagamentoCommand = new SettingsTest().PagamentoAdicionarCommand;

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
            Assert.AreEqual(pagamentoCommand.ArquivoPagamento, retornoDados.ArquivoPagamento);
            Assert.AreEqual(pagamentoCommand.ArquivoComprovante, retornoDados.ArquivoComprovante);
            Assert.AreEqual(Convert.ToDateTime(pagamentoCommand.DataPagamento).Date, Convert.ToDateTime(retornoDados.DataPagamento).Date);
        }

        [Test]
        public void Handler_AtualizarPagamento()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var tipoPagamento = new SettingsTest().Pagamento1.TipoPagamento;
            var empresa = new SettingsTest().Pagamento1.Empresa;
            var pessoa = new SettingsTest().Pagamento1.Pessoa;
            var pagamento = new SettingsTest().Pagamento1;

            var pagamentoCommand = new SettingsTest().PagamentoAtualizarCommand;

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
            Assert.AreEqual(pagamentoCommand.ArquivoPagamento, retornoDados.ArquivoPagamento);
            Assert.AreEqual(pagamentoCommand.ArquivoComprovante, retornoDados.ArquivoComprovante);
            Assert.AreEqual(Convert.ToDateTime(pagamentoCommand.DataPagamento).Date, Convert.ToDateTime(retornoDados.DataPagamento).Date);
        }

        [Test]
        public void Handler_ApagarPagamento()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var tipoPagamento = new SettingsTest().Pagamento1.TipoPagamento;
            var empresa = new SettingsTest().Pagamento1.Empresa;
            var pessoa = new SettingsTest().Pagamento1.Pessoa;
            var pagamento = new SettingsTest().Pagamento1;

            var pagamentoCommand = new SettingsTest().PagamentoApagarCommand;

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