using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.TiposPagamentos.Commands.Input
{
    public class AdicionarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        public string Descricao { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                AddNotificacao("Descricao", "Descricao é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descricao", "Descricao maior que o esperado");

            return Valido;
        }
    }
}