using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Entidades
{
    public class Pagamento : Notificadora
    {
        public int Id { get; }
        public TipoPagamento TipoPagamento { get; }
        public Empresa Empresa { get; }
        public Pessoa Pessoa { get; }
        public Descricao250Caracteres Descricao { get; }
        public double Valor { get; }
        public DateTime DataPagamento { get; }
        public DateTime DataVencimento { get; }

        public Pagamento(int id, 
                         TipoPagamento tipoPagamento, 
                         Empresa empresa, 
                         Pessoa pessoa, 
                         Descricao250Caracteres descricao, 
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