using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Commands.Usuario.Output
{
    public class AtualizarUsuarioCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AtualizarUsuarioCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}