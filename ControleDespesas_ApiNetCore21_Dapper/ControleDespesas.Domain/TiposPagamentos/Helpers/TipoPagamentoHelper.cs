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
            return new TipoPagamento(command.Descricao);
        }

        public static TipoPagamento GerarEntidade(AtualizarTipoPagamentoCommand command)
        {
            return new TipoPagamento(command.Id, command.Descricao);
        }

        public static TipoPagamentoCommandOutput GerarDadosRetorno(TipoPagamento tipoPagamento)
        {
            return new TipoPagamentoCommandOutput(tipoPagamento.Id, tipoPagamento.Descricao);
        }

        public static CommandOutput GerarDadosRetornoDelete(long id)
        {
            return new CommandOutput(id);
        }
    }
}