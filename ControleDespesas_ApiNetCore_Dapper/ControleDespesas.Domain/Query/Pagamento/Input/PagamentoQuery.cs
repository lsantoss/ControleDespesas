using ControleDespesas.Domain.Enums;
using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Domain.Query.Pagamento.Input
{
    public class PagamentoQuery : Notificadora, QueryPadrao
    {
        [Required]
        public int IdPessoa { get; set; }

        public EPagamentoStatus? Status { get; set; }

        public bool ValidarQuery()
        {
            try
            {
                if (IdPessoa <= 0)
                    AddNotificacao("IdPessoa", "IdPessoa não é valido");

                return Valido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}