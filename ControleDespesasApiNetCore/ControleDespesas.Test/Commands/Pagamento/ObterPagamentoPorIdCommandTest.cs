using ControleDespesas.Dominio.Commands.Pagamento.Input;
using NUnit.Framework;

namespace ControleDespesas.Test.Commands.Pagamento
{
    public class ObterPagamentoPorIdCommandTest
    {
        private ObterPagamentoPorIdCommand _command;

        [SetUp]
        public void Setup() => _command = new ObterPagamentoPorIdCommand() { Id = 1 };

        [Test]
        public void ValidarCommand_Valido()
        {
            Assert.True(_command.ValidarCommand());
            Assert.AreEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdZerado()
        {
            _command.Id = 0;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [Test]
        public void ValidarCommand_IdNegativo()
        {
            _command.Id = -1;
            Assert.False(_command.ValidarCommand());
            Assert.AreNotEqual(0, _command.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _command = null;
    }
}