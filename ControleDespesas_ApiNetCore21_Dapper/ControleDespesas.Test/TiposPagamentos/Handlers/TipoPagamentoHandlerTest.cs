using ControleDespesas.Domain.TiposPagamentos.Commands.Input;
using ControleDespesas.Domain.TiposPagamentos.Commands.Output;
using ControleDespesas.Domain.TiposPagamentos.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.TiposPagamentos.Handlers
{
    [TestFixture]
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
        public void Handler_AdicionarTipoPagamento_Valido()
        {
            var tipoPagamentoCommand = new SettingsTest().TipoPagamentoAdicionarCommand;

            var retorno = _handler.Handler(tipoPagamentoCommand);
            var retornoDados = (TipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.AreEqual(201, retorno.StatusCode);
            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
            Assert.Null(retorno.Erros);
        }

        [Test]
        public void Handler_AdicionarTipoPagamento_Nulo_Invalido()
        {
            AdicionarTipoPagamentoCommand command = null;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(400, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AdicionarTipoPagamento_Invalido(string descricao)
        {
            var command = new AdicionarTipoPagamentoCommand
            {
                Descricao = descricao
            };

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AdicionarTipoPagamento_Descricao_Invalido(string descricao)
        {
            var command = new AdicionarTipoPagamentoCommand
            {
                Descricao = descricao
            };

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_AtualizarTipoPagamento_Valido()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var tipoPagamentoCommand = new SettingsTest().TipoPagamentoAtualizarCommand;

            var retorno = _handler.Handler(tipoPagamentoCommand.Id, tipoPagamentoCommand);
            var retornoDados = (TipoPagamentoCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.AreEqual(200, retorno.StatusCode);
            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(tipoPagamentoCommand.Id, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
            Assert.Null(retorno.Erros);
        }

        [Test]
        public void Handler_AtualizarTipoPagamento_Nulo_Invalido()
        {
            AtualizarTipoPagamentoCommand command = null;

            var retorno = _handler.Handler(0, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(400, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(0, "")]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AtualizarTipoPagamento_Invalido(long id, string descricao)
        {
            var command = new AtualizarTipoPagamentoCommand
            {
                Id = id,
                Descricao = descricao
            };

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(1)]
        public void Handler_AtualizarTipoPagamento_Id_NaoCadastrado(long id)
        {
            var command = new SettingsTest().TipoPagamentoAtualizarCommand;
            command.Id = id;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Handler_AtualizarTipoPagamento_Id_Invalido(long id)
        {
            var command = new SettingsTest().TipoPagamentoAtualizarCommand;
            command.Id = id;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AtualizarTipoPagamento_Descricao_Invalido(string descicao)
        {
            var command = new SettingsTest().TipoPagamentoAtualizarCommand;
            command.Descricao = descicao;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_ApagarTipoPagamento_Valido()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var retorno = _handler.Handler(tipoPagamento.Id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.AreEqual(200, retorno.StatusCode);
            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(tipoPagamento.Id, retornoDados.Id);
            Assert.Null(retorno.Erros);
        }

        [Test]
        [TestCase(1)]
        public void Handler_ApagarTipoPagamento_Id_NaoCadastrado(long id)
        {
            var retorno = _handler.Handler(id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retornoDados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}