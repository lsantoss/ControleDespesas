using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Helpers
{
    public class PessoaHelperTest
    {
        public PessoaHelperTest() { }

        [Fact]
        public void GerarEntidade_AdcionarPessoaCommand()
        {
            var command = new AdicionarPessoaCommand()
            {
                Nome = "Lucas",
                ImagemPerfil = "Imagem.png"
            };

            var entidade = PessoaHelper.GerarEntidade(command);

            Assert.Equal(0, entidade.Id);
            Assert.Equal("Lucas", entidade.Nome.ToString());
            Assert.Equal("Imagem.png", entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Nome.Notificacoes.Count);
        }

        [Fact]
        public void GerarEntidade_AtualizarPessoaCommand()
        {
            var command = new AtualizarPessoaCommand()
            {
                Id = 1,
                Nome = "Lucas",
                ImagemPerfil = "Imagem.png"
            };

            var entidade = PessoaHelper.GerarEntidade(command);

            Assert.Equal(1, entidade.Id);
            Assert.Equal("Lucas", entidade.Nome.ToString());
            Assert.Equal("Imagem.png", entidade.ImagemPerfil);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Nome.Notificacoes.Count);
        }

        [Fact]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new Pessoa(
                1, new Texto("Lucas", "Nome", 100),
                "Imagem.png"
            );

            var command = PessoaHelper.GerarDadosRetornoInsert(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("Lucas", command.Nome);
            Assert.Equal("Imagem.png", command.ImagemPerfil);
        }

        [Fact]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new Pessoa(
                1, new Texto("Lucas", "Nome", 100),
                "Imagem.png"
            );

            var command = PessoaHelper.GerarDadosRetornoUpdate(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("Lucas", command.Nome);
            Assert.Equal("Imagem.png", command.ImagemPerfil);
        }

        [Fact]
        public void GerarDadosRetornoDelte()
        {
            var command = PessoaHelper.GerarDadosRetornoDelete(1);
            Assert.Equal(1, command.Id);
        }
    }
}