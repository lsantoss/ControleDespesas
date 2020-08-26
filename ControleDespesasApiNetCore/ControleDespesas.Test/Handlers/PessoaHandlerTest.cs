using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
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
    public class PessoaHandlerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarPessoa()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IPessoaRepositorio IPessoaRepos = new PessoaRepositorio(mockOptions.Object);

            PessoaHandler handler = new PessoaHandler(IPessoaRepos);

            AdicionarPessoaCommand pessoaCommand = new AdicionarPessoaCommand()
            {
                Nome = "NomePessoa",
                ImagemPerfil = "ImagemPessoa"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(pessoaCommand);
            AdicionarPessoaCommandOutput retornoDados = (AdicionarPessoaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(pessoaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(pessoaCommand.ImagemPerfil, retornoDados.ImagemPerfil);
        }

        [Test]
        public void Handler_AtualizarPessoa()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            new PessoaRepositorio(mockOptions.Object).Salvar(pessoa);

            IPessoaRepositorio IPessoaRepos = new PessoaRepositorio(mockOptions.Object);
            PessoaHandler handler = new PessoaHandler(IPessoaRepos);

            AtualizarPessoaCommand pessoaCommand = new AtualizarPessoaCommand()
            {
                Id = 1,
                Nome = "NomePessoa - Editada",
                ImagemPerfil = "ImagemPessoa - Editada"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(pessoaCommand);
            AtualizarPessoaCommandOutput retornoDados = (AtualizarPessoaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pessoaCommand.Id, retornoDados.Id);
            Assert.AreEqual(pessoaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(pessoaCommand.ImagemPerfil, retornoDados.ImagemPerfil);
        }

        [Test]
        public void Handler_ApagarPessoa()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPessoa");
            new PessoaRepositorio(mockOptions.Object).Salvar(pessoa);

            IPessoaRepositorio IPessoaRepos = new PessoaRepositorio(mockOptions.Object);
            PessoaHandler handler = new PessoaHandler(IPessoaRepos);

            ApagarPessoaCommand pessoaCommand = new ApagarPessoaCommand() { Id = 1 };

            ICommandResult<Notificacao> retorno = handler.Handler(pessoaCommand);
            ApagarPessoaCommandOutput retornoDados = (ApagarPessoaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pessoaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}