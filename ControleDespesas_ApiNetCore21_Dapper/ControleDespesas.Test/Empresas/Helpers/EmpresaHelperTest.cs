using ControleDespesas.Domain.Empresas.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Empresas.Helpers
{
    public class EmpresaHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarEmpresaCommand()
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
        public void GerarEntidade_AtualizarEmpresaCommand()
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
        public void GerarDadosRetornoInsert()
        {
            var entidade = new SettingsTest().Empresa1;

            var command = EmpresaHelper.GerarDadosRetornoInsert(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome, command.Nome);
            Assert.AreEqual(entidade.Logo, command.Logo);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new SettingsTest().Empresa1;

            var command = EmpresaHelper.GerarDadosRetornoUpdate(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome, command.Nome);
            Assert.AreEqual(entidade.Logo, command.Logo);
        }

        [Test]
        public void GerarDadosRetornoDelete()
        {
            var entidade = new SettingsTest().Empresa1;

            var command = EmpresaHelper.GerarDadosRetornoDelete(entidade.Id);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}