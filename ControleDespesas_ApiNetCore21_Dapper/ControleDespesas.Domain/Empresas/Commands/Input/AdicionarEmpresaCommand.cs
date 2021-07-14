using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Empresas.Commands.Input
{
    public class AdicionarEmpresaCommand : Notificadora, CommandPadrao
    {
        public string Nome { get; set; }
        public string Logo { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrWhiteSpace(Logo))
                AddNotificacao("Logo", "Logo é um campo obrigatório");

            return Valido;
        }
    }
}