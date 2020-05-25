using ControleDespesas.Dominio.Commands.Pessoa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pessoa
{
    public class AtualizarPessoaCommandTest
    {
        private readonly AtualizarPessoaCommand _commandReadOnly;

        public AtualizarPessoaCommandTest()
        {
            _commandReadOnly = new AtualizarPessoaCommand()
            {
                Id = 1,
                Nome = "Lucas",
                ImagemPerfil = "base64String"
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_True()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            command.Nome = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_False()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            command.Nome = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_True()
        {
            AtualizarPessoaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_False()
        {
            AtualizarPessoaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_ImagemPerfilMinimoDeCaractetes_True()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            command.ImagemPerfil = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_ImagemPerfilMinimoDeCaractetes_False()
        {
            AtualizarPessoaCommand command = _commandReadOnly;
            command.ImagemPerfil = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}