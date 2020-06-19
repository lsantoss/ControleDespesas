using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.TipoPagamento;
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
    public class TipoPagamentoRepositorio : ITipoPagamentoRepositorio
    {
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public TipoPagamentoRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public string Salvar(TipoPagamento tipoPagamento)
        {
            try
            {
                parametros.Add("Descricao", tipoPagamento.Descricao.ToString(), DbType.String);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Salvar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.Salvar() - " + e.Message);
            }
        }

        public string Atualizar(TipoPagamento tipoPagamento)
        {
            try
            {
                parametros.Add("Id", tipoPagamento.Id, DbType.Int32);
                parametros.Add("Descricao", tipoPagamento.Descricao.ToString(), DbType.String);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Atualizar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.Atualizar() - " + e.Message);
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Deletar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.Deletar() - " + e.Message);
            }
        }

        public TipoPagamentoQueryResult Obter(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Obter, parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.Obter() - " + e.Message);
            }
        }

        public List<TipoPagamentoQueryResult> Listar()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Listar).ToList();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.Listar() - " + e.Message);
            }
        }

        public bool CheckId(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<bool>(TipoPagamentoQueries.CheckId, parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.CheckId() - " + e.Message);
            }
        }

        public int LocalizarMaxId()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<int>(TipoPagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new RepositoryException("RepositoryException: TipoPagamentoRepositorio.LocalizarMaxId() - " + e.Message);
            }
        }
    }
}