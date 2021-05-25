using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Pessoas.Entities
{
    public class PessoaTest : BaseTest
    {
        private Pessoa _pessoa;

        [SetUp]
        public void Setup() => _pessoa = new SettingsTest().Pessoa1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.True(_pessoa.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarEntidade_IdUsuarioInvalido(int id)
        {
            _pessoa.Usuario.DefinirId(id);
            _pessoa.Validar();

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_NomeInvalido(string nome)
        {
            _pessoa.DefinirNome(nome);
            _pessoa.Validar();

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ValidarEntidade_ImagemPerfilInvalida(string imagemPerfil)
        {
            _pessoa.DefinirImagemPerfil(imagemPerfil);
            _pessoa.Validar();

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void DefinirId(int id)
        {
            _pessoa.DefinirId(id);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(id, _pessoa.Id);
            Assert.True(_pessoa.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase("Lucas")]
        [TestCase("Ronaldo")]
        public void DefinirNome(string nome)
        {
            _pessoa.DefinirNome(nome);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(nome, _pessoa.Nome);
            Assert.True(_pessoa.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        public void DefinirUsuario()
        {
            var usuario = new SettingsTest().Usuario1;

            _pessoa.DefinirUsuario(usuario);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(usuario, _pessoa.Usuario);
            Assert.True(_pessoa.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase("Imagem.png")]
        [TestCase("Logo.png")]
        public void DefinirImagemPerfil(string logo)
        {
            _pessoa.DefinirImagemPerfil(logo);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(logo, _pessoa.ImagemPerfil);
            Assert.True(_pessoa.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pessoa = null;
    }
}