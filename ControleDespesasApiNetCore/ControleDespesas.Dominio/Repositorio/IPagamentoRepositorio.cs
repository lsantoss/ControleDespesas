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

        List<PagamentoQueryResult> ListarPagamentoConcluido(int idPessoa);
        List<PagamentoQueryResult> ListarPagamentoPendente(int idPessoa);

        PagamentoCalculoEstatisticaQueryResult CalcularValorGastoTotal(int idPessoa);
        PagamentoCalculoEstatisticaQueryResult CalcularValorGastoAno(int idPessoa, int ano);
        PagamentoCalculoEstatisticaQueryResult CalcularValorGastoAnoMes(int idPessoa, int ano, int mes);
        PagamentoCalculoEstatisticaQueryResult CalcularValorGastoMedioAno(int idPessoa, int ano);
        PagamentoCalculoEstatisticaQueryResult CalcularValorGastoMedioAnoMes(int idPessoa, int ano, int mes);

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}