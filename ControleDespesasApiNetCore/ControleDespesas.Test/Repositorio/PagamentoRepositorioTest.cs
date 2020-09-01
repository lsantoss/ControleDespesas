using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Repositorio
{
    public class PagamentoRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            var retorno = repositoryPagamento.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.AreEqual(pagamento.Empresa.Id, retorno.Empresa.Id);
            Assert.AreEqual(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.AreEqual(pagamento.Descricao.ToString(), retorno.Descricao);
            Assert.AreEqual(pagamento.Valor, retorno.Valor);
            Assert.AreEqual(pagamento.DataVencimento.Date, retorno.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento.DataPagamento).Date, Convert.ToDateTime(retorno.DataPagamento).Date);
        }

        [Test]
        public void Atualizar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            pagamento = new Pagamento(
                1,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento - Editado", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            repositoryPagamento.Atualizar(pagamento);

            var retorno = repositoryPagamento.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.AreEqual(pagamento.Empresa.Id, retorno.Empresa.Id);
            Assert.AreEqual(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.AreEqual(pagamento.Descricao.ToString(), retorno.Descricao);
            Assert.AreEqual(pagamento.Valor, retorno.Valor);
            Assert.AreEqual(pagamento.DataVencimento.Date, retorno.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento.DataPagamento).Date, Convert.ToDateTime(retorno.DataPagamento).Date);
        }

        [Test]
        public void Deletar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            repositoryPagamento.Deletar(1);

            var retorno = repositoryPagamento.Listar();

            Assert.AreEqual(2, retorno[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, retorno[0].Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), retorno[0].Descricao);
            Assert.AreEqual(pagamento1.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);
        }

        [Test]
        public void Obter()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            var retorno = repositoryPagamento.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.AreEqual(pagamento.Empresa.Id, retorno.Empresa.Id);
            Assert.AreEqual(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.AreEqual(pagamento.Descricao.ToString(), retorno.Descricao);
            Assert.AreEqual(pagamento.Valor, retorno.Valor);
            Assert.AreEqual(pagamento.DataVencimento.Date, retorno.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento.DataPagamento).Date, Convert.ToDateTime(retorno.DataPagamento).Date);
        }

        [Test]
        public void Listar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            var retorno = repositoryPagamento.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, retorno[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento0.Empresa.Id, retorno[0].Empresa.Id);
            Assert.AreEqual(pagamento0.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento0.Descricao.ToString(), retorno[0].Descricao);
            Assert.AreEqual(pagamento0.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento0.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento0.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);

            Assert.AreEqual(2, retorno[1].Id);
            Assert.AreEqual(pagamento0.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, retorno[1].Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, retorno[1].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), retorno[1].Descricao);
            Assert.AreEqual(pagamento1.Valor, retorno[1].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, retorno[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[1].DataPagamento).Date);
        }

        [Test]
        public void CheckId()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            var idExistente = repositoryPagamento.CheckId(1);
            var idNaoExiste = repositoryPagamento.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            var repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            var repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            var maxId = repositoryPagamento.LocalizarMaxId();

            Assert.AreEqual(2, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}