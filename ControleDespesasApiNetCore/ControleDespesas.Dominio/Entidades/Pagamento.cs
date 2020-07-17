using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Entidades
{
    public class Pagamento : Notificadora
    {
        public int Id { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public Empresa Empresa { get; set; }
        public Pessoa Pessoa { get; set; }
        public Texto Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }

        public Pagamento(int id, 
                         TipoPagamento tipoPagamento, 
                         Empresa empresa, 
                         Pessoa pessoa,
                         Texto descricao, 
                         double valor, 
                         DateTime dataPagamento, 
                         DateTime dataVencimento)
        {
            Id = id;
            TipoPagamento = tipoPagamento;
            Empresa = empresa;
            Pessoa = pessoa;
            Descricao = descricao;
            Valor = valor;
            DataPagamento = dataPagamento;
            DataVencimento = dataVencimento;
        }

        public Pagamento(int id)
        {
            Id = id;
        }
    }
}