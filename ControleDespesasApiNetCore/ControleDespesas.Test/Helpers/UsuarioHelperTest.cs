using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Test.AppConfigurations.Settings;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class UsuarioHelperTest
    {
        private readonly SettingsTest _settingsTest;

        public UsuarioHelperTest() => _settingsTest = new SettingsTest();

        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarUsuarioCommand()
        {
            var command = _settingsTest.UsuarioAdicionarCommand;

            var entidade = UsuarioHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login.ToString());
            Assert.AreEqual(command.Senha, entidade.Senha.ToString());
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.True(entidade.Valido); 
            Assert.True(entidade.Login.Valido); 
            Assert.True(entidade.Senha.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Login.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Senha.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarUsuarioCommand()
        {
            var command = _settingsTest.UsuarioAtualizarCommand;

            var entidade = UsuarioHelper.GerarEntidade(command);

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Login, entidade.Login.ToString());
            Assert.AreEqual(command.Senha, entidade.Senha.ToString());
            Assert.AreEqual(command.Privilegio, entidade.Privilegio);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Login.Valido);
            Assert.True(entidade.Senha.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Login.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Senha.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = _settingsTest.Usuario1;

            var command = UsuarioHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Login.ToString(), command.Login);
            Assert.AreEqual(entidade.Senha.ToString(), command.Senha);
            Assert.AreEqual(entidade.Privilegio, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = _settingsTest.Usuario1;

            var command = UsuarioHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Login.ToString(), command.Login);
            Assert.AreEqual(entidade.Senha.ToString(), command.Senha);
            Assert.AreEqual(entidade.Privilegio, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = _settingsTest.Empresa1;
            var command = UsuarioHelper.GerarDadosRetornoDelete(entidade.Id);
            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}