using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.TiposPagamentos.Commands.Output;
using ControleDespesas.Domain.TiposPagamentos.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Response;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.TiposPagamentos.Controllers
{
    public class TipoPagamentoControllerTest : DatabaseTest
    {
        private readonly ITipoPagamentoRepository _repository;
        private readonly ITipoPagamentoHandler _handler;
        private readonly TipoPagamentoController _controller;

        public TipoPagamentoControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new TipoPagamentoRepository(MockSettingsInfraData);
            _handler = new TipoPagamentoHandler(_repository);
            _controller = new TipoPagamentoController(_repository, _handler);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void TipoPagamentos()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repository.Salvar(tipoPagamento2);

            var tipoPagamento3 = new SettingsTest().TipoPagamento3;
            _repository.Salvar(tipoPagamento3);

            var response = _controller.TipoPagamentos();
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<TipoPagamentoQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(tipoPagamento1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(tipoPagamento1.Descricao, responseObj.Value.Dados[0].Descricao);

            Assert.AreEqual(tipoPagamento2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(tipoPagamento2.Descricao, responseObj.Value.Dados[1].Descricao);

            Assert.AreEqual(tipoPagamento3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(tipoPagamento3.Descricao, responseObj.Value.Dados[2].Descricao);
        }

        [Test]
        public void TipoPagamento()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repository.Salvar(tipoPagamento2);

            var tipoPagamento3 = new SettingsTest().TipoPagamento3;
            _repository.Salvar(tipoPagamento3);

            var response = _controller.TipoPagamento(tipoPagamento2.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<TipoPagamentoQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(tipoPagamento2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(tipoPagamento2.Descricao, responseObj.Value.Dados.Descricao);
        }

        [Test]
        public void TipoPagamentoInserir()
        {
            var command = new SettingsTest().TipoPagamentoAdicionarCommand;
            var response = _controller.TipoPagamentoInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<TipoPagamentoCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(201, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo Pagamento gravado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Descricao, responseObj.Value.Dados.Descricao);
        }

        [Test]
        public void TipoPagamentoAlterar()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var command = new SettingsTest().TipoPagamentoAtualizarCommand;
            var response = _controller.TipoPagamentoAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<TipoPagamentoCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

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
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var response = _controller.TipoPagamentoExcluir(tipoPagamento.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Tipo Pagamento excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(tipoPagamento.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}