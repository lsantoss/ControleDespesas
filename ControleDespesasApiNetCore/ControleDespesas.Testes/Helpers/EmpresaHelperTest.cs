using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Helpers
{
    public class EmpresaHelperTest
    {
        public EmpresaHelperTest() { }

        [Fact]
        public void GerarEntidade_AdcionarEmpresaCommand()
        {
            var command = new AdicionarEmpresaCommand() { 
                Nome = "Empresa",
                Logo = "Logo.png"
            };

            var entidade = EmpresaHelper.GerarEntidade(command);

            Assert.Equal(0, entidade.Id);
            Assert.Equal("Empresa", entidade.Nome.ToString());
            Assert.Equal("Logo.png", entidade.Logo); 
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Nome.Notificacoes.Count);
        }

        [Fact]
        public void GerarEntidade_AtualizarEmpresaCommand()
        {
            var command = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "Empresa",
                Logo = "Logo.png"
            };

            var entidade = EmpresaHelper.GerarEntidade(command);

            Assert.Equal(1, entidade.Id);
            Assert.Equal("Empresa", entidade.Nome.ToString());
            Assert.Equal("Logo.png", entidade.Logo);
            Assert.True(entidade.Valido);
            Assert.True(entidade.Nome.Valido);
            Assert.Equal(0, entidade.Notificacoes.Count);
            Assert.Equal(0, entidade.Nome.Notificacoes.Count);
        }

        [Fact]
        public void GerarDadosRetornoInsert()
        {
            var entidade = new Empresa(
                1, 
                new Texto("Empresa", "Nome", 100), 
                "Logo.png"
            );

            var command = EmpresaHelper.GerarDadosRetornoInsert(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("Empresa", command.Nome);
            Assert.Equal("Logo.png", command.Logo);
        }

        [Fact]
        public void GerarDadosRetornoUpdate()
        {
            var entidade = new Empresa(
                1,
                new Texto("Empresa", "Nome", 100),
                "Logo.png"
            );

            var command = EmpresaHelper.GerarDadosRetornoUpdate(entidade);

            Assert.Equal(1, command.Id);
            Assert.Equal("Empresa", command.Nome);
            Assert.Equal("Logo.png", command.Logo);
        }

        [Fact]
        public void GerarDadosRetornoDelte()
        {
            var command = EmpresaHelper.GerarDadosRetornoDelete(1);
            Assert.Equal(1, command.Id);
        }
    }
}