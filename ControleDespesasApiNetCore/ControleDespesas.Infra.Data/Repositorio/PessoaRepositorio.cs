using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Pessoa;
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
    public class PessoaRepositorio : IPessoaRepositorio
    {
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

                _ctx.SQLServerConexao.Execute(PessoaQueries.Salvar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: PessoaRepositorio.Salvar() - " + e.Message);
            }
        }

        public string Atualizar(Pessoa pessoa)
        {
            try
            {
                parametros.Add("Id", pessoa.Id, DbType.Int32);
                parametros.Add("Nome", pessoa.Nome.ToString(), DbType.String);
                parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                _ctx.SQLServerConexao.Execute(PessoaQueries.Atualizar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: PessoaRepositorio.Atualizar() - " + e.Message);
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                _ctx.SQLServerConexao.Execute(PessoaQueries.Deletar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: PessoaRepositorio.Deletar() - " + e.Message);
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
                throw new RepositoryException("RepositoryException: PessoaRepositorio.Obter() - " + e.Message);
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
                throw new RepositoryException("RepositoryException: PessoaRepositorio.Listar() - " + e.Message);
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
                throw new RepositoryException("RepositoryException: PessoaRepositorio.CheckId() - " + e.Message);
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
                throw new RepositoryException("RepositoryException: PessoaRepositorio.LocalizarMaxId() - " + e.Message);
            }
        }
    }
}