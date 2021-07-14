using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Usuarios.Enums;
using LSCode.Validador.ValidacoesBooleanas;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDespesas.Domain.Usuarios.Entities
{
    public class Usuario : Notificadora
    {
        public long Id { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public EPrivilegioUsuario Privilegio { get; private set; }
        public IReadOnlyCollection<Pessoa> Pessoas => _pessoas.ToList();

        private readonly IList<Pessoa> _pessoas = new List<Pessoa>();

        public Usuario(long id)
        {
            DefinirId(id);
        }

        public Usuario(string login, string senha, EPrivilegioUsuario privilegio)
        {
            DefinirLogin(login);
            DefinirSenha(senha);
            DefinirPrivilegio(privilegio);
        }

        public Usuario(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            DefinirId(id);
            DefinirLogin(login);
            DefinirSenha(senha);
            DefinirPrivilegio(privilegio);
        }

        public void DefinirId(long id)
        {
            Id = id;

            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");
        }

        public void DefinirLogin(string login)
        {
            Login = login;

            if (string.IsNullOrWhiteSpace(Login))
                AddNotificacao("Login", "Login é um campo obrigatório");
            else if (Login.Length > 50)
                AddNotificacao("Login", "Login maior que o esperado");
        }

        public void DefinirSenha(string senha)
        {
            Senha = senha;

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
        }

        public void DefinirPrivilegio(EPrivilegioUsuario privilegio)
        {
            Privilegio = privilegio;

            if (!Enum.IsDefined(typeof(EPrivilegioUsuario), privilegio))
                AddNotificacao("Privilegio", "Privilegio não é válido");
        }

        public void AdicionarPessoa(Pessoa pessoa)
        {
            _pessoas.Add(pessoa);
        }

        public void AdicionarPessoas(List<Pessoa> pessoas)
        {
            foreach(var pessoa in pessoas)
                _pessoas.Add(pessoa);
        }

        public void RemoverPessoas()
        {
            _pessoas.Clear();
        }
    }
}