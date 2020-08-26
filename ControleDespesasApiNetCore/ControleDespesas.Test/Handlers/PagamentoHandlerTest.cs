using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            new TipoPagamentoRepositorio(mockOptions.Object).Salvar(tipoPagamento);

            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            new EmpresaRepositorio(mockOptions.Object).Salvar(empresa);

            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            new PessoaRepositorio(mockOptions.Object).Salvar(pessoa);

            IPagamentoRepositorio IPagamentoRepos = new PagamentoRepositorio(mockOptions.Object);
            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptions.Object);
            IPessoaRepositorio IPessoaRepos = new PessoaRepositorio(mockOptions.Object);
            ITipoPagamentoRepositorio ITipoPagamentoRepos = new TipoPagamentoRepositorio(mockOptions.Object);
            PagamentoHandler handler = new PagamentoHandler(IPagamentoRepos, IEmpresaRepos, IPessoaRepos, ITipoPagamentoRepos);

            AdicionarPagamentoCommand pagamentoCommand = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = 1,
                IdEmpresa = 1,
                IdPessoa = 1,
                Descricao = "DescriçãoPagamento",
                Valor = 100,
                DataVencimento = DateTime.Now.AddDays(1),
                DataPagamento = DateTime.Now
            };

            ICommandResult<Notificacao> retorno = handler.Handler(pagamentoCommand);
            AdicionarPagamentoCommandOutput retornoDados = (AdicionarPagamentoCommandOutput)retorno.Dados;

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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            new TipoPagamentoRepositorio(mockOptions.Object).Salvar(tipoPagamento);

            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            new EmpresaRepositorio(mockOptions.Object).Salvar(empresa);

            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            new PessoaRepositorio(mockOptions.Object).Salvar(pessoa);

            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now);
            new PagamentoRepositorio(mockOptions.Object).Salvar(pagamento);

            IPagamentoRepositorio IPagamentoRepos = new PagamentoRepositorio(mockOptions.Object);
            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptions.Object);
            IPessoaRepositorio IPessoaRepos = new PessoaRepositorio(mockOptions.Object);
            ITipoPagamentoRepositorio ITipoPagamentoRepos = new TipoPagamentoRepositorio(mockOptions.Object);
            PagamentoHandler handler = new PagamentoHandler(IPagamentoRepos, IEmpresaRepos, IPessoaRepos, ITipoPagamentoRepos);

            AtualizarPagamentoCommand pagamentoCommand = new AtualizarPagamentoCommand()
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

            ICommandResult<Notificacao> retorno = handler.Handler(pagamentoCommand);
            AtualizarPagamentoCommandOutput retornoDados = (AtualizarPagamentoCommandOutput)retorno.Dados;

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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            new TipoPagamentoRepositorio(mockOptions.Object).Salvar(tipoPagamento);

            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            new EmpresaRepositorio(mockOptions.Object).Salvar(empresa);

            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            new PessoaRepositorio(mockOptions.Object).Salvar(pessoa);

            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now);
            new PagamentoRepositorio(mockOptions.Object).Salvar(pagamento);

            IPagamentoRepositorio IPagamentoRepos = new PagamentoRepositorio(mockOptions.Object);
            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptions.Object);
            IPessoaRepositorio IPessoaRepos = new PessoaRepositorio(mockOptions.Object);
            ITipoPagamentoRepositorio ITipoPagamentoRepos = new TipoPagamentoRepositorio(mockOptions.Object);
            PagamentoHandler handler = new PagamentoHandler(IPagamentoRepos, IEmpresaRepos, IPessoaRepos, ITipoPagamentoRepos);

            ApagarPagamentoCommand pagamentoCommand = new ApagarPagamentoCommand() { Id = 1 };

            ICommandResult<Notificacao> retorno = handler.Handler(pagamentoCommand);
            ApagarPagamentoCommandOutput retornoDados = (ApagarPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pagamentoCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}