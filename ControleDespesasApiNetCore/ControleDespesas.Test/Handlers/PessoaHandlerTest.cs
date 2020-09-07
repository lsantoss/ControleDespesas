using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
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
    public class PessoaHandlerTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly PessoaRepositorio _repository;
        private readonly PessoaHandler _handler;

        public PessoaHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new PessoaRepositorio(_mockOptions.Object);
            _handler = new PessoaHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPessoa()
        {
            var pessoaCommand = new AdicionarPessoaCommand()
            {
                Nome = "NomePessoa",
                ImagemPerfil = "ImagemPessoa"
            };

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
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");

            var pessoaCommand = new AtualizarPessoaCommand()
            {
                Id = 1,
                Nome = "NomePessoa - Editada",
                ImagemPerfil = "ImagemPessoa - Editada"
            };

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
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");

            var pessoaCommand = new ApagarPessoaCommand() { Id = 1 };

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