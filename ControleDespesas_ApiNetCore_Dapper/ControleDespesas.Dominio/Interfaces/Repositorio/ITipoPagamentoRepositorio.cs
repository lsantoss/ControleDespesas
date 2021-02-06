﻿using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Query.TipoPagamento;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Interfaces.Repositorio
{
    public interface ITipoPagamentoRepositorio
    {
        TipoPagamento Salvar(TipoPagamento tipoPagamento);
        void Atualizar(TipoPagamento tipoPagamento);
        void Deletar(int id);

        TipoPagamentoQueryResult Obter(int id);
        List<TipoPagamentoQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}