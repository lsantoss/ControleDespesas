using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class EmpresaHelperTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarEmpresaCommand()
        {
            var command = new AdicionarEmpresaCommand() { 
                Nome = "Empresa",
                Logo = "Logo.png"
            };

            var entidade = EmpresaHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual("Empresa", entidade.Nome.ToString());
            Assert.AreEqual("Logo.png", entidade.Logo); 
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarEmpresaCommand()
        {
            var command = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "Empresa",
                Logo = "Logo.png"
            };

            var entidade = EmpresaHelper.GerarEntidade(command);

            Assert.AreEqual(1, entidade.Id);
            Assert.AreEqual("Empresa", entidade.Nome.ToString());
            Assert.AreEqual("Logo.png", entidade.Logo);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new Empresa(
                1, 
                new Texto("Empresa", "Nome", 100), 
                "Logo.png"
            );

            var command = EmpresaHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("Empresa", command.Nome);
            Assert.AreEqual("Logo.png", command.Logo);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new Empresa(
                1,
                new Texto("Empresa", "Nome", 100),
                "Logo.png"
            );

            var command = EmpresaHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("Empresa", command.Nome);
            Assert.AreEqual("Logo.png", command.Logo);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var command = EmpresaHelper.GerarDadosRetornoDelete(1);
            Assert.AreEqual(1, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}