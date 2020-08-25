using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Testes.Entidades
{
    public class TipoPagamentoTest
    {
        private TipoPagamento _tipoPagamento;

        [SetUp]
        public void Setup()
        {
            int id = 1;
            Texto descricao = new Texto("Luz Elétrica", "Descrição", 250);
            _tipoPagamento = new TipoPagamento(id, descricao);
        }

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_tipoPagamento.Valido);
            Assert.True(_tipoPagamento.Descricao.Valido);
            Assert.AreEqual(0, _tipoPagamento.Notificacoes.Count);
            Assert.AreEqual(0, _tipoPagamento.Descricao.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_DescricaoInvalida()
        {
            _tipoPagamento.Descricao = new Texto(@"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                                                   aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Descrição", 250);

            Assert.False(_tipoPagamento.Descricao.Valido);
            Assert.AreNotEqual(0, _tipoPagamento.Descricao.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _tipoPagamento = null;
        }
    }
}