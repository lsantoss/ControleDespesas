using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Factory;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class EmpresaRepositorioTest : DatabaseFactory
    {
        private readonly EmpresaRepositorio _repository;

        public EmpresaRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            _repository = new EmpresaRepositorio(Options.Create(MockSettingsInfraData));
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var empresa = MockSettingsTest.Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Atualizar()
        {
            var empresa = MockSettingsTest.Empresa1;           
            _repository.Salvar(empresa);

            empresa = MockSettingsTest.Empresa1Editada;
            _repository.Atualizar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Deletar()
        {
            var empresa1 = MockSettingsTest.Empresa1;
            var empresa2 = MockSettingsTest.Empresa2;
            var empresa3 = MockSettingsTest.Empresa3;

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
            var empresa = MockSettingsTest.Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Listar()
        {
            var empresa1 = MockSettingsTest.Empresa1;
            var empresa2 = MockSettingsTest.Empresa2;
            var empresa3 = MockSettingsTest.Empresa3;

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
            var empresa = MockSettingsTest.Empresa1;
            _repository.Salvar(empresa);

            var idExistente = _repository.CheckId(empresa.Id);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var empresa1 = MockSettingsTest.Empresa1;
            var empresa2 = MockSettingsTest.Empresa2;
            var empresa3 = MockSettingsTest.Empresa3;

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