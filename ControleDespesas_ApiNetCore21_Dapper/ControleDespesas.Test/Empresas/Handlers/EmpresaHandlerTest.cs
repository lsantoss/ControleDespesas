using ControleDespesas.Domain.Empresas.Commands.Input;
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
        public void Handler_AdicionarEmpresa_Valido()
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;

            var retorno = _handler.Handler(command);
            var retornoDados = (EmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(201, retorno.StatusCode);
            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(command.Nome, retornoDados.Nome);
            Assert.AreEqual(command.Logo, retornoDados.Logo);
            Assert.Null(retorno.Erros);
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        public void Handler_AdicionarEmpresa_Invalido(string nome, string logo)
        {
            var command = new AdicionarEmpresaCommand
            {
                Nome = nome,
                Logo = logo
            };

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AdicionarEmpresa_Nome_Invalido(string nome)
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;
            command.Nome = nome;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Handler_AdicionarEmpresa_Logo_Invalido(string logo)
        {
            var command = new SettingsTest().EmpresaAdicionarCommand;
            command.Logo = logo;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_AtualizarEmpresa_Valido()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var command = new SettingsTest().EmpresaAtualizarCommand;

            var retorno = _handler.Handler(empresa.Id, command);
            var retornoDados = (EmpresaCommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(200, retorno.StatusCode);
            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(command.Id, retornoDados.Id);
            Assert.AreEqual(command.Nome, retornoDados.Nome);
            Assert.AreEqual(command.Logo, retornoDados.Logo);
            Assert.Null(retorno.Erros);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(0, "", "")]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        public void Handler_AtualizarEmpresa_Invalido(long id, string nome, string logo)
        {
            var command = new AtualizarEmpresaCommand
            {
                Id = id,
                Nome = nome,
                Logo = logo
            };

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(1)]
        public void Handler_AtualizarEmpresa_Id_NaoCadastrado(long id)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Id = id;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Handler_AtualizarEmpresa_Id_Invalido(long id)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Id = id;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Handler_AtualizarEmpresa_Nome_Invalido(string nome)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Nome = nome;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Handler_AtualizarEmpresa_Logo_Invalido(string logo)
        {
            var command = new SettingsTest().EmpresaAtualizarCommand;
            command.Logo = logo;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_ApagarEmpresa_Valido()
        {
            var empresa = new SettingsTest().Empresa1;
            _repository.Salvar(empresa);

            var retorno = _handler.Handler(empresa.Id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(200, retorno.StatusCode);
            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Empresa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(empresa.Id, retornoDados.Id);
            Assert.Null(retorno.Erros);
        }

        [Test]
        [TestCase(1)]
        public void Handler_ApagarEmpresa_Id_NaoCadastrado(long id)
        {
            var retorno = _handler.Handler(id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retornoDados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}