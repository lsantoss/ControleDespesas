using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class UsuarioHelperTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarUsuarioCommand()
        {
            var command = new AdicionarUsuarioCommand()
            {
                Login = "Login",
                Senha = "Senha123",
                Privilegio = EPrivilegioUsuario.Admin
            };

            var entidade = UsuarioHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual("Login", entidade.Login.ToString());
            Assert.AreEqual("Senha123", entidade.Senha.ToString());
            Assert.AreEqual(EPrivilegioUsuario.Admin, entidade.Privilegio);
            Assert.True(entidade.Valido); 
            Assert.True(entidade.Login.Valido); 
            Assert.True(entidade.Senha.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Login.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Senha.Notificacoes.Count);
        }

        [Test]
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

            Assert.AreEqual(1, entidade.Id);
            Assert.AreEqual("Login", entidade.Login.ToString());
            Assert.AreEqual("Senha123", entidade.Senha.ToString());
            Assert.AreEqual(EPrivilegioUsuario.Admin, entidade.Privilegio);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Login.Valido);
            Assert.True(entidade.Senha.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Login.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Senha.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new Usuario(
                1, 
                new Texto("Login", "Login", 50), 
                new SenhaMedia("Senha123"), 
                EPrivilegioUsuario.Admin
            );

            var command = UsuarioHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("Login", command.Login);
            Assert.AreEqual("Senha123", command.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Admin, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new Usuario(
                1,
                new Texto("Login", "Login", 50),
                new SenhaMedia("Senha123"),
                EPrivilegioUsuario.Admin
            );

            var command = UsuarioHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("Login", command.Login);
            Assert.AreEqual("Senha123", command.Senha);
            Assert.AreEqual(EPrivilegioUsuario.Admin, command.Privilegio);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var command = UsuarioHelper.GerarDadosRetornoDelete(1);
            Assert.AreEqual(1, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}