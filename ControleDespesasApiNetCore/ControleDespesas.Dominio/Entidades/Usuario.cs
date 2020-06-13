using ControleDespesas.Dominio.Enums;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Entidades
{
    public class Usuario : Notificadora
    {
        public int Id { get; }
        public Descricao50Caracteres Login { get; }
        public Descricao50Caracteres Senha { get; }
        public EPrivilegioUsuario Privilegio { get; }

        public Usuario(int id, Descricao50Caracteres login, Descricao50Caracteres senha, EPrivilegioUsuario privilegio)
        {
            Id = id;
            Login = login;
            Senha = senha;
            Privilegio = privilegio;
        }

        public Usuario(int id)
        {
            Id = id;
        }
    }
}