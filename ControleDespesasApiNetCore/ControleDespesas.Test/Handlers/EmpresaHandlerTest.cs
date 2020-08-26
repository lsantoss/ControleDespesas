using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
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
    public class EmpresaHandlerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarEmpresa()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptions.Object);

            EmpresaHandler handler = new EmpresaHandler(IEmpresaRepos);

            AdicionarEmpresaCommand empresaCommand = new AdicionarEmpresaCommand()
            {
                Nome = "NomeEmpresa",
                Logo = "LogoEmpresa"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(empresaCommand);
            AdicionarEmpresaCommandOutput retornoDados = (AdicionarEmpresaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_AtualizarEmpresa()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            new EmpresaRepositorio(mockOptions.Object).Salvar(empresa);

            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptions.Object);
            EmpresaHandler handler = new EmpresaHandler(IEmpresaRepos);

            AtualizarEmpresaCommand empresaCommand = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "NomeEmpresa - Editada",
                Logo = "LogoEmpresa - Editado"
            };

            ICommandResult<Notificacao> retorno = handler.Handler(empresaCommand);
            AtualizarEmpresaCommandOutput retornoDados = (AtualizarEmpresaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_ApagarEmpresa()
        {
            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            new EmpresaRepositorio(mockOptions.Object).Salvar(empresa);

            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptions.Object);
            EmpresaHandler handler = new EmpresaHandler(IEmpresaRepos);

            ApagarEmpresaCommand empresaCommand = new ApagarEmpresaCommand() { Id = 1 };

            ICommandResult<Notificacao> retorno = handler.Handler(empresaCommand);
            ApagarEmpresaCommandOutput retornoDados = (ApagarEmpresaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}