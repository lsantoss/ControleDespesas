using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Logs;
using ControleDespesas.Infra.Settings;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class LogRequestResponseRepository : ILogRequestResponseRepository
    {
        private readonly SettingsInfraData _settingsInfraData;
        private readonly DynamicParameters _parametros = new DynamicParameters();

        public LogRequestResponseRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
        }

        public void Salvar(LogRequestResponse entidade)
        {
            _parametros.Add("@MachineName", entidade.MachineName);
            _parametros.Add("@DataRequest", entidade.DataRequest);
            _parametros.Add("@DataResponse", entidade.DataResponse);
            _parametros.Add("@EndPoint", entidade.EndPoint);
            _parametros.Add("@Request", entidade.Request);
            _parametros.Add("@Response", entidade.Response);
            _parametros.Add("@TempoDuracao", entidade.TempoDuracao);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(LogRequestResponseQueries.Salvar, _parametros);
            }
        }

        public LogRequestResponse Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<LogRequestResponse>(LogRequestResponseQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public List<LogRequestResponse> Listar()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<LogRequestResponse>(LogRequestResponseQueries.Listar).ToList();
            }
        }
    }
}