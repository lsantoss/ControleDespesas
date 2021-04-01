using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Interfaces.Repositories
{
    public interface ITipoPagamentoRepository
    {
        int Salvar(TipoPagamento tipoPagamento);
        void Atualizar(TipoPagamento tipoPagamento);
        void Deletar(int id);

        TipoPagamentoQueryResult Obter(int id);
        List<TipoPagamentoQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}