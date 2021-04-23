using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Infra.Logs;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class LogRequestResponseRepository : ILogRequestResponseRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public LogRequestResponseRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public void Adicionar(LogRequestResponse entidade)
        {
            _parametros.Add("@MachineName", entidade.MachineName);
            _parametros.Add("@DataRequest", entidade.DataRequest);
            _parametros.Add("@DataResponse", entidade.DataResponse);
            _parametros.Add("@EndPoint", entidade.EndPoint);
            _parametros.Add("@Request", entidade.Request);
            _parametros.Add("@Response", entidade.Response);
            _parametros.Add("@TempoDuracao", entidade.TempoDuracao);

            _dataContext.SQLServerConexao.Execute(LogRequestResponseQueries.Adicionar, _parametros);
        }
    }
}