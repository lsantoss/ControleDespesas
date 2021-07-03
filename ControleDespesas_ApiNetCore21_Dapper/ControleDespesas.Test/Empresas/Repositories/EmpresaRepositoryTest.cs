using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System.Data.SqlClient;

namespace ControleDespesas.Test.Empresas.Repositories
{
    [TestFixture]
    public class EmpresaRepositoryTest : DatabaseTest
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new EmpresaRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar_Valido()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome, retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Salvar_Nome_Invalido(string nome)
        {
            var empresa = new SettingsTest().Empresa1;
            empresa.DefinirNome(nome);

            TestContext.WriteLine(empresa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repository.Salvar(empresa); });
        }

        [Test]
        [TestCase(null)]
        public void Salvar_Logo_Invalido(string logo)
        {
            var empresa = new SettingsTest().Empresa1;
            empresa.DefinirLogo(logo);

            TestContext.WriteLine(empresa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repository.Salvar(empresa); });
        }

        [Test]
        public void Atualizar_Valido()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            empresa = new SettingsTest().Empresa1Editada;
            _repository.Atualizar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome, retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Atualizar_Nome_Invalido_Exception(string nome)
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            empresa = new SettingsTest().Empresa1Editada;
            empresa.DefinirNome(nome);

            TestContext.WriteLine(empresa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repository.Atualizar(empresa); });
        }

        [Test]
        [TestCase(null)]
        public void Atualizar_Logo_Invalido_Exception(string logo)
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            empresa = new SettingsTest().Empresa1Editada;
            empresa.DefinirLogo(logo);

            TestContext.WriteLine(empresa.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repository.Atualizar(empresa); });
        }

        [Test]
        public void Deletar()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            _repository.Deletar(empresa2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(empresa1.Id, retorno[0].Id);
            Assert.AreEqual(empresa1.Nome, retorno[0].Nome);
            Assert.AreEqual(empresa1.Logo, retorno[0].Logo);

            Assert.AreEqual(empresa3.Id, retorno[1].Id);
            Assert.AreEqual(empresa3.Nome, retorno[1].Nome);
            Assert.AreEqual(empresa3.Logo, retorno[1].Logo);
        }

        [Test]
        public void Obter_ObjetoPreenchido()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var retorno = _repository.Obter(empresa.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome, retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Obter_ObjetoNulo()
        {
            var retorno = _repository.Obter(1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.Null(retorno);
        }

        [Test]
        public void Listar_ListaPreenchida()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

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
        public void Listar_ListaVazia()
        {
            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(0, retorno.Count);
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
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            var maxId = _repository.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(empresa3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}