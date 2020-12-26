using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Pagamento
{
    public class ObterArquivoComprovanteCommandTest : BaseTest
    {
        private ObterArquivoComprovanteCommand _command;

        [SetUp]
        public void Setup() => _command = new SettingsTest().PagamentoObterArquivoComprovanteCommand;

        [Test]
        public void ValidarCommand_Valido()
        {
            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.True(valido);
            Assert.AreEqual(0, notificacoes);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdPagamentoInvalido(int idPagamento)
        {
            _command.IdPagamento = idPagamento;
            var valido = _command.ValidarCommand();
            var notificacoes = _command.Notificacoes.Count;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_command));

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}