using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Testes.AppConfigurations.Factory;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ControleDespesas.Testes.Handlers
{
    public class EmpresaHandlerTest : DatabaseFactory
    {
        public EmpresaHandlerTest()
        {
            DroparBaseDeDados();
            CriarBaseDeDadosETabelas();
        }

        [Fact]
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

            Assert.True(retorno.Sucesso);
            Assert.Equal("Empresa gravada com sucesso!", retorno.Mensagem);
            Assert.Equal(1, ((AdicionarEmpresaCommandOutput)retorno.Dados).Id);
            Assert.Equal(empresaCommand.Nome, ((AdicionarEmpresaCommandOutput)retorno.Dados).Nome);
            Assert.Equal(empresaCommand.Logo, ((AdicionarEmpresaCommandOutput)retorno.Dados).Logo);
        }

        [Fact]
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

            Assert.True(retorno.Sucesso);
            Assert.Equal("Empresa atualizada com sucesso!", retorno.Mensagem);
            Assert.Equal(empresaCommand.Id, ((AtualizarEmpresaCommandOutput)retorno.Dados).Id);
            Assert.Equal(empresaCommand.Nome, ((AtualizarEmpresaCommandOutput)retorno.Dados).Nome);
            Assert.Equal(empresaCommand.Logo, ((AtualizarEmpresaCommandOutput)retorno.Dados).Logo);
        }

        [Fact]
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

            Assert.True(retorno.Sucesso);
            Assert.Equal("Empresa excluída com sucesso!", retorno.Mensagem);
            Assert.Equal(empresaCommand.Id, ((ApagarEmpresaCommandOutput)retorno.Dados).Id);
        }
    }
}