using ControleDespesas.Dominio.Enums;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Commands.Usuario.Input
{
    public class AtualizarUsuarioCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

                AddNotificacao(new ContratoValidacao().TamanhoMinimo(Login, 1, "Login", "Login é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Login, 50, "Login", "Login maior que o esperado"));

                AddNotificacao(new ContratoValidacao().TamanhoMinimo(Senha, 1, "Senha", "Senha é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Senha, 50, "Senha", "Senha maior que o esperado"));

                AddNotificacao(new ContratoValidacao().EhMaior((int)Privilegio, 0, "Privilegio", "Privilégio é um campo obrigatório"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}