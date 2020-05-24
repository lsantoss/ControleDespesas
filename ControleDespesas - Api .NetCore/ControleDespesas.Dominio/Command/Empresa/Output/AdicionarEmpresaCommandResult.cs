using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Command.Empresa.Output
{
    public class AdicionarEmpresaCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AdicionarEmpresaCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}