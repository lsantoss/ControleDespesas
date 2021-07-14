using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.TiposPagamentos.Entities
{
    public class TipoPagamento : Notificadora
    {
        public long Id { get; private set; }
        public string Descricao { get; private set; }

        public TipoPagamento(long id)
        {
            DefinirId(id);
        }

        public TipoPagamento(string descricao)
        {
            DefinirDescricao(descricao);
        }

        public TipoPagamento(long id, string descricao)
        {
            DefinirId(id);
            DefinirDescricao(descricao);
        }

        public void DefinirId(long id)
        {
            Id = id;

            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");
        }

        public void DefinirDescricao(string descricao)
        {
            Descricao = descricao;

            if (string.IsNullOrWhiteSpace(Descricao))
                AddNotificacao("Descricao", "Descricao é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descricao", "Descricao maior que o esperado");
        }
    }
}