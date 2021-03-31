using ControleDespesas.Infra.Interfaces.Commands;

namespace ControleDespesas.Infra.Commands
{
    public class CommandOutput : ICommandOutput
    {
        public int Id { get; set; }
    }
}