using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Testes.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Repositorio
{
    public class EmpresaRepositorioTest : DatabaseFactory
    {
        public EmpresaRepositorioTest()
        {
            DroparBaseDeDados();
            CriarBaseDeDadosETabelas();
        }

        [Fact]
        public void Salvar()
        {
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa);

            EmpresaQueryResult retorno = repository.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(empresa.Nome.ToString(), retorno.Nome);
            Assert.Equal(empresa.Logo, retorno.Logo);
        }

        [Fact]
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

            Assert.Equal(empresa.Id, retorno.Id);
            Assert.Equal(empresa.Nome.ToString(), retorno.Nome);
            Assert.Equal(empresa.Logo, retorno.Logo);
        }

        [Fact]
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

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(empresa0.Nome.ToString(), retorno[0].Nome);
            Assert.Equal(empresa0.Logo, retorno[0].Logo);

            Assert.Equal(3, retorno[1].Id);
            Assert.Equal(empresa2.Nome.ToString(), retorno[1].Nome);
            Assert.Equal(empresa2.Logo, retorno[1].Logo);
        }

        [Fact]
        public void Obter()
        {
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa);

            EmpresaQueryResult retorno = repository.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(empresa.Nome.ToString(), retorno.Nome);
            Assert.Equal(empresa.Logo, retorno.Logo);
        }

        [Fact]
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

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(empresa0.Nome.ToString(), retorno[0].Nome);
            Assert.Equal(empresa0.Logo, retorno[0].Logo);

            Assert.Equal(2, retorno[1].Id);
            Assert.Equal(empresa1.Nome.ToString(), retorno[1].Nome);
            Assert.Equal(empresa1.Logo, retorno[1].Logo);

            Assert.Equal(3, retorno[2].Id);
            Assert.Equal(empresa2.Nome.ToString(), retorno[2].Nome);
            Assert.Equal(empresa2.Logo, retorno[2].Logo);
        }

        [Fact]
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

        [Fact]
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

            Assert.Equal(3, maxId);
        }
    }
}