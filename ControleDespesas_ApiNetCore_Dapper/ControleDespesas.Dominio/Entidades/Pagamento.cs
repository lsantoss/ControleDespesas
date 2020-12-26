﻿using LSCode.Validador.ValidacoesNotificacoes;
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
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ArquivoPagamento { get; set; }
        public string ArquivoComprovante { get; set; }

        public Pagamento(int id, 
                         TipoPagamento tipoPagamento, 
                         Empresa empresa, 
                         Pessoa pessoa,
                         Texto descricao, 
                         double valor, 
                         DateTime dataVencimento, 
                         DateTime? dataPagamento,
                         string arquivoPagamento,
                         string arquivoComprovante)
        {
            Id = id;
            TipoPagamento = tipoPagamento;
            Empresa = empresa;
            Pessoa = pessoa;
            Descricao = descricao;
            Valor = valor;
            DataVencimento = dataVencimento;
            DataPagamento = dataPagamento;
            ArquivoPagamento = arquivoPagamento;
            ArquivoComprovante = arquivoComprovante;
        }

        public Pagamento(int id) => Id = id;
    }
}