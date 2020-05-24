using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Command.Empresa.Input
{
    public class ApagarEmpresaCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }

        public bool ValidarCommand()
        {
            AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

            return Valido;
        }
    }
}