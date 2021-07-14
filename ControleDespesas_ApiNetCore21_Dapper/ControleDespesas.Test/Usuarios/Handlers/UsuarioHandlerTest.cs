using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Commands.Output;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Domain.Usuarios.Handlers;
using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Handlers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Handlers
{
    [TestFixture]
    class UsuarioHandlerTest : DatabaseTest
    {
        private readonly ITokenJwtHelper _tokenJwtHelper;
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioHandler _handler;

        public UsuarioHandlerTest()
        {
            CriarBaseDeDadosETabelas();

            _tokenJwtHelper = new TokenJwtHelper(MockSettingsApi);
            _repository = new UsuarioRepository(MockSettingsInfraData);
            _handler = new UsuarioHandler(_repository, _tokenJwtHelper);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarUsuario_Valido()
        {
            var usuarioCommand = new SettingsTest().UsuarioAdicionarCommand;
            var retorno = _handler.Handler(usuarioCommand);
            var retornoDados = (UsuarioCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(usuarioCommand.Privilegio, retornoDados.Privilegio);
        }

        [Test]
        public void Handler_AdicionarUsuario_Nulo_Invalido()
        {
            AdicionarUsuarioCommand command = null;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(400, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null, null, -1)]
        [TestCase("", "", 0)]
        [TestCase("", "aaaaa1", 0)]
        public void Handler_AdicionarUsuario_Invalido(string login, string senha, EPrivilegioUsuario privilegio)
        {
            var command = new AdicionarUsuarioCommand
            {
                Login = login,
                Senha = senha,
                Privilegio = privilegio
            };

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AdicionarUsuario_Login_Invalido(string login)
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;
            command.Login = login;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void Handler_AdicionarUsuario_Senha_Invalido(string senha)
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;
            command.Senha = senha;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void Handler_AdicionarUsuario_Privilegio_Invalido(EPrivilegioUsuario privilegio)
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;
            command.Privilegio = privilegio;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_AtualizarUsuario_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var usuarioCommand = new SettingsTest().UsuarioAtualizarCommand;

            var retorno = _handler.Handler(usuarioCommand.Id, usuarioCommand);
            var retornoDados = (UsuarioCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuarioCommand.Id, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(usuarioCommand.Privilegio, retornoDados.Privilegio);
        }

        [Test]
        public void Handler_AtualizarUsuario_Nulo_Invalido()
        {
            AtualizarUsuarioCommand command = null;

            var retorno = _handler.Handler(0, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(400, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0, null, null, -1)]
        [TestCase(0, "", "", 0)]
        [TestCase(-1, "", "aaaaa1", 0)]
        public void Handler_AtualizarUsuario_Invalido(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            var command = new AtualizarUsuarioCommand
            {
                Id = id,
                Login = login,
                Senha = senha,
                Privilegio = privilegio
            };

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(1)]
        public void Handler_AtualizarUsuario_Id_NaoCadastrado(long id)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Id = id;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Handler_AtualizarUsuario_Id_Invalido(long id)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Id = id;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AtualizarUsuario_Login_Invalido(string login)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Login = login;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("1Aaaaaaaaaaaaaaa")]
        [TestCase("aaaaa1")]
        [TestCase("AAAAA1")]
        [TestCase("AAAAAa")]
        public void Handler_AtualizarUsuario_Senha_Invalido(string senha)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Senha = senha;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void Handler_AtualizarUsuario_Privilegio_Invalido(EPrivilegioUsuario privilegio)
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;
            command.Privilegio = privilegio;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_ApagarUsuario_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var retorno = _handler.Handler(usuario.Id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuario.Id, retornoDados.Id);
        }

        [Test]
        [TestCase(1)]
        public void Handler_ApagarUsuario_Id_NaoCadastrado(long id)
        {
            var retorno = _handler.Handler(id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retornoDados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_LoginUsuario_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var usuarioCommand = new SettingsTest().UsuarioLoginCommand;

            var retorno = _handler.Handler(usuarioCommand);
            var retornoDados = (TokenCommandOutput)retorno.Dados;

            var usuarioQR = _repository.Logar(usuarioCommand.Login, usuarioCommand.Senha);
            var token = _tokenJwtHelper.GenerarTokenJwt(usuarioQR);

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(token, retornoDados.Token);
        }

        [Test]
        public void Handler_LoginUsuario_Nulo_Invalido()
        {
            LoginUsuarioCommand command = null;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(400, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void Handler_LoginUsuario_Invalido(string login, string senha)
        {
            var command = new LoginUsuarioCommand
            {
                Login = login,
                Senha = senha
            };

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_LoginUsuario_Login_Invalido(string login)
        {
            var command = new SettingsTest().UsuarioLoginCommand;
            command.Login = login;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Handler_LoginUsuario_Senha_Invalido(string senha)
        {
            var command = new SettingsTest().UsuarioLoginCommand;
            command.Senha = senha;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase("loginNaoCadastrado")]
        public void Handler_LoginUsuario_Login_NaoCadastrado_Invalido(string login)
        {
            var command = new SettingsTest().UsuarioLoginCommand;
            command.Login = login;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase("senhaIncorreta")]
        public void Handler_LoginUsuario_Senha_Incorreta_Invalido(string senha)
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var command = new SettingsTest().UsuarioLoginCommand;
            command.Senha = senha;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}