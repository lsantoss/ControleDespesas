using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class TipoPagamentoTest
    {
        private readonly TipoPagamento _tipoPagamentoTeste;

        public TipoPagamentoTest()
        {
            int id = 1;
            Descricao250Caracteres descricao = new Descricao250Caracteres("Luz Elétrica", "Descrição");
            _tipoPagamentoTeste = new TipoPagamento(id, descricao);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            TipoPagamento tipoPagamento = _tipoPagamentoTeste;
            int resultado = tipoPagamento.Notificacoes.Count;
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void ValidarEntidade_DescricaoInvalida()
        {
            string descricaoLonga = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            TipoPagamento tipoPagamento = new TipoPagamento(
                _tipoPagamentoTeste.Id,
                new Descricao250Caracteres(descricaoLonga, "Descrição")
            );

            bool resultado = tipoPagamento.Descricao.Valido;
            Assert.False(resultado);
        }
    }
}