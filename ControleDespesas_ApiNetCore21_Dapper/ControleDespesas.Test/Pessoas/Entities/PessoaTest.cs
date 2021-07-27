using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Pessoas.Entities
{
    [TestFixture]
    public class PessoaTest : BaseTest
    {
        private Pessoa _pessoa;

        [SetUp]
        public void Setup() => _pessoa = new SettingsTest().Pessoa1;

        [Test]
        [TestCase(1, 1, "Lucas", "ImagemPerfil1.png")]
        public void Construtores_Valido(long id, long idUsuario, string nome, string imagemPerfil)
        {
            var _pessoa1 = new Pessoa(id);
            var _pessoa2 = new Pessoa(idUsuario, nome, imagemPerfil);
            var _pessoa3 = new Pessoa(id, idUsuario, nome, imagemPerfil);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_pessoa1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_pessoa2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_pessoa3.FormatarJsonDeSaida());

            Assert.True(_pessoa1.Valido);
            Assert.AreEqual(id, _pessoa1.Id);
            Assert.AreEqual(0, _pessoa1.IdUsuario);
            Assert.Null(_pessoa1.Nome);
            Assert.Null(_pessoa1.ImagemPerfil);
            Assert.AreEqual(0, _pessoa1.Pagamentos.Count);
            Assert.AreEqual(0, _pessoa1.Notificacoes.Count);

            Assert.True(_pessoa2.Valido);
            Assert.AreEqual(0, _pessoa2.Id);
            Assert.AreEqual(idUsuario, _pessoa2.IdUsuario);
            Assert.AreEqual(nome, _pessoa2.Nome);
            Assert.AreEqual(imagemPerfil, _pessoa2.ImagemPerfil);
            Assert.AreEqual(0, _pessoa2.Pagamentos.Count);
            Assert.AreEqual(0, _pessoa2.Notificacoes.Count);

            Assert.True(_pessoa3.Valido);
            Assert.AreEqual(id, _pessoa3.Id);
            Assert.AreEqual(idUsuario, _pessoa3.IdUsuario);
            Assert.AreEqual(nome, _pessoa3.Nome);
            Assert.AreEqual(imagemPerfil, _pessoa3.ImagemPerfil);
            Assert.AreEqual(0, _pessoa3.Pagamentos.Count);
            Assert.AreEqual(0, _pessoa3.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, 0, null, null)]
        [TestCase(-1, -1, "", "")]
        [TestCase(0, 0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase(0, 0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void Construtores_Invalido(long id, long idUsuario, string nome, string imagemPerfil)
        {
            var _pessoa1 = new Pessoa(id);
            var _pessoa2 = new Pessoa(idUsuario, nome, imagemPerfil);
            var _pessoa3 = new Pessoa(id, idUsuario, nome, imagemPerfil);

            TestContext.WriteLine("Contrutor 1:");
            TestContext.WriteLine(_pessoa1.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 2:");
            TestContext.WriteLine(_pessoa2.FormatarJsonDeSaida());
            TestContext.WriteLine("\nContrutor 3:");
            TestContext.WriteLine(_pessoa3.FormatarJsonDeSaida());

            Assert.False(_pessoa1.Valido);
            Assert.AreEqual(id, _pessoa1.Id);
            Assert.AreEqual(0, _pessoa1.IdUsuario);
            Assert.Null(_pessoa1.Nome);
            Assert.Null(_pessoa1.ImagemPerfil);
            Assert.AreEqual(0, _pessoa1.Pagamentos.Count);
            Assert.AreNotEqual(0, _pessoa1.Notificacoes.Count);

            Assert.False(_pessoa2.Valido);
            Assert.AreEqual(0, _pessoa2.Id);
            Assert.AreEqual(idUsuario, _pessoa2.IdUsuario);
            Assert.AreEqual(nome, _pessoa2.Nome);
            Assert.AreEqual(imagemPerfil, _pessoa2.ImagemPerfil);
            Assert.AreEqual(0, _pessoa2.Pagamentos.Count);
            Assert.AreNotEqual(0, _pessoa2.Notificacoes.Count);

            Assert.False(_pessoa3.Valido);
            Assert.AreEqual(id, _pessoa3.Id);
            Assert.AreEqual(idUsuario, _pessoa3.IdUsuario);
            Assert.AreEqual(nome, _pessoa3.Nome);
            Assert.AreEqual(imagemPerfil, _pessoa3.ImagemPerfil);
            Assert.AreEqual(0, _pessoa3.Pagamentos.Count);
            Assert.AreNotEqual(0, _pessoa3.Notificacoes.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void DefinirId_Invalido(long id)
        {
            _pessoa.DefinirId(id);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(id, _pessoa.Id);
            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void DefinirIdUsuario_Invalido(long idUsuario)
        {
            _pessoa.DefinirIdUsuario(idUsuario);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(idUsuario, _pessoa.IdUsuario);
            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void DefinirNome_Invalido(string nome)
        {
            _pessoa.DefinirNome(nome);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(nome, _pessoa.Nome);
            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void DefinirImagemPerfil_Invalido(string imagemPerfil)
        {
            _pessoa.DefinirImagemPerfil(imagemPerfil);

            TestContext.WriteLine(_pessoa.FormatarJsonDeSaida());

            Assert.AreEqual(imagemPerfil, _pessoa.ImagemPerfil);
            Assert.False(_pessoa.Valido);
            Assert.AreNotEqual(0, _pessoa.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pessoa = null;
    }
}