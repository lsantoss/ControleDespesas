using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Enums;
using ControleDespesas.Domain.Query.Pagamento.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Interfaces.Repositories
{
    public interface IPagamentoRepository
    {
        int Salvar(Pagamento pagamento);
        void Atualizar(Pagamento pagamento);
        void Deletar(int id);

        PagamentoQueryResult Obter(int id);
        List<PagamentoQueryResult> Listar(int idPessoa, EPagamentoStatus? status);

        PagamentoArquivoQueryResult ObterArquivoPagamento(int idPagamento);
        PagamentoArquivoQueryResult ObterArquivoComprovante(int idPagamento);

        PagamentoGastosQueryResult ObterGastos(int idPessoa, int? ano, int? mes);

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}