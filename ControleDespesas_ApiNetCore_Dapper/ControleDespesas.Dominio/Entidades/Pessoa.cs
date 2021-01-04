using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Entidades
{
    public class Pessoa : Notificadora
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }

        public Pessoa(int id, Usuario usuario, string nome, string imagemPerfil)
        {
            Id = id;
            Usuario = usuario;
            Nome = nome;
            ImagemPerfil = imagemPerfil;

            Validar();
        }

        public Pessoa(int id) => Id = id;

        public void Validar()
        {
            if (Usuario.Id <= 0)
                AddNotificacao("Id do Usuário", "Id do Usuário não é valido");

            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");

            if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrEmpty(ImagemPerfil))
                AddNotificacao("Imagem de Perfil", "Imagem de Perfil é um campo obrigatório");
        }
    }
}