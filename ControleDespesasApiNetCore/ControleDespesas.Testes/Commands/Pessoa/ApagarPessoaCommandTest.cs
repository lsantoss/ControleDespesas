using ControleDespesas.Dominio.Commands.Pessoa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pessoa
{
    public class ApagarPessoaCommandTest
    {
        private readonly ApagarPessoaCommand _commandReadOnly;

        public ApagarPessoaCommandTest()
        {
            _commandReadOnly = new ApagarPessoaCommand()
            {
                Id = 1
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            ApagarPessoaCommand command = _commandReadOnly;
            bool resultado = command.ValidarCommand();
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            ApagarPessoaCommand command = _commandReadOnly;
            command.Id = 0;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            ApagarPessoaCommand command = _commandReadOnly;
            command.Id = -1;
            bool resultado = command.ValidarCommand();
            Assert.False(resultado);
        }
    }
}