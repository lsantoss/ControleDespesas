using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Pagamento;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class PagamentoControllerTest : DatabaseTest
    {
        private readonly EmpresaRepositorio _repositoryEmpresa;
        private readonly PessoaRepositorio _repositoryPessoa;
        private readonly TipoPagamentoRepositorio _repositoryTipoPagamento;
        private readonly PagamentoRepositorio _repositoryPagamento;
        private readonly PagamentoHandler _handler;
        private readonly PagamentoController _controller;

        public PagamentoControllerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);
            var optionsAPI = Options.Create(MockSettingsAPI);

            _repositoryEmpresa = new EmpresaRepositorio(optionsInfraData);
            _repositoryPessoa = new PessoaRepositorio(optionsInfraData);
            _repositoryTipoPagamento = new TipoPagamentoRepositorio(optionsInfraData);
            _repositoryPagamento = new PagamentoRepositorio(optionsInfraData);
            _handler = new PagamentoHandler(_repositoryPagamento, _repositoryEmpresa, _repositoryPessoa, _repositoryTipoPagamento);
            _controller = new PagamentoController(_repositoryPagamento, _handler, optionsAPI);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsAPI.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void PagamentoHealthCheck()
        {            
            var response = _controller.PagamentoHealthCheck().Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Pagamento OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Pagamentos()
        {
            var pagamento1 = new SettingsTest().Pagamento1;
            var pagamento2 = new SettingsTest().Pagamento2;
            var pagamento3 = new SettingsTest().Pagamento3;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPessoa.Salvar(pagamento3.Pessoa);
            _repositoryPagamento.Salvar(pagamento3);

            var command = MockSettingsTest.PagamentoObterPorIdPessoaCommand;

            var response = _controller.Pagamentos(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PagamentoQueryResult>, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pagamentos obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pagamento1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, responseObj.Value.Dados[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, responseObj.Value.Dados[0].Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, responseObj.Value.Dados[0].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), responseObj.Value.Dados[0].Descricao);
            Assert.AreEqual(pagamento1.Valor, responseObj.Value.Dados[0].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, responseObj.Value.Dados[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[0].DataPagamento).Date);

            Assert.AreEqual(pagamento2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, responseObj.Value.Dados[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, responseObj.Value.Dados[1].Empresa.Id);
            Assert.AreEqual(pagamento2.Pessoa.Id, responseObj.Value.Dados[1].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao.ToString(), responseObj.Value.Dados[1].Descricao);
            Assert.AreEqual(pagamento2.Valor, responseObj.Value.Dados[1].Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, responseObj.Value.Dados[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[1].DataPagamento).Date);
        }

        [Test]
        public void Pagamento()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;

            var command = MockSettingsTest.PagamentoObterPorIdCommand;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            var response = _controller.Pagamento(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PagamentoQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pagamento obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(pagamento2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, responseObj.Value.Dados.TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, responseObj.Value.Dados.Empresa.Id);
            Assert.AreEqual(pagamento2.Pessoa.Id, responseObj.Value.Dados.Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao.ToString(), responseObj.Value.Dados.Descricao);
            Assert.AreEqual(pagamento2.Valor, responseObj.Value.Dados.Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, responseObj.Value.Dados.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados.DataPagamento).Date);
        }

        [Test]
        public void PagamentosConcluidos()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;
            var pagamento3 = MockSettingsTest.Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var command = MockSettingsTest.PagamentoObterPorIdPessoaCommand;

            var response = _controller.PagamentoConcluido(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PagamentoQueryResult>, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pagamentos obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(2, responseObj.Value.Dados.Count);

            Assert.AreEqual(pagamento1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, responseObj.Value.Dados[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, responseObj.Value.Dados[0].Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, responseObj.Value.Dados[0].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), responseObj.Value.Dados[0].Descricao);
            Assert.AreEqual(pagamento1.Valor, responseObj.Value.Dados[0].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, responseObj.Value.Dados[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[0].DataPagamento).Date);

            Assert.AreEqual(pagamento2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(pagamento2.TipoPagamento.Id, responseObj.Value.Dados[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento2.Empresa.Id, responseObj.Value.Dados[1].Empresa.Id);
            Assert.AreEqual(pagamento2.Pessoa.Id, responseObj.Value.Dados[1].Pessoa.Id);
            Assert.AreEqual(pagamento2.Descricao.ToString(), responseObj.Value.Dados[1].Descricao);
            Assert.AreEqual(pagamento2.Valor, responseObj.Value.Dados[1].Valor);
            Assert.AreEqual(pagamento2.DataVencimento.Date, responseObj.Value.Dados[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento2.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[1].DataPagamento).Date);
        }

        [Test]
        public void PagamentosPendentes()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;
            var pagamento3 = MockSettingsTest.Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var command = MockSettingsTest.PagamentoObterPorIdPessoaCommand;

            var response = _controller.PagamentoPendente(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PagamentoQueryResult>, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pagamentos obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Count);

            Assert.AreEqual(pagamento3.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pagamento3.TipoPagamento.Id, responseObj.Value.Dados[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento3.Empresa.Id, responseObj.Value.Dados[0].Empresa.Id);
            Assert.AreEqual(pagamento3.Pessoa.Id, responseObj.Value.Dados[0].Pessoa.Id);
            Assert.AreEqual(pagamento3.Descricao.ToString(), responseObj.Value.Dados[0].Descricao);
            Assert.AreEqual(pagamento3.Valor, responseObj.Value.Dados[0].Valor);
            Assert.AreEqual(pagamento3.DataVencimento.Date, responseObj.Value.Dados[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento3.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[0].DataPagamento).Date);
        }

        [Test]
        public void ObterGastos()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;
            var pagamento3 = MockSettingsTest.Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            var valorTotalEsperado = pagamento1.Valor + pagamento2.Valor + pagamento3.Valor;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var command = MockSettingsTest.PagamentoObterGastosCommand;

            var response = _controller.ObterGastos(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PagamentoGastosQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Cáculo obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(valorTotalEsperado, responseObj.Value.Dados.Valor);
        }

        [Test]
        public void ObterGastosAno()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;
            var pagamento3 = MockSettingsTest.Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            var valorTotalEsperado = pagamento2.Valor + pagamento3.Valor;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var command = MockSettingsTest.PagamentoObterGastosAnoCommand;

            var response = _controller.ObterGastosAno(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PagamentoGastosQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Cáculo obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(valorTotalEsperado, responseObj.Value.Dados.Valor);
        }

        [Test]
        public void ObterGastosAnoMes()
        {
            var pagamento1 = MockSettingsTest.Pagamento1;
            var pagamento2 = MockSettingsTest.Pagamento2;
            var pagamento3 = MockSettingsTest.Pagamento3;

            pagamento3.Pessoa = pagamento1.Pessoa;

            var valorTotalEsperado = pagamento2.Valor + pagamento3.Valor;

            _repositoryTipoPagamento.Salvar(pagamento1.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento1.Empresa);
            _repositoryPessoa.Salvar(pagamento1.Pessoa);
            _repositoryPagamento.Salvar(pagamento1);

            _repositoryTipoPagamento.Salvar(pagamento2.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento2.Empresa);
            _repositoryPagamento.Salvar(pagamento2);

            _repositoryTipoPagamento.Salvar(pagamento3.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento3.Empresa);
            _repositoryPagamento.Salvar(pagamento3);

            var command = MockSettingsTest.PagamentoObterGastosAnoMesCommand;

            var response = _controller.ObterGastosAnoMes(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PagamentoGastosQueryResult, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Cáculo obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(valorTotalEsperado, responseObj.Value.Dados.Valor);
        }

        [Test]
        public void PagamentoInserir()
        {
            var tipoPagamento = MockSettingsTest.Pagamento1.TipoPagamento;
            var empresa = MockSettingsTest.Pagamento1.Empresa ;
            var pessoa = MockSettingsTest.Pagamento1.Pessoa;

            var command = MockSettingsTest.PagamentoAdicionarCommand;

            _repositoryTipoPagamento.Salvar(tipoPagamento);
            _repositoryEmpresa.Salvar(empresa);
            _repositoryPessoa.Salvar(pessoa);

            var response = _controller.PagamentoInserir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarPagamentoCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pagamento gravado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.IdTipoPagamento, responseObj.Value.Dados.IdTipoPagamento);
            Assert.AreEqual(command.IdEmpresa, responseObj.Value.Dados.IdEmpresa);
            Assert.AreEqual(command.IdPessoa, responseObj.Value.Dados.IdPessoa);
            Assert.AreEqual(command.Descricao.ToString(), responseObj.Value.Dados.Descricao);
            Assert.AreEqual(command.Valor, responseObj.Value.Dados.Valor);
            Assert.AreEqual(command.DataVencimento.Date, responseObj.Value.Dados.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(command.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados.DataPagamento).Date);
        }

        [Test]
        public void PagamentoAlterar()
        {
            var pagamento = MockSettingsTest.Pagamento1;

            var command = MockSettingsTest.PagamentoAtualizarCommand;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa);
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var response = _controller.PagamentoAlterar(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarPagamentoCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pagamento atualizado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.IdTipoPagamento, responseObj.Value.Dados.IdTipoPagamento);
            Assert.AreEqual(command.IdEmpresa, responseObj.Value.Dados.IdEmpresa);
            Assert.AreEqual(command.IdPessoa, responseObj.Value.Dados.IdPessoa);
            Assert.AreEqual(command.Descricao.ToString(), responseObj.Value.Dados.Descricao);
            Assert.AreEqual(command.Valor, responseObj.Value.Dados.Valor);
            Assert.AreEqual(command.DataVencimento.Date, responseObj.Value.Dados.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(command.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados.DataPagamento).Date);
        }

        [Test]
        public void PagamentoExcluir()
        {
            var pagamento = MockSettingsTest.Pagamento1;

            var command = MockSettingsTest.PagamentoApagarCommand;

            _repositoryTipoPagamento.Salvar(pagamento.TipoPagamento);
            _repositoryEmpresa.Salvar(pagamento.Empresa) ;
            _repositoryPessoa.Salvar(pagamento.Pessoa);
            _repositoryPagamento.Salvar(pagamento);

            var response = _controller.PagamentoExcluir(command).Result;

            var responseJson = JsonConvert.SerializeObject(response);

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarPagamentoCommandOutput, Notificacao>>>(responseJson);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(responseObj));

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}