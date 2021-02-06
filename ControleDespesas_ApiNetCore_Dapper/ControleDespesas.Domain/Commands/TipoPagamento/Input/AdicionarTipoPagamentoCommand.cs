using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Domain.Commands.TipoPagamento.Input
{
    public class AdicionarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        [Required]
        public string Descricao { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(Descricao))
                    AddNotificacao("Descricao", "Descricao é um campo obrigatório");
                else if (Descricao.Length > 250)
                    AddNotificacao("Descricao", "Descricao maior que o esperado");

                return Valido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}