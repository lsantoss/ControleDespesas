using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Factory;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    class UsuarioHandlerTest : DatabaseFactory
    {
        private readonly UsuarioRepositorio _repository;
        private readonly UsuarioHandler _handler;

        public UsuarioHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repository = new UsuarioRepositorio(optionsInfraData);
            _handler = new UsuarioHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarUsuario()
        {
            var usuarioCommand = MockSettingsTest.UsuarioAdicionarCommand;

            var retorno = _handler.Handler(usuarioCommand);

            var retornoDados = (AdicionarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário gravado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(usuarioCommand.Privilegio, retornoDados.Privilegio);
        }

        [Test]
        public void Handler_AtualizarUsuario()
        {
            var usuario = MockSettingsTest.Usuario1;

            var usuarioCommand = MockSettingsTest.UsuarioAtualizarCommand;

            _repository.Salvar(usuario);

            var retorno = _handler.Handler(usuarioCommand);

            var retornoDados = (AtualizarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário atualizado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuarioCommand.Id, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(usuarioCommand.Privilegio, retornoDados.Privilegio);
        }

        [Test]
        public void Handler_ApagarUsuario()
        {
            var usuario = MockSettingsTest.Usuario1;
            _repository.Salvar(usuario);

            var usuarioCommand = MockSettingsTest.UsuarioApagarCommand;

            var retorno = _handler.Handler(usuarioCommand);
            var retornoDados = (ApagarUsuarioCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário excluído com sucesso!", retorno.Mensagem);
            Assert.AreEqual(usuarioCommand.Id, retornoDados.Id);
        }

        [Test]
        public void Handler_LoginUsuario()
        {
            var usuario = MockSettingsTest.Usuario1;

            var usuarioCommand = MockSettingsTest.UsuarioLoginCommand;

            _repository.Salvar(usuario);

            var retorno = _handler.Handler(usuarioCommand);

            var retornoDados = (UsuarioQueryResult)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Admin, retornoDados.Privilegio);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}