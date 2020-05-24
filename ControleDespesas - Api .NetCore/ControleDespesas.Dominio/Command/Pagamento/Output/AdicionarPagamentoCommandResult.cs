using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Command.Pagamento.Output
{
    public class AdicionarPagamentoCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AdicionarPagamentoCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}