using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositories
{
    public class UsuarioRepositoryTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new UsuarioRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var retorno = _repository.Obter(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Atualizar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            usuario = new SettingsTest().Usuario1Editado;
            _repository.Atualizar(usuario);

            var retorno = _repository.Obter(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Deletar()
        {
            var usuario1 = new SettingsTest().Usuario1;
            var usuario2 = new SettingsTest().Usuario2;
            var usuario3 = new SettingsTest().Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            _repository.Deletar(usuario2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login, retorno[0].Login);
            Assert.AreEqual(usuario1.Senha, retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(usuario3.Id, retorno[1].Id);
            Assert.AreEqual(usuario3.Login, retorno[1].Login);
            Assert.AreEqual(usuario3.Senha, retorno[1].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[1].Privilegio);
        }

        [Test]
        public void Obter()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var retorno = _repository.Obter(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Listar()
        {
            var usuario1 = new SettingsTest().Usuario1;
            var usuario2 = new SettingsTest().Usuario2;
            var usuario3 = new SettingsTest().Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login, retorno[0].Login);
            Assert.AreEqual(usuario1.Senha, retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(usuario2.Id, retorno[1].Id);
            Assert.AreEqual(usuario2.Login, retorno[1].Login);
            Assert.AreEqual(usuario2.Senha, retorno[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, retorno[1].Privilegio);

            Assert.AreEqual(usuario3.Id, retorno[2].Id);
            Assert.AreEqual(usuario3.Login, retorno[2].Login);
            Assert.AreEqual(usuario3.Senha, retorno[2].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[2].Privilegio);
        }

        [Test]
        public void Logar()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var retorno = _repository.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void CheckLogin()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var loginExistente = _repository.CheckLogin(usuario.Login.ToString());
            var loginNaoExiste = _repository.CheckLogin("LoginErrado");

            TestContext.WriteLine(loginExistente);
            TestContext.WriteLine(loginNaoExiste);

            Assert.True(loginExistente);
            Assert.False(loginNaoExiste);
        }

        [Test]
        public void CheckId()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var idExistente = _repository.CheckId(usuario.Id);
            var idNaoExiste = _repository.CheckId(25);

            TestContext.WriteLine(idExistente);
            TestContext.WriteLine(idNaoExiste);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var usuario1 = new SettingsTest().Usuario1;
            var usuario2 = new SettingsTest().Usuario2;
            var usuario3 = new SettingsTest().Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var maxId = _repository.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(usuario3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}