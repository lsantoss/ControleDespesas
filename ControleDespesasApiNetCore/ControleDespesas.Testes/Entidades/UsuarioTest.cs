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
            Descricao50Caracteres login = new Descricao50Caracteres("lucas123", "Login");
            Descricao50Caracteres senha = new Descricao50Caracteres("123", "Senha");
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
                new Descricao50Caracteres(loginLongo, "Login"),
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
                new Descricao50Caracteres(senhaLoga, "Senha"),
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
                new Descricao50Caracteres(loginLongo, "Login"),
                new Descricao50Caracteres(senhaLoga, "Login"),
                _usuarioTeste.Privilegio
            );

            bool resultadoLogin = usuario.Login.Valido;
            bool resultadoSenha = usuario.Senha.Valido;
            Assert.False(resultadoLogin);
            Assert.False(resultadoSenha);
        }
    }
}