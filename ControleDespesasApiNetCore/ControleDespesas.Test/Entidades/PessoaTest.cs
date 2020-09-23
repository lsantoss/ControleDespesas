using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Settings;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class PessoaTest
    {
        private Pessoa _pessoa;

        [SetUp]
        public void Setup() => _pessoa = new SettingsTest().Pessoa1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_pessoa.Valido);
            Assert.True(_pessoa.Nome.Valido);
            Assert.AreEqual(0, _pessoa.Notificacoes.Count);
            Assert.AreEqual(0, _pessoa.Nome.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_NomeInvalido()
        {
            _pessoa.Nome = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Nome", 100);

            Assert.False(_pessoa.Nome.Valido);
            Assert.AreNotEqual(0, _pessoa.Nome.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pessoa = null;
    }
}