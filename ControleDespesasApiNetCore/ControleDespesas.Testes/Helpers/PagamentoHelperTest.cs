using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ControleDespesas.Testes.Helpers
{
    public class PagamentoHelperTest
    {
        public PagamentoHelperTest() { }

        [Fact]
        public void GerarEntidade_AdcionarPagamentoCommand()
        {
            var dataVencimento = DateTime.Now.AddDays(1);
            var dataPagamento = DateTime.Now;

            var command = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = 1,
                IdEmpresa = 2,
                IdPessoa = 3,
                Descricao = "Descricao",
                Valor = 100,
                DataVencimento = dataVencimento,
                DataPagamento = dataPagamento
            };

            var entidade = PagamentoHelper.GerarEntidade(command);

            Assert.Equal(0, entidade.Id);
            Assert.Equal(1, entidade.TipoPagamento.Id);
            Assert.Equal(2, entidade.Empresa.Id);
            Assert.Equal(3, entidade.Pessoa.Id);
            Assert.Equal("Descricao", entidade.Descricao.ToString());
            Assert.Equal(100, entidade.Valor);
            Assert.Equal(dataVencimento, entidade.DataVencimento);
            Assert.Equal(dataPagamento, entidade.DataPagamento);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Descricao.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Descricao.Notificacoes.Count);
        }

        [Fact]
        public void GerarEntidade_AtualizarPagamentoCommand()
        {
            var dataVencimento = DateTime.Now.AddDays(1);
            var dataPagamento = DateTime.Now;

            var command = new AtualizarPagamentoCommand()
            {
                Id = 15,
                IdTipoPagamento = 1,
                IdEmpresa = 2,
                IdPessoa = 3,
                Descricao = "Descricao",
                Valor = 100,
                DataVencimento = dataVencimento,
                DataPagamento = dataPagamento
            };

            var entidade = PagamentoHelper.GerarEntidade(command);

            Assert.Equal(15, entidade.Id);
            Assert.Equal(1, entidade.TipoPagamento.Id);
            Assert.Equal(2, entidade.Empresa.Id);
            Assert.Equal(3, entidade.Pessoa.Id);
            Assert.Equal("Descricao", entidade.Descricao.ToString());
            Assert.Equal(100, entidade.Valor);
            Assert.Equal(dataVencimento, entidade.DataVencimento);
            Assert.Equal(dataPagamento, entidade.DataPagamento);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Descricao.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Descricao.Notificacoes.Count);
        }

        [Fact]
        public void GerarDadosRetornoInsert()
        {
            var dataVencimento = DateTime.Now.AddDays(1);
            var dataPagamento = DateTime.Now;

            var entidade = new Pagamento(
                15,
                new TipoPagamento(1),
                new Empresa(2),
                new Pessoa(3),
                new Texto("Descricao", "Descricao", 250),
                100,
                dataVencimento,
                dataPagamento
            );

            var command = PagamentoHelper.GerarDadosRetornoInsert(entidade);

            Assert.Equal(15, command.Id);
            Assert.Equal(1, command.IdTipoPagamento);
            Assert.Equal(2, command.IdEmpresa);
            Assert.Equal(3, command.IdPessoa);
            Assert.Equal("Descricao", command.Descricao);
            Assert.Equal(100, command.Valor);
            Assert.Equal(dataVencimento, command.DataVencimento);
            Assert.Equal(dataPagamento, command.DataPagamento);
        }

        [Fact]
        public void GerarDadosRetornoUpdate()
        {
            var dataVencimento = DateTime.Now.AddDays(1);
            var dataPagamento = DateTime.Now;

            var entidade = new Pagamento(
                15,
                new TipoPagamento(1),
                new Empresa(2),
                new Pessoa(3),
                new Texto("Descricao", "Descricao", 250),
                100,
                dataVencimento,
                dataPagamento
            );

            var command = PagamentoHelper.GerarDadosRetornoUpdate(entidade);

            Assert.Equal(15, command.Id);
            Assert.Equal(1, command.IdTipoPagamento);
            Assert.Equal(2, command.IdEmpresa);
            Assert.Equal(3, command.IdPessoa);
            Assert.Equal("Descricao", command.Descricao);
            Assert.Equal(100, command.Valor);
            Assert.Equal(dataVencimento, command.DataVencimento);
            Assert.Equal(dataPagamento, command.DataPagamento);
        }

        [Fact]
        public void GerarDadosRetornoDelte()
        {
            var command = PagamentoHelper.GerarDadosRetornoDelete(1);
            Assert.Equal(1, command.Id);
        }
    }
}