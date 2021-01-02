using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Entidades
{
    public class Empresa : Notificadora
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }

        public Empresa(int id, string nome, string logo)
        {
            Id = id;
            Nome = nome;
            Logo = logo;

            Validar();
        }

        public Empresa(int id) => Id = id;

        public void Validar()
        {
            AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Nome, "Nome", "Nome é um campo obrigatório"));
            AddNotificacao(new ContratoValidacao().TamanhoMaximo(Nome, 100, "Nome", "Nome maior que o esperado"));

            AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Logo, "Logo", "Logo é um campo obrigatório"));
        }
    }
}