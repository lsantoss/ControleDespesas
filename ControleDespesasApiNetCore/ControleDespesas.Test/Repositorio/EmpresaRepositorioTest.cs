using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class EmpresaRepositorioTest : DatabaseFactory
    {
        private readonly SettingsTest _settingsTest;
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly EmpresaRepositorio _repository;

        public EmpresaRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            _settingsTest = new SettingsTest();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new EmpresaRepositorio(_mockOptions.Object);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var empresa = _settingsTest.Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Atualizar()
        {
            var empresa = _settingsTest.Empresa1;           
            _repository.Salvar(empresa);

            empresa = _settingsTest.Empresa1Editada;
            _repository.Atualizar(empresa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Deletar()
        {
            var empresa1 = _settingsTest.Empresa1;
            var empresa2 = _settingsTest.Empresa2;
            var empresa3 = _settingsTest.Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            _repository.Deletar(2);

            var retorno = _repository.Listar();

            Assert.AreEqual(empresa1.Id, retorno[0].Id);
            Assert.AreEqual(empresa1.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(empresa1.Logo, retorno[0].Logo);

            Assert.AreEqual(empresa3.Id, retorno[1].Id);
            Assert.AreEqual(empresa3.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(empresa3.Logo, retorno[1].Logo);
        }

        [Test]
        public void Obter()
        {
            var empresa = _settingsTest.Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Listar()
        {
            var empresa1 = _settingsTest.Empresa1;
            var empresa2 = _settingsTest.Empresa2;
            var empresa3 = _settingsTest.Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            var retorno = _repository.Listar();

            Assert.AreEqual(empresa1.Id, retorno[0].Id);
            Assert.AreEqual(empresa1.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(empresa1.Logo, retorno[0].Logo);

            Assert.AreEqual(empresa2.Id, retorno[1].Id);
            Assert.AreEqual(empresa2.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(empresa2.Logo, retorno[1].Logo);

            Assert.AreEqual(empresa3.Id, retorno[2].Id);
            Assert.AreEqual(empresa3.Nome.ToString(), retorno[2].Nome);
            Assert.AreEqual(empresa3.Logo, retorno[2].Logo);
        }

        [Test]
        public void CheckId()
        {
            var empresa = _settingsTest.Empresa1;
            _repository.Salvar(empresa);

            var idExistente = _repository.CheckId(1);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var empresa1 = _settingsTest.Empresa1;
            var empresa2 = _settingsTest.Empresa2;
            var empresa3 = _settingsTest.Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            var maxId = _repository.LocalizarMaxId();

            Assert.AreEqual(empresa3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}