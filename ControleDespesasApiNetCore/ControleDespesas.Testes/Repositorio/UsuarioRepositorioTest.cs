using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Validador.ValueObjects;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Repositorio
{
    public class UsuarioRepositorioTest
    {
        //[Fact]
        //public void AdicionarUsuario_DeveRetornarSucesso()
        //{
        //    Usuario usuario = new Usuario(
        //        0, 
        //        new Descricao50Caracteres("lucas123", "Login"),
        //        new Descricao50Caracteres("123", "Senha"),
        //        EPrivilegioUsuario.Admin
        //    );

        //    Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
        //    mock.Setup(m => m.Salvar(usuario)).Returns("Sucesso");
        //    string resultado = mock.Object.Salvar(usuario);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void AtualizarUsuario_DeveRetornarSucesso()
        //{
        //    Usuario usuario = new Usuario(
        //        1,
        //        new Descricao50Caracteres("lucas123", "Login"),
        //        new Descricao50Caracteres("123", "Senha"),
        //        EPrivilegioUsuario.Admin
        //    );

        //    Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
        //    mock.Setup(m => m.Atualizar(usuario)).Returns("Sucesso");
        //    string resultado = mock.Object.Atualizar(usuario);
        //    Assert.Equal("Sucesso", resultado);
        //}

        //[Fact]
        //public void ApagarUsuario_DeveRetornarSucesso()
        //{
        //    Usuario usuario = new Usuario(
        //        1,
        //        new Descricao50Caracteres("lucas123", "Login"),
        //        new Descricao50Caracteres("123", "Senha"),
        //        EPrivilegioUsuario.Admin
        //    );

        //    Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
        //    mock.Setup(m => m.Deletar(usuario.Id)).Returns("Sucesso");
        //    string resultado = mock.Object.Deletar(usuario.Id);
        //    Assert.Equal("Sucesso", resultado);
        //}

        [Fact]
        public void ObterUsuario_DeveRetornarSucesso()
        {
            Usuario usuario = new Usuario(
                1,
                new Texto("lucas123", "Login", 50),
                new SenhaMedia("123"),
                EPrivilegioUsuario.Admin
            );

            UsuarioQueryResult usuarioQueryResult = new UsuarioQueryResult
            {
                Id = 1,
                Login = "lucas123",
                Senha = "123",
                Privilegio= EPrivilegioUsuario.Admin
            };

            Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
            mock.Setup(m => m.Obter(usuario.Id)).Returns(usuarioQueryResult);
            UsuarioQueryResult resultado = mock.Object.Obter(usuario.Id);
            Assert.Equal(usuarioQueryResult, resultado);
        }

        [Fact]
        public void ListarUsuarios_DeveRetornarSucesso()
        {
            List<UsuarioQueryResult> listaUsuariosQueryResult = new List<UsuarioQueryResult>();
            listaUsuariosQueryResult.Add(new UsuarioQueryResult
            {
                Id = 1,
                Login = "lucas123",
                Senha = "123",
                Privilegio = EPrivilegioUsuario.Admin
            });
            listaUsuariosQueryResult.Add(new UsuarioQueryResult
            {
                Id = 2,
                Login = "renan123",
                Senha = "123",
                Privilegio = EPrivilegioUsuario.ReadOnly
            });

            Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
            mock.Setup(m => m.Listar()).Returns(listaUsuariosQueryResult);
            List<UsuarioQueryResult> resultado = mock.Object.Listar();
            Assert.Equal(listaUsuariosQueryResult, resultado);
        }

        [Fact]
        public void LogarUsuario_DeveRetornarSucesso()
        {
            Usuario usuario = new Usuario(
                1,
                new Texto("lucas123", "Login", 50),
                new SenhaMedia("123"),
                EPrivilegioUsuario.Admin
            );

            UsuarioQueryResult usuarioQueryResult = new UsuarioQueryResult
            {
                Id = 1,
                Login = "lucas123",
                Senha = "123",
                Privilegio = EPrivilegioUsuario.Admin
            };

            Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
            mock.Setup(m => m.Logar(usuario.Login.ToString(), usuario.Senha.ToString())).Returns(usuarioQueryResult);
            UsuarioQueryResult resultado = mock.Object.Logar(usuario.Login.ToString(), usuario.Senha.ToString());
            Assert.Equal(usuarioQueryResult, resultado);
        }

        [Fact]
        public void CheckLogin_DeveRetornarSucesso()
        {
            Usuario usuario = new Usuario(
                1,
                new Texto("lucas123", "Login", 50),
                new SenhaMedia("123"),
                EPrivilegioUsuario.Admin
            );

            Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
            mock.Setup(m => m.CheckLogin(usuario.Login.ToString())).Returns(true);
            bool resultado = mock.Object.CheckLogin(usuario.Login.ToString());
            Assert.True(resultado);
        }

        [Fact]
        public void CheckId_DeveRetornarSucesso()
        {
            Usuario usuario = new Usuario(
                1,
                new Texto("lucas123", "Login", 50),
                new SenhaMedia("123"),
                EPrivilegioUsuario.Admin
            );

            Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
            mock.Setup(m => m.CheckId(usuario.Id)).Returns(true);
            bool resultado = mock.Object.CheckId(usuario.Id);
            Assert.True(resultado);
        }

        [Fact]
        public void LocalizarMaxId_DeveRetornarSucesso()
        {
            Mock<IUsuarioRepositorio> mock = new Mock<IUsuarioRepositorio>();
            mock.Setup(m => m.LocalizarMaxId()).Returns(10);
            int resultado = mock.Object.LocalizarMaxId();
            Assert.Equal(10, resultado);
        }
    }
}