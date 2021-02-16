using ControleDespesas.Domain.Query.Pagamento.Input;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Query.Pagamento
{
    public class PagamentoQueryTest : BaseTest
    {
        private PagamentoQuery _query;

        [SetUp]
        public void Setup() => _query = new SettingsTest().PagamentoQuery;

        [Test]
        public void ValidarCommand_Valido()
        {
            var valido = _query.ValidarQuery();
            var notificacoes = _query.Notificacoes.Count;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_query));

            Assert.True(valido);
            Assert.AreEqual(0, notificacoes);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdPessoaInvalido(int idPessoa)
        {
            _query.IdPessoa = idPessoa;
            var valido = _query.ValidarQuery();
            var notificacoes = _query.Notificacoes.Count;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_query));

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [TearDown]
        public void TearDown() => _query = null;
    }
}