using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControleDespesas.Domain.Pessoas.Query.Parameters
{
    public class ObterPessoasQuery : Notificadora, QueryPadrao
    {
        [BindNever]
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