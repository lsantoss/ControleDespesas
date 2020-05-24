using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Command.Empresa.Input
{
    public class AtualizarEmpresaCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }

        public bool ValidarCommand()
        {
            AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

            AddNotificacao(new ContratoValidacao().TamanhoMinimo(Nome, 1, "Nome", "Nome é um campo obrigatório"));
            AddNotificacao(new ContratoValidacao().TamanhoMaximo(Nome, 100, "Nome", "Nome maior que o esperado"));

            AddNotificacao(new ContratoValidacao().TamanhoMinimo(Logo, 1, "Logo", "Logo é um campo obrigatório"));

            return Valido;
        }
    }
}