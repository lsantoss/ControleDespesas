﻿using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Commands.Usuario.Input
{
    public class ApagarUsuarioCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}