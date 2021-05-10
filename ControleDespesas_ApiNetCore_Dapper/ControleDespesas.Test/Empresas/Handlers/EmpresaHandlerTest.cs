using ControleDespesas.Domain.Empresas.Commands.Output;
using ControleDespesas.Domain.Empresas.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Empresas.Handlers
{
    public class EmpresaHandlerTest : DatabaseTest
    {
        private readonly IEmpresaRepository _repository;
        private readonly IEmpresaHandler _handler;

        public EmpresaHandlerTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new EmpresaRepository(MockSettingsInfraData);
            _handler = new EmpresaHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarEmpresa()
        {
            var empresaCommand = new SettingsTest().EmpresaAdicionarCommand;
            var retorno = _handler.Handler(empresaCommand);
            var retornoDados = (EmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_AtualizarEmpresa()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var empresaCommand = new SettingsTest().EmpresaAtualizarCommand;
            var retorno = _handler.Handler(empresa.Id, empresaCommand);
            var retornoDados = (EmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_ApagarEmpresa()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var retorno = _handler.Handler(empresa.Id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresa.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}