using ControleDespesas.Dominio.Commands.Pessoa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Pessoa
{
    public class ObterPessoaPorIdCommandTest
    {
        private ObterPessoaPorIdCommand _command;

        public ObterPessoaPorIdCommandTest() => _command = new ObterPessoaPorIdCommand() { Id = 1 };

        [Fact]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.Equal(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_IdZerado()
        {
            _command.Id = 0;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_IdNegativo()
        {
            _command.Id = -1;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }
    }
}