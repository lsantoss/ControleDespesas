using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class UsuarioRepositorioTest : DatabaseTest
    {
        private readonly UsuarioRepositorio _repository;

        public UsuarioRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repository = new UsuarioRepositorio(optionsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var usuario = MockSettingsTest.Usuario1;
            _repository.Salvar(usuario);

            var retorno = _repository.Obter(usuario.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Atualizar()
        {
            var usuario = MockSettingsTest.Usuario1;
            _repository.Salvar(usuario);

            usuario = MockSettingsTest.Usuario1Editado;
            _repository.Atualizar(usuario);

            var retorno = _repository.Obter(usuario.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Deletar()
        {
            var usuario1 = MockSettingsTest.Usuario1;
            var usuario2 = MockSettingsTest.Usuario2;
            var usuario3 = MockSettingsTest.Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            _repository.Deletar(usuario2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login.ToString(), retorno[0].Login);
            Assert.AreEqual(usuario1.Senha.ToString(), retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(usuario3.Id, retorno[1].Id);
            Assert.AreEqual(usuario3.Login.ToString(), retorno[1].Login);
            Assert.AreEqual(usuario3.Senha.ToString(), retorno[1].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[1].Privilegio);
        }

        [Test]
        public void Obter()
        {
            var usuario = MockSettingsTest.Usuario1;
            _repository.Salvar(usuario);

            var retorno = _repository.Obter(usuario.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Listar()
        {
            var usuario1 = MockSettingsTest.Usuario1;
            var usuario2 = MockSettingsTest.Usuario2;
            var usuario3 = MockSettingsTest.Usuario3;

            _repository.Salvar(usuario1);
            _repository.Salvar(usuario2);
            _repository.Salvar(usuario3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login.ToString(), retorno[0].Login);
            Assert.AreEqual(usuario1.Senha.ToString(), retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(usuario2.Id, retorno[1].Id);
            Assert.AreEqual(usuario2.Login.ToString(), retorno[1].Login);
            Assert.AreEqual(usuario2.Senha.ToString(), retorno[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, retorno[1].Privilegio);

            Assert.AreEqual(usuario3.Id, retorno[2].Id);
            Assert.AreEqual(usuario3.Login.ToString(), retorno[2].Login);
            Assert.AreEqual(usuario3.Senha.ToString(), retorno[2].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[2].Privilegio);
        }

        [Test]
        public void Logar()
        {
            var usuario = MockSettingsTest.Usuario1;
            _repository.Salvar(usuario);

            var retorno = _repository.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login.ToString(), retorno.Login);
            Assert.AreEqual(usuario.Senha.ToString(), retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void CheckLogin()
        {
            var usuario = MockSettingsTest.Usuario1;
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
            var usuario = MockSettingsTest.Usuario1;
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
            var usuario1 = MockSettingsTest.Usuario1;
            var usuario2 = MockSettingsTest.Usuario2;
            var usuario3 = MockSettingsTest.Usuario3;

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