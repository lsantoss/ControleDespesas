using ControleDespesas.Domain.Pagamentos.Enums;
using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Pagamentos.Query.Parameters
{
    public class PagamentoQuery : Notificadora, QueryPadrao
    {
        public int IdPessoa { get; set; }
        public EPagamentoStatus? Status { get; set; }

        public bool ValidarQuery()
        {
            if (IdPessoa <= 0)
                AddNotificacao("IdPessoa", "IdPessoa não é valido");

            return Valido;
        }
    }
}