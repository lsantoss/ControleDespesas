﻿using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class ObterPagamentoCalculoEstatisticaAnoMesCommand : Notificadora, CommandPadrao
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
                AddNotificacao(new ContratoValidacao().EhMaior(IdPessoa, 0, "IdPessoa", "IdPessoa não é valido"));
                AddNotificacao(new ContratoValidacao().EhMaior(Ano, 0, "Ano", "Ano não é valido"));
                AddNotificacao(new ContratoValidacao().EhMaior(Mes, 0, "Mes", "Mês não é valido"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}