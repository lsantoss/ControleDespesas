using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface ITipoPagamentoRepositorio
    {
        string Salvar(TipoPagamento tipoPagamento);
        string Atualizar(TipoPagamento tipoPagamento);
        string Deletar(int id);

        TipoPagamentoQueryResult ObterTipoPagamento(int id);
        List<TipoPagamentoQueryResult> ListarTipoPagamentos();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}