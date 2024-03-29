﻿using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Commands.Output;
using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Empresas.Helpers
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

        public static EmpresaCommandOutput GerarDadosRetornoInsert(Empresa empresa)
        {
            return new EmpresaCommandOutput
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Logo = empresa.Logo
            };
        }

        public static EmpresaCommandOutput GerarDadosRetornoUpdate(Empresa empresa)
        {
            return new EmpresaCommandOutput
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Logo = empresa.Logo
            };
        }

        public static CommandOutput GerarDadosRetornoDelete(int id)
        {
            return new CommandOutput { Id = id };
        }
    }
}