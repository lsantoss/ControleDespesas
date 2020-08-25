using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Testes.Entidades
{
    public class UsuarioTest
    {
        private Usuario _usuario;

        [SetUp]
        public void Setup()
        {
            int id = 1;
            Texto login = new Texto("lucas@123", "Login", 50);
            SenhaMedia senha = new SenhaMedia("Senha123");
            EPrivilegioUsuario privilegio = EPrivilegioUsuario.Admin;
            _usuario = new Usuario(id, login, senha, privilegio);
        }

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_usuario.Valido);
            Assert.True(_usuario.Login.Valido);
            Assert.True(_usuario.Senha.Valido);
            Assert.AreEqual(0, _usuario.Notificacoes.Count);
            Assert.AreEqual(0, _usuario.Login.Notificacoes.Count);
            Assert.AreEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_LoginInvalido()
        {
            _usuario.Login = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Login", 50);

            Assert.False(_usuario.Login.Valido);
            Assert.AreNotEqual(0, _usuario.Login.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_SenhaInvalida()
        {
            _usuario.Senha = new SenhaMedia("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            Assert.False(_usuario.Senha.Valido);
            Assert.AreNotEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_LoginESenhaInvalida()
        {
            _usuario.Login = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Login", 50);
            _usuario.Senha = new SenhaMedia("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            Assert.False(_usuario.Login.Valido);
            Assert.False(_usuario.Senha.Valido);
            Assert.AreNotEqual(0, _usuario.Login.Notificacoes.Count);
            Assert.AreNotEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _usuario = null;
    }
}