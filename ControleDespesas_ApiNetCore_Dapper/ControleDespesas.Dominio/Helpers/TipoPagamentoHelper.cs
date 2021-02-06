using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class TipoPagamentoHelper
    {
        public static TipoPagamento GerarEntidade(AdicionarTipoPagamentoCommand command)
        {
            try
            {
                string descricao = command.Descricao;

                TipoPagamento tipoPagamento = new TipoPagamento(0, descricao);
                tipoPagamento.Validar();
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
                string descricao = command.Descricao;

                TipoPagamento tipoPagamento = new TipoPagamento(id, descricao);
                tipoPagamento.Validar();
                return tipoPagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AdicionarTipoPagamentoCommandOutput GerarDadosRetornoInsert(TipoPagamento tipoPagamento)
        {
            try
            {
                return new AdicionarTipoPagamentoCommandOutput
                {
                    Id = tipoPagamento.Id,
                    Descricao = tipoPagamento.Descricao,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AtualizarTipoPagamentoCommandOutput GerarDadosRetornoUpdate(TipoPagamento tipoPagamento)
        {
            try
            {
                return new AtualizarTipoPagamentoCommandOutput
                {
                    Id = tipoPagamento.Id,
                    Descricao = tipoPagamento.Descricao,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static ApagarTipoPagamentoCommandOutput GerarDadosRetornoDelete(int id)
        {
            try
            {
                return new ApagarTipoPagamentoCommandOutput { Id = id };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}