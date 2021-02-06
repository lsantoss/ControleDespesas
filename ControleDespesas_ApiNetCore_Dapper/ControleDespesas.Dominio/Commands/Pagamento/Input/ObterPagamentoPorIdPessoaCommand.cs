﻿using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Domain.Commands.Pagamento.Input
{
    public class ObterPagamentoPorIdPessoaCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int IdPessoa { get; set; }

        public bool ValidarCommand()
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