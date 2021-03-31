using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Entities
{
    public class Pessoa : Notificadora
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public string Nome { get; private set; }
        public string ImagemPerfil { get; private set; }

        public Pessoa(int id, Usuario usuario, string nome, string imagemPerfil)
        {
            Id = id;
            Usuario = usuario;
            Nome = nome;
            ImagemPerfil = imagemPerfil;
        }

        public Pessoa(int id)
        {
            Id = id;
        }

        public void DefinirId(int id)
        {
            Id = id;
        }

        public void DefinirUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }

        public void DefinirNome(string nome)
        {
            Nome = nome;
        }

        public void DefinirImagemPerfil(string imagemPerfil)
        {
            ImagemPerfil = imagemPerfil;
        }

        public void Validar()
        {
            if (Usuario.Id <= 0)
                AddNotificacao("Id do Usuário", "Id do Usuário não é valido");

            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrEmpty(ImagemPerfil))
                AddNotificacao("ImagemPerfil", "ImagemPerfil é um campo obrigatório");
        }
    }
}