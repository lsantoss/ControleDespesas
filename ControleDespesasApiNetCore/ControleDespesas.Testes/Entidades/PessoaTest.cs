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
            Descricao100Caracteres nome = new Descricao100Caracteres("Lucas", "Nome");
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
                new Descricao100Caracteres(nomeLongo, "Nome"),
                _pessoaTeste.ImagemPerfil
            );

            bool resultado = pessoa.Nome.Valido;
            Assert.False(resultado);
        }
    }
}