using ControleDespesas.Domain.TiposPagamentos.Commands.Input;
using ControleDespesas.Domain.TiposPagamentos.Commands.Output;
using ControleDespesas.Domain.TiposPagamentos.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.TiposPagamentos.Helpers
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

        public static TipoPagamentoCommandOutput GerarDadosRetornoInsert(TipoPagamento tipoPagamento)
        {
            return new TipoPagamentoCommandOutput
            {
                Id = tipoPagamento.Id,
                Descricao = tipoPagamento.Descricao,
            };
        }

        public static TipoPagamentoCommandOutput GerarDadosRetornoUpdate(TipoPagamento tipoPagamento)
        {
            return new TipoPagamentoCommandOutput
            {
                Id = tipoPagamento.Id,
                Descricao = tipoPagamento.Descricao,
            };
        }

        public static CommandOutput GerarDadosRetornoDelete(int id)
        {
            return new CommandOutput { Id = id };
        }
    }
}