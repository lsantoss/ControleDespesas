using ControleDespesas.Domain.Query.Empresa.Results;
using ControleDespesas.Domain.Query.Pessoa.Results;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using System;

namespace ControleDespesas.Domain.Query.Pagamento.Results
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