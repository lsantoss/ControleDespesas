using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pagamento;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Repositorio
{
    public interface IPagamentoRepositorio
    {
        void Salvar(Pagamento pagamento);
        void Atualizar(Pagamento pagamento);
        void Deletar(int id);

        PagamentoQueryResult Obter(int id);
        List<PagamentoQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}