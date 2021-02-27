using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Commands.Empresa.Output;
using ControleDespesas.Domain.Entities;

namespace ControleDespesas.Domain.Helpers
{
    public static class EmpresaHelper
    {
        public static Empresa GerarEntidade(AdicionarEmpresaCommand command)
        {
            Empresa empresa = new Empresa(0, command.Nome, command.Logo);
            empresa.Validar();
            return empresa;
        }

        public static Empresa GerarEntidade(AtualizarEmpresaCommand command)
        {
            Empresa empresa = new Empresa(command.Id, command.Nome, command.Logo);
            empresa.Validar();
            return empresa;
        }

        public static AdicionarEmpresaCommandOutput GerarDadosRetornoInsert(Empresa empresa)
        {
            return new AdicionarEmpresaCommandOutput
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Logo = empresa.Logo
            };
        }

        public static AtualizarEmpresaCommandOutput GerarDadosRetornoUpdate(Empresa empresa)
        {
            return new AtualizarEmpresaCommandOutput
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Logo = empresa.Logo
            };
        }

        public static ApagarEmpresaCommandOutput GerarDadosRetornoDelete(int id)
        {
            return new ApagarEmpresaCommandOutput { Id = id };
        }
    }
}