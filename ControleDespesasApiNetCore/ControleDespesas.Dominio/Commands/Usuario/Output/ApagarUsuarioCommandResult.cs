using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Commands.Usuario.Output
{
    public class ApagarUsuarioCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public ApagarUsuarioCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}