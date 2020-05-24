using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Command.Pessoa.Output
{
    public class AdicionarPessoaCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AdicionarPessoaCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}