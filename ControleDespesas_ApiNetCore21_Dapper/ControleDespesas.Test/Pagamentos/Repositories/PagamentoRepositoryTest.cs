using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Enums;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Pagamentos.Repositories
{
    [TestFixture]
    public class PagamentoRepositoryTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly ITipoPagamentoRepository _repositoryTipoPagamento;
        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly IPagamentoRepository _repositoryPagamento;

        public PagamentoRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryTipoPagamento = new TipoPagamentoRepository(MockSettingsInfraData);
            _repositoryEmpresa = new EmpresaRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _repositoryPagamento = new PagamentoRepository(MockSettingsInfraData);
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

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento.Id, retorno.Id);
            Assert.AreEqual(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.AreEqual(pagamento.Empresa.Id, retorno.Empresa.Id);
            //Assert.AreEqual(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.AreEqual(pagamento.Descricao, retorno.Descricao);
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

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento.Id, retorno.Id);
            Assert.AreEqual(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.AreEqual(pagamento.Empresa.Id, retorno.Empresa.Id);
            //Assert.AreEqual(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.AreEqual(pagamento.Descricao, retorno.Descricao);
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
            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryPagamento.Deletar(pagamento1.Id);

            var retorno = _repositoryPagamento.Listar(pagamento1.Pessoa.Id, null);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento2.Id, retorno[0].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, retorno[0].Empresa.Id);
            //Assert.AreEqual(pagamento2.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao, retorno[0].Descricao);
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

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento.Id, retorno.Id);
            Assert.AreEqual(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.AreEqual(pagamento.Empresa.Id, retorno.Empresa.Id);
            //Assert.AreEqual(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.AreEqual(pagamento.Descricao, retorno.Descricao);
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
            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            var pagamento3 = new SettingsTest().Pagamento3;
            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPessoa.Salvar(pagamento3.Pessoa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.Listar(pagamento1.Pessoa.Id, null);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento1.Id, retorno[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, retorno[0].Empresa.Id);
            //Assert.AreEqual(pagamento1.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao, retorno[0].Descricao);
            Assert.AreEqual(pagamento1.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);

            Assert.AreEqual(pagamento2.Id, retorno[1].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, retorno[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, retorno[1].Empresa.Id);
            //Assert.AreEqual(pagamento2.Pessoa.Id, retorno[1].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao, retorno[1].Descricao);
            Assert.AreEqual(pagamento2.Valor, retorno[1].Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, retorno[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(retorno[1].DataPagamento).Date);
        }

        [Test]
        public void ListarPagos()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            var pagamento3 = new SettingsTest().Pagamento3;
            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPessoa.Salvar(pagamento3.Pessoa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.Listar(pagamento1.Pessoa.Id, EPagamentoStatus.Pago);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento1.Id, retorno[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, retorno[0].Empresa.Id);
            //Assert.AreEqual(pagamento1.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao, retorno[0].Descricao);
            Assert.AreEqual(pagamento1.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);

            Assert.AreEqual(pagamento2.Id, retorno[1].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, retorno[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, retorno[1].Empresa.Id);
            //Assert.AreEqual(pagamento2.Pessoa.Id, retorno[1].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao, retorno[1].Descricao);
            Assert.AreEqual(pagamento2.Valor, retorno[1].Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, retorno[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(retorno[1].DataPagamento).Date);
        }

        [Test]
        public void ListarPendentes()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            var pagamento3 = new SettingsTest().Pagamento3;
            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPessoa.Salvar(pagamento3.Pessoa);
            _repositoryPagamento.Salvar(pagamento3);

            var retorno = _repositoryPagamento.Listar(pagamento3.Pessoa.Id, EPagamentoStatus.Pendente);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento3.Id, retorno[0].Id);
            Assert.AreEqual(pagamento3.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento3.Empresa.Id, retorno[0].Empresa.Id);
            //Assert.AreEqual(pagamento3.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.AreEqual(pagamento3.Descricao, retorno[0].Descricao);
            Assert.AreEqual(pagamento3.Valor, retorno[0].Valor);
            Assert.AreEqual(pagamento3.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento3.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);
        }

        [Test]
        public void ObterArquivoPagamento()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento = new SettingsTest().Pagamento1;
            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _repositoryPagamento.ObterArquivoPagamento(pagamento.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento.ArquivoPagamento, retorno.Arquivo);
        }

        [Test]
        public void ObterArquivoComprovante()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento = new SettingsTest().Pagamento1;
            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var retorno = _repositoryPagamento.ObterArquivoComprovante(pagamento.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pagamento.ArquivoComprovante, retorno.Arquivo);
        }

        [Test]
        public void ObterGastos()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pagamento1 = new SettingsTest().Pagamento1;
            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            var pagamento3 = new SettingsTest().Pagamento3;
            pagamento3.DefinirPessoa(pagamento1.Pessoa);
            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var valorTotalEsperado = pagamento2.Valor + pagamento3.Valor;

            var retorno = _repositoryPagamento.ObterGastos(pagamento1.Pessoa.Id, 2020, 11);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

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
            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
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