using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Domain.Query.Pessoa.Input
{
    public class ObterPessoasQuery : Notificadora, QueryPadrao
    {
        [Required]
        public int IdUsuario { get; set; }

        public bool ValidarQuery()
        {
            try
            {
                if (IdUsuario <= 0)
                    AddNotificacao("IdUsuario", "IdUsuario não é valido");

                return Valido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}