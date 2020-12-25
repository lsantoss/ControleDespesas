using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Repositorio
{
    public class PagamentoRepositorioTest : DatabaseTest
    {
        private readonly UsuarioRepositorio _repositoryUsuario;
        private readonly TipoPagamentoRepositorio _repositoryTipoPagamento;
        private readonly EmpresaRepositorio _repositoryEmpresa;
        private readonly PessoaRepositorio _repositoryPessoa;
        private readonly PagamentoRepositorio _repositoryPagamento;

        public PagamentoRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repositoryUsuario = new UsuarioRepositorio(optionsInfraData);
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
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento = new SettingsTest().Pagamento1;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _repositoryPagamento.Obter(pagamento.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento = new SettingsTest().Pagamento1;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            pagamento = new SettingsTest().Pagamento1Editado;

            _repositoryPagamento.Atualizar(pagamento);

            var retorno = _repositoryPagamento.Obter(pagamento.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryPagamento.Deletar(pagamento1.Id);

            var retorno = _repositoryPagamento.Listar(pagamento1.Pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento = new SettingsTest().Pagamento1;
            
            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _repositoryPagamento.Obter(pagamento.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPessoa.Salvar(pagamento3.Pessoa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.Listar(pagamento1.Pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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
        public void ListarPagamentoConcluido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.ListarPagamentoConcluido(pagamento1.Pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(2, retorno.Count);

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
        public void ListarPagamentoPendente()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.ListarPagamentoPendente(pagamento1.Pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(1, retorno.Count);

            Assert.AreEqual(pagamento3.Id, retorno[0].Id);
            Assert.AreEqual(pagamento3.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento3.Empresa.Id, retorno[0].Empresa.Id);
            Assert.AreEqual(pagamento3.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento3.Descricao.ToString(), retorno[0].Descricao);
            Assert.AreEqual(pagamento3.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento3.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento3.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);
        }

        [Test]
        public void CalcularValorGastoTotal()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            var valorTotalEsperado = pagamento1.Valor + pagamento2.Valor + pagamento3.Valor;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.CalcularValorGastoTotal(pagamento1.Pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(valorTotalEsperado, retorno.Valor);
        }

        [Test]
        public void CalcularValorGastoAno()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            var valorTotalEsperado = pagamento2.Valor + pagamento3.Valor;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.CalcularValorGastoAno(pagamento1.Pessoa.Id, 2020);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(valorTotalEsperado, retorno.Valor);
        }

        [Test]
        public void CalcularValorGastoAnoMes()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            var valorTotalEsperado = pagamento2.Valor + pagamento3.Valor;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.CalcularValorGastoAnoMes(pagamento1.Pessoa.Id, 2020, 11);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(valorTotalEsperado, retorno.Valor);
        }

        [Test]
        public void CheckId()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento = new SettingsTest().Pagamento1;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var idExistente = _repositoryPagamento.CheckId(pagamento.Id);
            var idNaoExiste = _repositoryPagamento.CheckId(25);

            TestContext.WriteLine(idExistente);
            TestContext.WriteLine(idNaoExiste);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            var maxId = _repositoryPagamento.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(pagamento2.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}