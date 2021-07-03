namespace ControleDespesas.Domain.TiposPagamentos.Commands.Output
{
    public class TipoPagamentoCommandOutput
    {
        public long Id { get; private set; }
        public string Descricao { get; private set; }

        public TipoPagamentoCommandOutput(long id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}