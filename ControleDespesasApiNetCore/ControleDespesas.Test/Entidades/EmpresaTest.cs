using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Testes.Entidades
{
    public class EmpresaTest
    {
        private Empresa _empresa;

        [SetUp]
        public void Setup()
        {
            int id = 1;
            Texto nome = new Texto("Oi", "Nome", 100);
            string logo = "base64String";
            _empresa = new Empresa(id, nome, logo);
        }

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_empresa.Valido);
            Assert.True(_empresa.Nome.Valido);
            Assert.AreEqual(0, _empresa.Notificacoes.Count);
            Assert.AreEqual(0, _empresa.Nome.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_NomeInvalido()
        {
            _empresa.Nome = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Nome", 100);

            Assert.False(_empresa.Nome.Valido);
            Assert.AreNotEqual(0, _empresa.Nome.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _empresa = null;
        }
    }
}