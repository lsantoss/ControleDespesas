using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;

namespace ControleDespesas.Domain.Pessoas.Commands.Input
{
    public class AtualizarPessoaCommand : Notificadora, CommandPadrao
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }

        public bool ValidarCommand()
        {
            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");

            if (IdUsuario <= 0)
                AddNotificacao("IdUsuario", "IdUsuario não é valido");

            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrEmpty(ImagemPerfil))
                AddNotificacao("ImagemPerfil", "ImagemPerfil é um campo obrigatório");

            return Valido;
        }
    }
}