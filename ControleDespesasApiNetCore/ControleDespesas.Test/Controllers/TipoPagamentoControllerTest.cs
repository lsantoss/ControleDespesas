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
using LSCode.Facilitador.Api.Models.Results;
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
        private readonly Mock<IOptions<SettingsAPI>> _mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
        private readonly TipoPagamentoRepositorio _repository;
        private readonly TipoPagamentoHandler _handler;
        private readonly TipoPagamentoController _controller;

        public TipoPagamentoControllerTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);
            _mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new TipoPagamentoRepositorio(_mockOptionsInfra.Object);
            _handler = new TipoPagamentoHandler(_repository);
            _controller = new TipoPagamentoController(_repository, _handler, _mockOptionsAPI.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void TipoPagamentoHealthCheck()
        {
            var response = _controller.TipoPagamentoHealthCheck().Result;

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

            _repository.Salvar(tipoPagamento0);
            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);

            var response = _controller.TipoPagamentos().Result;

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

            _repository.Salvar(tipoPagamento0);
            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);

            var response = _controller.TipoPagamento(command).Result;

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

            var response = _controller.TipoPagamentoInserir(command).Result;

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

            _repository.Salvar(tipoPagamento);

            var response = _controller.TipoPagamentoAlterar(command).Result;

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

            _repository.Salvar(tipoPagamento);

            var response = _controller.TipoPagamentoExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarTipoPagamentoCommandOutput, Notificacao>>>(responseJson);

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