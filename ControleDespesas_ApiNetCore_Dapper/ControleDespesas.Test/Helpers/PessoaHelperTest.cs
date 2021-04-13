using ControleDespesas.Domain.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class PessoaHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarPessoaCommand()
        {
            var command = new SettingsTest().PessoaAdicionarCommand;

            var entidade = PessoaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.IdUsuario, entidade.Usuario.Id);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarPessoaCommand()
        {
            var command = new SettingsTest().PessoaAtualizarCommand;

            var entidade = PessoaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.IdUsuario, entidade.Usuario.Id);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new SettingsTest().Pessoa1;

            var command = PessoaHelper.GerarDadosRetornoInsert(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Usuario.Id, command.IdUsuario);
            Assert.AreEqual(entidade.Nome, command.Nome);
            Assert.AreEqual(entidade.ImagemPerfil, command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new SettingsTest().Pessoa1;

            var command = PessoaHelper.GerarDadosRetornoUpdate(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Usuario.Id, command.IdUsuario);
            Assert.AreEqual(entidade.Nome, command.Nome);
            Assert.AreEqual(entidade.ImagemPerfil, command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = new SettingsTest().Pessoa1;

            var command = PessoaHelper.GerarDadosRetornoDelete(entidade.Id);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}