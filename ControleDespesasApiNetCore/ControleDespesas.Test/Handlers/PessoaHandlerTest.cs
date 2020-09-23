using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    public class PessoaHandlerTest : DatabaseFactory
    {
        private readonly SettingsTest _settingsTest;
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly PessoaRepositorio _repository;
        private readonly PessoaHandler _handler;

        public PessoaHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            _settingsTest = new SettingsTest();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new PessoaRepositorio(_mockOptions.Object);
            _handler = new PessoaHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPessoa()
        {
            var pessoaCommand = _settingsTest.PessoaAdicionarCommand;

            var retorno = _handler.Handler(pessoaCommand);

            var retornoDados = (AdicionarPessoaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(pessoaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(pessoaCommand.ImagemPerfil, retornoDados.ImagemPerfil);
        }

        [Test]
        public void Handler_AtualizarPessoa()
        {
            var pessoa = _settingsTest.Pessoa1;

            var pessoaCommand = _settingsTest.PessoaAtualizarCommand;

            _repository.Salvar(pessoa);

            var retorno = _handler.Handler(pessoaCommand);

            var retornoDados = (AtualizarPessoaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pessoaCommand.Id, retornoDados.Id);
            Assert.AreEqual(pessoaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(pessoaCommand.ImagemPerfil, retornoDados.ImagemPerfil);
        }

        [Test]
        public void Handler_ApagarPessoa()
        {
            var pessoa = _settingsTest.Pessoa1;

            var pessoaCommand = _settingsTest.PessoaApagarCommand;

            _repository.Salvar(pessoa);

            var retorno = _handler.Handler(pessoaCommand);

            var retornoDados = (ApagarPessoaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pessoaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}