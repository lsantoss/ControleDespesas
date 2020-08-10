using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using System;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class PagamentoTest
    {
        private readonly Pagamento _pagamentoTeste;

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
            _pagamentoTeste = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Pagamento pagamento = _pagamentoTeste;
            int resultado = pagamento.Notificacoes.Count;
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void ValidarEntidade_DescricaoInvalida()
        {
            string descricaoLonga = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"+
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"+
                                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Pagamento pagamento = new Pagamento(
                _pagamentoTeste.Id,
                _pagamentoTeste.TipoPagamento,
                _pagamentoTeste.Empresa,
                _pagamentoTeste.Pessoa,
                new Texto(descricaoLonga, "Descrição", 250),
                _pagamentoTeste.Valor, 
                _pagamentoTeste.DataVencimento,
                _pagamentoTeste.DataPagamento
            );

            bool resultado = pagamento.Descricao.Valido;
            Assert.False(resultado);
        }
    }
}