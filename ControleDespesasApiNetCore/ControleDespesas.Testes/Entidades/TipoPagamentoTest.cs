using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class TipoPagamentoTest
    {
        private TipoPagamento _tipoPagamento;

        public TipoPagamentoTest()
        {
            int id = 1;
            Texto descricao = new Texto("Luz Elétrica", "Descrição", 250);
            _tipoPagamento = new TipoPagamento(id, descricao);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_tipoPagamento.Valido);
            Assert.True(_tipoPagamento.Descricao.Valido);
            Assert.Equal(0, _tipoPagamento.Notificacoes.Count);
            Assert.Equal(0, _tipoPagamento.Descricao.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_DescricaoInvalida()
        {
            _tipoPagamento.Descricao = new Texto(@"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Descrição", 250);

            Assert.False(_tipoPagamento.Descricao.Valido);
            Assert.NotEqual(0, _tipoPagamento.Descricao.Notificacoes.Count);
        }
    }
}