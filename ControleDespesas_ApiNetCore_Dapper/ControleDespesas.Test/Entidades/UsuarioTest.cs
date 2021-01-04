using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class UsuarioTest : BaseTest
    {
        private Usuario _usuario;

        [SetUp]
        public void Setup() => _usuario = new SettingsTest().Usuario1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_usuario));

            Assert.True(_usuario.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_LoginInvalido()
        {
            _usuario.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            _usuario.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_usuario));

            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
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
            _usuario.Senha = senha;
            _usuario.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_usuario));

            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_LoginESenhaInvalida()
        {
            _usuario.Login = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            _usuario.Senha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            _usuario.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_usuario));

            Assert.False(_usuario.Valido);
            Assert.AreNotEqual(0, _usuario.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _usuario = null;
    }
}