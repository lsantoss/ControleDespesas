using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Command.Pagamento.Output
{
    public class ApagarPagamentoCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public ApagarPagamentoCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}