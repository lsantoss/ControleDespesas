using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class EmpresaHelper
    {
        /// <summary> Helper para auxiliar na geração de entidade Empresa a partir de um command AdicionarEmpresaCommand </summary>
        /// <remarks> Converte AdicionarEmpresaCommand para Empresa </remarks>
        /// <param name="command"> Parâmetro de entrada: AdicionarEmpresaCommand </param>
        /// <returns> Retorna: Empresa </returns>
        public static Empresa GerarEntidade(AdicionarEmpresaCommand command)
        {
            try
            {
                Texto nome = new Texto(command.Nome, "Nome", 100);
                string logo = command.Logo;

                Empresa empresa = new Empresa(0, nome, logo);
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary> Helper para auxiliar na geração de entidade Empresa a partir de um command AtualizarEmpresaCommand </summary>
        /// <remarks> Converte AtualizarEmpresaCommand para Empresa </remarks>
        /// <param name="command"> Parâmetro de entrada: AdicionarEmpresaCommand </param>
        /// <returns> Retorna: Empresa </returns>
        public static Empresa GerarEntidade(AtualizarEmpresaCommand command)
        {
            try
            {
                int id = command.Id;
                Texto nome = new Texto(command.Nome, "Nome", 100);
                string logo = command.Logo;

                Empresa empresa = new Empresa(id, nome, logo);
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
                    Nome = empresa.Nome.ToString(),
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
                    Nome = empresa.Nome.ToString(),
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