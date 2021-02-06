using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.TipoPagamento.Input
{
    public class AtualizarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (Id <= 0)
                    AddNotificacao("Id", "Id não é valido");

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