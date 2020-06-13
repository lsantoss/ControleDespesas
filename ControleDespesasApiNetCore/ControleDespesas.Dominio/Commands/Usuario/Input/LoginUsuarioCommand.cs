using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Commands.Usuario.Input
{
    public class LoginUsuarioCommand : Notificadora, CommandPadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool ValidarCommand()
        {
            AddNotificacao(new ContratoValidacao().TamanhoMinimo(Login, 1, "Login", "Login é um campo obrigatório"));
            AddNotificacao(new ContratoValidacao().TamanhoMaximo(Login, 50, "Login", "Login maior que o esperado"));

            AddNotificacao(new ContratoValidacao().TamanhoMinimo(Senha, 1, "Senha", "Senha é um campo obrigatório"));
            AddNotificacao(new ContratoValidacao().TamanhoMaximo(Senha, 50, "Senha", "Senha maior que o esperado"));

            return Valido;
        }
    }
}