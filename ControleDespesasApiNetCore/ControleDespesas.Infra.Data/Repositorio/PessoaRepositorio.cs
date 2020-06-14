﻿using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Pessoa;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        StringBuilder Sql = new StringBuilder();
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public PessoaRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public string Salvar(Pessoa pessoa)
        {
            try
            {
                parametros.Add("Nome", pessoa.Nome.ToString(), DbType.String);
                parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                Sql.Clear();
                Sql.Append("INSERT INTO Pessoa (");
                Sql.Append("Nome, ");
                Sql.Append("ImagemPerfil) ");
                Sql.Append("VALUES(");
                Sql.Append("@Nome, ");
                Sql.Append("@ImagemPerfil)");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Atualizar(Pessoa pessoa)
        {
            try
            {
                parametros.Add("Id", pessoa.Id, DbType.Int32);
                parametros.Add("Nome", pessoa.Nome.ToString(), DbType.String);
                parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                Sql.Clear();
                Sql.Append("UPDATE Pessoa SET ");
                Sql.Append("Nome = @Nome, ");
                Sql.Append("ImagemPerfil = @ImagemPerfil ");
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
                Sql.Append("DELETE FROM Pessoa ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public PessoaQueryResult ObterPessoa(int id)
        {
            parametros.Add("Id", id, DbType.Int32);

            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Nome AS Nome,");
            Sql.Append("ImagemPerfil AS ImagemPerfil ");
            Sql.Append("FROM Pessoa ");
            Sql.Append("WHERE Id = @Id ");

            return _ctx.SQLServerConexao.Query<PessoaQueryResult>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public List<PessoaQueryResult> ListarPessoas()
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Nome AS Nome,");
            Sql.Append("ImagemPerfil AS ImagemPerfil ");
            Sql.Append("FROM Pessoa ");
            Sql.Append("ORDER BY Id ASC ");

            return _ctx.SQLServerConexao.Query<PessoaQueryResult>(Sql.ToString()).ToList();
        }

        public bool CheckId(int id)
        {
            parametros.Add("Id", id, DbType.Int32);

            Sql.Clear();
            Sql.Append("SELECT Id FROM Pessoa WHERE Id = @Id ");

            return _ctx.SQLServerConexao.Query<bool>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            Sql.Clear();
            Sql.Append("SELECT MAX(Id) FROM Pessoa");

            return _ctx.SQLServerConexao.Query<int>(Sql.ToString()).FirstOrDefault();
        }
    }
}