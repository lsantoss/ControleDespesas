using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class PessoaHelperTest
    {
        private readonly SettingsTest _settingsTest;

        public PessoaHelperTest() => _settingsTest = new SettingsTest();

        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarPessoaCommand()
        {
            var command = _settingsTest.PessoaAdicionarCommand;

            var entidade = PessoaHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome.ToString());
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarPessoaCommand()
        {
            var command = _settingsTest.PessoaAtualizarCommand;

            var entidade = PessoaHelper.GerarEntidade(command);

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.Nome, entidade.Nome.ToString());
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = _settingsTest.Pessoa1;

            var command = PessoaHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome.ToString(), command.Nome);
            Assert.AreEqual(entidade.ImagemPerfil, command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = _settingsTest.Pessoa1;

            var command = PessoaHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.Nome.ToString(), command.Nome);
            Assert.AreEqual(entidade.ImagemPerfil, command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var entidade = _settingsTest.Pessoa1;
            var command = PessoaHelper.GerarDadosRetornoDelete(entidade.Id);
            Assert.AreEqual(entidade.Id, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}