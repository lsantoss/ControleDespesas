﻿using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Commands.Empresa.Output;
using ControleDespesas.Domain.Entities;
using System;

namespace ControleDespesas.Domain.Helpers
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
                throw e;
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
                throw e;
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
                throw e;
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
                throw e;
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
                throw e;
            }
        }
    }
}