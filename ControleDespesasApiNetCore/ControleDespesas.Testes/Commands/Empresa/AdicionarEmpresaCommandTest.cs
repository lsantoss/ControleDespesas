using ControleDespesas.Dominio.Commands.Empresa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Empresa
{
    public class AdicionarEmpresaCommandTest
    {
        private readonly AdicionarEmpresaCommand _commandReadOnly;

        public AdicionarEmpresaCommandTest()
        {
            _commandReadOnly = new AdicionarEmpresaCommand()
            {
                Nome = "Lucas",
                Logo = "base64String"
            };
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_True()
        {
            AdicionarEmpresaCommand command = _commandReadOnly;
            command.Nome = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetes_False()
        {
            AdicionarEmpresaCommand command = _commandReadOnly;
            command.Nome = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_True()
        {
            AdicionarEmpresaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes_False()
        {
            AdicionarEmpresaCommand commandTest = _commandReadOnly;
            commandTest.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            bool resultado = commandTest.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_LogoMinimoDeCaractetes_True()
        {
            AdicionarEmpresaCommand command = _commandReadOnly;
            command.Logo = "a";
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_LogoMinimoDeCaractetes_False()
        {
            AdicionarEmpresaCommand command = _commandReadOnly;
            command.Logo = "";
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}