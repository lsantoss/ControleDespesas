using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
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
                Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");
                double valor = command.Valor;
                DateTime dataPagamento = command.DataPagamento;
                DateTime dataVencimento = command.DataVencimento;

                Pagamento pagamento = new Pagamento(0, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);
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
                Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");
                double valor = command.Valor;
                DateTime dataPagamento = command.DataPagamento;
                DateTime dataVencimento = command.DataVencimento;

                Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);
                return pagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static object GerarDadosRetornoCommandResult(Pagamento pagamento)
        {
            try
            {
                return new
                {
                    Id = pagamento.Id,
                    IdTipoPagamento = pagamento.TipoPagamento.Id,
                    IdEmpresa = pagamento.Empresa.Id,
                    IdPessoa = pagamento.Pessoa.Id,
                    Descricao = pagamento.Descricao.ToString(),
                    Valor = pagamento.Valor,
                    DataPagamento = pagamento.DataPagamento,
                    DataVencimento = pagamento.DataVencimento
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}