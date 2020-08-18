using ControleDespesas.Dominio.Commands.Empresa.Input;
using Xunit;

namespace ControleDespesas.Testes.Commands.Empresa
{
    public class AdicionarEmpresaCommandTest
    {
        private AdicionarEmpresaCommand _command;

        public AdicionarEmpresaCommandTest()
        {
            _command = new AdicionarEmpresaCommand()
            {
                Nome = "Lucas",
                Logo = "base64String"
            };
        }

        [Fact]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.Equal(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetesNull()
        {
            _command.Nome = null;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_NomeMinimoDeCaractetesEmpty()
        {
            _command.Nome = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_NomeMaximoDeCaractetes()
        {
            _command.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_LogoMinimoDeCaractetesNull()
        {
            _command.Logo = null;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }

        [Fact]
        public void ValidarCommand_LogoMinimoDeCaractetesEmpty()
        {
            _command.Logo = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.NotEqual(0, _command.Notificacoes.Count);
        }
    }
}