using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Entidades
{
    public class Empresa : Notificadora
    {
        public int Id { get; set; }
        public Texto Nome { get; set; }
        public string Logo { get; set; }

        public Empresa(int id, Texto nome, string logo)
        {
            Id = id;
            Nome = nome;
            Logo = logo;
        }

        public Empresa(int id) => Id = id;
    }
}