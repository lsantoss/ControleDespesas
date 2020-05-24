using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Command.TipoPagamento.Output
{
    public class AdicionarTipoPagamentoCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AdicionarTipoPagamentoCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}