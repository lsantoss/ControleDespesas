using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Testes.AppConfigurations.QueriesSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleDespesas.Testes.AppConfigurations.Factory
{
    public class DatabaseFactory
    {
        private readonly string _connectionReal;
        private readonly string _connectionTest;
        protected readonly SettingsInfraData _settingsInfraData;

        public DatabaseFactory()
        {
            _connectionReal = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesas;Data Source=SANTOS-PC\SQLEXPRESS;";
            _connectionTest = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesasTest;Data Source=SANTOS-PC\SQLEXPRESS;";

            _settingsInfraData = new SettingsInfraData()
            {
                ConnectionString = _connectionTest
            };
        }

        public void CriarBaseDeDadosETabelas()
        {
            RodarScripts(QueriesSQLServer.QueriesCreate);
        }

        public void DroparBaseDeDados()
        {
            RodarScripts(QueriesSQLServer.QueriesDrop);
        }

        public void RodarScripts(List<string> queries)
        {
            using (SqlConnection con = new SqlConnection(_connectionReal))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    foreach (string sql in queries)
                    {
                        try
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }
    }
}