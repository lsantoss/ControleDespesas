using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Pessoas.Helpers
{
    [TestFixture]
    public class PessoaHelperTest : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarPessoaCommand_Valido()
        {
            var command = new SettingsTest().PessoaAdicionarCommand;

            var entidade = PessoaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.IdUsuario, entidade.IdUsuario);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.AreEqual(0, entidade.Pagamentos.Count);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(-1, "", "")]
        [TestCase(0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase(0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void GerarEntidade_AdcionarPessoaCommand_Invalido(long idUsuario, string nome, string imagemPerfil)
        {
            var command = new AdicionarPessoaCommand
            {
                IdUsuario = idUsuario,
                Nome = nome,
                ImagemPerfil = imagemPerfil
            };

            var entidade = PessoaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual(command.IdUsuario, entidade.IdUsuario);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.AreEqual(0, entidade.Pagamentos.Count);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarPessoaCommand_Valido()
        {
            var command = new SettingsTest().PessoaAtualizarCommand;

            var entidade = PessoaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.IdUsuario, entidade.IdUsuario);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.AreEqual(0, entidade.Pagamentos.Count);
            Assert.True(entidade.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        [TestCase(0, 0, null, null)]
        [TestCase(-1, -1, "", "")]
        [TestCase(0, 0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase(0, 0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void GerarEntidade_AtualizarPessoaCommand_Invalido(long id, long idUsuario, string nome, string imagemPerfil)
        {
            var command = new AtualizarPessoaCommand
            {
                Id = id,
                IdUsuario = idUsuario,
                Nome = nome,
                ImagemPerfil = imagemPerfil
            };

            var entidade = PessoaHelper.GerarEntidade(command);

            TestContext.WriteLine(entidade.FormatarJsonDeSaida());

            Assert.AreEqual(command.Id, entidade.Id);
            Assert.AreEqual(command.IdUsuario, entidade.IdUsuario);
            Assert.AreEqual(command.Nome, entidade.Nome);
            Assert.AreEqual(command.ImagemPerfil, entidade.ImagemPerfil);
            Assert.AreEqual(0, entidade.Pagamentos.Count);
            Assert.False(entidade.Valido);
            Assert.AreNotEqual(0, entidade.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetorno_Pessoa()
        {
            var entidade = new SettingsTest().Pessoa1;

            var command = PessoaHelper.GerarDadosRetorno(entidade);

            TestContext.WriteLine(command.FormatarJsonDeSaida());

            Assert.AreEqual(entidade.Id, command.Id);
            Assert.AreEqual(entidade.IdUsuario, command.IdUsuario);
            Assert.AreEqual(entidade.Nome, command.Nome);
            Assert.AreEqual(entidade.ImagemPerfil, command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetorno_Id()
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