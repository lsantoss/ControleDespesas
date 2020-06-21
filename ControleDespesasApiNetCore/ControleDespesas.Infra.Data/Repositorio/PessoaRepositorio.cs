using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Infra.Data.Queries;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public PessoaRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public void Salvar(Pessoa pessoa)
        {
            try
            {
                parametros.Add("Nome", pessoa.Nome.ToString(), DbType.String);
                parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                _ctx.SQLServerConexao.Execute(PessoaQueries.Salvar, parametros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Atualizar(Pessoa pessoa)
        {
            try
            {
                parametros.Add("Id", pessoa.Id, DbType.Int32);
                parametros.Add("Nome", pessoa.Nome.ToString(), DbType.String);
                parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                _ctx.SQLServerConexao.Execute(PessoaQueries.Atualizar, parametros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                _ctx.SQLServerConexao.Execute(PessoaQueries.Deletar, parametros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PessoaQueryResult Obter(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PessoaQueryResult>(PessoaQueries.Obter, parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PessoaQueryResult> Listar()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<PessoaQueryResult>(PessoaQueries.Listar).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CheckId(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<bool>(PessoaQueries.CheckId, parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int LocalizarMaxId()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<int>(PessoaQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}