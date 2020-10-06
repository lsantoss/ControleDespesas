using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    public class TipoPagamentoHandlerTest : DatabaseFactory
    {
        private readonly TipoPagamentoRepositorio _repository;
        private readonly TipoPagamentoHandler _handler;

        public TipoPagamentoHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repository = new TipoPagamentoRepositorio(optionsInfraData);
            _handler = new TipoPagamentoHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarTipoPagamento()
        {
            var tipoPagamentoCommand = MockSettingsTest.TipoPagamentoAdicionarCommand;

            var retorno = _handler.Handler(tipoPagamentoCommand);

            var retornoDados = (AdicionarTipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_AtualizarTipoPagamento()
        {
            var tipoPagamento = MockSettingsTest.TipoPagamento1;

            var empresaCommand = MockSettingsTest.TipoPagamentoAtualizarCommand;

            _repository.Salvar(tipoPagamento);

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (AtualizarTipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_ApagarTipoPagamento()
        {
            var tipoPagamento = MockSettingsTest.TipoPagamento1;

            var empresaCommand = MockSettingsTest.TipoPagamentoApagarCommand;

            _repository.Salvar(tipoPagamento);

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (ApagarTipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}