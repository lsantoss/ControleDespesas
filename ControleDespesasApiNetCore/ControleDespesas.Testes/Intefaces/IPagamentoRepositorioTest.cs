using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using LSCode.Validador.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Intefaces
{
    public class IPagamentoRepositorioTest
    {
        [Fact]
        public void AdicionarPagamento_DeveRetornarSucesso()
        {
            int id = 0;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Descricao250Caracteres descricao = new Descricao250Caracteres("Pagamento do mês de Maio de Luz Elétrica", "Descrição");
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.Salvar(pagamento)).Returns("Sucesso");
            string resultado = mock.Object.Salvar(pagamento);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void AtualizarPagamento_DeveRetornarSucesso()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Descricao250Caracteres descricao = new Descricao250Caracteres("Pagamento do mês de Maio de Luz Elétrica", "Descrição");
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.Atualizar(pagamento)).Returns("Sucesso");
            string resultado = mock.Object.Atualizar(pagamento);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void ApagarPagamento_DeveRetornarSucesso()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Descricao250Caracteres descricao = new Descricao250Caracteres("Pagamento do mês de Maio de Luz Elétrica", "Descrição");
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.Deletar(pagamento.Id)).Returns("Sucesso");
            string resultado = mock.Object.Deletar(pagamento.Id);
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public void ObterPagamento_DeveRetornarSucesso()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Descricao250Caracteres descricao = new Descricao250Caracteres("Pagamento do mês de Maio de Luz Elétrica", "Descrição");
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            PagamentoQueryResult pagamentoQueryResult = new PagamentoQueryResult
            {
                Id = 1,
                Descricao = "Pagamento do mês de Maio de Luz Elétrica",
                Valor = 89.75,
                DataPagamento = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(1),
                TipoPagamento = new TipoPagamentoQueryResult
                {
                    Id = 1,
                    Descricao = "Luz Elétrica"
                },
                Empresa = new EmpresaQueryResult
                {
                    Id = 15,
                    Nome = "Cemig",
                    Logo = "base64String"
                },
                Pessoa = new PessoaQueryResult
                {
                    Id = 1,
                    Nome = "Lucas",
                    ImagemPerfil = "base64String"
                }
            };

            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.ObterPagamento(pagamento.Id)).Returns(pagamentoQueryResult);
            PagamentoQueryResult resultado = mock.Object.ObterPagamento(pagamento.Id);
            Assert.Equal(pagamentoQueryResult, resultado);
        }

        [Fact]
        public void ListarPagamentos_DeveRetornarSucesso()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Descricao250Caracteres descricao = new Descricao250Caracteres("Pagamento do mês de Maio de Luz Elétrica", "Descrição");
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            List<PagamentoQueryResult> listaPagamentoQueryResult = new List<PagamentoQueryResult>();
            listaPagamentoQueryResult.Add(new PagamentoQueryResult
            {
                Id = 1,
                Descricao = "Pagamento do mês de Maio de Luz Elétrica",
                Valor = 89.75,
                DataPagamento = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(1),
                TipoPagamento = new TipoPagamentoQueryResult
                {
                    Id = 1,
                    Descricao = "Luz Elétrica"
                },
                Empresa = new EmpresaQueryResult
                {
                    Id = 15,
                    Nome = "Cemig",
                    Logo = "base64String"
                },
                Pessoa = new PessoaQueryResult
                {
                    Id = 1,
                    Nome = "Lucas",
                    ImagemPerfil = "base64String"
                }
            });
            listaPagamentoQueryResult.Add(new PagamentoQueryResult
            {
                Id = 2,
                Descricao = "Pagamento do mês de Maio de Saneamento",
                Valor = 89.75,
                DataPagamento = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(1),
                TipoPagamento = new TipoPagamentoQueryResult
                {
                    Id = 2,
                    Descricao = "Saneamento"
                },
                Empresa = new EmpresaQueryResult
                {
                    Id = 4,
                    Nome = "Cesama",
                    Logo = "base64String"
                },
                Pessoa = new PessoaQueryResult
                {
                    Id = 1,
                    Nome = "Lucas",
                    ImagemPerfil = "base64String"
                }
            });

            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.ListarPagamentos()).Returns(listaPagamentoQueryResult);
            List<PagamentoQueryResult> resultado = mock.Object.ListarPagamentos();
            Assert.Equal(listaPagamentoQueryResult, resultado);
        }

        [Fact]
        public void CheckId_DeveRetornarSucesso()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Descricao250Caracteres descricao = new Descricao250Caracteres("Pagamento do mês de Maio de Luz Elétrica", "Descrição");
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.CheckId(pagamento.Id)).Returns(true);
            bool resultado = mock.Object.CheckId(pagamento.Id);
            Assert.True(resultado);
        }

        [Fact]
        public void LocalizarMaxId_DeveRetornarSucesso()
        {
            Mock<IPagamentoRepositorio> mock = new Mock<IPagamentoRepositorio>();
            mock.Setup(m => m.LocalizarMaxId()).Returns(10);
            int resultado = mock.Object.LocalizarMaxId();
            Assert.Equal(10, resultado);
        }
    }
}