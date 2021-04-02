﻿using ControleDespesas.Domain.Commands.Pagamento.Input;
using ControleDespesas.Domain.Commands.Pagamento.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Helpers
{
    public static class PagamentoHelper
    {
        public static Pagamento GerarEntidade(AdicionarPagamentoCommand command)
        {
            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(command.IdTipoPagamento),
                new Empresa(command.IdEmpresa),
                new Pessoa(command.IdPessoa),
                command.Descricao,
                command.Valor,
                command.DataVencimento,
                command.DataPagamento,
                command.ArquivoPagamento,
                command.ArquivoComprovante);

            pagamento.Validar();
            return pagamento;
        }

        public static Pagamento GerarEntidade(AtualizarPagamentoCommand command)
        {
            Pagamento pagamento = new Pagamento(
                command.Id,
                new TipoPagamento(command.IdTipoPagamento),
                new Empresa(command.IdEmpresa),
                new Pessoa(command.IdPessoa),
                command.Descricao,
                command.Valor,
                command.DataVencimento,
                command.DataPagamento,
                command.ArquivoPagamento,
                command.ArquivoComprovante);

            pagamento.Validar();
            return pagamento;
        }

        public static PagamentoCommandOutput GerarDadosRetornoInsert(Pagamento pagamento)
        {
            return new PagamentoCommandOutput
            {
                Id = pagamento.Id,
                IdTipoPagamento = pagamento.TipoPagamento.Id,
                IdEmpresa = pagamento.Empresa.Id,
                IdPessoa = pagamento.Pessoa.Id,
                Descricao = pagamento.Descricao,
                Valor = pagamento.Valor,
                DataVencimento = pagamento.DataVencimento,
                DataPagamento = pagamento.DataPagamento,
                ArquivoPagamento = pagamento.ArquivoPagamento,
                ArquivoComprovante = pagamento.ArquivoComprovante
            };
        }

        public static PagamentoCommandOutput GerarDadosRetornoUpdate(Pagamento pagamento)
        {
            return new PagamentoCommandOutput
            {
                Id = pagamento.Id,
                IdTipoPagamento = pagamento.TipoPagamento.Id,
                IdEmpresa = pagamento.Empresa.Id,
                IdPessoa = pagamento.Pessoa.Id,
                Descricao = pagamento.Descricao,
                Valor = pagamento.Valor,
                DataVencimento = pagamento.DataVencimento,
                DataPagamento = pagamento.DataPagamento,
                ArquivoPagamento = pagamento.ArquivoPagamento,
                ArquivoComprovante = pagamento.ArquivoComprovante
            };
        }

        public static CommandOutput GerarDadosRetornoDelete(int id)
        {
            return new CommandOutput { Id = id };
        }
    }
}