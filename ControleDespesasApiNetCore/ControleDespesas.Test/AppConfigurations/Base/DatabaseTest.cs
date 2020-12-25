using ControleDespesas.Infra.Data.Settings;
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
        private string ConnectionReal { get; set; }
        private string ConnectionTest { get; set; }
        protected SettingsInfraData MockSettingsInfraData { get; }

        public DatabaseTest()
        {
            ConfigurarParamentrosConexaoBaseDeDados();

            MockSettingsInfraData = new SettingsInfraData() { ConnectionString = ConnectionTest };            
        }        

        private void ConfigurarParamentrosConexaoBaseDeDados()
        {
            try
            {
                if (MockSettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
                {
                    switch (MockSettingsTest.BancoDeDadosRelacional)
                    {
                        case EBancoDadosRelacional.SQLServer:
                            ConnectionReal = MockSettingsTest.ConnectionSQLServerReal;
                            ConnectionTest = MockSettingsTest.ConnectionSQLServerTest;
                            break;

                        case EBancoDadosRelacional.MySQL:
                            ConnectionReal = MockSettingsTest.ConnectionMySqlReal;
                            ConnectionTest = MockSettingsTest.ConnectionMySqlTest;
                            break;

                        case EBancoDadosRelacional.SQLite:
                            ConnectionReal = MockSettingsTest.ConnectionSQLiteReal;
                            ConnectionTest = MockSettingsTest.ConnectionSQLiteTest;
                            break;

                        case EBancoDadosRelacional.PostgreSQL:
                            ConnectionReal = MockSettingsTest.ConnectionPostgreSQLReal;
                            ConnectionTest = MockSettingsTest.ConnectionPostgreSQLTest;
                            break;

                        case EBancoDadosRelacional.Oracle:
                            ConnectionReal = MockSettingsTest.ConnectionOracleReal;
                            ConnectionTest = MockSettingsTest.ConnectionOracleTest;
                            break;
                    }
                }
                else if (MockSettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
                {
                    switch (MockSettingsTest.BancoDeDadosNaoRelacional)
                    {
                        case EBancoDadosNaoRelacional.MongoDB:
                            ConnectionReal = MockSettingsTest.ConnectionMongoDBReal;
                            ConnectionTest = MockSettingsTest.ConnectionMongoDBTest;
                            break;
                    }
                }
                else
                {
                    throw new Exception("Tipo de base de dados incorreto.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao configurar tipo de base de dados para testes: {ex.Message}");
            }
        }

        private void RodarScripts(List<string> queries)
        {
            try
            {
                DataContext ctx = new DataContext(EBancoDadosRelacional.SQLServer, ConnectionReal);

                foreach (string sql in queries) 
                    ctx.SQLServerConexao.Execute(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao rodar scripts: {ex.Message}");
            }
        }

        protected void CriarBaseDeDadosETabelas()
        {
            try
            {
                if (MockSettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
                {
                    switch (MockSettingsTest.BancoDeDadosRelacional)
                    {
                        case EBancoDadosRelacional.SQLServer:
                            RodarScripts(QueriesSQLServer.QueriesCreate);
                            break;

                        case EBancoDadosRelacional.MySQL:
                            throw new NotImplementedException("Queries MySQL ainda não foram criadas");

                        case EBancoDadosRelacional.SQLite:
                            throw new NotImplementedException("Queries SQLite ainda não foram criadas");

                        case EBancoDadosRelacional.PostgreSQL:
                            throw new NotImplementedException("Queries PostgreSQL ainda não foram criadas");

                        case EBancoDadosRelacional.Oracle:
                            throw new NotImplementedException("Queries Oracle ainda não foram criadas");
                    }
                }
                else if (MockSettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
                {
                    switch (MockSettingsTest.BancoDeDadosNaoRelacional)
                    {
                        case EBancoDadosNaoRelacional.MongoDB:
                            throw new NotImplementedException("Collections no MongoDB ainda não foram criadas");
                    }
                }
                else
                {
                    throw new Exception("Tipo de base de dados incorreto.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao rodar scripts de criação de base de dados e tabelas: {ex.Message}");
            }
        }

        protected void DroparTabelas()
        {
            try
            {
                if (MockSettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
                {
                    switch (MockSettingsTest.BancoDeDadosRelacional)
                    {
                        case EBancoDadosRelacional.SQLServer:
                            RodarScripts(QueriesSQLServer.QueriesDrop);
                            break;

                        case EBancoDadosRelacional.MySQL:
                            throw new NotImplementedException("Queries MySQL ainda não foram criadas");

                        case EBancoDadosRelacional.SQLite:
                            throw new NotImplementedException("Queries SQLite ainda não foram criadas");

                        case EBancoDadosRelacional.PostgreSQL:
                            throw new NotImplementedException("Queries PostgreSQL ainda não foram criadas");

                        case EBancoDadosRelacional.Oracle:
                            throw new NotImplementedException("Queries Oracle ainda não foram criadas");
                    }
                }
                else if (MockSettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
                {
                    switch (MockSettingsTest.BancoDeDadosNaoRelacional)
                    {
                        case EBancoDadosNaoRelacional.MongoDB:
                            throw new NotImplementedException("Collections no MongoDB ainda não foram criadas");
                    }
                }
                else
                {
                    throw new Exception("Tipo de base de dados incorreto.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao rodar scripts de criação de base de dados e tabelas: {ex.Message}");
            }
        }
    }
}