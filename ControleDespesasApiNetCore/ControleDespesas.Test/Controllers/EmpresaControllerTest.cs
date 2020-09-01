using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Empresa;
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
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void EmpresaHealthCheck()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            var handler = new EmpresaHandler(repository);

            var controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
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
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            var handler = new EmpresaHandler(repository);

            var controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

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

        [Test]
        public void Empresa()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            var handler = new EmpresaHandler(repository);

            var controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            repository.Salvar(empresa0);
            repository.Salvar(empresa1);
            repository.Salvar(empresa2);

            var command = new ObterEmpresaPorIdCommand() { Id = 2 };

            var responseJson = controller.Empresa(command).Result.ToJson().Replace("_id", "id");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<EmpresaQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(2, responseObj.Value.Dados.Id);
            Assert.AreEqual(empresa1.Nome.ToString(), responseObj.Value.Dados.Nome);
            Assert.AreEqual(empresa1.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaInserir()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            var handler = new EmpresaHandler(repository);

            var controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var command = new AdicionarEmpresaCommand() 
            { 
                Nome = "NomeEmpresa",
                Logo = "LogoEmpresa"
            };

            var responseJson = controller.EmpresaInserir(command).Result.ToJson().Replace("_id", "id");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarEmpresaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaAlterar()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            var handler = new EmpresaHandler(repository);

            var controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "LogoEmpresa");
            repository.Salvar(empresa);

            var command = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "NomeEmpresa - Editada",
                Logo = "LogoEmpresa - Editada"
            };

            var responseJson = controller.EmpresaAlterar(command).Result.ToJson().Replace("_id", "id");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarEmpresaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaExcluir()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new EmpresaRepositorio(mockOptionsInfra.Object);
            var handler = new EmpresaHandler(repository);

            var controller = new EmpresaController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "LogoEmpresa");
            repository.Salvar(empresa);

            var command = new ApagarEmpresaCommand() { Id = 1 };

            var responseJson = controller.EmpresaExcluir(command).Result.ToJson().Replace("_id", "id");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarEmpresaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}