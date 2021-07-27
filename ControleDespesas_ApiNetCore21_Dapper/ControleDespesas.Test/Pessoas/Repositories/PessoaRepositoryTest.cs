using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System.Data.SqlClient;

namespace ControleDespesas.Test.Pessoas.Repositories
{
    [TestFixture]
    public class PessoaRepositoryTest : DatabaseTest
    {
        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly ITipoPagamentoRepository _repositoryTipoPagamento;
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly IPagamentoRepository _repositoryPagamento;

        public PessoaRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryEmpresa = new EmpresaRepository(MockSettingsInfraData);
            _repositoryTipoPagamento = new TipoPagamentoRepository(MockSettingsInfraData);
            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _repositoryPagamento = new PagamentoRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _repositoryPessoa.Obter(pessoa.Id, pessoa.IdUsuario);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.IdUsuario, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(2)]
        public void Salvar_IdUsuario_Invalido(long idUsuario)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            pessoa.DefinirIdUsuario(idUsuario);

            TestContext.WriteLine(pessoa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryPessoa.Salvar(pessoa); });
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Salvar_Nome_Invalido(string nome)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            pessoa.DefinirNome(nome);

            TestContext.WriteLine(pessoa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryPessoa.Salvar(pessoa); });
        }

        [Test]
        [TestCase(null)]
        public void Salvar_ImagemPerfil_Invalido(string imagemPerfil)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            pessoa.DefinirImagemPerfil(imagemPerfil);

            TestContext.WriteLine(pessoa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryPessoa.Salvar(pessoa); });
        }

        [Test]
        public void Atualizar_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            pessoa = new SettingsTest().Pessoa1Editada;
            _repositoryPessoa.Atualizar(pessoa);

            var retorno = _repositoryPessoa.Obter(pessoa.Id, pessoa.IdUsuario);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.IdUsuario, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Atualizar_Nome_Valido(string nome)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            pessoa.DefinirNome(nome);

            TestContext.WriteLine(pessoa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryPessoa.Atualizar(pessoa); });
        }

        [Test]
        [TestCase(null)]
        public void Atualizar_ImagemPerfil_Invalido(string imagemPerfil)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            pessoa.DefinirImagemPerfil(imagemPerfil);

            TestContext.WriteLine(pessoa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryPessoa.Atualizar(pessoa); });
        }

        [Test]
        public void Deletar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            _repositoryPessoa.Deletar(pessoa2.Id, pessoa2.IdUsuario);

            var retorno = _repositoryPessoa.Listar(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa1.Id, retorno[0].Id);
            Assert.AreEqual(pessoa1.IdUsuario, retorno[0].IdUsuario);
            Assert.AreEqual(pessoa1.Nome, retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, retorno[1].Id);
            Assert.AreEqual(pessoa3.IdUsuario, retorno[1].IdUsuario);
            Assert.AreEqual(pessoa3.Nome, retorno[1].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[1].ImagemPerfil);
        }

        [Test]
        public void Obter_ObjetoPreenchido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _repositoryPessoa.Obter(pessoa.Id, pessoa.IdUsuario);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.IdUsuario, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Obter_ObjetoNulo()
        {
            var retorno = _repositoryPessoa.Obter(1, 1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.Null(retorno);
        }

        [Test]
        public void Obter_ContendoRegistrosFilhos_ObjetoPreenchido()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repositoryEmpresa.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repositoryEmpresa.Salvar(empresa2);

            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repositoryTipoPagamento.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repositoryTipoPagamento.Salvar(tipoPagamento2);

            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var pagamento1 = new SettingsTest().Pagamento1;
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryPagamento.Salvar(pagamento2);

            pessoa.AdicionarPagamento(pagamento1);
            pessoa.AdicionarPagamento(pagamento2);

            var retorno = _repositoryPessoa.ObterContendoRegistrosFilhos(pessoa.Id, pessoa.IdUsuario);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.IdUsuario, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
            Assert.AreEqual(pessoa.Pagamentos.Count, retorno.Pagamentos.Count);
        }

        [Test]
        public void Obter_ContendoRegistrosFilhos_ObjetoNulo()
        {
            var retorno = _repositoryPessoa.ObterContendoRegistrosFilhos(1, 1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.Null(retorno);
        }

        [Test]
        public void Listar_ListaPreenchida()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            var retorno = _repositoryPessoa.Listar(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa1.Id, retorno[0].Id);
            Assert.AreEqual(pessoa1.IdUsuario, retorno[0].IdUsuario);
            Assert.AreEqual(pessoa1.Nome, retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(pessoa2.Id, retorno[1].Id);
            Assert.AreEqual(pessoa2.IdUsuario, retorno[1].IdUsuario);
            Assert.AreEqual(pessoa2.Nome, retorno[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, retorno[1].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, retorno[2].Id);
            Assert.AreEqual(pessoa3.IdUsuario, retorno[2].IdUsuario);
            Assert.AreEqual(pessoa3.Nome, retorno[2].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[2].ImagemPerfil);
        }

        [Test]
        public void Listar_ListaVazia()
        {
            var retorno = _repositoryPessoa.Listar(1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(0, retorno.Count);
        }

        [Test]
        public void Listar_ContendoRegistrosFilhos_ListaPreenchida()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repositoryEmpresa.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repositoryEmpresa.Salvar(empresa2);

            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repositoryTipoPagamento.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repositoryTipoPagamento.Salvar(tipoPagamento2);

            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pagamento1 = new SettingsTest().Pagamento1;
            _repositoryPagamento.Salvar(pagamento1);

            var pagamento2 = new SettingsTest().Pagamento2;
            _repositoryPagamento.Salvar(pagamento2);

            pessoa1.AdicionarPagamento(pagamento1);
            pessoa1.AdicionarPagamento(pagamento2);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            var retorno = _repositoryPessoa.ListarContendoRegistrosFilhos(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa1.Id, retorno[0].Id);
            Assert.AreEqual(pessoa1.IdUsuario, retorno[0].IdUsuario);
            Assert.AreEqual(pessoa1.Nome, retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);
            Assert.AreEqual(pessoa1.Pagamentos.Count, retorno[0].Pagamentos.Count);

            Assert.AreEqual(pessoa2.Id, retorno[1].Id);
            Assert.AreEqual(pessoa2.IdUsuario, retorno[1].IdUsuario);
            Assert.AreEqual(pessoa2.Nome, retorno[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, retorno[1].ImagemPerfil);
            Assert.AreEqual(pessoa2.Pagamentos.Count, retorno[1].Pagamentos.Count);

            Assert.AreEqual(pessoa3.Id, retorno[2].Id);
            Assert.AreEqual(pessoa3.IdUsuario, retorno[2].IdUsuario);
            Assert.AreEqual(pessoa3.Nome, retorno[2].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[2].ImagemPerfil);
            Assert.AreEqual(pessoa3.Pagamentos.Count, retorno[2].Pagamentos.Count);
        }

        [Test]
        public void Listar_ContendoRegistrosFilhos_ListaVazia()
        {
            var retorno = _repositoryPessoa.ListarContendoRegistrosFilhos(1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(0, retorno.Count);
        }

        [Test]
        public void CheckId_Encontrado()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var idExistente = _repositoryPessoa.CheckId(pessoa.Id);

            TestContext.WriteLine(idExistente);

            Assert.True(idExistente);
        }

        [Test]
        public void CheckId_NaoEncontrado()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var idNaoExiste = _repositoryPessoa.CheckId(25);

            TestContext.WriteLine(idNaoExiste);

            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            var maxId = _repositoryPessoa.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(pessoa3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}