using System;

namespace ControleDespesas.Domain.Pagamentos.Commands.Output
{
    public class PagamentoCommandOutput
    {
        public int Id { get; set; }
        public int IdTipoPagamento { get; set; }
        public long IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ArquivoPagamento { get; set; }
        public string ArquivoComprovante { get; set; }
    }
}