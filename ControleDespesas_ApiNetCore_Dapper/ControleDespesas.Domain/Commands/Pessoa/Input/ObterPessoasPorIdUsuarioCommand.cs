using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Domain.Commands.Pessoa.Input
{
    public class ObterPessoasPorIdUsuarioCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int IdUsuario { get; set; }

        public bool ValidarCommand()
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