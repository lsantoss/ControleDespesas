using ControleDespesas.Domain.Query.Pessoa.Input;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Query.Pessoa
{
    public class ObterPessoasQueryTest : BaseTest
    {
        private ObterPessoasQuery _query;

        [SetUp]
        public void Setup() => _query = new SettingsTest().PessoaObterQuery;

        [Test]
        public void ValidarCommand_Valido()
        {
            var valido = _query.ValidarQuery();
            var notificacoes = _query.Notificacoes.Count;

            TestContext.WriteLine(_query.FormatarJsonDeSaida());

            Assert.True(valido);
            Assert.AreEqual(0, notificacoes);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarCommand_IdUsuarioInvalido(int idUsuario)
        {
            _query.IdUsuario = idUsuario;
            var valido = _query.ValidarQuery();
            var notificacoes = _query.Notificacoes.Count;

            TestContext.WriteLine(_query.FormatarJsonDeSaida());

            Assert.False(valido);
            Assert.AreNotEqual(0, notificacoes);
        }

        [TearDown]
        public void TearDown() => _query = null;
    }
}