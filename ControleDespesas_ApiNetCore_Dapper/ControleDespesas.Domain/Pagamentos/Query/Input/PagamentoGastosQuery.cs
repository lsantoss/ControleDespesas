using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Pagamentos.Query.Input
{
    public class PagamentoGastosQuery : Notificadora, QueryPadrao
    {
        public int IdPessoa { get; set; }
        public int? Ano { get; set; }
        public int? Mes { get; set; }

        public bool ValidarQuery()
        {
            if (IdPessoa <= 0)
                AddNotificacao("IdPessoa", "IdPessoa não é valido");

            if (Ano != null && Ano <= 0)
                AddNotificacao("Ano", "Ano não é valido");

            if (Mes != null && Ano == null)
                AddNotificacao("Ano", "Ao indicar o Mes o campo Ano é obrigatório");

            if (Mes != null && (Mes < 1 || Mes > 12))
                AddNotificacao("Mes", "Mes não é valido");

            return Valido;
        }
    }
}