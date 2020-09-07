using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Handlers
{
    public class PagamentoHandlerTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly TipoPagamentoRepositorio _repositoryTipoPagamento;
        private readonly EmpresaRepositorio _repositoryEmpresa;
        private readonly PessoaRepositorio _repositoryPessoa;
        private readonly PagamentoRepositorio _repositoryPagamento;
        private readonly PagamentoHandler _handler;

        public PagamentoHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repositoryTipoPagamento = new TipoPagamentoRepositorio(_mockOptions.Object);
            _repositoryEmpresa = new EmpresaRepositorio(_mockOptions.Object);
            _repositoryPessoa = new PessoaRepositorio(_mockOptions.Object);
            _repositoryPagamento = new PagamentoRepositorio(_mockOptions.Object);
            _handler = new PagamentoHandler(_repositoryPagamento, _repositoryEmpresa, _repositoryPessoa, _repositoryTipoPagamento);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPagamento()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");

            var pagamentoCommand = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = 1,
                IdEmpresa = 1,
                IdPessoa = 1,
                Descricao = "DescriçãoPagamento",
                Valor = 100,
                DataVencimento = DateTime.Now.AddDays(1),
                DataPagamento = DateTime.Now
            };

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _handler.Handler(pagamentoCommand);

            var retornoDados = (AdicionarPagamentoCommandOutput)retorno.Dados;

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
            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            var pagamento = new Pagamento(0, new TipoPagamento(1), new Empresa(1), new Pessoa(1), new Texto("DescriçãoPagamento", "Descrição", 250), 100, DateTime.Now.AddDays(1), DateTime.Now);

            var pagamentoCommand = new AtualizarPagamentoCommand()
            {
                Id = 1,
                IdTipoPagamento = 1,
                IdEmpresa = 1,
                IdPessoa = 1,
                Descricao = "DescriçãoPagamento - Editada",
                Valor = 150,
                DataVencimento = pagamento.DataVencimento.AddDays(3),
                DataPagamento = DateTime.Now.AddDays(2)
            };

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _handler.Handler(pagamentoCommand);

            var retornoDados = (AtualizarPagamentoCommandOutput)retorno.Dados;

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
            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            var pagamento = new Pagamento(0, new TipoPagamento(1), new Empresa(1), new Pessoa(1), new Texto("DescriçãoPagamento", "Descrição", 250), 100, DateTime.Now.AddDays(1), DateTime.Now);

            var pagamentoCommand = new ApagarPagamentoCommand() { Id = 1 };

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _handler.Handler(pagamentoCommand);

            var retornoDados = (ApagarPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pagamentoCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}