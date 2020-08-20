using ControleDespesas.Testes.AppConfigurations.QueriesSQL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ControleDespesas.Testes.AppConfigurations.Factory
{
    public class DatabaseFactory
    {
        private string _connectionTest;

        public DatabaseFactory()
        {
            _connectionTest = ConfigurationManager.ConnectionStrings["connectionSetUpTest"].ConnectionString;
        }

        //[OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RodarScripts(QueriesSQLServer.QueriesCreate);
        }

        //[OneTimeTearDown]
        public void OneTimeTearDown()
        {
            RodarScripts(QueriesSQLServer.QueriesDrop);
        }

        public void RodarScripts(List<string> queries)
        {
            using (SqlConnection con = new SqlConnection(_connectionTest))
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