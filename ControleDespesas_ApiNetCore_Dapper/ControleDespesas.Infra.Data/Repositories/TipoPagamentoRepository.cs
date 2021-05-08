using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
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

        public int Salvar(TipoPagamento tipoPagamento)
        {
            _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<int>(TipoPagamentoQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(TipoPagamento tipoPagamento)
        {
            _parametros.Add("Id", tipoPagamento.Id, DbType.Int32);
            _parametros.Add("Descricao", tipoPagamento.Descricao, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(TipoPagamentoQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(TipoPagamentoQueries.Deletar, _parametros);
            }
        }

        public TipoPagamentoQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

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

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(TipoPagamentoQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public int LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<int>(TipoPagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}