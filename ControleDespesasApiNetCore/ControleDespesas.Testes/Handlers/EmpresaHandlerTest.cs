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
            Mock<IEmpresaRepositorio> mockIEmpresaRepositorio = new Mock<IEmpresaRepositorio>();
            mockIEmpresaRepositorio.Setup(m => m.LocalizarMaxId()).Returns(1);

            EmpresaHandler handler = new EmpresaHandler(mockIEmpresaRepositorio.Object);

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
    }
}