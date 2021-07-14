using ControleDespesas.Domain.Usuarios.Enums;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesBooleanas;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Usuarios.Commands.Input
{
    public class AdicionarUsuarioCommand : Notificadora, CommandPadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Login))
                AddNotificacao("Login", "Login é um campo obrigatório");
            else if (Login.Length > 50)
                AddNotificacao("Login", "Login maior que o esperado");

            if (string.IsNullOrWhiteSpace(Senha))
                AddNotificacao("Senha", "Senha é um campo obrigatório");
            else if (Senha.Length < 6)
                AddNotificacao("Senha", "Senha deve conter no mínimo 6 caracteres");
            else if (Senha.Length > 15)
                AddNotificacao("Senha", "Senha deve conter no máximo 15 caracteres");
            else if (!ValidacaoBooleana.ContemLetraMaiuscula(Senha))
                AddNotificacao("Senha", "Senha deve conter no mínimo 1 letra maíuscula");
            else if (!ValidacaoBooleana.ContemLetraMinuscula(Senha))
                AddNotificacao("SenhaMedia", "Senha deve conter no mínimo 1 letra minúscula");
            else if (!ValidacaoBooleana.ContemNumero(Senha))
                AddNotificacao("SenhaMedia", "Senha deve conter no mínimo 1 número");

            if (!Enum.IsDefined(typeof(EPrivilegioUsuario), Privilegio))
                AddNotificacao("Privilegio", "Privilegio não é válido");

            return Valido;
        }
    }
}