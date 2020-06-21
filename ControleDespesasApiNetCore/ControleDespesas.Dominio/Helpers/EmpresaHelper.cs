using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class EmpresaHelper
    {
        public static Empresa GerarEntidade(AdicionarEmpresaCommand command)
        {
            try
            {
                Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
                string logo = command.Logo;

                Empresa empresa = new Empresa(0, nome, logo);
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
                Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
                string logo = command.Logo;

                Empresa empresa = new Empresa(id, nome, logo);
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static object GerarDadosRetornoCommandResult(Empresa empresa)
        {
            try
            {
                return new
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome.ToString(),
                    Logo = empresa.Logo
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}