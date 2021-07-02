using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Empresas.Query.Results;
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

        public long Salvar(Empresa empresa)
        {
            _parametros.Add("Nome", empresa.Nome, DbType.String);
            _parametros.Add("Logo", empresa.Logo, DbType.String);

            using(var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<long>(EmpresaQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(Empresa empresa)
        {
            _parametros.Add("Id", empresa.Id, DbType.Int64);
            _parametros.Add("Nome", empresa.Nome, DbType.String);
            _parametros.Add("Logo", empresa.Logo, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(EmpresaQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(EmpresaQueries.Deletar, _parametros);
            }
        }

        public EmpresaQueryResult Obter(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

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

        public bool CheckId(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(EmpresaQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public long LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<long>(EmpresaQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}