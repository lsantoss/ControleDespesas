using ControleDespesas.Dominio.Commands.Empresa.Input;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Empresa
{
    public class AdicionarEmpresaCommandTest
    {
        private AdicionarEmpresaCommand _command;

        [SetUp]
        public void Setup()
        {
            _command = new AdicionarEmpresaCommand()
            {
                Nome = "Lucas",
                Logo = "base64String"
            };
        }

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_NomeMinimoDeCaractetesNull()
        {
            _command.Nome = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_NomeMinimoDeCaractetesEmpty()
        {
            _command.Nome = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_NomeMaximoDeCaractetes()
        {
            _command.Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_LogoMinimoDeCaractetesNull()
        {
            _command.Logo = null;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_LogoMinimoDeCaractetesEmpty()
        {
            _command.Logo = string.Empty;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}