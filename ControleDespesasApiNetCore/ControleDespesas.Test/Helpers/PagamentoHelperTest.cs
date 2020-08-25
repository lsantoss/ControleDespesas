using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;
using System;

namespace ControleDespesas.Test.Helpers
{
    public class PagamentoHelperTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
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

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(1, entidade.TipoPagamento.Id);
            Assert.AreEqual(2, entidade.Empresa.Id);
            Assert.AreEqual(3, entidade.Pessoa.Id);
            Assert.AreEqual("Descricao", entidade.Descricao.ToString());
            Assert.AreEqual(100, entidade.Valor);
            Assert.AreEqual(dataVencimento, entidade.DataVencimento);
            Assert.AreEqual(dataPagamento, entidade.DataPagamento);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Descricao.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Descricao.Notificacoes.Count);
        }

        [Test]
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

            Assert.AreEqual(15, entidade.Id);
            Assert.AreEqual(1, entidade.TipoPagamento.Id);
            Assert.AreEqual(2, entidade.Empresa.Id);
            Assert.AreEqual(3, entidade.Pessoa.Id);
            Assert.AreEqual("Descricao", entidade.Descricao.ToString());
            Assert.AreEqual(100, entidade.Valor);
            Assert.AreEqual(dataVencimento, entidade.DataVencimento);
            Assert.AreEqual(dataPagamento, entidade.DataPagamento);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Descricao.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Descricao.Notificacoes.Count);
        }

        [Test]
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

            Assert.AreEqual(15, command.Id);
            Assert.AreEqual(1, command.IdTipoPagamento);
            Assert.AreEqual(2, command.IdEmpresa);
            Assert.AreEqual(3, command.IdPessoa);
            Assert.AreEqual("Descricao", command.Descricao);
            Assert.AreEqual(100, command.Valor);
            Assert.AreEqual(dataVencimento, command.DataVencimento);
            Assert.AreEqual(dataPagamento, command.DataPagamento);
        }

        [Test]
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

            Assert.AreEqual(15, command.Id);
            Assert.AreEqual(1, command.IdTipoPagamento);
            Assert.AreEqual(2, command.IdEmpresa);
            Assert.AreEqual(3, command.IdPessoa);
            Assert.AreEqual("Descricao", command.Descricao);
            Assert.AreEqual(100, command.Valor);
            Assert.AreEqual(dataVencimento, command.DataVencimento);
            Assert.AreEqual(dataPagamento, command.DataPagamento);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var command = PagamentoHelper.GerarDadosRetornoDelete(1);
            Assert.AreEqual(1, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}