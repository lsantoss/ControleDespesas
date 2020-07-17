using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Entidades
{
    public class TipoPagamento : Notificadora
    {
        public int Id { get; set; }
        public Texto Descricao { get; set; }

        public TipoPagamento(int id, Texto descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public TipoPagamento(int id)
        {
            Id = id;
        }
    }
}