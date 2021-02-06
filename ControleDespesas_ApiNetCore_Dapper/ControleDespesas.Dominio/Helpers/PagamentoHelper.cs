﻿using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class PagamentoHelper
    {
        public static Pagamento GerarEntidade(AdicionarPagamentoCommand command)
        {
            try
            {
                TipoPagamento tipoPagamento = new TipoPagamento(command.IdTipoPagamento);
                Empresa empresa = new Empresa(command.IdEmpresa);
                Pessoa pessoa = new Pessoa(command.IdPessoa);
                string descricao = command.Descricao;
                double valor = command.Valor;
                DateTime dataVencimento = command.DataVencimento;
                DateTime? dataPagamento = command.DataPagamento;
                string arquivoPagamento = command.ArquivoPagamento;
                string arquivoComprovante = command.ArquivoComprovante;

                Pagamento pagamento = new Pagamento(0, tipoPagamento, empresa, pessoa, descricao, valor, dataVencimento, dataPagamento, arquivoPagamento, arquivoComprovante);
                pagamento.Validar();
                return pagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Pagamento GerarEntidade(AtualizarPagamentoCommand command)
        {
            try
            {
                int id = command.Id;
                TipoPagamento tipoPagamento = new TipoPagamento(command.IdTipoPagamento);
                Empresa empresa = new Empresa(command.IdEmpresa);
                Pessoa pessoa = new Pessoa(command.IdPessoa);
                string descricao = command.Descricao;
                double valor = command.Valor;
                DateTime dataVencimento = command.DataVencimento;
                DateTime? dataPagamento = command.DataPagamento;
                string arquivoPagamento = command.ArquivoPagamento;
                string arquivoComprovante = command.ArquivoComprovante;

                Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataVencimento, dataPagamento, arquivoPagamento, arquivoComprovante);
                pagamento.Validar();
                return pagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AdicionarPagamentoCommandOutput GerarDadosRetornoInsert(Pagamento pagamento)
        {
            try
            {
                return new AdicionarPagamentoCommandOutput
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AtualizarPagamentoCommandOutput GerarDadosRetornoUpdate(Pagamento pagamento)
        {
            try
            {
                return new AtualizarPagamentoCommandOutput
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static ApagarPagamentoCommandOutput GerarDadosRetornoDelete(int id)
        {
            try
            {
                return new ApagarPagamentoCommandOutput { Id = id };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}