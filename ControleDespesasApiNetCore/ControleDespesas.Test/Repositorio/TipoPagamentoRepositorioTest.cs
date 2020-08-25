using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.TipoPagamento;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControleDespesas.Test.Repositorio
{
    public class TipoPagamentoRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento);

            TipoPagamentoQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Atualizar()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento);

            tipoPagamento = new TipoPagamento(1, new Texto("DescriçãoTipoPagamento - Editada", "Descrição", 250));
            repository.Atualizar(tipoPagamento);

            TipoPagamentoQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(tipoPagamento.Id, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Deletar()
        {
            TipoPagamento tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            TipoPagamento tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            TipoPagamento tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento0);
            repository.Salvar(tipoPagamento1);
            repository.Salvar(tipoPagamento2);

            repository.Deletar(2);

            List<TipoPagamentoQueryResult> retorno = repository.Listar();

            Assert.AreEqual(1, retorno[0].Id);
            Assert.AreEqual(tipoPagamento0.Descricao.ToString(), retorno[0].Descricao);

            Assert.AreEqual(3, retorno[1].Id);
            Assert.AreEqual(tipoPagamento2.Descricao.ToString(), retorno[1].Descricao);
        }

        [Test]
        public void Obter()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento);

            TipoPagamentoQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(tipoPagamento.Descricao.ToString(), retorno.Descricao);
        }

        [Test]
        public void Listar()
        {
            TipoPagamento tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            TipoPagamento tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            TipoPagamento tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento0);
            repository.Salvar(tipoPagamento1);
            repository.Salvar(tipoPagamento2);

            List<TipoPagamentoQueryResult> retorno = repository.Listar();

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
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento);

            bool idExistente = repository.CheckId(1);
            bool idNaoExiste = repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            TipoPagamento tipoPagamento0 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento0", "Descrição", 250));
            TipoPagamento tipoPagamento1 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento1", "Descrição", 250));
            TipoPagamento tipoPagamento2 = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento2", "Descrição", 250));

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repository = new TipoPagamentoRepositorio(mockOptions.Object);
            repository.Salvar(tipoPagamento0);
            repository.Salvar(tipoPagamento1);
            repository.Salvar(tipoPagamento2);

            int maxId = repository.LocalizarMaxId();

            Assert.AreEqual(3, maxId);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}