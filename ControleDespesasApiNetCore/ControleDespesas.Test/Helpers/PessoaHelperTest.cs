using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Helpers
{
    public class PessoaHelperTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GerarEntidade_AdcionarPessoaCommand()
        {
            var command = new AdicionarPessoaCommand()
            {
                Nome = "Lucas",
                ImagemPerfil = "Imagem.png"
            };

            var entidade = PessoaHelper.GerarEntidade(command);

            Assert.AreEqual(0, entidade.Id);
            Assert.AreEqual("Lucas", entidade.Nome.ToString());
            Assert.AreEqual("Imagem.png", entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarEntidade_AtualizarPessoaCommand()
        {
            var command = new AtualizarPessoaCommand()
            {
                Id = 1,
                Nome = "Lucas",
                ImagemPerfil = "Imagem.png"
            };

            var entidade = PessoaHelper.GerarEntidade(command);

            Assert.AreEqual(1, entidade.Id);
            Assert.AreEqual("Lucas", entidade.Nome.ToString());
            Assert.AreEqual("Imagem.png", entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.AreEqual(0, entidade.Notificacoes.Count);
            Assert.AreEqual(0, entidade.Nome.Notificacoes.Count);
        }

        [Test]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new Pessoa(
                1, new Texto("Lucas", "Nome", 100),
                "Imagem.png"
            );

            var command = PessoaHelper.GerarDadosRetornoInsert(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("Lucas", command.Nome);
            Assert.AreEqual("Imagem.png", command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new Pessoa(
                1, new Texto("Lucas", "Nome", 100),
                "Imagem.png"
            );

            var command = PessoaHelper.GerarDadosRetornoUpdate(entidade);

            Assert.AreEqual(1, command.Id);
            Assert.AreEqual("Lucas", command.Nome);
            Assert.AreEqual("Imagem.png", command.ImagemPerfil);
        }

        [Test]
        public void GerarDadosRetornoDelte()
        {
            var command = PessoaHelper.GerarDadosRetornoDelete(1);
            Assert.AreEqual(1, command.Id);
        }

        [TearDown]
        public void TearDown() { }
    }
}