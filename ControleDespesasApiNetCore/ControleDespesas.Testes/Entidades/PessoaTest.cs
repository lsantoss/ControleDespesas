using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class PessoaTest
    {
        private Pessoa _pessoa;

        public PessoaTest()
        {
            int id = 1;
            Texto nome = new Texto("Lucas", "Nome", 100);
            string imagemPerfil = "base64String";
            _pessoa = new Pessoa(id, nome, imagemPerfil);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_pessoa.Valido);
            Assert.True(_pessoa.Nome.Valido);
            Assert.Equal(0, _pessoa.Notificacoes.Count);
            Assert.Equal(0, _pessoa.Nome.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_NomeInvalido()
        {
            _pessoa.Nome = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Nome", 100);

            Assert.False(_pessoa.Nome.Valido);
            Assert.NotEqual(0, _pessoa.Nome.Notificacoes.Count);
        }
    }
}