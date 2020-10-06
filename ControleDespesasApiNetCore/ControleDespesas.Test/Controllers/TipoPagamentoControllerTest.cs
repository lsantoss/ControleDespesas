using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.TipoPagamento;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class TipoPagamentoControllerTest : DatabaseFactory
    {
        private readonly TipoPagamentoRepositorio _repository;
        private readonly TipoPagamentoHandler _handler;
        private readonly TipoPagamentoController _controller;

        public TipoPagamentoControllerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);
            var optionsAPI = Options.Create(MockSettingsAPI);

            _repository = new TipoPagamentoRepositorio(optionsInfraData);
            _handler = new TipoPagamentoHandler(_repository);
            _controller = new TipoPagamentoController(_repository, _handler, optionsAPI);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void TipoPagamentoHealthCheck()
        {
            var response = _controller.TipoPagamentoHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            var outputTest = FotmatadorJson.FormatarJsonDeSaida<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);
            TestContext.WriteLine(outputTest);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Tipo de Pagamento OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void TipoPagamentos()
        {
            var tipoPagamento1 = MockSettingsTest.TipoPagamento1;
            var tipoPagamento2 = MockSettingsTest.TipoPagamento2;
            var tipoPagamento3 = MockSettingsTest.TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            var response = _controller.TipoPagamentos().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<TipoPagamentoQueryResult>, Notificacao>>>(responseJson);

            var outputTest = FotmatadorJson.FormatarJsonDeSaida<ApiTestResponse<ApiResponseModel<List<TipoPagamentoQueryResult>, Notificacao>>>(responseJson);
            TestContext.WriteLine(outputTest);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de tipos de pagamento obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(tipoPagamento1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(tipoPagamento1.Descricao.ToString(), responseObj.Value.Dados[0].Descricao);

            Assert.AreEqual(tipoPagamento2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), responseObj.Value.Dados[1].Descricao);

            Assert.AreEqual(tipoPagamento3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(tipoPagamento3.Descricao.ToString(), responseObj.Value.Dados[2].Descricao);
        }

        [Test]
        public void TipoPagamento()
        {
            var tipoPagamento1 = MockSettingsTest.TipoPagamento1;
            var tipoPagamento2 = MockSettingsTest.TipoPagamento2;
            var tipoPagamento3 = MockSettingsTest.TipoPagamento3;

            var command = MockSettingsTest.TipoPagamentoObterPorIdCommand;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            var response = _controller.TipoPagamento(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<TipoPagamentoQueryResult, Notificacao>>>(responseJson);

            var outputTest = FotmatadorJson.FormatarJsonDeSaida<ApiTestResponse<ApiResponseModel<TipoPagamentoQueryResult, Notificacao>>>(responseJson);
            TestContext.WriteLine(outputTest);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo de pagameto obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(tipoPagamento2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), responseObj.Value.Dados.Descricao);
        }

        [Test]
        public void TipoPagamentoInserir()
        {
            var command = MockSettingsTest.TipoPagamentoAdicionarCommand;

            var response = _controller.TipoPagamentoInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

            var outputTest = FotmatadorJson.FormatarJsonDeSaida<ApiTestResponse<ApiResponseModel<AdicionarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);
            TestContext.WriteLine(outputTest);

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
            var tipoPagamento = MockSettingsTest.TipoPagamento1;

            var command = MockSettingsTest.TipoPagamentoAtualizarCommand;

            _repository.Salvar(tipoPagamento);

            var response = _controller.TipoPagamentoAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

            var outputTest = FotmatadorJson.FormatarJsonDeSaida<ApiTestResponse<ApiResponseModel<AtualizarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);
            TestContext.WriteLine(outputTest);

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
            var tipoPagamento = MockSettingsTest.TipoPagamento1;

            var command = MockSettingsTest.TipoPagamentoApagarCommand;

            _repository.Salvar(tipoPagamento);

            var response = _controller.TipoPagamentoExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

            var outputTest = FotmatadorJson.FormatarJsonDeSaida<ApiTestResponse<ApiResponseModel<ApagarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);
            TestContext.WriteLine(outputTest);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}