using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Empresas.Helpers
{
    [TestFixture]
    public class EmpresaHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarEmpresaCommand_Valido()
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;

            var entidade = EmpresaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.Logo, entidade.Logo);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        public void GerarEntidade_AdcionarEmpresaCommand_Invalido(string nome, string logo)
        {
            var command = new AdicionarEmpresaCommand 
            { 
                Nome = nome,
                Logo = logo
            };

            var entidade = EmpresaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.Logo, entidade.Logo);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarEmpresaCommand_Valido()
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;

            var entidade = EmpresaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.Logo, entidade.Logo);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(0, "", "")]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        public void GerarEntidade_AtualizarEmpresaCommand_Invalido(long id, string nome, string logo)
        {
            var command = new AtualizarEmpresaCommand
            {
                Id = id,
                Nome = nome,
                Logo = logo
            };

            var entidade = EmpresaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.Logo, entidade.Logo);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetorno_Empresa()
        {
            var entidade = new SettingsTest().Empresa1;

            var command = EmpresaHelper.GerarDadosRetorno(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome, command.Nome);
            Assert.AreEqual(entidade.Logo, command.Logo);
        }

        [Test]
        public void GerarDadosRetorno_Id()
        {
            var entidade = new SettingsTest().Empresa1;

            var command = EmpresaHelper.GerarDadosRetorno(entidade.Id);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}