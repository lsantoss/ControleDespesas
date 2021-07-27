using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Helpers
{
    [TestFixture]
    public class UsuarioHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarUsuarioCommand_Valido()
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;

            var entidade = UsuarioHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login);
            Assert.AreEqual(command.Senha, entidade.Senha);
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.AreEqual(0, entidade.Pessoas.Count);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(null, null, -1)]
        [TestCase("", "", 0)]
        [TestCase("", "aaaaa1", 0)]
        public void GerarEntidade_AdcionarUsuarioCommand_Invalido(string login, string senha, EPrivilegioUsuario privilegio)
        {
            var command = new AdicionarUsuarioCommand
            {
                Login = login,
                Senha = senha,
                Privilegio = privilegio
            };

            var entidade = UsuarioHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login);
            Assert.AreEqual(command.Senha, entidade.Senha);
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.AreEqual(0, entidade.Pessoas.Count);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarUsuarioCommand_Valido()
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;

            var entidade = UsuarioHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login);
            Assert.AreEqual(command.Senha, entidade.Senha);
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.AreEqual(0, entidade.Pessoas.Count);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null, null, -1)]
        [TestCase(0, "", "", 0)]
        [TestCase(-1, "", "aaaaa1", 0)]
        public void GerarEntidade_AtualizarUsuarioCommand_Invalido(long id, string login, string senha, EPrivilegioUsuario privilegio)
        {
            var command = new AtualizarUsuarioCommand
            {
                Id = id,
                Login = login,
                Senha = senha,
                Privilegio = privilegio
            };

            var entidade = UsuarioHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login);
            Assert.AreEqual(command.Senha, entidade.Senha);
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.AreEqual(0, entidade.Pessoas.Count);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetorno_Usuario()
        {
            var entidade = new SettingsTest().Usuario1;

            var command = UsuarioHelper.GerarDadosRetorno(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Login, command.Login);
            Assert.AreEqual(entidade.Senha, command.Senha);
            Assert.AreEqual(entidade.Privilegio, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetorno_Id()
        {
            var entidade = new SettingsTest().Usuario1;

            var command = UsuarioHelper.GerarDadosRetornoDelete(entidade.Id);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}