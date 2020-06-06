using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using LSCode.Validador.ValueObjects;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Intefaces
{
    public class IEmpresaRepositorioTest
    {
        [Fact]
        public void AdicionarEmpresa_DeveRetornarSucesso()
        {
            Empresa empresa = new Empresa(0, new Descricao100Caracteres("Oi", "Nome"), "base64String");
            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.Salvar(empresa)).Returns("Sucesso");
            string resultado = mock.Object.Salvar(empresa);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void AtualizarEmpresa_DeveRetornarSucesso()
        {
            Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");
            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.Atualizar(empresa)).Returns("Sucesso");
            string resultado = mock.Object.Atualizar(empresa);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void ApagarEmpresa_DeveRetornarSucesso()
        {
            Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");
            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.Deletar(empresa.Id)).Returns("Sucesso");
            string resultado = mock.Object.Deletar(empresa.Id);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void ObterEmpresa_DeveRetornarSucesso()
        {
            Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");

            EmpresaQueryResult empresaQueryResult = new EmpresaQueryResult
            {
                Id = 1,
                Nome = "Oi",
                Logo = "base64String"
            };

            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.ObterEmpresa(empresa.Id)).Returns(empresaQueryResult);
            EmpresaQueryResult resultado = mock.Object.ObterEmpresa(empresa.Id);
            Assert.Equal(empresaQueryResult, resultado);
        }

        [Fact]
        public void ListarEmpresas_DeveRetornarSucesso()
        {
            Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");

            List<EmpresaQueryResult> listaEmpresasQueryResult = new List<EmpresaQueryResult>();
            listaEmpresasQueryResult.Add(new EmpresaQueryResult
            {
                Id = 1,
                Nome = "Oi",
                Logo = "base64String"
            });
            listaEmpresasQueryResult.Add(new EmpresaQueryResult
            {
                Id = 2,
                Nome = "Vivo",
                Logo = "base64String"
            });

            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.ListarEmpresas()).Returns(listaEmpresasQueryResult);
            List<EmpresaQueryResult> resultado = mock.Object.ListarEmpresas();
            Assert.Equal(listaEmpresasQueryResult, resultado);
        }

        [Fact]
        public void CheckId_DeveRetornarSucesso()
        {
            Empresa empresa = new Empresa(1, new Descricao100Caracteres("Oi", "Nome"), "base64String");
            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.CheckId(empresa.Id)).Returns(true);
            bool resultado = mock.Object.CheckId(empresa.Id);
            Assert.True(resultado);
        }

        [Fact]
        public void LocalizarMaxId_DeveRetornarSucesso()
        {
            Mock<IEmpresaRepositorio> mock = new Mock<IEmpresaRepositorio>();
            mock.Setup(m => m.LocalizarMaxId()).Returns(10);
            int resultado = mock.Object.LocalizarMaxId();
            Assert.Equal(10, resultado);
        }
    }
}