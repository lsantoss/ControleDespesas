using ControleDespesas.Infra.Settings;
using ControleDespesas.Test.AppConfigurations.QueriesSQL;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Test.AppConfigurations.Base
{
    [TestFixture]
    public class DatabaseTest : BaseTest
    {
        protected SettingsInfraData MockSettingsInfraData { get; }

        public DatabaseTest()
        {
            MockSettingsInfraData = new SettingsInfraData() { ConnectionString = MockSettingsTest.ConnectionSQLServerTest };            
        }  

        private void RodarScripts(List<string> queries)
        {
            try
            {
                DataContext ctx = new DataContext(EBancoDadosRelacional.SQLServer, MockSettingsInfraData.ConnectionString);

                foreach (string sql in queries) 
                    ctx.SQLServerConexao.Execute(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao rodar scripts: {ex.Message}");
            }
        }

        protected void CriarBaseDeDadosETabelas() => RodarScripts(QueriesSQLServer.QueriesCreate);

        protected void DroparTabelas() => RodarScripts(QueriesSQLServer.QueriesDrop);
    }
}