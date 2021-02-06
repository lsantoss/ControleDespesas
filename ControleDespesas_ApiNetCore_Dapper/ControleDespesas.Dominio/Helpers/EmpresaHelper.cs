using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class EmpresaHelper
    {
        public static Empresa GerarEntidade(AdicionarEmpresaCommand command)
        {
            try
            {
                string nome = command.Nome;
                string logo = command.Logo;

                Empresa empresa = new Empresa(0, nome, logo);
                empresa.Validar();
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Empresa GerarEntidade(AtualizarEmpresaCommand command)
        {
            try
            {
                int id = command.Id;
                string nome = command.Nome;
                string logo = command.Logo;

                Empresa empresa = new Empresa(id, nome, logo);
                empresa.Validar();
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AdicionarEmpresaCommandOutput GerarDadosRetornoInsert(Empresa empresa)
        {
            try
            {
                return new AdicionarEmpresaCommandOutput
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome,
                    Logo = empresa.Logo
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AtualizarEmpresaCommandOutput GerarDadosRetornoUpdate(Empresa empresa)
        {
            try
            {
                return new AtualizarEmpresaCommandOutput
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome,
                    Logo = empresa.Logo
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static ApagarEmpresaCommandOutput GerarDadosRetornoDelete(int id)
        {
            try
            {
                return new ApagarEmpresaCommandOutput { Id = id };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}