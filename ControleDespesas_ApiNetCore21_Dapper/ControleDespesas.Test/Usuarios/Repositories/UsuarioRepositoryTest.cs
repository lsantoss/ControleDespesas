using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using ControleDespesas.Test.AppConfigurations.Util;
using NUnit.Framework;
using System.Data.SqlClient;

namespace ControleDespesas.Test.Usuarios.Repositories
{
    [TestFixture]
    public class UsuarioRepositoryTest : DatabaseTest
    {
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPessoaRepository _repositoryPessoa;

        public UsuarioRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repositoryUsuario = new UsuarioRepository(MockSettingsInfraData);
            _repositoryPessoa = new PessoaRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var retorno = _repositoryUsuario.Obter(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Salvar_Login_Invalido(string login)
        {
            var usuario = new SettingsTest().Usuario1;
            usuario.DefinirLogin(login);

            TestContext.WriteLine(usuario.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryUsuario.Salvar(usuario); });
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Salvar_Senha_Invalido(string senha)
        {
            var usuario = new SettingsTest().Usuario1;
            usuario.DefinirSenha(senha);

            TestContext.WriteLine(usuario.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryUsuario.Salvar(usuario); });
        }

        [Test]
        public void Atualizar_Valido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            usuario = new SettingsTest().Usuario1Editado;
            _repositoryUsuario.Atualizar(usuario);

            var retorno = _repositoryUsuario.Obter(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Atualizar_Login_Valido(string login)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            usuario.DefinirLogin(login);            

            TestContext.WriteLine(usuario.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryUsuario.Atualizar(usuario); });
        }

        [Test]
        [TestCase(null)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Atualizar_Senha_Valido(string senha)
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            usuario.DefinirSenha(senha);

            TestContext.WriteLine(usuario.FormatarJsonDeSaida());

            Assert.Throws<SqlException>(() => { _repositoryUsuario.Atualizar(usuario); });
        }

        [Test]
        public void Deletar()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario1);

            var usuario2 = new SettingsTest().Usuario2;
            _repositoryUsuario.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repositoryUsuario.Salvar(usuario3);

            _repositoryUsuario.Deletar(usuario2.Id);

            var retorno = _repositoryUsuario.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login, retorno[0].Login);
            Assert.AreEqual(usuario1.Senha, retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(usuario3.Id, retorno[1].Id);
            Assert.AreEqual(usuario3.Login, retorno[1].Login);
            Assert.AreEqual(usuario3.Senha, retorno[1].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[1].Privilegio);
        }

        [Test]
        public void Obter_ObjetoPreenchido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var retorno = _repositoryUsuario.Obter(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Obter_ObjetoNulo()
        {
            var retorno = _repositoryUsuario.Obter(1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.Null(retorno);
        }

        [Test]
        public void Obter_ContendoRegistrosFilhos_ObjetoPreenchido()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            usuario.AdicionarPessoa(pessoa1);
            usuario.AdicionarPessoa(pessoa2);
            usuario.AdicionarPessoa(pessoa3);

            var retorno = _repositoryUsuario.ObterContendoRegistrosFilhos(usuario.Id);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
            Assert.AreEqual(usuario.Pessoas.Count, retorno.Pessoas.Count);
        }

        [Test]
        public void Obter_ContendoRegistrosFilhos_ObjetoNulo()
        {
            var retorno = _repositoryUsuario.ObterContendoRegistrosFilhos(1);

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.Null(retorno);
        }

        [Test]
        public void Listar_ListaPreenchida()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario1);

            var usuario2 = new SettingsTest().Usuario2;
            _repositoryUsuario.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repositoryUsuario.Salvar(usuario3);

            var retorno = _repositoryUsuario.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login, retorno[0].Login);
            Assert.AreEqual(usuario1.Senha, retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);

            Assert.AreEqual(usuario2.Id, retorno[1].Id);
            Assert.AreEqual(usuario2.Login, retorno[1].Login);
            Assert.AreEqual(usuario2.Senha, retorno[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, retorno[1].Privilegio);

            Assert.AreEqual(usuario3.Id, retorno[2].Id);
            Assert.AreEqual(usuario3.Login, retorno[2].Login);
            Assert.AreEqual(usuario3.Senha, retorno[2].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[2].Privilegio);
        }

        [Test]
        public void Listar_ListaVazia()
        {
            var retorno = _repositoryUsuario.Listar();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(0, retorno.Count);
        }

        [Test]
        public void Listar_ContendoRegistrosFilhos_ListaPreenchida()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario1);

            var pessoa1 = new SettingsTest().Pessoa1;
            _repositoryPessoa.Salvar(pessoa1);

            var pessoa2 = new SettingsTest().Pessoa2;
            _repositoryPessoa.Salvar(pessoa2);

            var pessoa3 = new SettingsTest().Pessoa3;
            _repositoryPessoa.Salvar(pessoa3);

            usuario1.AdicionarPessoa(pessoa1);
            usuario1.AdicionarPessoa(pessoa2);
            usuario1.AdicionarPessoa(pessoa3);

            var usuario2 = new SettingsTest().Usuario2;
            _repositoryUsuario.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repositoryUsuario.Salvar(usuario3);

            var retorno = _repositoryUsuario.ListarContendoRegistrosFilhos();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario1.Id, retorno[0].Id);
            Assert.AreEqual(usuario1.Login, retorno[0].Login);
            Assert.AreEqual(usuario1.Senha, retorno[0].Senha);
            Assert.AreEqual(usuario1.Privilegio, retorno[0].Privilegio);
            Assert.AreEqual(usuario1.Pessoas.Count, retorno[0].Pessoas.Count);

            Assert.AreEqual(usuario2.Id, retorno[1].Id);
            Assert.AreEqual(usuario2.Login, retorno[1].Login);
            Assert.AreEqual(usuario2.Senha, retorno[1].Senha);
            Assert.AreEqual(usuario2.Privilegio, retorno[1].Privilegio);
            Assert.AreEqual(usuario2.Pessoas.Count, retorno[1].Pessoas.Count);

            Assert.AreEqual(usuario3.Id, retorno[2].Id);
            Assert.AreEqual(usuario3.Login, retorno[2].Login);
            Assert.AreEqual(usuario3.Senha, retorno[2].Senha);
            Assert.AreEqual(usuario3.Privilegio, retorno[2].Privilegio);
            Assert.AreEqual(usuario3.Pessoas.Count, retorno[2].Pessoas.Count);
        }

        [Test]
        public void Listar_ContendoRegistrosFilhos_ListaVazia()
        {
            var retorno = _repositoryUsuario.ListarContendoRegistrosFilhos();

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(0, retorno.Count);
        }

        [Test]
        public void Logar_LoginEfetuado()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var retorno = _repositoryUsuario.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.AreEqual(usuario.Id, retorno.Id);
            Assert.AreEqual(usuario.Login, retorno.Login);
            Assert.AreEqual(usuario.Senha, retorno.Senha);
            Assert.AreEqual(usuario.Privilegio, retorno.Privilegio);
        }

        [Test]
        public void Logar_LoginNaoEfetuado()
        {
            var usuario = new SettingsTest().Usuario1;

            var retorno = _repositoryUsuario.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            TestContext.WriteLine(retorno.FormatarJsonDeSaida());

            Assert.Null(retorno);
        }

        [Test]
        public void CheckLogin_LoginEncontrado()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var loginExistente = _repositoryUsuario.CheckLogin(usuario.Login.ToString());

            TestContext.WriteLine(loginExistente);

            Assert.True(loginExistente);
        }

        [Test]
        public void CheckLogin_LoginNaoEncontrado()
        {
            var loginNaoExiste = _repositoryUsuario.CheckLogin("LoginErrado");

            TestContext.WriteLine(loginNaoExiste);

            Assert.False(loginNaoExiste);
        }

        [Test]
        public void CheckId_Encontrado()
        {
            var usuario = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario);

            var idExistente = _repositoryUsuario.CheckId(usuario.Id);

            TestContext.WriteLine(idExistente);

            Assert.True(idExistente);
        }

        [Test]
        public void CheckId_NaoEncontrado()
        {
            var idNaoExiste = _repositoryUsuario.CheckId(25);

            TestContext.WriteLine(idNaoExiste);

            Assert.False(idNaoExiste);
        }

        [Test]
        public void LocalizarMaxId()
        {
            var usuario1 = new SettingsTest().Usuario1;
            _repositoryUsuario.Salvar(usuario1);

            var usuario2 = new SettingsTest().Usuario2;
            _repositoryUsuario.Salvar(usuario2);

            var usuario3 = new SettingsTest().Usuario3;
            _repositoryUsuario.Salvar(usuario3);

            var maxId = _repositoryUsuario.LocalizarMaxId();

            TestContext.WriteLine(maxId);

            Assert.AreEqual(usuario3.Id, maxId);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}