using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.TipoPagamento;
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
    public class TipoPagamentoRepositorio : ITipoPagamentoRepositorio
    {
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public TipoPagamentoRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public void Salvar(TipoPagamento tipoPagamento)
        {
            try
            {
                parametros.Add("Descricao", tipoPagamento.Descricao.ToString(), DbType.String);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Salvar, parametros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Atualizar(TipoPagamento tipoPagamento)
        {
            try
            {
                parametros.Add("Id", tipoPagamento.Id, DbType.Int32);
                parametros.Add("Descricao", tipoPagamento.Descricao.ToString(), DbType.String);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Atualizar, parametros);
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

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Deletar, parametros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
            }
        }
    }
}