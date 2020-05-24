﻿using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        StringBuilder Sql = new StringBuilder();
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public EmpresaRepositorio()
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, Settings.Settings.ConnectionString);
        }

        public string Salvar(Empresa empresa)
        {
            try
            {
                parametros.Add("Nome", empresa.Nome.ToString(), DbType.String);
                parametros.Add("Logo", empresa.Logo, DbType.String);

                Sql.Clear();
                Sql.Append("INSERT INTO Empresa (");
                Sql.Append("Nome, ");
                Sql.Append("Logo) ");
                Sql.Append("VALUES(");
                Sql.Append("@Nome, ");
                Sql.Append("@Logo)");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Atualizar(Empresa empresa)
        {
            try
            {
                parametros.Add("Id", empresa.Id, DbType.Int32);
                parametros.Add("Nome", empresa.Nome.ToString(), DbType.String);
                parametros.Add("Logo", empresa.Logo, DbType.String);

                Sql.Clear();
                Sql.Append("UPDATE Empresa SET ");
                Sql.Append("Nome = @Nome, ");
                Sql.Append("Logo = @Logo ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                Sql.Clear();
                Sql.Append("DELETE FROM Empresa ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public EmpresaQueryResult ObterEmpresa(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Nome AS Nome,");
            Sql.Append("Logo AS Logo ");
            Sql.Append("FROM Empresa ");
            Sql.Append("WHERE Id = @Id ");

            return _ctx.SQLServerConexao.Query<EmpresaQueryResult>(Sql.ToString(), new { Id = id }).FirstOrDefault();
        }

        public List<EmpresaQueryResult> ListarEmpresas()
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Nome AS Nome,");
            Sql.Append("Logo AS Logo ");
            Sql.Append("FROM Empresa ");
            Sql.Append("ORDER BY Id ASC ");

            return _ctx.SQLServerConexao.Query<EmpresaQueryResult>(Sql.ToString()).ToList();
        }

        public bool CheckId(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT Id ");
            Sql.Append("FROM Empresa ");
            Sql.Append("where Id = @Id ");

            return _ctx.SQLServerConexao.Query<bool>(Sql.ToString(), new { Id = id }).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            Sql.Clear();
            Sql.Append("SELECT MAX(Id) ");
            Sql.Append("FROM Empresa");

            return _ctx.SQLServerConexao.Query<int>(Sql.ToString()).FirstOrDefault();
        }
    }
}