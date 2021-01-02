using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class EmpresaRepositorioTest : DatabaseTest
    {
        private readonly EmpresaRepositorio _repository;

        public EmpresaRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

             _repository = new EmpresaRepositorio(optionsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome, retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Atualizar()
        {
            var empresa = new SettingsTest().Empresa1;           
            _repository.Salvar(empresa);

            empresa = new SettingsTest().Empresa1Editada;
            _repository.Atualizar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome, retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Deletar()
        {
            var empresa1 = new SettingsTest().Empresa1;
            var empresa2 = new SettingsTest().Empresa2;
            var empresa3 = new SettingsTest().Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            _repository.Deletar(empresa2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(empresa1.Id, retorno[0].Id);
            Assert.AreEqual(empresa1.Nome, retorno[0].Nome);
            Assert.AreEqual(empresa1.Logo, retorno[0].Logo);

            Assert.AreEqual(empresa3.Id, retorno[1].Id);
            Assert.AreEqual(empresa3.Nome, retorno[1].Nome);
            Assert.AreEqual(empresa3.Logo, retorno[1].Logo);
        }

        [Test]
        public void Obter()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome, retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Listar()
        {
            var empresa1 = new SettingsTest().Empresa1;
            var empresa2 = new SettingsTest().Empresa2;
            var empresa3 = new SettingsTest().Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(empresa1.Id, retorno[0].Id);
            Assert.AreEqual(empresa1.Nome, retorno[0].Nome);
            Assert.AreEqual(empresa1.Logo, retorno[0].Logo);

            Assert.AreEqual(empresa2.Id, retorno[1].Id);
            Assert.AreEqual(empresa2.Nome, retorno[1].Nome);
            Assert.AreEqual(empresa2.Logo, retorno[1].Logo);

            Assert.AreEqual(empresa3.Id, retorno[2].Id);
            Assert.AreEqual(empresa3.Nome, retorno[2].Nome);
            Assert.AreEqual(empresa3.Logo, retorno[2].Logo);
        }

        [Test]
        public void CheckId()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var idExistente = _repository.CheckId(empresa.Id);
            var idNaoExiste = _repository.CheckId(25);

            TestContext.WriteLine(idExistente);
            TestContext.WriteLine(idNaoExiste);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var empresa1 = new SettingsTest().Empresa1;
            var empresa2 = new SettingsTest().Empresa2;
            var empresa3 = new SettingsTest().Empresa3;

            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);
            _repository.Salvar(empresa3);

            var maxId = _repository.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(empresa3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}