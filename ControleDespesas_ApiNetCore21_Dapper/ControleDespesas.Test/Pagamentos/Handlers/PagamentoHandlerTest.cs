using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Commands.Output;
using ControleDespesas.Domain.Pagamentos.Handlers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Pagamentos.Handlers
{
    public class PagamentoHandlerTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly ITipoPagamentoRepository _repositoryTipoPagamento;
        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly IPagamentoRepository _repositoryPagamento;
        private readonly IPagamentoHandler _handler;

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
            _repositoryTipoPagamento.Salvar(tipoPagamento);

            var empresa = new SettingsTest().Pagamento1.Empresa;
            _repositoryEmpresa.Salvar(empresa);

            var pessoa = new SettingsTest().Pagamento1.Pessoa;
            _repositoryPessoa.Salvar(pessoa);

            var pagamentoCommand = new SettingsTest().PagamentoAdicionarCommand;
            var retorno = _handler.Handler(pagamentoCommand);
            var retornoDados = (PagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

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
            _repositoryTipoPagamento.Salvar(tipoPagamento);

            var empresa = new SettingsTest().Pagamento1.Empresa;
            _repositoryEmpresa.Salvar(empresa);

            var pessoa = new SettingsTest().Pagamento1.Pessoa;
            _repositoryPessoa.Salvar(pessoa);

            var pagamento = new SettingsTest().Pagamento1;
            _repositoryPagamento.Salvar(pagamento);

            var pagamentoCommand = new SettingsTest().PagamentoAtualizarCommand;
            var retorno = _handler.Handler(pagamentoCommand.Id, pagamentoCommand);
            var retornoDados = (PagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

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
            _repositoryTipoPagamento.Salvar(tipoPagamento);

            var empresa = new SettingsTest().Pagamento1.Empresa;
            _repositoryEmpresa.Salvar(empresa);

            var pessoa = new SettingsTest().Pagamento1.Pessoa;
            _repositoryPessoa.Salvar(pessoa);

            var pagamento = new SettingsTest().Pagamento1;
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _handler.Handler(pagamento.Id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pagamento.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}