using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Settings;
using Dapper;
using System.Data.SqlClient;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly SettingsInfraData _settingsInfraData;

        public HealthCheckRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
        }

        public void Verificar()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Query(HealthCheckQueries.VerificarELMAH_Error);
                connection.Query(HealthCheckQueries.VerificarLogRequestResponse);
                connection.Query(HealthCheckQueries.VerificarTipoPagamento);
                connection.Query(HealthCheckQueries.VerificarEmpresa);
                connection.Query(HealthCheckQueries.VerificarUsuario);
                connection.Query(HealthCheckQueries.VerificarPessoa);
                connection.Query(HealthCheckQueries.VerificarPagamento);
            }
        }
    }
}