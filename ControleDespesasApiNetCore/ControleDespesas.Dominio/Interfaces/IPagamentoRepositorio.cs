using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pagamento;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface IPagamentoRepositorio
    {
        string Salvar(Pagamento pagamento);
        string Atualizar(Pagamento pagamento);
        string Deletar(int id);

        PagamentoQueryResult ObterPagamento(int id);
        List<PagamentoQueryResult> ListarPagamentos();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}