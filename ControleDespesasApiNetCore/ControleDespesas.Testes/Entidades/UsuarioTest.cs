using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class UsuarioTest
    {
        private Usuario _usuario;

        public UsuarioTest()
        {
            int id = 1;
            Texto login = new Texto("lucas@123", "Login", 50);
            SenhaMedia senha = new SenhaMedia("Senha123");
            EPrivilegioUsuario privilegio = EPrivilegioUsuario.Admin;
            _usuario = new Usuario(id, login, senha, privilegio);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_usuario.Valido);
            Assert.True(_usuario.Login.Valido);
            Assert.True(_usuario.Senha.Valido);
            Assert.Equal(0, _usuario.Notificacoes.Count);
            Assert.Equal(0, _usuario.Login.Notificacoes.Count);
            Assert.Equal(0, _usuario.Senha.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_LoginInvalido()
        {
            _usuario.Login = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Login", 50);

            Assert.False(_usuario.Login.Valido);
            Assert.NotEqual(0, _usuario.Login.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_SenhaInvalida()
        {
            _usuario.Senha = new SenhaMedia("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            Assert.False(_usuario.Senha.Valido);
            Assert.NotEqual(0, _usuario.Senha.Notificacoes.Count);
        }

        [Fact]
        public void ValidarEntidade_LoginESenhaInvalida()
        {
            _usuario.Login = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Login", 50);
            _usuario.Senha = new SenhaMedia("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            Assert.False(_usuario.Login.Valido);
            Assert.False(_usuario.Senha.Valido);
            Assert.NotEqual(0, _usuario.Login.Notificacoes.Count);
            Assert.NotEqual(0, _usuario.Senha.Notificacoes.Count);
        }
    }
}