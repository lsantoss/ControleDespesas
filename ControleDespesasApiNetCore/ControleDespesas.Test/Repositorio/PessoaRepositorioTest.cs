using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class PessoaRepositorioTest : DatabaseFactory
    {
        private readonly SettingsTest _settingsTest;
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly PessoaRepositorio _repository;

        public PessoaRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            _settingsTest = new SettingsTest();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new PessoaRepositorio(_mockOptions.Object);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var pessoa = _settingsTest.Pessoa1;
            _repository.Salvar(pessoa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Atualizar()
        {
            var pessoa = _settingsTest.Pessoa1;
            _repository.Salvar(pessoa);

            pessoa = _settingsTest.Pessoa1Editada;
            _repository.Atualizar(pessoa);

            var retorno = _repository.Obter(pessoa.Id);

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Deletar()
        {
            var pessoa1 = _settingsTest.Pessoa1;
            var pessoa2 = _settingsTest.Pessoa2;
            var pessoa3 = _settingsTest.Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            _repository.Deletar(pessoa2.Id);

            var retorno = _repository.Listar();

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
            var pessoa = _settingsTest.Pessoa1;

            _repository.Salvar(pessoa);

            var retorno = _repository.Obter(pessoa.Id);

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Listar()
        {
            var pessoa1 = _settingsTest.Pessoa1;
            var pessoa2 = _settingsTest.Pessoa2;
            var pessoa3 = _settingsTest.Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            var retorno = _repository.Listar();

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
            var pessoa = _settingsTest.Pessoa1;
            _repository.Salvar(pessoa);

            var idExistente = _repository.CheckId(1);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var pessoa1 = _settingsTest.Pessoa1;
            var pessoa2 = _settingsTest.Pessoa2;
            var pessoa3 = _settingsTest.Pessoa3;

            _repository.Salvar(pessoa1);
            _repository.Salvar(pessoa2);
            _repository.Salvar(pessoa3);

            var maxId = _repository.LocalizarMaxId();

            Assert.AreEqual(pessoa3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}