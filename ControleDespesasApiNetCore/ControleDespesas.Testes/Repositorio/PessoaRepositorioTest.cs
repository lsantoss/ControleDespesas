using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Validador.ValueObjects;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Repositorio
{
    public class PessoaRepositorioTest
    {
        //[Fact]
        //public void AdicionarPessoa_DeveRetornarSucesso()
        //{
        //    Pessoa pessoa = new Pessoa(0, new Descricao100Caracteres("Lucas", "Nome"), "base64String");
        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.Salvar(pessoa)).Returns("Sucesso");
        //    string resultado = mock.Object.Salvar(pessoa);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void AtualizarPessoa_DeveRetornarSucesso()
        //{
        //    Pessoa pessoa = new Pessoa(1, new Descricao100Caracteres("Lucas", "Nome"), "base64String");
        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.Atualizar(pessoa)).Returns("Sucesso");
        //    string resultado = mock.Object.Atualizar(pessoa);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void ApagarPessoa_DeveRetornarSucesso()
        //{
        //    Pessoa pessoa = new Pessoa(1, new Descricao100Caracteres("Lucas", "Nome"), "base64String");
        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.Deletar(pessoa.Id)).Returns("Sucesso");
        //    string resultado = mock.Object.Deletar(pessoa.Id);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void ObterPessoa_DeveRetornarSucesso()
        //{
        //    Pessoa pessoa = new Pessoa(1, new Texto("Lucas", "Nome", 100), "base64String");

        //    PessoaQueryResult pessoaQueryResult = new PessoaQueryResult
        //    {
        //        Id = 1,
        //        Nome = "Lucas",
        //        ImagemPerfil = "base64String"
        //    };

        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.Obter(pessoa.Id)).Returns(pessoaQueryResult);
        //    PessoaQueryResult resultado = mock.Object.Obter(pessoa.Id);
        //    Assert.Equal(pessoaQueryResult, resultado);
        //}

        //[Fact]
        //public void ListarPessoas_DeveRetornarSucesso()
        //{
        //    Pessoa pessoa = new Pessoa(1, new Texto("Lucas", "Nome", 100), "base64String");

        //    List<PessoaQueryResult> listaPessoasQueryResult = new List<PessoaQueryResult>();
        //    listaPessoasQueryResult.Add(new PessoaQueryResult
        //    {
        //        Id = 1,
        //        Nome = "Lucas",
        //        ImagemPerfil = "base64String"
        //    });
        //    listaPessoasQueryResult.Add(new PessoaQueryResult
        //    {
        //        Id = 2,
        //        Nome = "Mattheus",
        //        ImagemPerfil = "base64String"
        //    });

        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.Listar()).Returns(listaPessoasQueryResult);
        //    List<PessoaQueryResult> resultado = mock.Object.Listar();
        //    Assert.Equal(listaPessoasQueryResult, resultado);
        //}

        //[Fact]
        //public void CheckId_DeveRetornarSucesso()
        //{
        //    Pessoa pessoa = new Pessoa(1, new Texto("Lucas", "Nome", 100), "base64String");
        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.CheckId(pessoa.Id)).Returns(true);
        //    bool resultado = mock.Object.CheckId(pessoa.Id);
        //    Assert.True(resultado);
        //}

        //[Fact]
        //public void LocalizarMaxId_DeveRetornarSucesso()
        //{
        //    Mock<IPessoaRepositorio> mock = new Mock<IPessoaRepositorio>();
        //    mock.Setup(m => m.LocalizarMaxId()).Returns(10);
        //    int resultado = mock.Object.LocalizarMaxId();
        //    Assert.Equal(10, resultado);
        //}
    }
}