using ControleDespesas.Domain.Entities;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Entities
{
    public class PagamentoTest : BaseTest
    {
        private Pagamento _pagamento;

        [SetUp]
        public void Setup() => _pagamento = new SettingsTest().Pagamento1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.True(_pagamento.Valido);
            Assert.AreEqual(0, _pagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarEntidade_IdTipoPagamentoInvalido(int idTipoPagamento)
        {
            _pagamento.TipoPagamento.Id = idTipoPagamento;
            _pagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.False(_pagamento.Valido);
            Assert.AreNotEqual(0, _pagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarEntidade_IdEmpresaInvalido(int idEmpresa)
        {
            _pagamento.Empresa.DefinirId(idEmpresa);
            _pagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.False(_pagamento.Valido);
            Assert.AreNotEqual(0, _pagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarEntidade_IdPesssoaInvalido(int idPessoa)
        {
            _pagamento.Pessoa.DefinirId(idPessoa);
            _pagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.False(_pagamento.Valido);
            Assert.AreNotEqual(0, _pagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ValidarEntidade_DescricaoInvalida(string descricao)
        {
            _pagamento.Descricao = descricao;
            _pagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.False(_pagamento.Valido);
            Assert.AreNotEqual(0, _pagamento.Notificacoes.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidarEntidade_ValorInvalido(double valor)
        {
            _pagamento.Valor = valor;
            _pagamento.Validar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(_pagamento));

            Assert.False(_pagamento.Valido);
            Assert.AreNotEqual(0, _pagamento.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _pagamento = null;
    }
}