using ControleDespesas.Infra.Logs;
using System.Collections.Generic;

namespace ControleDespesas.Infra.Interfaces.Repositories
{
    public interface ILogRequestResponseRepository
    {
        void Salvar(LogRequestResponse entidade);
        LogRequestResponse Obter(int id);
        List<LogRequestResponse> Listar();
    }
}