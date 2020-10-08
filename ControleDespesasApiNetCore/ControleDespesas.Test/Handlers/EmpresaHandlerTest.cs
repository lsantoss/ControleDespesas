using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Util;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    public class EmpresaHandlerTest : DatabaseTest
    {
        private readonly EmpresaRepositorio _repository;
        private readonly EmpresaHandler _handler;

        public EmpresaHandlerTest()
        {
            CriarBaseDeDadosETabelas();
            var optionsInfraData = Options.Create(MockSettingsInfraData);

            _repository = new EmpresaRepositorio(optionsInfraData);
            _handler = new EmpresaHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarEmpresa()
        {
            var empresaCommand = MockSettingsTest.EmpresaAdicionarCommand;

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (AdicionarEmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_AtualizarEmpresa()
        {
            var empresaCommand = MockSettingsTest.EmpresaAtualizarCommand;

            var empresa = MockSettingsTest.Empresa1;
            _repository.Salvar(empresa);

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (AtualizarEmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_ApagarEmpresa()
        {
            var empresaCommand = MockSettingsTest.EmpresaApagarCommand;

            var empresa = MockSettingsTest.Empresa1;
            _repository.Salvar(empresa);

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (ApagarEmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(FotmatadorJson.FormatarJsonDeSaida(retornoDados));

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}