using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class TipoPagamentoRepository : ITipoPagamentoRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public TipoPagamentoRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public TipoPagamento Salvar(TipoPagamento tipoPagamento)
        {
            try
            {
                _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

                tipoPagamento.Id = _dataContext.SQLServerConexao.ExecuteScalar<int>(TipoPagamentoQueries.Salvar, _parametros);
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

                _dataContext.SQLServerConexao.Execute(TipoPagamentoQueries.Atualizar, _parametros);
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

                _dataContext.SQLServerConexao.Execute(TipoPagamentoQueries.Deletar, _parametros);
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

                return _dataContext.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Obter, _parametros).FirstOrDefault();
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
                return _dataContext.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Listar).ToList();
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

                return _dataContext.SQLServerConexao.Query<bool>(TipoPagamentoQueries.CheckId, _parametros).FirstOrDefault();
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
                return _dataContext.SQLServerConexao.Query<int>(TipoPagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}