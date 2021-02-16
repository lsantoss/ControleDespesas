﻿using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Commands.Usuario.Input
{
    public class LoginUsuarioCommand : Notificadora, CommandPadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(Login))
                    AddNotificacao("Login", "Login é um campo obrigatório");
                else if (Login.Length > 50)
                    AddNotificacao("Login", "Login maior que o esperado");

                if (string.IsNullOrEmpty(Senha))
                    AddNotificacao("Senha", "Senha é um campo obrigatório");

                return Valido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}