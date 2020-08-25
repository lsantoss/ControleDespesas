using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesasApiNetCore.TestNUnit.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class EmpresaRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup()
        {
            CriarBaseDeDadosETabelas();
        }

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

        [TearDown]
        public void TearDown()
        {
            DroparBaseDeDados();
        }
    }
}