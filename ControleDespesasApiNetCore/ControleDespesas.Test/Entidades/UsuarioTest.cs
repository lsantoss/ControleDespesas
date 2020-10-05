using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class UsuarioTest : BaseTest
    {
        private Usuario _usuario;

        [SetUp]
        public void Setup() => _usuario = new UsuarioTest().MockSettingsTest.Usuario1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_usuario.Valido);
            Assert.True(_usuario.Login.Valido);
            Assert.True(_usuario.Senha.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
            Assert.AreEqual(0, _usuario.Login.Notificacoes.Count);
            Assert.AreEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_LoginInvalido()
        {
            _usuario.Login = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Login", 50);

            Assert.False(_usuario.Login.Valido);
            Assert.AreNotEqual(0, _usuario.Login.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void ValidarEntidade_SenhaInvalida(string senha)
        {
            _usuario.Senha = new SenhaMedia(senha);

            Assert.False(_usuario.Senha.Valido);
            Assert.AreNotEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_LoginESenhaInvalida()
        {
            _usuario.Login = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Login", 50);
            _usuario.Senha = new SenhaMedia("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            Assert.False(_usuario.Login.Valido);
            Assert.False(_usuario.Senha.Valido);
            Assert.AreNotEqual(0, _usuario.Login.Notificacoes.Count);
            Assert.AreNotEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _usuario = null;
    }
}