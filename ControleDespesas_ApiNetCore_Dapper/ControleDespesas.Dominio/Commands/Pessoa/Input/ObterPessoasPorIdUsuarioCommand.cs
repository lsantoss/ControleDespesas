using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pessoa.Input
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
                    AddNotificacao("Id do Usuário", "Id do Usuário não é valido");

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}