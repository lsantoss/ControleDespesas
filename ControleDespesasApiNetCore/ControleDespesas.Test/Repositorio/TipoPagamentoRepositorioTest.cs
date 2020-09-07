using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Repositorio
{
    public class TipoPagamentoRepositorioTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly TipoPagamentoRepositorio _repository;

        public TipoPagamentoRepositorioTest()
        {
            CriarBaseDeDadosETabelas();
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new TipoPagamentoRepositorio(_mockOptions.Object);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Atualizar()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            _repository.Salvar(tipoPagamento);

            tipoPagamento = new TipoPagamento(1, new Texto("DescriçãoTipoPagamento - Editada", "Descrição", 250));
            _repository.Atualizar(tipoPagamento);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Deletar()
        {
            var tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            var tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            var tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            _repository.Salvar(tipoPagamento0);
            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);

            _repository.Deletar(2);

            var retorno = _repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(tipoPagamento0.Descricao.ToString(), retorno[0].Descricao);

            Assert.AreEqual(3, retorno[1].Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), retorno[1].Descricao);
        }

        [Test]
        public void Obter()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            _repository.Salvar(tipoPagamento);

            var retorno = _repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Listar()
        {
            var tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            var tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            var tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            _repository.Salvar(tipoPagamento0);
            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);

            var retorno = _repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(tipoPagamento0.Descricao.ToString(), retorno[0].Descricao);

            Assert.AreEqual(2, retorno[1].Id);
            Assert.AreEqual(tipoPagamento1.Descricao.ToString(), retorno[1].Descricao);

            Assert.AreEqual(3, retorno[2].Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), retorno[2].Descricao);
        }

        [Test]
        public void CheckId()
        {
            var tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            _repository.Salvar(tipoPagamento);

            var idExistente = _repository.CheckId(1);
            var idNaoExiste = _repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            var tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            var tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            _repository.Salvar(tipoPagamento0);
            _repository.Salvar(tipoPagamento1);
            _repository.Salvar(tipoPagamento2);

            var maxId = _repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}