using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly DataContext _dataContext;

        public HealthCheckRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public void Verificar()
        {
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarELMAH_Error);
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarLogRequestResponse);
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarTipoPagamento);
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarEmpresa);
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarUsuario);
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarPessoa);
            _dataContext.SQLServerConexao.Query(HealthCheckQueries.VerificarPagamento);
        }
    }
}