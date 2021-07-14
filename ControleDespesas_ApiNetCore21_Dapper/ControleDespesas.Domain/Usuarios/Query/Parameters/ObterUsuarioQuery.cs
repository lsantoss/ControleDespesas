using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Usuarios.Query.Parameters
{
    public class ObterUsuarioQuery : Notificadora, QueryPadrao
    {
        public bool RegistrosFilhos { get; set; }

        public bool ValidarQuery()
        {
            return Valido;
        }
    }
}