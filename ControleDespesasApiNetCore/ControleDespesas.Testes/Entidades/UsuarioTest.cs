using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class UsuarioTest
    {
        private readonly Usuario _usuarioTeste;

        public UsuarioTest()
        {
            int id = 1;
            Texto login = new Texto("lucas123", "Login", 50);
            SenhaMedia senha = new SenhaMedia("123");
            EPrivilegioUsuario privilegio = EPrivilegioUsuario.Admin;
            _usuarioTeste = new Usuario(id, login, senha, privilegio);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Usuario usuario = _usuarioTeste;
            int resultado = usuario.Notificacoes.Count;
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void ValidarEntidade_LoginInvalido()
        {
            string loginLongo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Usuario usuario = new Usuario(
                _usuarioTeste.Id,
                new Texto(loginLongo, "Login", 50),
                _usuarioTeste.Senha,
                _usuarioTeste.Privilegio
            );

            bool resultado = usuario.Login.Valido;
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarEntidade_SenhaInvalida()
        {
            string senhaLoga = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Usuario usuario = new Usuario(
                _usuarioTeste.Id,
                _usuarioTeste.Login,
                new SenhaMedia(senhaLoga),
                _usuarioTeste.Privilegio
            );

            bool resultado = usuario.Senha.Valido;
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarEntidade_LoginESenhaInvalida()
        {
            string loginLongo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            string senhaLoga = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Usuario usuario = new Usuario(
                _usuarioTeste.Id,
                new Texto(loginLongo, "Login", 50),
                new SenhaMedia(senhaLoga),
                _usuarioTeste.Privilegio
            );

            bool resultadoLogin = usuario.Login.Valido;
            bool resultadoSenha = usuario.Senha.Valido;
            Assert.False(resultadoLogin);
            Assert.False(resultadoSenha);
        }
    }
}