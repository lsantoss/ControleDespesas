using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Helpers
{
    public class UsuarioHelperTest
    {
        public UsuarioHelperTest() { }

        [Fact]
        public void GerarEntidade_AdcionarUsuarioCommand()
        {
            var command = new AdicionarUsuarioCommand()
            {
                Login = "Login",
                Senha = "Senha123",
                Privilegio = EPrivilegioUsuario.Admin
            };

            var entidade = UsuarioHelper.GerarEntidade(command);

            Assert.Equal(0, entidade.Id);
            Assert.Equal("Login", entidade.Login.ToString());
            Assert.Equal("Senha123", entidade.Senha.ToString());
            Assert.Equal(EPrivilegioUsuario.Admin, entidade.Privilegio);
            Assert.True(entidade.Valido); 
            Assert.True(entidade.Login.Valido); 
            Assert.True(entidade.Senha.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Login.Notificacoes.Count);
            Assert.Equal(0, entidade.Senha.Notificacoes.Count);
        }

        [Fact]
        public void GerarEntidade_AtualizarUsuarioCommand()
        {
            var command = new AtualizarUsuarioCommand()
            {
                Id = 1,
                Login = "Login",
                Senha = "Senha123",
                Privilegio = EPrivilegioUsuario.Admin
            };

            var entidade = UsuarioHelper.GerarEntidade(command);

            Assert.Equal(1, entidade.Id);
            Assert.Equal("Login", entidade.Login.ToString());
            Assert.Equal("Senha123", entidade.Senha.ToString());
            Assert.Equal(EPrivilegioUsuario.Admin, entidade.Privilegio);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Login.Valido);
            Assert.True(entidade.Senha.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Login.Notificacoes.Count);
            Assert.Equal(0, entidade.Senha.Notificacoes.Count);
        }

        [Fact]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new Usuario(
                1, 
                new Texto("Login", "Login", 50), 
                new SenhaMedia("Senha123"), 
                EPrivilegioUsuario.Admin
            );

            var command = UsuarioHelper.GerarDadosRetornoInsert(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("Login", command.Login);
            Assert.Equal("Senha123", command.Senha);
            Assert.Equal(EPrivilegioUsuario.Admin, command.Privilegio);
        }

        [Fact]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new Usuario(
                1,
                new Texto("Login", "Login", 50),
                new SenhaMedia("Senha123"),
                EPrivilegioUsuario.Admin
            );

            var command = UsuarioHelper.GerarDadosRetornoUpdate(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("Login", command.Login);
            Assert.Equal("Senha123", command.Senha);
            Assert.Equal(EPrivilegioUsuario.Admin, command.Privilegio);
        }

        [Fact]
        public void GerarDadosRetornoDelte()
        {
            var command = UsuarioHelper.GerarDadosRetornoDelete(1);
            Assert.Equal(1, command.Id);
        }
    }
}