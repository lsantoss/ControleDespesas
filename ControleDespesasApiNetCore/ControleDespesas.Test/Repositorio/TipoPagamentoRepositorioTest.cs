using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Factory;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class TipoPagamentoRepositorioTest : DatabaseFactory
    {
        private readonly TipoPagamentoRepositorio _repository;

        public TipoPagamentoRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            _repository = new TipoPagamentoRepositorio(Options.Create(MockSettingsInfraData));
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var tipoPagamento = MockSettingsTest.TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Atualizar()
        {
            var tipoPagamento = MockSettingsTest.TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            tipoPagamento = MockSettingsTest.TipoPagamento1Editado;
            _repository.Atualizar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Deletar()
        {
            var tipoPagamento1 = MockSettingsTest.TipoPagamento1;
            var tipoPagamento2 = MockSettingsTest.TipoPagamento2;
            var tipoPagamento3 = MockSettingsTest.TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            _repository.Deletar(tipoPagamento2.Id);

            var retorno = _repository.Listar();

            Assert.AreEqual(tipoPagamento1.Id, retorno[0].Id);
            Assert.AreEqual(tipoPagamento1.Descricao.ToString(), retorno[0].Descricao);

            Assert.AreEqual(tipoPagamento3.Id, retorno[1].Id);
            Assert.AreEqual(tipoPagamento3.Descricao.ToString(), retorno[1].Descricao);
        }

        [Test]
        public void Obter()
        {
            var tipoPagamento = MockSettingsTest.TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Listar()
        {
            var tipoPagamento1 = MockSettingsTest.TipoPagamento1;
            var tipoPagamento2 = MockSettingsTest.TipoPagamento2;
            var tipoPagamento3 = MockSettingsTest.TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            var retorno = _repository.Listar();

            Assert.AreEqual(tipoPagamento1.Id, retorno[0].Id);
            Assert.AreEqual(tipoPagamento1.Descricao.ToString(), retorno[0].Descricao);

            Assert.AreEqual(tipoPagamento2.Id, retorno[1].Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), retorno[1].Descricao);

            Assert.AreEqual(tipoPagamento3.Id, retorno[2].Id);
            Assert.AreEqual(tipoPagamento3.Descricao.ToString(), retorno[2].Descricao);
        }

        [Test]
        public void CheckId()
        {
            var tipoPagamento = MockSettingsTest.TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var idExistente = _repository.CheckId(tipoPagamento.Id);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var tipoPagamento1 = MockSettingsTest.TipoPagamento1;
            var tipoPagamento2 = MockSettingsTest.TipoPagamento2;
            var tipoPagamento3 = MockSettingsTest.TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            var maxId = _repository.LocalizarMaxId();

            Assert.AreEqual(tipoPagamento3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}