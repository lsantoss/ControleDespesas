using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class ObterGastosAnoMesCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int IdPessoa { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        public int Mes { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (IdPessoa <= 0)
                    AddNotificacao("IdPessoa", "IdPessoa não é valido");

                if (Ano <= 0)
                    AddNotificacao("Ano", "Ano não é valido");

                if (Mes <= 0)
                    AddNotificacao("Mes", "Mes não é valido");

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}