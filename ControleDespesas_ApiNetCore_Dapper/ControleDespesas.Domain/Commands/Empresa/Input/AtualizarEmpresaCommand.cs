using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;

namespace ControleDespesas.Domain.Commands.Empresa.Input
{
    public class AtualizarEmpresaCommand : Notificadora, CommandPadrao
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }

        public bool ValidarCommand()
        {
            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");

            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrEmpty(Logo))
                AddNotificacao("Logo", "Logo é um campo obrigatório");

            return Valido;
        }
    }
}