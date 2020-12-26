﻿using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pagamento;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Repositorio
{
    public interface IPagamentoRepositorio
    {
        Pagamento Salvar(Pagamento pagamento);
        void Atualizar(Pagamento pagamento);
        void Deletar(int id);

        PagamentoQueryResult Obter(int id);
        List<PagamentoQueryResult> Listar(int idPessoa);

        List<PagamentoQueryResult> ListarPagamentoConcluido(int idPessoa);
        List<PagamentoQueryResult> ListarPagamentoPendente(int idPessoa);

        PagamentoArquivoPagamentoQueryResult ObterArquivoPagamento(int idPagamento);
        PagamentoArquivoComprovanteQueryResult ObterArquivoComprovante(int idPagamento);

        PagamentoGastosQueryResult CalcularValorGastoTotal(int idPessoa);
        PagamentoGastosQueryResult CalcularValorGastoAno(int idPessoa, int ano);
        PagamentoGastosQueryResult CalcularValorGastoAnoMes(int idPessoa, int ano, int mes);

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}