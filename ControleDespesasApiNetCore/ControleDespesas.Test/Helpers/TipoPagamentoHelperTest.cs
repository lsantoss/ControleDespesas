using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class TipoPagamentoHelperTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarTipoPagamentoCommand()
        {
            var command = new AdicionarTipoPagamentoCommand()
            {
                Descricao = "TipoDePagamento"
            };

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual("TipoDePagamento", entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarTipoPagamentoCommand()
        {
            var command = new AtualizarTipoPagamentoCommand()
            {
                Id = 1,
                Descricao = "TipoDePagamento"
            };

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            Assert.AreEqual(1, entidade.Id);
            Assert.AreEqual("TipoDePagamento", entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new TipoPagamento(1, new Texto("TipoDePagamento", "Descricao", 250));

            var command = TipoPagamentoHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("TipoDePagamento", command.Descricao);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new TipoPagamento(1, new Texto("TipoDePagamento", "Descricao", 250));

            var command = TipoPagamentoHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("TipoDePagamento", command.Descricao);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var command = TipoPagamentoHelper.GerarDadosRetornoDelete(1);
            Assert.AreEqual(1, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}