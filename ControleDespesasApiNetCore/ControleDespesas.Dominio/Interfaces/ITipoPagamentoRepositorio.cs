using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.TipoPagamento;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface ITipoPagamentoRepositorio
    {
        string Salvar(TipoPagamento tipoPagamento);
        string Atualizar(TipoPagamento tipoPagamento);
        string Deletar(int id);

        TipoPagamentoQueryResult Obter(int id);
        List<TipoPagamentoQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}