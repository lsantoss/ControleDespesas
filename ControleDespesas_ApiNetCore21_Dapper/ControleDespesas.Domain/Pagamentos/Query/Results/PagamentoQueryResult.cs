using ControleDespesas.Domain.Empresas.Query.Results;
using ControleDespesas.Domain.Pessoas.Query.Results;
using ControleDespesas.Domain.TiposPagamentos.Query.Results;
using System;

namespace ControleDespesas.Domain.Pagamentos.Query.Results
{
    public class PagamentoQueryResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public TipoPagamentoQueryResult TipoPagamento { get; set; }
        public EmpresaQueryResult Empresa { get; set; }
        public PessoaQueryResult Pessoa { get; set; }
    }
}