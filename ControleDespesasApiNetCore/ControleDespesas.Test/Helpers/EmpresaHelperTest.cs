using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Test.AppConfigurations.Factory;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class EmpresaHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarEmpresaCommand()
        {
            var command = MockSettingsTest.EmpresaAdicionarCommand;

            var entidade = EmpresaHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome.ToString());
            Assert.AreEqual(command.Logo, entidade.Logo); 
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarEmpresaCommand()
        {
            var command = MockSettingsTest.EmpresaAtualizarCommand;

            var entidade = EmpresaHelper.GerarEntidade(command);

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome.ToString());
            Assert.AreEqual(command.Logo, entidade.Logo);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = MockSettingsTest.Empresa1;

            var command = EmpresaHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome.ToString(), command.Nome);
            Assert.AreEqual(entidade.Logo, command.Logo);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = MockSettingsTest.Empresa1;

            var command = EmpresaHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome.ToString(), command.Nome);
            Assert.AreEqual(entidade.Logo, command.Logo);
        }

        [Test]
        public void GerarDadosRetornoDelete()
        {
            var entidade = MockSettingsTest.Empresa1;
            var command = EmpresaHelper.GerarDadosRetornoDelete(entidade.Id);
            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}