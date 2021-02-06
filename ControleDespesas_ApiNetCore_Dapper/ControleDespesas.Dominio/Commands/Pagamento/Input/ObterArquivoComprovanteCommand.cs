using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class ObterArquivoComprovanteCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int IdPagamento { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (IdPagamento <= 0)
                    AddNotificacao("IdPagamento", "IdPagamento não é valido");

                return Valido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}