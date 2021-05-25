using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.TiposPagamentos.Entities
{
    public class TipoPagamento : Notificadora
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }

        public TipoPagamento(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public TipoPagamento(int id)
        {
            Id = id;
        }

        public void DefinirId(int id)
        {
            Id = id;
        }

        public void DefinirDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Descricao))
                AddNotificacao("Descricao", "Descricao é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descricao", "Descricao maior que o esperado");
        }
    }
}