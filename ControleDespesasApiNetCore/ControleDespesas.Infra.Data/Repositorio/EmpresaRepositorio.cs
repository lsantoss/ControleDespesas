using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Infra.Data.Queries;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using LSCode.Facilitador.Api.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public EmpresaRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public string Salvar(Empresa empresa)
        {
            try
            {
                parametros.Add("Nome", empresa.Nome.ToString(), DbType.String);
                parametros.Add("Logo", empresa.Logo, DbType.String);

                _ctx.SQLServerConexao.Execute(EmpresaQueries.Salvar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.Salvar() - " + e.Message);
            }
        }

        public string Atualizar(Empresa empresa)
        {
            try
            {
                parametros.Add("Id", empresa.Id, DbType.Int32);
                parametros.Add("Nome", empresa.Nome.ToString(), DbType.String);
                parametros.Add("Logo", empresa.Logo, DbType.String);

                _ctx.SQLServerConexao.Execute(EmpresaQueries.Atualizar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.Atualizar() - " + e.Message);
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);                

                _ctx.SQLServerConexao.Execute(EmpresaQueries.Deletar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.Deletar() - " + e.Message);
            }
        }

        public EmpresaQueryResult Obter(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<EmpresaQueryResult>(EmpresaQueries.Obter, parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.Obter() - " + e.Message);
            }
        }

        public List<EmpresaQueryResult> Listar()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<EmpresaQueryResult>(EmpresaQueries.Listar).ToList();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.Listar() - " + e.Message);
            }
        }

        public bool CheckId(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<bool>(EmpresaQueries.CheckId, parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.CheckId() - " + e.Message);
            }
        }

        public int LocalizarMaxId()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<int>(EmpresaQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: EmpresaRepositorio.LocalizarMaxId() - " + e.Message);
            }
        }
    }
}