using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.TipoPagamento;
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
    public class TipoPagamentoControllerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void TipoPagamentoHealthCheck()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var handler = new TipoPagamentoHandler(repository);

            var controller = new TipoPagamentoController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var response = controller.TipoPagamentoHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Tipo de Pagamento OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void TipoPagamentos()
        {
            var tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            var tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            var tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var handler = new TipoPagamentoHandler(repository);

            var controller = new TipoPagamentoController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            repository.Salvar(tipoPagamento0);
            repository.Salvar(tipoPagamento1);
            repository.Salvar(tipoPagamento2);

            var response = controller.TipoPagamentos().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<TipoPagamentoQueryResult>, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de tipos de pagamento obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(tipoPagamento0.Descricao.ToString(), responseObj.Value.Dados[0].Descricao);

            Assert.AreEqual(2, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(tipoPagamento1.Descricao.ToString(), responseObj.Value.Dados[1].Descricao);

            Assert.AreEqual(3, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), responseObj.Value.Dados[2].Descricao);
        }

        [Test]
        public void TipoPagamento()
        {
            var tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            var tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            var tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            var command = new ObterTipoPagamentoPorIdCommand() { Id = 2 };

            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var handler = new TipoPagamentoHandler(repository);

            var controller = new TipoPagamentoController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            repository.Salvar(tipoPagamento0);
            repository.Salvar(tipoPagamento1);
            repository.Salvar(tipoPagamento2);

            var response = controller.TipoPagamento(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<TipoPagamentoQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo de pagameto obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(2, responseObj.Value.Dados.Id);
            Assert.AreEqual(tipoPagamento1.Descricao.ToString(), responseObj.Value.Dados.Descricao);
        }

        [Test]
        public void TipoPagamentoInserir()
        {
            var command = new AdicionarTipoPagamentoCommand()
            {
                Descricao = "DesciçãoTipoPagamento",
            };

            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var handler = new TipoPagamentoHandler(repository);

            var controller = new TipoPagamentoController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var response = controller.TipoPagamentoInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Descricao, responseObj.Value.Dados.Descricao);
        }

        [Test]
        public void TipoPagamentoAlterar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));

            var command = new AtualizarTipoPagamentoCommand()
            {
                Id = 1,
                Descricao = "DescriçãoTipoPagamento - Editada"
            };

            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var handler = new TipoPagamentoHandler(repository);

            var controller = new TipoPagamentoController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            repository.Salvar(tipoPagamento);

            var response = controller.TipoPagamentoAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo Pagamento atualizado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Descricao, responseObj.Value.Dados.Descricao);
        }

        [Test]
        public void TipoPagamentoExcluir()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));

            var command = new ApagarTipoPagamentoCommand() { Id = 1 };

            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repository = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var handler = new TipoPagamentoHandler(repository);

            var controller = new TipoPagamentoController(repository, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            repository.Salvar(tipoPagamento);

            var response = controller.TipoPagamentoExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}