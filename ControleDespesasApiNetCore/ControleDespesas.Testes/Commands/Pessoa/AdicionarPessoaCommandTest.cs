using ControleDespesas.Dominio.Commands.Pessoa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pessoa
{
    public class AdicionarPessoaCommandTest
    {
        private readonly AdicionarPessoaCommand _commandReadOnly;

        public AdicionarPessoaCommandTest()
        {
            _commandReadOnly = new AdicionarPessoaCommand()
            {
                Nome = "Lucas",
                ImagemPerfil = "base64String"
            };
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_True()
        {
            AdicionarPessoaCommand command = _commandReadOnly;
            command.Nome = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_False()
        {
            AdicionarPessoaCommand command = _commandReadOnly;
            command.Nome = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_True()
        {
            AdicionarPessoaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_False()
        {
            AdicionarPessoaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_ImagemPerfilMinimoDeCaractetes_True()
        {
            AdicionarPessoaCommand command = _commandReadOnly;
            command.ImagemPerfil = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_ImagemPerfilMinimoDeCaractetes_False()
        {
            AdicionarPessoaCommand command = _commandReadOnly;
            command.ImagemPerfil = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}