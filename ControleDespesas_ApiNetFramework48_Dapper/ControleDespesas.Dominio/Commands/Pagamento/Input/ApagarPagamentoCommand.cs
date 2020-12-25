using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class ApagarPagamentoCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }

        public bool ValidarCommand()
        {
            AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

            return Valido;
        }
    }
}