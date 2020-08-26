using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
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
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            ITipoPagamentoRepositorio ITipoPagamentoRepos = new TipoPagamentoRepositorio(mockOptions.Object);

            TipoPagamentoHandler handler = new TipoPagamentoHandler(ITipoPagamentoRepos);

            AdicionarTipoPagamentoCommand tipoPagamentoCommand = new AdicionarTipoPagamentoCommand()
            {
                Descricao = "DesciçãooTipoPagamento"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(tipoPagamentoCommand);
            AdicionarTipoPagamentoCommandOutput retornoDados = (AdicionarTipoPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(tipoPagamentoCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_AtualizarTipoPagamento()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            new TipoPagamentoRepositorio(mockOptions.Object).Salvar(tipoPagamento);

            ITipoPagamentoRepositorio ITipoPagamentoRepos = new TipoPagamentoRepositorio(mockOptions.Object);
            TipoPagamentoHandler handler = new TipoPagamentoHandler(ITipoPagamentoRepos);

            AtualizarTipoPagamentoCommand empresaCommand = new AtualizarTipoPagamentoCommand()
            {
                Id = 1,
                Descricao = "DesciçãooTipoPagamento - Editada"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(empresaCommand);
            AtualizarTipoPagamentoCommandOutput retornoDados = (AtualizarTipoPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Descricao, retornoDados.Descricao);
        }

        [Test]
        public void Handler_ApagarTipoPagamento()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DesciçãooTipoPagamento", "Desciçãoo", 250));
            new TipoPagamentoRepositorio(mockOptions.Object).Salvar(tipoPagamento);

            ITipoPagamentoRepositorio ITipoPagamentoRepos = new TipoPagamentoRepositorio(mockOptions.Object);
            TipoPagamentoHandler handler = new TipoPagamentoHandler(ITipoPagamentoRepos);

            ApagarTipoPagamentoCommand empresaCommand = new ApagarTipoPagamentoCommand() { Id = 1 };

            ICommandResult<Notificacao> retorno = handler.Handler(empresaCommand);
            ApagarTipoPagamentoCommandOutput retornoDados = (ApagarTipoPagamentoCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}