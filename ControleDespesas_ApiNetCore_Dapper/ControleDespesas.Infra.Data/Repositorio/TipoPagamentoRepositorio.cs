using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.TipoPagamento;
using ControleDespesas.Dominio.Interfaces.Repositorio;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Data.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
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
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _ctx;

        public TipoPagamentoRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DataContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public TipoPagamento Salvar(TipoPagamento tipoPagamento)
        {
            try
            {
                _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

                tipoPagamento.Id = _ctx.SQLServerConexao.ExecuteScalar<int>(TipoPagamentoQueries.Salvar, _parametros);
                return tipoPagamento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Atualizar(TipoPagamento tipoPagamento)
        {
            try
            {
                _parametros.Add("Id", tipoPagamento.Id, DbType.Int32);
                _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Atualizar, _parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                _ctx.SQLServerConexao.Execute(TipoPagamentoQueries.Deletar, _parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TipoPagamentoQueryResult Obter(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Obter, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
            }
        }

        public bool CheckId(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<bool>(TipoPagamentoQueries.CheckId, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
            }
        }
    }
}