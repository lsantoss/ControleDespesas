using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Dominio.Query.TipoPagamento;
using System;

namespace ControleDespesas.Dominio.Query.Pagamento
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