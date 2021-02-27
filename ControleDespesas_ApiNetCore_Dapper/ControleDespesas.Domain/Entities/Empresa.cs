using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Entities
{
    public class Empresa : Notificadora
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }

        public Empresa(int id, string nome, string logo)
        {
            Id = id;
            Nome = nome;
            Logo = logo;
        }

        public Empresa(int id)
        {
            Id = id;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");

            if (string.IsNullOrEmpty(Logo))
                AddNotificacao("Logo", "Logo é um campo obrigatório");
        }
    }
}