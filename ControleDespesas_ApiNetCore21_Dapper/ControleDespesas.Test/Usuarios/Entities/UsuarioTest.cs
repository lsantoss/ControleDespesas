using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Entities
{
    public class UsuarioTest : BaseTest
    {
        private Usuario _usuario;

        [SetUp]
        public void Setup() => _usuario = new SettingsTest().Usuario1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_LoginInvalido(string login)
        {
            _usuario.DefinirLogin(login);
            _usuario.Validar();

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

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
        public void ValidarEntidade_SenhaInvalida(string senha)
        {
            _usuario.DefinirSenha(senha);
            _usuario.Validar();

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void DefinirId(int id)
        {
            _usuario.DefinirId(id);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(id, _usuario.Id);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase("lucas@com.br")]
        [TestCase("carlos@com.br")]
        public void DefinirLogin(string login)
        {
            _usuario.DefinirLogin(login);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(login, _usuario.Login);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase("Minhasenha123")]
        [TestCase("Senha123456")]
        public void DefinirSenha(string senha)
        {
            _usuario.DefinirSenha(senha);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(senha, _usuario.Senha);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        [TestCase(EPrivilegioUsuario.Administrador)]
        [TestCase(EPrivilegioUsuario.Escrita)]
        [TestCase(EPrivilegioUsuario.SomenteLeitura)]
        public void DefinirPrivilegio(EPrivilegioUsuario privilegio)
        {
            _usuario.DefinirPrivilegio(privilegio);

            TestContext.WriteLine(_usuario.FormatarJsonDeSaida());

            Assert.AreEqual(privilegio, _usuario.Privilegio);
            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _usuario = null;
    }
}