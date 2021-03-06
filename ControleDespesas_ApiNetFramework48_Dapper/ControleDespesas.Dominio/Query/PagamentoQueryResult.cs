﻿using System;

namespace ControleDespesas.Dominio.Query
{
    public class PagamentoQueryResult
    {
        public int Id { get; set; }        
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public TipoPagamentoQueryResult TipoPagamento { get; set; }
        public EmpresaQueryResult Empresa { get; set; }
        public PessoaQueryResult Pessoa { get; set; }
    }
}