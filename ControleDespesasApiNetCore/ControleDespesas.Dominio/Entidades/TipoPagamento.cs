using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Entidades
{
    public class TipoPagamento : Notificadora
    {
        public int Id { get; }
        public Descricao250Caracteres Descricao { get; }

        public TipoPagamento(int id, Descricao250Caracteres descricao)
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