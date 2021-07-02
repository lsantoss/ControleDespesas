namespace ControleDespesas.Domain.Empresas.Commands.Output
{
    public class EmpresaCommandOutput
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public string Logo { get; private set; }

        public EmpresaCommandOutput(long id, string nome, string logo)
        {
            Id = id;
            Nome = nome;
            Logo = logo;
        }
    }
}