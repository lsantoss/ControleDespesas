using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;

namespace ControleDespesas.Domain.Pessoas.Query.Parameters
{
    public class ObterPessoasQuery : Notificadora, QueryPadrao
    {
        [JsonIgnore]
        public long IdUsuario { get; set; }
        public bool RegistrosFilhos { get; set; }

        public bool ValidarQuery()
        {
            if (IdUsuario <= 0)
                AddNotificacao("IdUsuario", "IdUsuario não é valido");

            return Valido;
        }
    }
}