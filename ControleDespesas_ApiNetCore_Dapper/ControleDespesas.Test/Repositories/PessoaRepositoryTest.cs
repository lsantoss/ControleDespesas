using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositories
{
    public class PessoaRepositoryTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPessoaRepository _repositoryPessoa;

        public PessoaRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _repositoryPessoa.Obter(pessoa.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Usuario.Id, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Atualizar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            pessoa = new SettingsTest().Pessoa1Editada;
            _repositoryPessoa.Atualizar(pessoa);

            var retorno = _repositoryPessoa.Obter(pessoa.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Usuario.Id, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
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

            _repositoryPessoa.Deletar(pessoa2.Id);

            var retorno = _repositoryPessoa.Listar(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa1.Id, retorno[0].Id);
            Assert.AreEqual(pessoa1.Usuario.Id, retorno[0].IdUsuario);
            Assert.AreEqual(pessoa1.Nome, retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, retorno[1].Id);
            Assert.AreEqual(pessoa3.Usuario.Id, retorno[1].IdUsuario);
            Assert.AreEqual(pessoa3.Nome, retorno[1].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[1].ImagemPerfil);
        }

        [Test]
        public void Obter()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _repositoryPessoa.Obter(pessoa.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Usuario.Id, retorno.IdUsuario);
            Assert.AreEqual(pessoa.Nome, retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Listar()
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
            Assert.AreEqual(pessoa1.Usuario.Id, retorno[0].IdUsuario);
            Assert.AreEqual(pessoa1.Nome, retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(pessoa2.Id, retorno[1].Id);
            Assert.AreEqual(pessoa2.Usuario.Id, retorno[1].IdUsuario);
            Assert.AreEqual(pessoa2.Nome, retorno[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, retorno[1].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, retorno[2].Id);
            Assert.AreEqual(pessoa3.Usuario.Id, retorno[2].IdUsuario);
            Assert.AreEqual(pessoa3.Nome, retorno[2].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[2].ImagemPerfil);
        }

        [Test]
        public void CheckId()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var idExistente = _repositoryPessoa.CheckId(pessoa.Id);
            var idNaoExiste = _repositoryPessoa.CheckId(25);

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