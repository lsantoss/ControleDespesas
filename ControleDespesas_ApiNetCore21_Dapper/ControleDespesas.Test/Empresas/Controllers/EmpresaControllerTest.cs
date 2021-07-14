using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Commands.Output;
using ControleDespesas.Domain.Empresas.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Empresas.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Response;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Models;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Empresas.Controllers
{
    [TestFixture]
    public class EmpresaControllerTest : DatabaseTest
    {
        private readonly IEmpresaRepository _repository;
        private readonly IEmpresaHandler _handler;
        private readonly EmpresaController _controller;

        public EmpresaControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new EmpresaRepository(MockSettingsInfraData);
            _handler = new EmpresaHandler(_repository);
            _controller = new EmpresaController(_repository, _handler);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Empresas_ListaPreenchida_200OK()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            var response = _controller.Empresas();
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<EmpresaQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(empresa1.Nome, responseObj.Value.Dados[0].Nome);
            Assert.AreEqual(empresa1.Logo, responseObj.Value.Dados[0].Logo);

            Assert.AreEqual(empresa2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(empresa2.Nome, responseObj.Value.Dados[1].Nome);
            Assert.AreEqual(empresa2.Logo, responseObj.Value.Dados[1].Logo);

            Assert.AreEqual(empresa3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(empresa3.Nome, responseObj.Value.Dados[2].Nome);
            Assert.AreEqual(empresa3.Logo, responseObj.Value.Dados[2].Logo);
        }

        [Test]
        public void Empresas_ListaVazia_204NoContent()
        {
            var response = _controller.Empresas();
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<EmpresaQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(204, responseObj.StatusCode);
        }

        [Test]
        public void Empresa_ObjetoPreenchido_200OK()
        {
            var empresa1 = new SettingsTest().Empresa1;
            _repository.Salvar(empresa1);

            var empresa2 = new SettingsTest().Empresa2;
            _repository.Salvar(empresa2);

            var empresa3 = new SettingsTest().Empresa3;
            _repository.Salvar(empresa3);

            var response = _controller.Empresa(empresa2.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(empresa2.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(empresa2.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void Empresa_ObjetoNull_204NoContent()
        {
            var response = _controller.Empresa(1);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(204, responseObj.StatusCode);
        }

        [Test]
        public void EmpresaInserir_201Created()
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;

            var response = _controller.EmpresaInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(201, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaInserir_CommandNull_400BadRequest()
        {
            var response = _controller.EmpresaInserir(null);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(400, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        public void EmpresaInserir_Invalido_422UnprocessableEntity(string nome, string logo)
        {
            var command = new AdicionarEmpresaCommand
            {
                Nome = nome,
                Logo = logo
            };

            var response = _controller.EmpresaInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void EmpresaInserir_Nome_Invalido_422UnprocessableEntity(string nome)
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;
            command.Nome = nome;

            var response = _controller.EmpresaInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void EmpresaInserir_Logo_Invalido_422UnprocessableEntity(string logo)
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;
            command.Logo = logo;

            var response = _controller.EmpresaInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        public void EmpresaAlterar_200OK()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var command = new SettingsTest().EmpresaAtualizarCommand;

            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Nome, responseObj.Value.Dados.Nome);
            Assert.AreEqual(command.Logo, responseObj.Value.Dados.Logo);
        }

        [Test]
        public void EmpresaAlterar_Command_Null_400BadRequest()
        {
            var response = _controller.EmpresaAlterar(0, null);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(400, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(1)]
        public void EmpresaAlterar_Id_NaoCadastrado_422UnprocessableEntity(long id)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Id = id;

            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void EmpresaAlterar_Id_Invalido_422UnprocessableEntity(long id)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Id = id;

            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(0, "", "")]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        public void EmpresaAlterar_Invalido_422UnprocessableEntity(long id, string nome, string logo)
        {
            var command = new AtualizarEmpresaCommand
            {
                Id = id,
                Nome = nome,
                Logo = logo
            };

            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void EmpresaAlterar_Nome_Invalido_422UnprocessableEntity(string nome)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Nome = nome;

            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void EmpresaAlterar_Logo_Invalido_422UnprocessableEntity(string logo)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Logo = logo;

            var response = _controller.EmpresaAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<EmpresaCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        public void EmpresaExcluir_200OK()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var response = _controller.EmpresaExcluir(empresa.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(empresa.Id, responseObj.Value.Dados.Id);
        }

        [Test]
        [TestCase(1)]
        public void EmpresaExcluir_Id_NaoCadastrado_422UnprocessableEntity(long id)
        {
            var response = _controller.EmpresaExcluir(id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}