using ControleDespesas.Infra.Settings;
using ControleDespesas.Test.AppConfigurations.QueriesSQL;
using Dapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleDespesas.Test.AppConfigurations.Base
{
    [TestFixture]
    public class DatabaseTest : BaseTest
    {
        protected SettingsInfraData MockSettingsInfraData { get; }

        private bool createDatabase = true;

        public DatabaseTest()
        {
            MockSettingsInfraData = new SettingsInfraData() 
            { 
                ConnectionString = MockSettingsTest.ConnectionSQLServerTest 
            };            
        }  

        protected void CriarBaseDeDadosETabelas() => RodarScripts(QueriesSQLServer.QueriesCreate);

        protected void DroparTabelas() => RodarScripts(QueriesSQLServer.QueriesDrop);

        private void RodarScripts(List<string> queries)
        {
            try
            {
                if (createDatabase)
                {
                    createDatabase = false;
                    using (var connection = new SqlConnection(MockSettingsTest.ConnectionSQLServerReal))
                    {
                        connection.Execute(QueriesSQLServer.CreateDataBase);
                    }
                }                

                foreach (string sql in queries)
                {
                    using (var connection = new SqlConnection(MockSettingsTest.ConnectionSQLServerTest))
                    {
                        connection.Execute(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao rodar scripts: {ex.Message}");
            }
        }
    }
}