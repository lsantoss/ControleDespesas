using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;

namespace ControleDespesas.Domain.TiposPagamentos.Commands.Input
{
    public class AtualizarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Descricao { get; set; }

        public bool ValidarCommand()
        {
            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");

            if (string.IsNullOrEmpty(Descricao))
                AddNotificacao("Descricao", "Descricao é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descricao", "Descricao maior que o esperado");

            return Valido;
        }
    }
}