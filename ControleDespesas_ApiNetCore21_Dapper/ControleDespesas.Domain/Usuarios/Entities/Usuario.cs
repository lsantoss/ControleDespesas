using ControleDespesas.Domain.Usuarios.Enums;
using LSCode.Validador.ValidacoesBooleanas;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Usuarios.Entities
{
    public class Usuario : Notificadora
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public EPrivilegioUsuario Privilegio { get; private set; }

        public Usuario(int id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            Id = id;
            Login = login;
            Senha = senha;
            Privilegio = privilegio;
        }

        public Usuario(int id)
        {
            Id = id;
        }

        public void DefinirId(int id)
        {
            Id = id;
        }

        public void DefinirLogin(string login)
        {
            Login = login;
        }

        public void DefinirSenha(string senha)
        {
            Senha = senha;
        }

        public void DefinirPrivilegio(EPrivilegioUsuario privilegio)
        {
            Privilegio = privilegio;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Login))
                AddNotificacao("Login", "Login é um campo obrigatório");
            else if (Login.Length > 50)
                AddNotificacao("Login", "Login maior que o esperado");

            if (string.IsNullOrEmpty(Senha))
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

            if ((int)Privilegio <= 0)
                AddNotificacao("Privilegio", "Privilegio é um campo obrigatório");
        }
    }
}