using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Empresas.Entities
{
    public class Empresa : Notificadora
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public string Logo { get; private set; }

        public Empresa(long id)
        {
            DefinirId(id);
        }

        public Empresa(string nome, string logo)
        {
            DefinirNome(nome);
            DefinirLogo(logo);
        }

        public Empresa(long id, string nome, string logo)
        {
            DefinirId(id);
            DefinirNome(nome);
            DefinirLogo(logo);
        }

        public void DefinirId(long id)
        {
            Id = id;

            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");
        }

        public void DefinirNome(string nome)
        {
            Nome = nome;

            if (string.IsNullOrWhiteSpace(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");
        }

        public void DefinirLogo(string logo)
        {
            Logo = logo;

            if (string.IsNullOrWhiteSpace(Logo))
                AddNotificacao("Logo", "Logo é um campo obrigatório");
        }
    }
}