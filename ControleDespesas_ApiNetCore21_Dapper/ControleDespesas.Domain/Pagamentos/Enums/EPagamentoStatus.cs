using System.ComponentModel;

namespace ControleDespesas.Domain.Pagamentos.Enums
{
    public enum EPagamentoStatus
    {
        [Description("Pendente")]
        Pendente = 1,

        [Description("Pago")]
        Pago = 2
    }
}