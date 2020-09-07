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
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class EmpresaControllerTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsAPI>> _mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
        private readonly EmpresaRepositorio _repository;
        private readonly EmpresaHandler _handler;
        private readonly EmpresaController _controller;

        public EmpresaControllerTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);
            _mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new EmpresaRepositorio(_mockOptionsInfra.Object);
            _handler = new EmpresaHandler(_repository);
            _controller = new EmpresaController(_repository, _handler, _mockOptionsAPI.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void EmpresaHealthCheck()
        {
            var response = _controller.EmpresaHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

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
            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            _repository.Salvar(empresa0);
            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);

            var response = _controller.Empresas().Result;

            var responseJson = JsonConvert.SerializeObject(response);

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
            var empresa0 = new Empresa(0, new Texto("NomeEmpresa0", "Nome", 100), "Logo0");
            var empresa1 = new Empresa(0, new Texto("NomeEmpresa1", "Nome", 100), "Logo1");
            var empresa2 = new Empresa(0, new Texto("NomeEmpresa2", "Nome", 100), "Logo2");

            var command = new ObterEmpresaPorIdCommand() { Id = 2 };

            _repository.Salvar(empresa0);
            _repository.Salvar(empresa1);
            _repository.Salvar(empresa2);

            var response = _controller.Empresa(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

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
            var command = new AdicionarEmpresaCommand()
            {
                Nome = "NomeEmpresa",
                Logo = "LogoEmpresa"
            };

            var response = _controller.EmpresaInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

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
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "LogoEmpresa");

            var command = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "NomeEmpresa - Editada",
                Logo = "LogoEmpresa - Editada"
            };

            _repository.Salvar(empresa);

            var response = _controller.EmpresaAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

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
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "LogoEmpresa");

            var command = new ApagarEmpresaCommand() { Id = 1 };

            _repository.Salvar(empresa);

            var response = _controller.EmpresaExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarEmpresaCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}