﻿using ControleDespesas.Domain.Usuarios.Commands.Output;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Domain.Usuarios.Handlers;
using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Handlers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Query.Results;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Handlers
{
    class UsuarioHandlerTest : DatabaseTest
    {
        private readonly ITokenJwtHelper _tokenJwtHelper;
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioHandler _handler;

        public UsuarioHandlerTest()
        {
            CriarBaseDeDadosETabelas();

            _tokenJwtHelper = new TokenJwtHelper(MockSettingsAPI);
            _repository = new UsuarioRepository(MockSettingsInfraData);
            _handler = new UsuarioHandler(_repository, _tokenJwtHelper);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarUsuario()
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
        public void Handler_AtualizarUsuario()
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
        public void Handler_ApagarUsuario()
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
        public void Handler_LoginUsuario()
        {
            var usuario = new SettingsTest().Usuario1;
            _repository.Salvar(usuario);

            var usuarioCommand = new SettingsTest().UsuarioLoginCommand;
            var retorno = _handler.Handler(usuarioCommand);
            var retornoDados = (UsuarioTokenQueryResult)retorno.Dados;

            var usuarioQR = _repository.Logar(usuarioCommand.Login, usuarioCommand.Senha);
            var token = _tokenJwtHelper.GenerarTokenJwt(usuarioQR);

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Usuário logado com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(usuarioCommand.Login, retornoDados.Login);
            Assert.AreEqual(usuarioCommand.Senha, retornoDados.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Administrador, retornoDados.Privilegio);
            Assert.AreEqual(token, retornoDados.Token);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}