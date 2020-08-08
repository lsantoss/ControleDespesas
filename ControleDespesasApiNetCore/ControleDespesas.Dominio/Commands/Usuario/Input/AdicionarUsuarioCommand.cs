using ControleDespesas.Dominio.Enums;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.Text.RegularExpressions;

namespace ControleDespesas.Dominio.Commands.Usuario.Input
{
    public class AdicionarUsuarioCommand : Notificadora, CommandPadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Login, "Login", "Login é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Login, 50, "Login", "Login maior que o esperado"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Senha, "Senha", "Senha é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Senha, 15, "Senha", "Senha maior que o esperado"));
                if (!Regex.IsMatch(Senha, @"[A-Z]+")) AddNotificacao("Senha", "Senha deve conter no mínimo 1 letra maíuscula");
                if (!Regex.IsMatch(Senha, @"[a-z]+")) AddNotificacao("SenhaMedia", "Senha deve conter no mínimo 1 letra minúscula");
                if (!Regex.IsMatch(Senha, @"[0-9]+")) AddNotificacao("SenhaMedia", "Senha deve conter no mínimo 1 número");

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