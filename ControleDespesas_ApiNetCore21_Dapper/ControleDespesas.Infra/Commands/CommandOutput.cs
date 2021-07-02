using ControleDespesas.Infra.Interfaces.Commands;

namespace ControleDespesas.Infra.Commands
{
    public class CommandOutput : ICommandOutput
    {
        public long Id { get; set; }

        public CommandOutput(long id)
        {
            Id = id;
        }
    }
}