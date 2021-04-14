using ControleDespesas.Infra.Logs;

namespace ControleDespesas.Infra.Interfaces.Repositories
{
    public interface ILogRequestResponseRepository
    {
        void Adicionar(LogRequestResponse entidade);
    }
}