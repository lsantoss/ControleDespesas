using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Helpers
{
    public class TipoPagamentoHelperTest
    {
        public TipoPagamentoHelperTest() { }

        [Fact]
        public void GerarEntidade_AdcionarTipoPagamentoCommand()
        {
            var command = new AdicionarTipoPagamentoCommand()
            {
                Descricao = "TipoDePagamento"
            };

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            Assert.Equal(0, entidade.Id);
            Assert.Equal("TipoDePagamento", entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
        }

        [Fact]
        public void GerarEntidade_AtualizarTipoPagamentoCommand()
        {
            var command = new AtualizarTipoPagamentoCommand()
            {
                Id = 1,
                Descricao = "TipoDePagamento"
            };

            var entidade = TipoPagamentoHelper.GerarEntidade(command);

            Assert.Equal(1, entidade.Id);
            Assert.Equal("TipoDePagamento", entidade.Descricao.ToString());
            Assert.True(entidade.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
        }

        [Fact]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new TipoPagamento(1, new Texto("TipoDePagamento", "Descricao", 250));

            var command = TipoPagamentoHelper.GerarDadosRetornoInsert(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("TipoDePagamento", command.Descricao);
        }

        [Fact]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new TipoPagamento(1, new Texto("TipoDePagamento", "Descricao", 250));

            var command = TipoPagamentoHelper.GerarDadosRetornoUpdate(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("TipoDePagamento", command.Descricao);
        }

        [Fact]
        public void GerarDadosRetornoDelte()
        {
            var command = TipoPagamentoHelper.GerarDadosRetornoDelete(1);
            Assert.Equal(1, command.Id);
        }
    }
}