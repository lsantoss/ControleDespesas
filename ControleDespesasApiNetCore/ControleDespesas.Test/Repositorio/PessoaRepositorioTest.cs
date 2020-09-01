using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class PessoaRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            var retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Atualizar()
        {
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            pessoa = new Pessoa(1, new Texto("NomePessoa - Editada", "Nome", 100), "ImagemPerfil - Editado");
            repository.Atualizar(pessoa);

            var retorno = repository.Obter(1);

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Deletar()
        {
            var pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            var pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            var pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            repository.Deletar(2);

            var retorno = repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(pessoa0.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(pessoa0.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(3, retorno[1].Id);
            Assert.AreEqual(pessoa2.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, retorno[1].ImagemPerfil);
        }

        [Test]
        public void Obter()
        {
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            var retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Listar()
        {
            var pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            var pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            var pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            var retorno = repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(pessoa0.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(pessoa0.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(2, retorno[1].Id);
            Assert.AreEqual(pessoa1.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[1].ImagemPerfil);

            Assert.AreEqual(3, retorno[2].Id);
            Assert.AreEqual(pessoa2.Nome.ToString(), retorno[2].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, retorno[2].ImagemPerfil);
        }

        [Test]
        public void CheckId()
        {
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            var idExistente = repository.CheckId(1);
            var idNaoExiste = repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            var pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            var pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            var maxId = repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}