using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class PessoaRepositorioTest : DatabaseTest
    {
        private readonly PessoaRepositorio _repository;

        public PessoaRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repository = new PessoaRepositorio(optionsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var pessoa = new SettingsTest().Pessoa1;
            _repository.Salvar(pessoa);

            var retorno = _repository.Obter(pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Atualizar()
        {
            var pessoa = new SettingsTest().Pessoa1;
            _repository.Salvar(pessoa);

            pessoa = new SettingsTest().Pessoa1Editada;
            _repository.Atualizar(pessoa);

            var retorno = _repository.Obter(pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Deletar()
        {
            var pessoa1 = new SettingsTest().Pessoa1;
            var pessoa2 = new SettingsTest().Pessoa2;
            var pessoa3 = new SettingsTest().Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            _repository.Deletar(pessoa2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(pessoa1.Id, retorno[0].Id);
            Assert.AreEqual(pessoa1.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, retorno[1].Id);
            Assert.AreEqual(pessoa3.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[1].ImagemPerfil);
        }

        [Test]
        public void Obter()
        {
            var pessoa = new SettingsTest().Pessoa1;

            _repository.Salvar(pessoa);

            var retorno = _repository.Obter(pessoa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Listar()
        {
            var pessoa1 = new SettingsTest().Pessoa1;
            var pessoa2 = new SettingsTest().Pessoa2;
            var pessoa3 = new SettingsTest().Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(pessoa1.Id, retorno[0].Id);
            Assert.AreEqual(pessoa1.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(pessoa1.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.AreEqual(pessoa2.Id, retorno[1].Id);
            Assert.AreEqual(pessoa2.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(pessoa2.ImagemPerfil, retorno[1].ImagemPerfil);

            Assert.AreEqual(pessoa3.Id, retorno[2].Id);
            Assert.AreEqual(pessoa3.Nome.ToString(), retorno[2].Nome);
            Assert.AreEqual(pessoa3.ImagemPerfil, retorno[2].ImagemPerfil);
        }

        [Test]
        public void CheckId()
        {
            var pessoa = new SettingsTest().Pessoa1;
            _repository.Salvar(pessoa);

            var idExistente = _repository.CheckId(pessoa.Id);
            var idNaoExiste = _repository.CheckId(25);

            TestContext.WriteLine(idExistente);
            TestContext.WriteLine(idNaoExiste);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var pessoa1 = new SettingsTest().Pessoa1;
            var pessoa2 = new SettingsTest().Pessoa2;
            var pessoa3 = new SettingsTest().Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            var maxId = _repository.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(pessoa3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}