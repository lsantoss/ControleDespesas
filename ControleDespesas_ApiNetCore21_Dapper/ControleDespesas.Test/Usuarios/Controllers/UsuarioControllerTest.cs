using ControleDespesas.Api.Controllers.ControleDespesas;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Commands.Output;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Domain.Usuarios.Handlers;
using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Handlers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Query.Parameters;
using ControleDespesas.Domain.Usuarios.Query.Results;
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

namespace ControleDespesas.Test.Usuarios.Controllers
{
    [TestFixture]
    public class UsuarioControllerTest : DatabaseTest
    {
        private readonly ITokenJwtHelper _tokenJwtHelper;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioHandler _handler;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            CriarBaseDeDadosETabelas();

            _tokenJwtHelper = new TokenJwtHelper(MockSettingsApi);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _repository = new UsuarioRepository(MockSettingsInfraData);
            _handler = new UsuarioHandler(_repository, _tokenJwtHelper);
            _controller = new UsuarioController(_repository, _handler);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["ChaveAPI"] = MockSettingsApi.ChaveAPI;
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Usuarios_ListaPreenchida_200OK()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repository.Salvar(usuario1);

            var usuario2 = new SettingsTest().Usuario2;
            _repository.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repository.Salvar(usuario3);

            var query = new ObterUsuarioQuery
            {
                RegistrosFilhos = false
            };

            var response = _controller.Usuarios(query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<UsuarioQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(usuario1.Login, responseObj.Value.Dados[0].Login);
            Assert.AreEqual(usuario1.Senha, responseObj.Value.Dados[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, responseObj.Value.Dados[0].Privilegio);
            Assert.AreEqual(usuario1.Pessoas.Count, responseObj.Value.Dados[0].Pessoas.Count);

            Assert.AreEqual(usuario2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(usuario2.Login, responseObj.Value.Dados[1].Login);
            Assert.AreEqual(usuario2.Senha, responseObj.Value.Dados[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, responseObj.Value.Dados[1].Privilegio);
            Assert.AreEqual(usuario2.Pessoas.Count, responseObj.Value.Dados[1].Pessoas.Count);

            Assert.AreEqual(usuario3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(usuario3.Login, responseObj.Value.Dados[2].Login);
            Assert.AreEqual(usuario3.Senha, responseObj.Value.Dados[2].Senha);
            Assert.AreEqual(usuario3.Privilegio, responseObj.Value.Dados[2].Privilegio);
            Assert.AreEqual(usuario3.Pessoas.Count, responseObj.Value.Dados[2].Pessoas.Count);
        }

        [Test]
        public void Usuarios_ContendoRegistrosFilhos_ListaPreenchida_200OK()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repository.Salvar(usuario1);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            usuario1.AdicionarPessoa(pessoa1);
            usuario1.AdicionarPessoa(pessoa2);
            usuario1.AdicionarPessoa(pessoa3);

            var usuario2 = new SettingsTest().Usuario2;
            _repository.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repository.Salvar(usuario3);

            var query = new ObterUsuarioQuery
            {
                RegistrosFilhos = true
            };

            var response = _controller.Usuarios(query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<UsuarioQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Lista obtida com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario1.Id, responseObj.Value.Dados[0].Id);
            Assert.AreEqual(usuario1.Login, responseObj.Value.Dados[0].Login);
            Assert.AreEqual(usuario1.Senha, responseObj.Value.Dados[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, responseObj.Value.Dados[0].Privilegio);
            Assert.AreEqual(usuario1.Pessoas.Count, responseObj.Value.Dados[0].Pessoas.Count);

            Assert.AreEqual(usuario2.Id, responseObj.Value.Dados[1].Id);
            Assert.AreEqual(usuario2.Login, responseObj.Value.Dados[1].Login);
            Assert.AreEqual(usuario2.Senha, responseObj.Value.Dados[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, responseObj.Value.Dados[1].Privilegio);
            Assert.AreEqual(usuario2.Pessoas.Count, responseObj.Value.Dados[1].Pessoas.Count);

            Assert.AreEqual(usuario3.Id, responseObj.Value.Dados[2].Id);
            Assert.AreEqual(usuario3.Login, responseObj.Value.Dados[2].Login);
            Assert.AreEqual(usuario3.Senha, responseObj.Value.Dados[2].Senha);
            Assert.AreEqual(usuario3.Privilegio, responseObj.Value.Dados[2].Privilegio);
            Assert.AreEqual(usuario3.Pessoas.Count, responseObj.Value.Dados[2].Pessoas.Count);
        }

        [Test]
        public void Usuarios_ListaVazia_204NoContent()
        {
            var query = new ObterUsuarioQuery
            {
                RegistrosFilhos = false
            };

            var response = _controller.Usuarios(query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<List<UsuarioQueryResult>>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(204, responseObj.StatusCode);
        }

        [Test]
        public void Usuario_ObjetoPreenchido_200OK()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repository.Salvar(usuario1);

            var usuario2 = new SettingsTest().Usuario2;
            _repository.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repository.Salvar(usuario3);

            var query = new ObterUsuarioQuery
            {
                RegistrosFilhos = false
            };

            var response = _controller.Usuario(usuario2.Id, query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(usuario2.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(usuario2.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(usuario2.Privilegio, responseObj.Value.Dados.Privilegio);
            Assert.AreEqual(usuario2.Pessoas.Count, responseObj.Value.Dados.Pessoas.Count);
        }

        [Test]
        public void Usuario_ContendoRegistrosFilhos_ObjetoPreenchido_200OK()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repository.Salvar(usuario1);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            usuario1.AdicionarPessoa(pessoa1);
            usuario1.AdicionarPessoa(pessoa2);
            usuario1.AdicionarPessoa(pessoa3);

            var usuario2 = new SettingsTest().Usuario2;
            _repository.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repository.Salvar(usuario3);

            var query = new ObterUsuarioQuery
            {
                RegistrosFilhos = true
            };

            var response = _controller.Usuario(usuario2.Id, query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Registro obtido com sucesso", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario2.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(usuario2.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(usuario2.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(usuario2.Privilegio, responseObj.Value.Dados.Privilegio);
            Assert.AreEqual(usuario2.Pessoas.Count, responseObj.Value.Dados.Pessoas.Count);
        }

        [Test]
        public void Usuario_ObjetoNull_204NoContent()
        {
            var query = new ObterUsuarioQuery
            {
                RegistrosFilhos = false
            };

            var response = _controller.Usuario(1, query);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioQueryResult>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(204, responseObj.StatusCode);
        }

        [Test]
        public void UsuarioInserir_201Created()
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;

            var response = _controller.UsuarioInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(201, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário gravado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(1, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(command.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(command.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [Test]
        public void UsuarioInserir_CommandNull_400BadRequest()
        {
            var response = _controller.UsuarioInserir(null);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(400, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null, null, -1)]
        [TestCase("", "", 0)]
        [TestCase("", "aaaaa1", 0)]
        public void UsuarioInserir_Invalido_422UnprocessableEntity(string login, string senha, EPrivilegioUsuario privilegio)
        {
            var command = new AdicionarUsuarioCommand
            {
                Login = login,
                Senha = senha,
                Privilegio = privilegio
            };

            var response = _controller.UsuarioInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void UsuarioInserir_Login_Invalido_422UnprocessableEntity(string login)
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;
            command.Login = login;

            var response = _controller.UsuarioInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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
        [TestCase("a")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void UsuarioInserir_Senha_Invalido_422UnprocessableEntity(string senha)
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;
            command.Senha = senha;

            var response = _controller.UsuarioInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void UsuarioInserir_Senha_Invalido_422UnprocessableEntity(EPrivilegioUsuario privilegio)
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;
            command.Privilegio = privilegio;

            var response = _controller.UsuarioInserir(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        public void UsuarioAlterar_200OK()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var command = new SettingsTest().UsuarioAtualizarCommand;

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário atualizado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(command.Id, responseObj.Value.Dados.Id);
            Assert.AreEqual(command.Login, responseObj.Value.Dados.Login);
            Assert.AreEqual(command.Senha, responseObj.Value.Dados.Senha);
            Assert.AreEqual(command.Privilegio, responseObj.Value.Dados.Privilegio);
        }

        [Test]
        public void UsuarioAlterar_Command_Null_400BadRequest()
        {
            var response = _controller.UsuarioAlterar(0, null);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(400, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(0, null, null, -1)]
        [TestCase(0, "", "", 0)]
        [TestCase(-1, "", "aaaaa1", 0)]
        public void UsuarioAlterar_Invalido_422UnprocessableEntity(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            var command = new AtualizarUsuarioCommand
            {
                Id = id,
                Login = login,
                Senha = senha,
                Privilegio = privilegio
            };

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(1)]
        public void UsuarioAlterar_Id_NaoCadastrado_422UnprocessableEntity(long id)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Id = id;

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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
        public void UsuarioAlterar_Id_Invalido_422UnprocessableEntity(long id)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Id = id;

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void UsuarioAlterar_Login_Invalido_422UnprocessableEntity(string login)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Login = login;

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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
        [TestCase("a")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void UsuarioAlterar_Senha_Invalido_422UnprocessableEntity(string senha)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Senha = senha;

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void UsuarioAlterar_Privilegio_Invalido_422UnprocessableEntity(EPrivilegioUsuario privilegio)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Privilegio = privilegio;

            var response = _controller.UsuarioAlterar(command.Id, command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        public void UsuarioExcluir_200OK()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var response = _controller.UsuarioExcluir(usuario.Id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);

            Assert.AreEqual(usuario.Id, responseObj.Value.Dados.Id);
        }

        [Test]
        [TestCase(1)]
        public void UsuarioExcluir_Id_NaoCadastrado_422UnprocessableEntity(long id)
        {
            var response = _controller.UsuarioExcluir(id);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<CommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        public void UsuarioLogin_200OK()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var usuarioQR = _repository.Logar(usuario.Login, usuario.Senha);
            var token = _tokenJwtHelper.GenerarTokenJwt(usuarioQR);

            var command = new SettingsTest().UsuarioLoginCommand;
            var response = _controller.UsuarioLogin(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<TokenCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(200, responseObj.StatusCode);
            Assert.True(responseObj.Value.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Erros);
            Assert.AreEqual(token, responseObj.Value.Dados.Token);
        }

        [Test]
        public void UsuarioLogin_Command_Null_400BadRequest()
        {
            var response = _controller.UsuarioLogin(null);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<TokenCommandOutput>>>(responseJson);

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
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void UsuarioLogin_Invalido_422UnprocessableEntity(string login, string senha)
        {
            var command = new LoginUsuarioCommand
            {
                Login = login,
                Senha = senha
            };

            var response = _controller.UsuarioLogin(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void UsuarioLogin_Login_Invalido_422UnprocessableEntity(string login)
        {
            var command = new SettingsTest().UsuarioLoginCommand;
            command.Login = login;

            var response = _controller.UsuarioLogin(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase("loginNaoCadastrado")]
        public void UsuarioLogin_Login_NaoCadastrado_422UnprocessableEntity(string login)
        {
            var command = new SettingsTest().UsuarioLoginCommand;
            command.Login = login;

            var response = _controller.UsuarioLogin(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void UsuarioLogin_Senha_Invalido_422UnprocessableEntity(string senha)
        {
            var command = new SettingsTest().UsuarioLoginCommand;
            command.Senha = senha;

            var response = _controller.UsuarioLogin(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

            TestContext.WriteLine(responseObj.FormatarJsonDeSaida());

            Assert.AreEqual(422, responseObj.StatusCode);
            Assert.False(responseObj.Value.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", responseObj.Value.Mensagem);
            Assert.Null(responseObj.Value.Dados);
            Assert.AreNotEqual(0, responseObj.Value.Erros.Count);
        }

        [Test]
        [TestCase("senhaIncorreta")]
        public void UsuarioLogin_Senha_Incorreta_422UnprocessableEntity(string senha)
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var command = new SettingsTest().UsuarioLoginCommand;
            command.Senha = senha;

            var response = _controller.UsuarioLogin(command);
            var responseJson = JsonConvert.SerializeObject(response);
            var responseObj = JsonConvert.DeserializeObject<ApiTestResponse<ApiResponse<UsuarioCommandOutput>>>(responseJson);

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