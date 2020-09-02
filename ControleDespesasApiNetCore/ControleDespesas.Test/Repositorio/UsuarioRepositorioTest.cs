using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class UsuarioRepositorioTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly UsuarioRepositorio _repository;

        public UsuarioRepositorioTest()
        {
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new UsuarioRepositorio(_mockOptions.Object);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Atualizar()
        {
            var usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            usuario = new Usuario(1, new Texto("NomeUsuario - Editado", "Nome", 50), new SenhaMedia("Senha123Editada"), EPrivilegioUsuario.ReadOnly);
            _repository.Atualizar(usuario);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Deletar()
        {
            var usuario0 = new Usuario(0, new Texto("NomeUsuario0", "Nome", 50), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            var usuario1 = new Usuario(0, new Texto("NomeUsuario1", "Nome", 50), new SenhaMedia("Senha1231"), EPrivilegioUsuario.ReadOnly);
            var usuario2 = new Usuario(0, new Texto("NomeUsuario2", "Nome", 50), new SenhaMedia("Senha1232"), EPrivilegioUsuario.Admin);

            _repository.Salvar(usuario0);
            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);

            _repository.Deletar(2);

            var retorno = _repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(usuario0.Login.ToString(), retorno[0].Login);
            Assert.AreEqual(usuario0.Senha.ToString(), retorno[0].Senha);
            Assert.AreEqual(usuario0.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(3, retorno[1].Id);
            Assert.AreEqual(usuario2.Login.ToString(), retorno[1].Login);
            Assert.AreEqual(usuario2.Senha.ToString(), retorno[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, retorno[1].Privilegio);
        }

        [Test]
        public void Obter()
        {
            var usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Listar()
        {
            var usuario0 = new Usuario(0, new Texto("NomeUsuario0", "Nome", 50), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            var usuario1 = new Usuario(0, new Texto("NomeUsuario1", "Nome", 50), new SenhaMedia("Senha1231"), EPrivilegioUsuario.ReadOnly);
            var usuario2 = new Usuario(0, new Texto("NomeUsuario2", "Nome", 50), new SenhaMedia("Senha1232"), EPrivilegioUsuario.Admin);

            _repository.Salvar(usuario0);
            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);

            var retorno = _repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(usuario0.Login.ToString(), retorno[0].Login);
            Assert.AreEqual(usuario0.Senha.ToString(), retorno[0].Senha);
            Assert.AreEqual(usuario0.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(2, retorno[1].Id);
            Assert.AreEqual(usuario1.Login.ToString(), retorno[1].Login);
            Assert.AreEqual(usuario1.Senha.ToString(), retorno[1].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[1].Privilegio);

            Assert.AreEqual(3, retorno[2].Id);
            Assert.AreEqual(usuario2.Login.ToString(), retorno[2].Login);
            Assert.AreEqual(usuario2.Senha.ToString(), retorno[2].Senha);
            Assert.AreEqual(usuario2.Privilegio, retorno[2].Privilegio);
        }

        [Test]
        public void Logar()
        {
            var usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            var retorno = _repository.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void CheckLogin()
        {
            var usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            var loginExistente = _repository.CheckLogin(usuario.Login.ToString());
            var loginNaoExiste = _repository.CheckLogin("LoginErrado");

            Assert.True(loginExistente);
            Assert.False(loginNaoExiste);
        }

        [Test]
        public void CheckId()
        {
            var usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            _repository.Salvar(usuario);

            var idExistente = _repository.CheckId(1);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var usuario0 = new Usuario(0, new Texto("NomeUsuario0", "Nome", 50), new SenhaMedia("Senha1230"), EPrivilegioUsuario.Admin);
            var usuario1 = new Usuario(0, new Texto("NomeUsuario1", "Nome", 50), new SenhaMedia("Senha1231"), EPrivilegioUsuario.ReadOnly);
            var usuario2 = new Usuario(0, new Texto("NomeUsuario2", "Nome", 50), new SenhaMedia("Senha1232"), EPrivilegioUsuario.Admin);

            _repository.Salvar(usuario0);
            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);

            var maxId = _repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}