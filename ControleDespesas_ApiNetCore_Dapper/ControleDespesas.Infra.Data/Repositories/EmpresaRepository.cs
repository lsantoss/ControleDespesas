using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Empresa.Results;
using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly SettingsInfraData _settingsInfraData;
        private readonly DynamicParameters _parametros = new DynamicParameters();

        public EmpresaRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
        }

        public int Salvar(Empresa empresa)
        {
            _parametros.Add("Nome", empresa.Nome, DbType.String);
            _parametros.Add("Logo", empresa.Logo, DbType.String);

            using(var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<int>(EmpresaQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(Empresa empresa)
        {
            _parametros.Add("Id", empresa.Id, DbType.Int32);
            _parametros.Add("Nome", empresa.Nome, DbType.String);
            _parametros.Add("Logo", empresa.Logo, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(EmpresaQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(EmpresaQueries.Deletar, _parametros);
            }
        }

        public EmpresaQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<EmpresaQueryResult>(EmpresaQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public List<EmpresaQueryResult> Listar()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<EmpresaQueryResult>(EmpresaQueries.Listar).ToList();
            }
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(EmpresaQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public int LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<int>(EmpresaQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}