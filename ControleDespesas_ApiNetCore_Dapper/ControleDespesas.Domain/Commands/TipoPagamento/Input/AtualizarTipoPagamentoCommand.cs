﻿using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;
using System;

namespace ControleDespesas.Domain.Commands.TipoPagamento.Input
{
    public class AtualizarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        [JsonIgnore]
        public int Id { get; set; }
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