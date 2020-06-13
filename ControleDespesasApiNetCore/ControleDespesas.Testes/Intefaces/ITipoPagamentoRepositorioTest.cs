using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.TipoPagamento;
using LSCode.Validador.ValueObjects;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Intefaces
{
    public class ITipoPagamentoRepositorioTest
    {
        [Fact]
        public void AdicionarTipoPagamento_DeveRetornarSucesso()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(1, new Descricao250Caracteres("Luz Elétrica", "Descrição"));
            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.Salvar(tipoPagamento)).Returns("Sucesso");
            string resultado = mock.Object.Salvar(tipoPagamento);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void AtualizarTipoPagamento_DeveRetornarSucesso()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(1, new Descricao250Caracteres("Luz Elétrica", "Descrição"));
            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.Atualizar(tipoPagamento)).Returns("Sucesso");
            string resultado = mock.Object.Atualizar(tipoPagamento);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void ApagarTipoPagamento_DeveRetornarSucesso()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(1, new Descricao250Caracteres("Luz Elétrica", "Descrição"));
            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.Deletar(tipoPagamento.Id)).Returns("Sucesso");
            string resultado = mock.Object.Deletar(tipoPagamento.Id);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void ObterTipoPagamento_DeveRetornarSucesso()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(1, new Descricao250Caracteres("Luz Elétrica", "Descrição"));

            TipoPagamentoQueryResult tipoPagamentoQueryResult = new TipoPagamentoQueryResult
            {
                Id = 1,
                Descricao = "Luz Elétrica"
            };

            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.ObterTipoPagamento(tipoPagamento.Id)).Returns(tipoPagamentoQueryResult);
            TipoPagamentoQueryResult resultado = mock.Object.ObterTipoPagamento(tipoPagamento.Id);
            Assert.Equal(tipoPagamentoQueryResult, resultado);
        }

        [Fact]
        public void ListarTipoPagamento_DeveRetornarSucesso()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(1, new Descricao250Caracteres("Luz Eletríca", "Descrição"));

            List<TipoPagamentoQueryResult> listaTipoPagamentoQueryResult = new List<TipoPagamentoQueryResult>();
            listaTipoPagamentoQueryResult.Add(new TipoPagamentoQueryResult
            {
                Id = 1,
                Descricao = "Luz Eletríca"
            });
            listaTipoPagamentoQueryResult.Add(new TipoPagamentoQueryResult
            {
                Id = 2,
                Descricao = "Saneamento"
            });

            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.ListarTipoPagamentos()).Returns(listaTipoPagamentoQueryResult);
            List<TipoPagamentoQueryResult> resultado = mock.Object.ListarTipoPagamentos();
            Assert.Equal(listaTipoPagamentoQueryResult, resultado);
        }

        [Fact]
        public void CheckId_DeveRetornarSucesso()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(1, new Descricao250Caracteres("Luz Eletríca", "Descrição"));
            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.CheckId(tipoPagamento.Id)).Returns(true);
            bool resultado = mock.Object.CheckId(tipoPagamento.Id);
            Assert.True(resultado);
        }

        [Fact]
        public void LocalizarMaxId_DeveRetornarSucesso()
        {
            Mock<ITipoPagamentoRepositorio> mock = new Mock<ITipoPagamentoRepositorio>();
            mock.Setup(m => m.LocalizarMaxId()).Returns(10);
            int resultado = mock.Object.LocalizarMaxId();
            Assert.Equal(10, resultado);
        }
    }
}