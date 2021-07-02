using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Commands.Output;
using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Empresas.Helpers
{
    public static class EmpresaHelper
    {
        public static Empresa GerarEntidade(AdicionarEmpresaCommand command)
        {
            return new Empresa(command.Nome, command.Logo);
        }

        public static Empresa GerarEntidade(AtualizarEmpresaCommand command)
        {
            return new Empresa(command.Id, command.Nome, command.Logo);
        }

        public static EmpresaCommandOutput GerarDadosRetorno(Empresa empresa)
        {
            return new EmpresaCommandOutput(empresa.Id, empresa.Nome, empresa.Logo);
        }

        public static CommandOutput GerarDadosRetorno(long id)
        {
            return new CommandOutput(id);
        }
    }
}