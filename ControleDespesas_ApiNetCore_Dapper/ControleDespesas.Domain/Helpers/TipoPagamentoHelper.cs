using ControleDespesas.Domain.Commands.TipoPagamento.Input;
using ControleDespesas.Domain.Commands.TipoPagamento.Output;
using ControleDespesas.Domain.Entities;

namespace ControleDespesas.Domain.Helpers
{
    public static class TipoPagamentoHelper
    {
        public static TipoPagamento GerarEntidade(AdicionarTipoPagamentoCommand command)
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, command.Descricao);
            tipoPagamento.Validar();
            return tipoPagamento;
        }

        public static TipoPagamento GerarEntidade(AtualizarTipoPagamentoCommand command)
        {
            TipoPagamento tipoPagamento = new TipoPagamento(command.Id, command.Descricao);
            tipoPagamento.Validar();
            return tipoPagamento;
        }

        public static AdicionarTipoPagamentoCommandOutput GerarDadosRetornoInsert(TipoPagamento tipoPagamento)
        {
            return new AdicionarTipoPagamentoCommandOutput
            {
                Id = tipoPagamento.Id,
                Descricao = tipoPagamento.Descricao,
            };
        }

        public static AtualizarTipoPagamentoCommandOutput GerarDadosRetornoUpdate(TipoPagamento tipoPagamento)
        {
            return new AtualizarTipoPagamentoCommandOutput
            {
                Id = tipoPagamento.Id,
                Descricao = tipoPagamento.Descricao,
            };
        }

        public static ApagarTipoPagamentoCommandOutput GerarDadosRetornoDelete(int id)
        {
            return new ApagarTipoPagamentoCommandOutput { Id = id };
        }
    }
}