using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Handlers;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesas.Test.Handlers
{
    public class EmpresaHandlerTest : DatabaseFactory
    {
        private readonly Mock<IOptions<SettingsInfraData>> _mockOptions = new Mock<IOptions<SettingsInfraData>>();
        private readonly EmpresaRepositorio _repository;
        private readonly EmpresaHandler _handler;

        public EmpresaHandlerTest()
        {
            _mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);
            _repository = new EmpresaRepositorio(_mockOptions.Object);
            _handler = new EmpresaHandler(_repository);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Handler_AdicionarEmpresa()
        {
            var empresaCommand = new AdicionarEmpresaCommand()
            {
                Nome = "NomeEmpresa",
                Logo = "LogoEmpresa"
            };

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (AdicionarEmpresaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_AtualizarEmpresa()
        {
            var empresaCommand = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "NomeEmpresa - Editada",
                Logo = "LogoEmpresa - Editado"
            };

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            _repository.Salvar(empresa);

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (AtualizarEmpresaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
            Assert.AreEqual(empresaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(empresaCommand.Logo, retornoDados.Logo);
        }

        [Test]
        public void Handler_ApagarEmpresa()
        {
            var empresaCommand = new ApagarEmpresaCommand() { Id = 1 };

            var empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            _repository.Salvar(empresa);

            var retorno = _handler.Handler(empresaCommand);

            var retornoDados = (ApagarEmpresaCommandOutput)retorno.Dados;

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresaCommand.Id, retornoDados.Id);
        }

        [TearDown]
        public void TearDown() => DroparBaseDeDados();
    }
}