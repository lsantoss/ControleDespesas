using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;
using System;

namespace ControleDespesas.Testes.Entidades
{
    public class PagamentoTest
    {
        private Pagamento _pagamento;

        [SetUp]
        public void Setup()
        {
            int id = 1;
            TipoPagamento tipoPagamento = new TipoPagamento(33);
            Empresa empresa = new Empresa(125);
            Pessoa pessoa = new Pessoa(45);
            Texto descricao = new Texto("Pagamento do mês de Maio de Luz Elétrica", "Descrição", 250);
            double valor = 89.75;
            DateTime dataPagamento = DateTime.Now;
            DateTime dataVencimento = DateTime.Now.AddDays(1);
            _pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);
        }

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_pagamento.Valido);
            Assert.True(_pagamento.Descricao.Valido);
            Assert.AreEqual(0, _pagamento.Notificacoes.Count);
            Assert.AreEqual(0, _pagamento.Descricao.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_DescricaoInvalida()
        {
            _pagamento.Descricao = new Texto(@"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                               aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                               aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Descrição", 250);

            Assert.False(_pagamento.Descricao.Valido);
            Assert.AreNotEqual(0, _pagamento.Descricao.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _pagamento = null;
        }
    }
}