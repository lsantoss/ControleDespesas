using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class ObterArquivoPagamentoCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int IdPagamento { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().EhMaior(IdPagamento, 0, "Id do Pagamento", "Id do Pagamento não é valido"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}