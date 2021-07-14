using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Usuarios.Entities
{
    [TestFixture]
    public class UsuarioTest : BaseTest
    {
        private Usuario _usuario;

        [SetUp]
        public void Setup() => _usuario = new SettingsTest().Usuario1;

        [Test]
        [TestCase(1, "Login", "Senha123", EPrivilegioUsuario.Administrador)]
        public void Construtores_Valido(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            var _usuario1 = new Usuario(id);
            var _usuario2 = new Usuario(login, senha, privilegio);
            var _usuario3 = new Usuario(id, login, senha, privilegio);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_usuario1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_usuario2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_usuario3.FormatarJsonDeSaida());

            Assert.True(_usuario1.Valido);
            Assert.AreEqual(id, _usuario1.Id);
            Assert.Null(_usuario1.Login);
            Assert.Null(_usuario1.Senha);
            Assert.AreEqual((EPrivilegioUsuario)0, _usuario1.Privilegio);
            Assert.AreEqual(0, _usuario1.Pessoas.Count);
            Assert.AreEqual(0, _usuario1.Notificacoes.Count);

            Assert.True(_usuario2.Valido);
            Assert.AreEqual(0, _usuario2.Id);
            Assert.AreEqual(login, _usuario2.Login);
            Assert.AreEqual(senha, _usuario2.Senha);
            Assert.AreEqual(privilegio, _usuario2.Privilegio);
            Assert.AreEqual(0, _usuario2.Pessoas.Count);
            Assert.AreEqual(0, _usuario2.Notificacoes.Count);

            Assert.True(_usuario3.Valido);
            Assert.AreEqual(id, _usuario3.Id);
            Assert.AreEqual(login, _usuario3.Login);
            Assert.AreEqual(senha, _usuario3.Senha);
            Assert.AreEqual(privilegio, _usuario3.Privilegio);
            Assert.AreEqual(0, _usuario3.Pessoas.Count);
            Assert.AreEqual(0, _usuario3.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null, null, -1)]
        [TestCase(0, "", "", 0)]
        [TestCase(-1, "", "aaaaa1", 0)]
        public void Construtores_Invalido(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            var _usuario1 = new Usuario(id);
            var _usuario2 = new Usuario(login, senha, privilegio);
            var _usuario3 = new Usuario(id, login, senha, privilegio);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_usuario1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_usuario2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_usuario3.FormatarJsonDeSaida());

            Assert.False(_usuario1.Valido);
            Assert.AreEqual(id, _usuario1.Id);
            Assert.Null(_usuario1.Login);
            Assert.Null(_usuario1.Senha);
            Assert.AreEqual((EPrivilegioUsuario)0, _usuario1.Privilegio);
            Assert.AreEqual(0, _usuario1.Pessoas.Count);
            Assert.AreNotEqual(0, _usuario1.Notificacoes.Count);

            Assert.False(_usuario2.Valido);
            Assert.AreEqual(0, _usuario2.Id);
            Assert.AreEqual(login, _usuario2.Login);
            Assert.AreEqual(senha, _usuario2.Senha);
            Assert.AreEqual(privilegio, _usuario2.Privilegio);
            Assert.AreEqual(0, _usuario2.Pessoas.Count);
            Assert.AreNotEqual(0, _usuario2.Notificacoes.Count);

            Assert.False(_usuario3.Valido);
            Assert.AreEqual(id, _usuario3.Id);
            Assert.AreEqual(login, _usuario3.Login);
            Assert.AreEqual(senha, _usuario3.Senha);
            Assert.AreEqual(privilegio, _usuario3.Privilegio);
            Assert.AreEqual(0, _usuario3.Pessoas.Count);
            Assert.AreNotEqual(0, _usuario3.Notificacoes.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void DefinirId_Invalido(long id)
        {
            _usuario.DefinirId(id);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(id, _usuario.Id);
            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void DefinirLogin_Invalido(string login)
        {
            _usuario.DefinirLogin(login);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(login, _usuario.Login);
            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void DefinirSenha_Invalido(string senha)
        {
            _usuario.DefinirSenha(senha);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(senha, _usuario.Senha);
            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void DefinirPrivilegio_Invalido(EPrivilegioUsuario privilegio)
        {
            _usuario.DefinirPrivilegio(privilegio);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(privilegio, _usuario.Privilegio);
            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        public void AdicionarPessoa_Valido()
        {
            _usuario.AdicionarPessoa(new SettingsTest().Pessoa1);
            _usuario.AdicionarPessoa(new SettingsTest().Pessoa1);
            _usuario.AdicionarPessoa(new SettingsTest().Pessoa1);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(3, _usuario.Pessoas.Count);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        public void AdicionarPessoas_Valido()
        {
            List<Pessoa> pessoas = new List<Pessoa>()
            {
                new SettingsTest().Pessoa1,
                new SettingsTest().Pessoa1,
                new SettingsTest().Pessoa1
            };

            _usuario.AdicionarPessoas(pessoas);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(3, _usuario.Pessoas.Count);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        public void RemoverPessoas_Valido()
        {
            _usuario.AdicionarPessoa(new SettingsTest().Pessoa1);
            _usuario.AdicionarPessoa(new SettingsTest().Pessoa1);
            _usuario.AdicionarPessoa(new SettingsTest().Pessoa1);

            _usuario.RemoverPessoas();

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(0, _usuario.Pessoas.Count);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _usuario = null;
    }
}