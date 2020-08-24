using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Query.Usuario;
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
    public class UsuarioRepositorioTest : DatabaseFactory
    {
        public UsuarioRepositorioTest()
        {
            DroparBaseDeDados();
            CriarBaseDeDadosETabelas();
        }

        [Fact]
        public void Salvar()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario);

            UsuarioQueryResult retorno = repository.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(usuario.Login.ToString(), retorno.Login);
            Assert.Equal(usuario.Senha.ToString(), retorno.Senha);
            Assert.Equal(usuario.Privilegio, retorno.Privilegio);
        }

        [Fact]
        public void Atualizar()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario);

            usuario = new Usuario(1, new Texto("NomeUsuario - Editado", "Nome", 50), new SenhaMedia("Senha123Editada"), EPrivilegioUsuario.ReadOnly);
            repository.Atualizar(usuario);

            UsuarioQueryResult retorno = repository.Obter(1);

            Assert.Equal(usuario.Id, retorno.Id);
            Assert.Equal(usuario.Login.ToString(), retorno.Login);
            Assert.Equal(usuario.Senha.ToString(), retorno.Senha);
            Assert.Equal(usuario.Privilegio, retorno.Privilegio);
        }

        [Fact]
        public void Deletar()
        {
            Usuario usuario0 = new Usuario(0, new Texto("NomeUsuario0", "Nome", 50), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            Usuario usuario1 = new Usuario(0, new Texto("NomeUsuario1", "Nome", 50), new SenhaMedia("Senha1231"), EPrivilegioUsuario.ReadOnly);
            Usuario usuario2 = new Usuario(0, new Texto("NomeUsuario2", "Nome", 50), new SenhaMedia("Senha1232"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario0);
            repository.Salvar(usuario1);
            repository.Salvar(usuario2);

            repository.Deletar(2);

            List<UsuarioQueryResult> retorno = repository.Listar();

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(usuario0.Login.ToString(), retorno[0].Login);
            Assert.Equal(usuario0.Senha.ToString(), retorno[0].Senha);
            Assert.Equal(usuario0.Privilegio, retorno[0].Privilegio);

            Assert.Equal(3, retorno[1].Id);
            Assert.Equal(usuario2.Login.ToString(), retorno[1].Login);
            Assert.Equal(usuario2.Senha.ToString(), retorno[1].Senha);
            Assert.Equal(usuario2.Privilegio, retorno[1].Privilegio);
        }

        [Fact]
        public void Obter()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario);

            UsuarioQueryResult retorno = repository.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(usuario.Login.ToString(), retorno.Login);
            Assert.Equal(usuario.Senha.ToString(), retorno.Senha);
            Assert.Equal(usuario.Privilegio, retorno.Privilegio);
        }

        [Fact]
        public void Listar()
        {
            Usuario usuario0 = new Usuario(0, new Texto("NomeUsuario0", "Nome", 50), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            Usuario usuario1 = new Usuario(0, new Texto("NomeUsuario1", "Nome", 50), new SenhaMedia("Senha1231"), EPrivilegioUsuario.ReadOnly);
            Usuario usuario2 = new Usuario(0, new Texto("NomeUsuario2", "Nome", 50), new SenhaMedia("Senha1232"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario0);
            repository.Salvar(usuario1);
            repository.Salvar(usuario2);

            List<UsuarioQueryResult> retorno = repository.Listar();

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(usuario0.Login.ToString(), retorno[0].Login);
            Assert.Equal(usuario0.Senha.ToString(), retorno[0].Senha);
            Assert.Equal(usuario0.Privilegio, retorno[0].Privilegio);

            Assert.Equal(2, retorno[1].Id);
            Assert.Equal(usuario1.Login.ToString(), retorno[1].Login);
            Assert.Equal(usuario1.Senha.ToString(), retorno[1].Senha);
            Assert.Equal(usuario1.Privilegio, retorno[1].Privilegio);

            Assert.Equal(3, retorno[2].Id);
            Assert.Equal(usuario2.Login.ToString(), retorno[2].Login);
            Assert.Equal(usuario2.Senha.ToString(), retorno[2].Senha);
            Assert.Equal(usuario2.Privilegio, retorno[2].Privilegio);
        }

        [Fact]
        public void Logar()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario);

            UsuarioQueryResult retorno = repository.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            Assert.Equal(1, retorno.Id);
            Assert.Equal(usuario.Login.ToString(), retorno.Login);
            Assert.Equal(usuario.Senha.ToString(), retorno.Senha);
            Assert.Equal(usuario.Privilegio, retorno.Privilegio);
        }

        [Fact]
        public void CheckLogin()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario);

            bool loginExistente = repository.CheckLogin(usuario.Login.ToString());
            bool loginNaoExiste = repository.CheckLogin("LoginErrado");

            Assert.True(loginExistente);
            Assert.False(loginNaoExiste);
        }

        [Fact]
        public void CheckId()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario);

            bool idExistente = repository.CheckId(1);
            bool idNaoExiste = repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Fact]
        public void LocalizarMaxId()
        {
            Usuario usuario0 = new Usuario(0, new Texto("NomeUsuario0", "Nome", 50), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            Usuario usuario1 = new Usuario(0, new Texto("NomeUsuario1", "Nome", 50), new SenhaMedia("Senha1231"), EPrivilegioUsuario.ReadOnly);
            Usuario usuario2 = new Usuario(0, new Texto("NomeUsuario2", "Nome", 50), new SenhaMedia("Senha1232"), EPrivilegioUsuario.Admin);

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptions.Object);
            repository.Salvar(usuario0);
            repository.Salvar(usuario1);
            repository.Salvar(usuario2);

            int maxId = repository.LocalizarMaxId();

            Assert.Equal(3, maxId);
        }
    }
}