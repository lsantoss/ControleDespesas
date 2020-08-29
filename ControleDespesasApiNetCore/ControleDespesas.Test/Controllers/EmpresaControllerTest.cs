using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Models;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class EmpresaControllerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() 
        {
            CriarBaseDeDadosETabelas();
        }

        [Test]
        public void EmpresaHealthCheck()
        {
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IEmpresaRepositorio repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            EmpresaHandler handler = new EmpresaHandler(repository);

            EmpresaController controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var responseJson = controller.EmpresaHealthCheck().Result.ToJson();

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Empresa OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Empresas()
        {
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            IEmpresaRepositorio repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            EmpresaHandler handler = new EmpresaHandler(repository);

            EmpresaController controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;
            
            Empresa empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            Empresa empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            Empresa empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            repository.Salvar(empresa0);
            repository.Salvar(empresa1);
            repository.Salvar(empresa2);

            var responseJson = controller.Empresas().Result.ToJson().Replace("_id","id");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<EmpresaQueryResult>, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de empresas obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(empresa0.Nome.ToString(), responseObj.Value.Dados[0].Nome);
            Assert.AreEqual(empresa0.Logo, responseObj.Value.Dados[0].Logo);

            Assert.AreEqual(2, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(empresa1.Nome.ToString(), responseObj.Value.Dados[1].Nome);
            Assert.AreEqual(empresa1.Logo, responseObj.Value.Dados[1].Logo);

            Assert.AreEqual(3, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(empresa2.Nome.ToString(), responseObj.Value.Dados[2].Nome);
            Assert.AreEqual(empresa2.Logo, responseObj.Value.Dados[2].Logo);
        }

        [TearDown]
        public void TearDown() 
        {
            DroparBaseDeDados();
        }
    }
}
