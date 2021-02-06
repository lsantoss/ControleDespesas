using ControleDespesas.Domain.Interfaces.Repositorio;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class TipoPagamentoRepositorioTest : DatabaseTest
    {
        private readonly ITipoPagamentoRepositorio _repository;

        public TipoPagamentoRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repository = new TipoPagamentoRepositorio(optionsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var tipoPagamento = new SettingsTest().TipoPagamento1;
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(tipoPagamento.Id);

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao, retorno.Descricao);
        }

        [Test]
        public void Deletar()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            var tipoPagamento3 = new SettingsTest().TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            _repository.Deletar(tipoPagamento2.Id);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao, retorno.Descricao);
        }

        [Test]
        public void Listar()
        {
            var tipoPagamento1 = new SettingsTest().TipoPagamento1;
            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            var tipoPagamento3 = new SettingsTest().TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            var retorno = _repository.Listar();

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retorno));

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
            var tipoPagamento2 = new SettingsTest().TipoPagamento2;
            var tipoPagamento3 = new SettingsTest().TipoPagamento3;

            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);
            _repository.Salvar(tipoPagamento3);

            var maxId = _repository.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(tipoPagamento3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}