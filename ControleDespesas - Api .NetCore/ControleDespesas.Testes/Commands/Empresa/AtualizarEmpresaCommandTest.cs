using ControleDespesas.Dominio.Commands.Empresa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Empresa
{
    public class AtualizarEmpresaCommandTest
    {
        private readonly AtualizarEmpresaCommand _commandReadOnly;

        public AtualizarEmpresaCommandTest()
        {
            _commandReadOnly = new AtualizarEmpresaCommand()
            {
                Id = 1,
                Nome = "Lucas",
                Logo = "base64String"
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_True()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            command.Nome = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_False()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            command.Nome = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_True()
        {
            AtualizarEmpresaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_False()
        {
            AtualizarEmpresaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_LogoMinimoDeCaractetes_True()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            command.Logo = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LogoMinimoDeCaractetes_False()
        {
            AtualizarEmpresaCommand command = _commandReadOnly;
            command.Logo = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}