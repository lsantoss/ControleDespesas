using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Empresa;
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
    public class EmpresaRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa);

            EmpresaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Atualizar()
        {
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa);

            empresa = new Empresa(1, new Texto("NomeEmpresa - Editada", "Nome", 100), "Logo - Editado");
            repository.Atualizar(empresa);

            EmpresaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(empresa.Id, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Deletar()
        {
            Empresa empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            Empresa empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            Empresa empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa0);
            repository.Salvar(empresa1);
            repository.Salvar(empresa2);

            repository.Deletar(2);

            List<EmpresaQueryResult> retorno = repository.Listar();

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
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa);

            EmpresaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(empresa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(empresa.Logo, retorno.Logo);
        }

        [Test]
        public void Listar()
        {
            Empresa empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            Empresa empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            Empresa empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa0);
            repository.Salvar(empresa1);
            repository.Salvar(empresa2);

            List<EmpresaQueryResult> retorno = repository.Listar();

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
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa);

            bool idExistente = repository.CheckId(1);
            bool idNaoExiste = repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            Empresa empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            Empresa empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            Empresa empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa0);
            repository.Salvar(empresa1);
            repository.Salvar(empresa2);

            int maxId = repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}