using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    public class TipoPagamentoHandlerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarTipoPagamento()
        {
            var tipoPagamentoCommand = new AdicionarTipoPagamentoCommand()
            {
                Descricao = "DesciçãooTipoPagamento"
            };

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptions.Object);

            var handler = new TipoPagamentoHandler(repository);

            var retorno = handler.Handler(tipoPagamentoCommand);

            var retornoDados = (AdicionarTipoPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_AtualizarTipoPagamento()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));

            var empresaCommand = new AtualizarTipoPagamentoCommand()
            {
                Id = 1,
                Descricao = "DesciçãooTipoPagamento - Editada"
            };

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptions.Object);

            var handler = new TipoPagamentoHandler(repository);

            repository.Salvar(tipoPagamento);

            var retorno = handler.Handler(empresaCommand);

            var retornoDados = (AtualizarTipoPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_ApagarTipoPagamento()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));

            var empresaCommand = new ApagarTipoPagamentoCommand() { Id = 1 };

            var mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptions.Object);

            var handler = new TipoPagamentoHandler(repository);

            repository.Salvar(tipoPagamento);

            var retorno = handler.Handler(empresaCommand);

            var retornoDados = (ApagarTipoPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}