using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Repositorio
{
    public class PessoaRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Atualizar()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            pessoa = new Pessoa(1, new Texto("NomePessoa - Editada", "Nome", 100), "ImagemPerfil - Editado");
            repository.Atualizar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Deletar()
        {
            Pessoa pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            Pessoa pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            Pessoa pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            repository.Deletar(2);

            List<PessoaQueryResult> retorno = repository.Listar();

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
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Listar()
        {
            Pessoa pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            Pessoa pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            Pessoa pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            List<PessoaQueryResult> retorno = repository.Listar();

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
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            bool idExistente = repository.CheckId(1);
            bool idNaoExiste = repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            Pessoa pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            Pessoa pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            Pessoa pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            int maxId = repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}