namespace ControleDespesas.Domain.Pessoas.Commands.Output
{
    public class PessoaCommandOutput
    {
        public int Id { get; set; }
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }
    }
}