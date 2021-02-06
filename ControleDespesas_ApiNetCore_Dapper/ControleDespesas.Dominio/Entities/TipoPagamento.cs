using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Entities
{
    public class TipoPagamento : Notificadora
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public TipoPagamento(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public TipoPagamento(int id) => Id = id;

        public void Validar()
        {
            try
            {
                if (string.IsNullOrEmpty(Descricao))
                    AddNotificacao("Descricao", "Descricao é um campo obrigatório");
                else if (Descricao.Length > 250)
                    AddNotificacao("Descricao", "Descricao maior que o esperado");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}