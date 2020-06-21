using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class TipoPagamentoHelper
    {
        public static TipoPagamento GerarEntidade(AdicionarTipoPagamentoCommand command)
        {
            try
            {
                Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");

                TipoPagamento tipoPagamento = new TipoPagamento(0, descricao);
                return tipoPagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static TipoPagamento GerarEntidade(AtualizarTipoPagamentoCommand command)
        {
            try
            {
                int id = command.Id;
                Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");

                TipoPagamento tipoPagamento = new TipoPagamento(id, descricao);
                return tipoPagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static object GerarDadosRetornoCommandResult(TipoPagamento tipoPagamento)
        {
            try
            {
                return new
                {
                    Id = tipoPagamento.Id,
                    Descricao = tipoPagamento.Descricao.ToString(),
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}