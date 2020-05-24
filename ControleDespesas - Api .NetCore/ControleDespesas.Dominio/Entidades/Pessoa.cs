using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Entidades
{
    public class Pessoa : Notificadora
    {
        public int Id { get; }
        public Descricao100Caracteres Nome { get; }
        public string ImagemPerfil { get; }

        public Pessoa(int id, Descricao100Caracteres nome, string imagemPerfil)
        {
            Id = id;
            Nome = nome;
            ImagemPerfil = imagemPerfil;
        }

        public Pessoa(int id)
        {
            Id = id;
        }
    }
}