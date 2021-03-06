﻿using LSCode.Facilitador.Api.Interfaces.Query;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Pessoas.Query.Parameters
{
    public class ObterPessoasQuery : Notificadora, QueryPadrao
    {
        public int IdUsuario { get; set; }

        public bool ValidarQuery()
        {
            if (IdUsuario <= 0)
                AddNotificacao("IdUsuario", "IdUsuario não é valido");

            return Valido;
        }
    }
}