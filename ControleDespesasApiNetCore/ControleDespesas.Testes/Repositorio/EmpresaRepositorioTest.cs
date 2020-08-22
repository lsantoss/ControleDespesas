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
            Empresa empresa0 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            Empresa empresa1 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");
            Empresa empresa2 = new Empresa(0, new Texto("NomeEmpresa3", "Nome", 100), "Logo3");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            EmpresaRepositorio repository = new EmpresaRepositorio(mockOptions.Object);
            repository.Salvar(empresa0);
            repository.Salvar(empresa1);
            repository.Salvar(empresa2);

            List<EmpresaQueryResult> retorno = repository.Listar();

            Assert.Equal(empresa0.Nome.ToString(), retorno[0].Nome);
            Assert.Equal(empresa1.Nome.ToString(), retorno[1].Nome);
            Assert.Equal(empresa2.Nome.ToString(), retorno[2].Nome);
            Assert.Equal(empresa0.Logo, retorno[0].Logo);
            Assert.Equal(empresa1.Logo, retorno[1].Logo);
            Assert.Equal(empresa2.Logo, retorno[2].Logo);
        }

        //[Fact]
        //public void AtualizarEmpresa_DeveRetornarSucesso()
        //{
        //    Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");
        //    Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
        //    mock.Setup(m => m.Atualizar(empresa)).Returns("Sucesso");
        //    string resultado = mock.Object.Atualizar(empresa);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void ApagarEmpresa_DeveRetornarSucesso()
        //{
        //    Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");
        //    Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
        //    mock.Setup(m => m.Deletar(empresa.Id)).Returns("Sucesso");
        //    string resultado = mock.Object.Deletar(empresa.Id);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void ObterEmpresa_DeveRetornarSucesso()
        //{
        //    Empresa empresa = new Empresa(1, new Texto("Oi", "Nome", 100), "base64String");

        //    EmpresaQueryResult empresaQueryResult = new EmpresaQueryResult
        //    {
        //        Id = 1,
        //        Nome = "Oi",
        //        Logo = "base64String"
        //    };

        //    Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
        //    mock.Setup(m => m.Obter(empresa.Id)).Returns(empresaQueryResult);
        //    EmpresaQueryResult resultado = mock.Object.Obter(empresa.Id);
        //    Assert.Equal(empresaQueryResult, resultado);
        //}

        //[Fact]
        //public void ListarEmpresas_DeveRetornarSucesso()
        //{
        //    Empresa empresa = new Empresa(1, new Texto("Oi", "Nome", 100), "base64String");

        //    List<EmpresaQueryResult> listaEmpresasQueryResult = new List<EmpresaQueryResult>();
        //    listaEmpresasQueryResult.Add(new EmpresaQueryResult
        //    {
        //        Id = 1,
        //        Nome = "Oi",
        //        Logo = "base64String"
        //    });
        //    listaEmpresasQueryResult.Add(new EmpresaQueryResult
        //    {
        //        Id = 2,
        //        Nome = "Vivo",
        //        Logo = "base64String"
        //    });

        //    Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
        //    mock.Setup(m => m.Listar()).Returns(listaEmpresasQueryResult);
        //    List<EmpresaQueryResult> resultado = mock.Object.Listar();
        //    Assert.Equal(listaEmpresasQueryResult, resultado);
        //}

        //[Fact]
        //public void CheckId_DeveRetornarSucesso()
        //{
        //    Empresa empresa = new Empresa(1, new Texto("Oi", "Nome", 100), "base64String");
        //    Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
        //    mock.Setup(m => m.CheckId(empresa.Id)).Returns(true);
        //    bool resultado = mock.Object.CheckId(empresa.Id);
        //    Assert.True(resultado);
        //}

        //[Fact]
        //public void LocalizarMaxId_DeveRetornarSucesso()
        //{
        //    Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
        //    mock.Setup(m => m.LocalizarMaxId()).Returns(10);
        //    int resultado = mock.Object.LocalizarMaxId();
        //    Assert.Equal(10, resultado);
        //}
    }
}