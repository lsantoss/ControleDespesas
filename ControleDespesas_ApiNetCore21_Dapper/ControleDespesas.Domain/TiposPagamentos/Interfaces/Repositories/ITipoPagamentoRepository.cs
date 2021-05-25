using ControleDespesas.Domain.TiposPagamentos.Entities;
using ControleDespesas.Domain.TiposPagamentos.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories
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