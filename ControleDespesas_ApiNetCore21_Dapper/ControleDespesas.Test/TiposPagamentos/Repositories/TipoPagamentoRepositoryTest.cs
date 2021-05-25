using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.TiposPagamentos.Repositories
{
    public class TipoPagamentoRepositoryTest : DatabaseTest
    {
        private readonly ITipoPagamentoRepository _repository;

        public TipoPagamentoRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new TipoPagamentoRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao, retorno.Descricao);
        }

        [Test]
        public void Atualizar()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            tipoPagamento = new SettingsTest().TipoPagamento1Editado;
            _repository.Atualizar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao, retorno.Descricao);
        }

        [Test]
        public void Deletar()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repository.Salvar(tipoPagamento2);

            var tipoPagamento3 = new SettingsTest().TipoPagamento3;
            _repository.Salvar(tipoPagamento3);

            _repository.Deletar(tipoPagamento2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(tipoPagamento1.Id, retorno[0].Id);
            Assert.AreEqual(tipoPagamento1.Descricao, retorno[0].Descricao);

            Assert.AreEqual(tipoPagamento3.Id, retorno[1].Id);
            Assert.AreEqual(tipoPagamento3.Descricao, retorno[1].Descricao);
        }

        [Test]
        public void Obter()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao, retorno.Descricao);
        }

        [Test]
        public void Listar()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repository.Salvar(tipoPagamento2);

            var tipoPagamento3 = new SettingsTest().TipoPagamento3;
            _repository.Salvar(tipoPagamento3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(tipoPagamento1.Id, retorno[0].Id);
            Assert.AreEqual(tipoPagamento1.Descricao, retorno[0].Descricao);

            Assert.AreEqual(tipoPagamento2.Id, retorno[1].Id);
            Assert.AreEqual(tipoPagamento2.Descricao, retorno[1].Descricao);

            Assert.AreEqual(tipoPagamento3.Id, retorno[2].Id);
            Assert.AreEqual(tipoPagamento3.Descricao, retorno[2].Descricao);
        }

        [Test]
        public void CheckId()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var idExistente = _repository.CheckId(tipoPagamento.Id);
            var idNaoExiste = _repository.CheckId(25);

            TestContext.WriteLine(idExistente);
            TestContext.WriteLine(idNaoExiste);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento1);

            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            _repository.Salvar(tipoPagamento2);

            var tipoPagamento3 = new SettingsTest().TipoPagamento3;
            _repository.Salvar(tipoPagamento3);

            var maxId = _repository.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(tipoPagamento3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}