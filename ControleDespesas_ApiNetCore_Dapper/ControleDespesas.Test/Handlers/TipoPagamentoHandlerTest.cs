using ControleDespesas.Domain.Commands.TipoPagamento.Output;
using ControleDespesas.Domain.Handlers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    public class TipoPagamentoHandlerTest : DatabaseTest
    {
        private readonly ITipoPagamentoRepository _repository;
        private readonly ITipoPagamentoHandler _handler;

        public TipoPagamentoHandlerTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new TipoPagamentoRepository(MockSettingsInfraData);
            _handler = new TipoPagamentoHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarTipoPagamento()
        {
            var tipoPagamentoCommand = new SettingsTest().TipoPagamentoAdicionarCommand;

            var retorno = _handler.Handler(tipoPagamentoCommand);

            var retornoDados = (TipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_AtualizarTipoPagamento()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;

            var tipoPagamentoCommand = new SettingsTest().TipoPagamentoAtualizarCommand;

            _repository.Salvar(tipoPagamento);

            var retorno = _handler.Handler(tipoPagamentoCommand.Id, tipoPagamentoCommand);

            var retornoDados = (TipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(tipoPagamentoCommand.Id, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_ApagarTipoPagamento()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;

            _repository.Salvar(tipoPagamento);

            var retorno = _handler.Handler(tipoPagamento.Id);

            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(tipoPagamento.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}