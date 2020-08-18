using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class EmpresaTest
    {
        private Empresa _empresa;

        public EmpresaTest()
        {
            int id = 1;
            Texto nome = new Texto("Oi", "Nome", 100);
            string logo = "base64String";
            _empresa = new Empresa(id, nome, logo);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_empresa.Valido);
            Assert.True(_empresa.Nome.Valido);
            Assert.Equal(0, _empresa.Notificacoes.Count);
            Assert.Equal(0, _empresa.Nome.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_NomeInvalido()
        {
            _empresa.Nome = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Nome", 100);

            Assert.False(_empresa.Nome.Valido);
            Assert.NotEqual(0, _empresa.Nome.Notificacoes.Count);
        }
    }
}