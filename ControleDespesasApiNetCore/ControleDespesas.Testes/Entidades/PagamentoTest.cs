using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using System;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class PagamentoTest
    {
        private Pagamento _pagamento;

        public PagamentoTest()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Texto descricao = new Texto("Pagamento do mês de Maio de Luz Elétrica", "Descrição", 250);
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            _pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_pagamento.Valido);
            Assert.True(_pagamento.Descricao.Valido);
            Assert.Equal(0, _pagamento.Notificacoes.Count);
            Assert.Equal(0, _pagamento.Descricao.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_DescricaoInvalida()
        {
            _pagamento.Descricao = new Texto(@"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                               aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                               aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Descrição", 250);

            Assert.False(_pagamento.Descricao.Valido);
            Assert.NotEqual(0, _pagamento.Descricao.Notificacoes.Count);
        }
    }
}