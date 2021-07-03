using ControleDespesas.Domain.TiposPagamentos.Entities;
using ControleDespesas.Domain.TiposPagamentos.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories
{
    public interface ITipoPagamentoRepository
    {
        long Salvar(TipoPagamento tipoPagamento);
        void Atualizar(TipoPagamento tipoPagamento);
        void Deletar(long id);

        TipoPagamentoQueryResult Obter(long id);
        List<TipoPagamentoQueryResult> Listar();

        bool CheckId(long id);
        long LocalizarMaxId();
    }
}