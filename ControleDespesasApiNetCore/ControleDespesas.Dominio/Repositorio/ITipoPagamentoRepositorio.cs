using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.TipoPagamento;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Repositorio
{
    public interface ITipoPagamentoRepositorio
    {
        void Salvar(TipoPagamento tipoPagamento);
        void Atualizar(TipoPagamento tipoPagamento);
        void Deletar(int id);

        TipoPagamentoQueryResult Obter(int id);
        List<TipoPagamentoQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}