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
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPagamento()
        {
            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            repositoryEmpresa.Salvar(empresa);

            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            repositoryPessoa.Salvar(pessoa);

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

            var retorno = handler.Handler(pagamentoCommand);
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
            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            repositoryEmpresa.Salvar(empresa);

            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            repositoryPessoa.Salvar(pessoa);

            var pagamento = new Pagamento(0, new TipoPagamento(1), new Empresa(1), new Pessoa(1), new Texto("DescriçãoPagamento", "Descrição", 250), 100, DateTime.Now.AddDays(1), DateTime.Now);
            repositoryPagamento.Salvar(pagamento);

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

            var retorno = handler.Handler(pagamentoCommand);
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
            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            repositoryEmpresa.Salvar(empresa);

            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            repositoryPessoa.Salvar(pessoa);

            var pagamento = new Pagamento(0, new TipoPagamento(1), new Empresa(1), new Pessoa(1), new Texto("DescriçãoPagamento", "Descrição", 250), 100, DateTime.Now.AddDays(1), DateTime.Now);
            repositoryPagamento.Salvar(pagamento);

            var pagamentoCommand = new ApagarPagamentoCommand() { Id = 1 };

            var retorno = handler.Handler(pagamentoCommand);
            var retornoDados = (ApagarPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pagamentoCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}