using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Commands.Output;
using ControleDespesas.Domain.Pessoas.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;

namespace ControleDespesas.Test.Pessoas.Handlers
{
    [TestFixture]
    public class PessoaHandlerTest : DatabaseTest
    {
        private IUsuarioRepository _repositoryUsuario;
        private IPessoaRepository _repositoryPessoa;
        private IPessoaHandler _handler;

        public PessoaHandlerTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _handler = new PessoaHandler(_repositoryPessoa, _repositoryUsuario);
        }

        [SetUp]
        public void Setup()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
            _handler = new PessoaHandler(_repositoryPessoa, _repositoryUsuario);
        }

        [Test]
        public void Handler_AdicionarPessoa_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoaCommand = new SettingsTest().PessoaAdicionarCommand;

            var retorno = _handler.Handler(pessoaCommand);
            var retornoDados = (PessoaCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa gravada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(1, retornoDados.Id);
            Assert.AreEqual(pessoaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(pessoaCommand.ImagemPerfil, retornoDados.ImagemPerfil);
        }

        [Test]
        public void Handler_AdicionarPessoa_Nulo_Invalido()
        {
            AdicionarPessoaCommand command = null;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(400, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0, null, null)]
        [TestCase(-1, "", "")]
        [TestCase(0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase(0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void Handler_AdicionarPessoa_Invalido(long idUsuario, string nome, string imagemPerfil)
        {
            var command = new AdicionarPessoaCommand
            {
                IdUsuario = idUsuario,
                Nome = nome,
                ImagemPerfil = imagemPerfil
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
        [TestCase(1)]
        public void Handler_AdicionarPessoa_IdUsuario_NaoCadastrado(long idUsuario)
        {
            var command = new SettingsTest().PessoaAdicionarCommand;
            command.IdUsuario = idUsuario;

            var retorno = _handler.Handler(command);

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
        public void Handler_AdicionarPessoa_IdUsuario_Invalido(long idUsuario)
        {
            var command = new SettingsTest().PessoaAdicionarCommand;
            command.IdUsuario = idUsuario;

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
        public void Handler_AdicionarPessoa_Nome_Invalido(string nome)
        {
            var command = new SettingsTest().PessoaAdicionarCommand;
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
        public void Handler_AdicionarPessoa_ImagemPerfil_Invalido(string imagemPerfil)
        {
            var command = new SettingsTest().PessoaAdicionarCommand;
            command.ImagemPerfil = imagemPerfil;

            var retorno = _handler.Handler(command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_AtualizarPessoa_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var pessoaCommand = new SettingsTest().PessoaAtualizarCommand;

            var retorno = _handler.Handler(pessoaCommand.Id, pessoaCommand);
            var retornoDados = (PessoaCommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa atualizada com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pessoaCommand.Id, retornoDados.Id);
            Assert.AreEqual(pessoaCommand.Nome, retornoDados.Nome);
            Assert.AreEqual(pessoaCommand.ImagemPerfil, retornoDados.ImagemPerfil);
        }

        [Test]
        [TestCase(0, 0, null, null)]
        [TestCase(-1, -1, "", "")]
        [TestCase(0, 0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "")]
        [TestCase(0, 0, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null)]
        public void Handler_AtualizarPessoa_Invalido(long id, long idUsuario, string nome, string imagemPerfil)
        {
            var command = new AtualizarPessoaCommand
            {
                Id = id,
                IdUsuario = idUsuario,
                Nome = nome,
                ImagemPerfil = imagemPerfil
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
        public void Handler_AtualizarPessoa_Id_NaoCadastrado(long id)
        {
            var command = new SettingsTest().PessoaAtualizarCommand;
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
        public void Handler_AtualizarPessoa_Id_Invalido(long id)
        {
            var command = new SettingsTest().PessoaAtualizarCommand;
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
        [TestCase(1)]
        public void Handler_AtualizarPessoa_IdUsuario_NaoCadastrado(long idUsuario)
        {
            var command = new SettingsTest().PessoaAtualizarCommand;
            command.IdUsuario = idUsuario;

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
        public void Handler_AtualizarPessoa_IdUsuario_Invalido(long idUsuario)
        {
            var command = new SettingsTest().PessoaAtualizarCommand;
            command.IdUsuario = idUsuario;

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
        public void Handler_AtualizarPessoa_Nome_Invalido(string nome)
        {
            var command = new SettingsTest().PessoaAtualizarCommand;
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
        public void Handler_AtualizarPessoa_ImagemPerfil_Invalido(string imagemPerfil)
        {
            var command = new SettingsTest().PessoaAtualizarCommand;
            command.ImagemPerfil = imagemPerfil;

            var retorno = _handler.Handler(command.Id, command);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Parâmentros inválidos", retorno.Mensagem);
            Assert.Null(retorno.Dados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        public void Handler_ApagarPessoa_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _handler.Handler(pessoa.Id, pessoa.IdUsuario);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retornoDados.FormatarJsonDeSaida());

            Assert.True(retorno.Sucesso);
            Assert.AreEqual("Pessoa excluída com sucesso!", retorno.Mensagem);
            Assert.AreEqual(pessoa.Id, retornoDados.Id);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        public void Handler_ApagarPessoa_Id_NaoCadastrado(long id)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var retorno = _handler.Handler(id, usuario.Id);
            var retornoDados = (CommandOutput)retorno.Dados;

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(422, retorno.StatusCode);
            Assert.False(retorno.Sucesso);
            Assert.AreEqual("Inconsistência(s) no(s) dado(s)", retorno.Mensagem);
            Assert.Null(retornoDados);
            Assert.AreNotEqual(0, retorno.Erros.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(2)]
        public void Handler_ApagarPessoa_IdUsuario_NaoCadastrado(long idUsuario)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa);

            var retorno = _handler.Handler(pessoa.Id, idUsuario);
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