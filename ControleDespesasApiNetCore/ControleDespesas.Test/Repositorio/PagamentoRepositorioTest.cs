using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Factory;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Repositorio
{
    public class PagamentoRepositorioTest : DatabaseFactory
    {
        private readonly TipoPagamentoRepositorio _repositoryTipoPagamento;
        private readonly EmpresaRepositorio _repositoryEmpresa;
        private readonly PessoaRepositorio _repositoryPessoa;
        private readonly PagamentoRepositorio _repositoryPagamento;

        public PagamentoRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repositoryTipoPagamento = new TipoPagamentoRepositorio(optionsInfraData);
            _repositoryEmpresa = new EmpresaRepositorio(optionsInfraData);
            _repositoryPessoa = new PessoaRepositorio(optionsInfraData);
            _repositoryPagamento = new PagamentoRepositorio(optionsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var pagamento = MockSettingsTest.Pagamento1;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _repositoryPagamento.Obter(pagamento.Id);

            Assert.AreEqual(pagamento.Id, retorno.Id);
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
            var pagamento = MockSettingsTest.Pagamento1;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            pagamento = MockSettingsTest.Pagamento1Editado;

            _repositoryPagamento.Atualizar(pagamento);

            var retorno = _repositoryPagamento.Obter(pagamento.Id);

            Assert.AreEqual(pagamento.Id, retorno.Id);
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
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPessoa.Salvar(pagamento2.Pessoa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryPagamento.Deletar(pagamento1.Id);

            var retorno = _repositoryPagamento.Listar();

            Assert.AreEqual(pagamento2.Id, retorno[0].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, retorno[0].Empresa.Id);
            Assert.AreEqual(pagamento2.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao.ToString(), retorno[0].Descricao);
            Assert.AreEqual(pagamento2.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);
        }

        [Test]
        public void Obter()
        {
            var pagamento = MockSettingsTest.Pagamento1;
            
            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _repositoryPagamento.Obter(pagamento.Id);

            Assert.AreEqual(pagamento.Id, retorno.Id);
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
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPessoa.Salvar(pagamento2.Pessoa);
            _repositoryPagamento.Salvar(pagamento2);

            var retorno = _repositoryPagamento.Listar();

            Assert.AreEqual(pagamento1.Id, retorno[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, retorno[0].Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), retorno[0].Descricao);
            Assert.AreEqual(pagamento1.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);

            Assert.AreEqual(pagamento2.Id, retorno[1].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, retorno[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, retorno[1].Empresa.Id);
            Assert.AreEqual(pagamento2.Pessoa.Id, retorno[1].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao.ToString(), retorno[1].Descricao);
            Assert.AreEqual(pagamento2.Valor, retorno[1].Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, retorno[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(retorno[1].DataPagamento).Date);
        }

        [Test]
        public void CheckId()
        {
            var pagamento = MockSettingsTest.Pagamento1;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var idExistente = _repositoryPagamento.CheckId(pagamento.Id);
            var idNaoExiste = _repositoryPagamento.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPessoa.Salvar(pagamento2.Pessoa);
            _repositoryPagamento.Salvar(pagamento2);

            var maxId = _repositoryPagamento.LocalizarMaxId();

            Assert.AreEqual(pagamento2.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}