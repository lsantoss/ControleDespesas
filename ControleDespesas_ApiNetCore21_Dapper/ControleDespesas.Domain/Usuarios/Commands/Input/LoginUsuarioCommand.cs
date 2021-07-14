using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Usuarios.Commands.Input
{
    public class LoginUsuarioCommand : Notificadora, CommandPadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Login))
                AddNotificacao("Login", "Login é um campo obrigatório");
            else if (Login.Length > 50)
                AddNotificacao("Login", "Login maior que o esperado");

            if (string.IsNullOrWhiteSpace(Senha))
                AddNotificacao("Senha", "Senha é um campo obrigatório");

            return Valido;
        }
    }
}