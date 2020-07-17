using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class PessoaTest
    {
        private readonly Pessoa _pessoaTeste;

        public PessoaTest()
        {
            int id = 1;
            Texto nome = new Texto("Lucas", "Nome", 100);
            string imagemPerfil = "base64String";
            _pessoaTeste = new Pessoa(id, nome, imagemPerfil);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Pessoa pessoa = _pessoaTeste;
            int resultado = pessoa.Notificacoes.Count;
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void ValidarEntidade_NomeInvalido()
        {
            string nomeLongo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Pessoa pessoa = new Pessoa(
                _pessoaTeste.Id,
                new Texto(nomeLongo, "Nome", 100),
                _pessoaTeste.ImagemPerfil
            );

            bool resultado = pessoa.Nome.Valido;
            Assert.False(resultado);
        }
    }
}