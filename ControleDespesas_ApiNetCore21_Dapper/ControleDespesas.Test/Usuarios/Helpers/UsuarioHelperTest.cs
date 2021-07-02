using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Helpers
{
    public class UsuarioHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarUsuarioCommand()
        {
            var command = new SettingsTest().UsuarioAdicionarCommand;

            var entidade = UsuarioHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login);
            Assert.AreEqual(command.Senha, entidade.Senha);
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarUsuarioCommand()
        {
            var command = new SettingsTest().UsuarioAtualizarCommand;

            var entidade = UsuarioHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login);
            Assert.AreEqual(command.Senha, entidade.Senha);
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new SettingsTest().Usuario1;

            var command = UsuarioHelper.GerarDadosRetornoInsert(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Login, command.Login);
            Assert.AreEqual(entidade.Senha, command.Senha);
            Assert.AreEqual(entidade.Privilegio, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new SettingsTest().Usuario1;

            var command = UsuarioHelper.GerarDadosRetornoUpdate(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Login, command.Login);
            Assert.AreEqual(entidade.Senha, command.Senha);
            Assert.AreEqual(entidade.Privilegio, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetornoDelte()
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