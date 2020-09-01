using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Pagamento;
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
using System;
using System.Collections.Generic;

namespace ControleDespesas.Test.Controllers
{
    public class PagamentoControllerTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void PagamentoHealthCheck()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptionsInfra.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptionsInfra.Object);
            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptionsInfra.Object);

            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var controller = new PagamentoController(repositoryPagamento, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var responseJson = controller.PagamentoHealthCheck().Result.ToJson();

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<string, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Value.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Pagamento OK", responseObj.Value.Dados);
            Assert.Null(responseObj.Value.Erros);
        }

        [Test]
        public void Pagamentos()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptionsInfra.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptionsInfra.Object);
            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptionsInfra.Object);

            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var controller = new PagamentoController(repositoryPagamento, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            repositoryTipoPagamento.Salvar(tipoPagamento);
            repositoryEmpresa.Salvar(empresa);
            repositoryPessoa.Salvar(pessoa);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            var responseJson = controller.Pagamentos().Result.ToJson().Replace("_id", "id").Replace(@"ISODate(", "").Replace(")", "");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<List<PagamentoQueryResult>, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista de pagamentos obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(pagamento1.TipoPagamento.Id, responseObj.Value.Dados[1].TipoPagamento.Id);
            Assert.AreEqual(pagamento0.Empresa.Id, responseObj.Value.Dados[0].Empresa.Id);
            Assert.AreEqual(pagamento0.Pessoa.Id, responseObj.Value.Dados[0].Pessoa.Id);
            Assert.AreEqual(pagamento0.Descricao.ToString(), responseObj.Value.Dados[0].Descricao);
            Assert.AreEqual(pagamento0.Valor, responseObj.Value.Dados[0].Valor);
            Assert.AreEqual(pagamento0.DataVencimento.Date, responseObj.Value.Dados[0].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento0.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[0].DataPagamento).Date);

            Assert.AreEqual(2, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(pagamento0.TipoPagamento.Id, responseObj.Value.Dados[0].TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, responseObj.Value.Dados[1].Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, responseObj.Value.Dados[1].Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), responseObj.Value.Dados[1].Descricao);
            Assert.AreEqual(pagamento1.Valor, responseObj.Value.Dados[1].Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, responseObj.Value.Dados[1].DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados[1].DataPagamento).Date);
        }

        [Test]
        public void Pagamento()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptionsInfra.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptionsInfra.Object);
            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptionsInfra.Object);

            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var controller = new PagamentoController(repositoryPagamento, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            var pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            repositoryTipoPagamento.Salvar(tipoPagamento);
            repositoryEmpresa.Salvar(empresa);
            repositoryPessoa.Salvar(pessoa);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            var command = new ObterPagamentoPorIdCommand() { Id = 2 };

            var responseJson = controller.Pagamento(command).Result.ToJson().Replace("_id", "id").Replace(@"ISODate(", "").Replace(")","");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<PagamentoQueryResult, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pagamento obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(2, responseObj.Value.Dados.Id);
            Assert.AreEqual(pagamento0.TipoPagamento.Id, responseObj.Value.Dados.TipoPagamento.Id);
            Assert.AreEqual(pagamento1.Empresa.Id, responseObj.Value.Dados.Empresa.Id);
            Assert.AreEqual(pagamento1.Pessoa.Id, responseObj.Value.Dados.Pessoa.Id);
            Assert.AreEqual(pagamento1.Descricao.ToString(), responseObj.Value.Dados.Descricao);
            Assert.AreEqual(pagamento1.Valor, responseObj.Value.Dados.Valor);
            Assert.AreEqual(pagamento1.DataVencimento.Date, responseObj.Value.Dados.DataVencimento.Date);
            Assert.AreEqual(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(responseObj.Value.Dados.DataPagamento).Date);
        }

        [Test]
        public void PagamentoInserir()
        {
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptionsInfra.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptionsInfra.Object);
            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptionsInfra.Object);

            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var controller = new PagamentoController(repositoryPagamento, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            repositoryTipoPagamento.Salvar(tipoPagamento);
            repositoryEmpresa.Salvar(empresa);
            repositoryPessoa.Salvar(pessoa);

            var command = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = 1,
                IdEmpresa = 1,
                IdPessoa = 1,
                Descricao = "DescriçãoPagamento",
                Valor = 100,
                DataVencimento = DateTime.Now.AddDays(1),
                DataPagamento = DateTime.Now
            };

            var responseJson = controller.PagamentoInserir(command).Result.ToJson().Replace("_id", "id").Replace(@"ISODate(", "").Replace(")", "");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AdicionarPagamentoCommandOutput, Notificacao>>>(responseJson);

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
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptionsInfra.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptionsInfra.Object);
            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptionsInfra.Object);

            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var controller = new PagamentoController(repositoryPagamento, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            repositoryTipoPagamento.Salvar(tipoPagamento);
            repositoryEmpresa.Salvar(empresa);
            repositoryPessoa.Salvar(pessoa);
            repositoryPagamento.Salvar(pagamento);

            var command = new AtualizarPagamentoCommand()
            {
                Id = 1,
                IdTipoPagamento = 1,
                IdEmpresa = 1,
                IdPessoa = 1,
                Descricao = "DescriçãoPagamento",
                Valor = 100,
                DataVencimento = DateTime.Now.AddDays(1),
                DataPagamento = DateTime.Now
            };

            var responseJson = controller.PagamentoAlterar(command).Result.ToJson().Replace("_id", "id").Replace(@"ISODate(", "").Replace(")", "");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<AtualizarPagamentoCommandOutput, Notificacao>>>(responseJson);

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
            var mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            var mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            var repositoryEmpresa = new EmpresaRepositorio(mockOptionsInfra.Object);
            var repositoryPessoa = new PessoaRepositorio(mockOptionsInfra.Object);
            var repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptionsInfra.Object);
            var repositoryPagamento = new PagamentoRepositorio(mockOptionsInfra.Object);

            var handler = new PagamentoHandler(repositoryPagamento, repositoryEmpresa, repositoryPessoa, repositoryTipoPagamento);

            var controller = new PagamentoController(repositoryPagamento, handler, mockOptionsAPI.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = _settingsAPI.ChaveAPI;

            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            var pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            var pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            repositoryTipoPagamento.Salvar(tipoPagamento);
            repositoryEmpresa.Salvar(empresa);
            repositoryPessoa.Salvar(pessoa);
            repositoryPagamento.Salvar(pagamento);

            var command = new ApagarPagamentoCommand() { Id = 1 };

            var responseJson = controller.PagamentoExcluir(command).Result.ToJson().Replace("_id", "id");

            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponseModel<ApagarPagamentoCommandOutput, Notificacao>>>(responseJson);

            Assert.AreEqual(200, responseObj.StatusCode);

            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Pagamento excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}