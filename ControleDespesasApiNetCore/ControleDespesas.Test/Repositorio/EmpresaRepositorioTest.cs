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
    public class EmpresaRepositorioTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly EmpresaRepositorio _repository;

        public EmpresaRepositorioTest()
        {
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new EmpresaRepositorio(_mockOptions.Object);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Atualizar()
        {
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");            
            _repository.Salvar(empresa);

            empresa = new Empresa(1, new Texto("NomeEmpresa - Editada", "Nome", 100), "Logo - Editado");
            _repository.Atualizar(empresa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Deletar()
        {
            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            _repository.Salvar(empresa0);
            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);

            _repository.Deletar(2);

            var retorno = _repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(empresa0.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(empresa0.Logo, retorno[0].Logo);

            Assert.AreEqual(3, retorno[1].Id);
            Assert.AreEqual(empresa2.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(empresa2.Logo, retorno[1].Logo);
        }

        [Test]
        public void Obter()
        {
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Listar()
        {
            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            _repository.Salvar(empresa0);
            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);

            var retorno = _repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(empresa0.Nome.ToString(), retorno[0].Nome);
            Assert.AreEqual(empresa0.Logo, retorno[0].Logo);

            Assert.AreEqual(2, retorno[1].Id);
            Assert.AreEqual(empresa1.Nome.ToString(), retorno[1].Nome);
            Assert.AreEqual(empresa1.Logo, retorno[1].Logo);

            Assert.AreEqual(3, retorno[2].Id);
            Assert.AreEqual(empresa2.Nome.ToString(), retorno[2].Nome);
            Assert.AreEqual(empresa2.Logo, retorno[2].Logo);
        }

        [Test]
        public void CheckId()
        {
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            _repository.Salvar(empresa);

            var idExistente = _repository.CheckId(1);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            _repository.Salvar(empresa0);
            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);

            var maxId = _repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}