using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Entities
{
    public class Empresa : Notificadora
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Logo { get; private set; }

        public Empresa(int id, string nome, string logo)
        {
            Id = id;
            Nome = nome;
            Logo = logo;
        }

        public Empresa(int id)
        {
            Id = id;
        }

        public void DefinirId(int id)
        {
            Id = id;
        }

        public void DefinirNome(string nome)
        {
            Nome = nome;
        }

        public void DefinirLogo(string logo)
        {
            Logo = logo;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrEmpty(Logo))
                AddNotificacao("Logo", "Logo é um campo obrigatório");
        }
    }
}