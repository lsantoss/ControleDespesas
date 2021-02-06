using ControleDespesas.Dominio.Entities;
using ControleDespesas.Dominio.Query.TipoPagamento;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces.Repositorio
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