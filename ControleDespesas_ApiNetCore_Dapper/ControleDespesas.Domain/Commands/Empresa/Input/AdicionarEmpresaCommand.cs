﻿using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Commands.Empresa.Input
{
    public class AdicionarEmpresaCommand : Notificadora, CommandPadrao
    {
        public string Nome { get; set; }
        public string Logo { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(Nome))
                    AddNotificacao("Nome", "Nome é um campo obrigatório");
                else if (Nome.Length > 100)
                    AddNotificacao("Nome", "Nome maior que o esperado");

                if (string.IsNullOrEmpty(Logo))
                    AddNotificacao("Logo", "Logo é um campo obrigatório");

                return Valido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}