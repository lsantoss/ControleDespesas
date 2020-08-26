using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Controllers
{
    public class EmpresaControllerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();
        
        [Test]
        public void EmpresaHealthCheck()
        {
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsINFRA = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsINFRA.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IEmpresaRepositorio IEmpresaRepos = new EmpresaRepositorio(mockOptionsINFRA.Object);
            EmpresaHandler handler = new EmpresaHandler(IEmpresaRepos);

            EmpresaController empresaController = new EmpresaController(IEmpresaRepos, handler, mockOptionsAPI.Object);

            //var retorno = empresaController.EmpresaHealthCheck();

            //Assert.True(retorno.Sucesso);
            //Assert.AreEqual("Sucesso", retorno.Mensagem);
            //Assert.AreEqual("API Controle de Despesas - Empresa OK", retorno.Dados);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}
