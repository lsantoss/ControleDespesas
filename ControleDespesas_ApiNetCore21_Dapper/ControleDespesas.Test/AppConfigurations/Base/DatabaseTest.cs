using ControleDespesas.Infra.Settings;
using Dapper;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ControleDespesas.Test.AppConfigurations.Base
{
    [TestFixture]
    public class DatabaseTest : BaseTest
    {
        protected SettingsInfraData MockSettingsInfraData { get; }

        private string ScriptsPath { get; }
        private bool CreateDatabase { get; set; }

        public DatabaseTest()
        {
            MockSettingsInfraData = new SettingsInfraData() 
            { 
                ConnectionString = MockSettingsTest.ConnectionSQLServerTest 
            };

            ScriptsPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\AppConfigurations\QueriesSQL";
            CreateDatabase = true;
        }  

        protected void CriarBaseDeDadosETabelas()
        {
            try
            {
                using (var streamReader = new StreamReader($@"{ScriptsPath}\CREATE.sql"))
                {
                    var scripts = streamReader.ReadToEnd().Split("GO");

                    if (CreateDatabase)
                    {
                        CreateDatabase = false;
                        using (var connection = new SqlConnection(MockSettingsTest.ConnectionSQLServerReal))
                        {
                            connection.Execute(scripts[0]);
                        }
                    }

                    using (var connection = new SqlConnection(MockSettingsTest.ConnectionSQLServerTest))
                    {
                        foreach(var script in scripts)
                            connection.Execute(script);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao nos scripts de create: {ex.Message}");
            }            
        }

        protected void DroparTabelas()
        {
            try
            {
                using (var streamReader = new StreamReader($@"{ScriptsPath}\DROP.sql"))
                {
                    using (var connection = new SqlConnection(MockSettingsTest.ConnectionSQLServerTest))
                    {
                        connection.Execute(streamReader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao nos scripts de drop: {ex.Message}");
            }
        }
    }
}