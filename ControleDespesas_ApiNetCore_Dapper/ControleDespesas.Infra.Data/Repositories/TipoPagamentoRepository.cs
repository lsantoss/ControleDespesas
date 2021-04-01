using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
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

        public int Salvar(TipoPagamento tipoPagamento)
        {
            _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

            return _dataContext.SQLServerConexao.ExecuteScalar<int>(TipoPagamentoQueries.Salvar, _parametros); ;
        }

        public void Atualizar(TipoPagamento tipoPagamento)
        {
            _parametros.Add("Id", tipoPagamento.Id, DbType.Int32);
            _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

            _dataContext.SQLServerConexao.Execute(TipoPagamentoQueries.Atualizar, _parametros);
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            _dataContext.SQLServerConexao.Execute(TipoPagamentoQueries.Deletar, _parametros);
        }

        public TipoPagamentoQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Obter, _parametros).FirstOrDefault();
        }

        public List<TipoPagamentoQueryResult> Listar()
        {
            return _dataContext.SQLServerConexao.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Listar).ToList();
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<bool>(TipoPagamentoQueries.CheckId, _parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            return _dataContext.SQLServerConexao.Query<int>(TipoPagamentoQueries.LocalizarMaxId).FirstOrDefault();
        }
    }
}