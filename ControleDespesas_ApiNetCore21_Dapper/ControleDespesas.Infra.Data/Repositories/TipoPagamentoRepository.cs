using ControleDespesas.Domain.TiposPagamentos.Entities;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Query.Results;
using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class TipoPagamentoRepository : ITipoPagamentoRepository
    {
        private readonly SettingsInfraData _settingsInfraData;
        private readonly DynamicParameters _parametros = new DynamicParameters();

        public TipoPagamentoRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
        }

        public long Salvar(TipoPagamento tipoPagamento)
        {
            _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<long>(TipoPagamentoQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(TipoPagamento tipoPagamento)
        {
            _parametros.Add("Id", tipoPagamento.Id, DbType.Int64);
            _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(TipoPagamentoQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(TipoPagamentoQueries.Deletar, _parametros);
            }
        }

        public TipoPagamentoQueryResult Obter(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public List<TipoPagamentoQueryResult> Listar()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<TipoPagamentoQueryResult>(TipoPagamentoQueries.Listar).ToList();
            }
        }

        public bool CheckId(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(TipoPagamentoQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public long LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<long>(TipoPagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}