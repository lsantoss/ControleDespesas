using System;

namespace ControleDespesas.Domain.Commands.Pagamento.Output
{
    public class AtualizarPagamentoCommandOutput
    {
        public int Id { get; set; }
        public int IdTipoPagamento { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ArquivoPagamento { get; set; }
        public string ArquivoComprovante { get; set; }
    }
}