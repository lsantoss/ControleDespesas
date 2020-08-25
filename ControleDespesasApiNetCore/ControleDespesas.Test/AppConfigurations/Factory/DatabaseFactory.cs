using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.QueriesSQL;
using ControleDespesas.Test.AppConfigurations.Settings;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Test.AppConfigurations.Factory
{
    [TestFixture]
    public class DatabaseFactory
    {
        private string _connectionReal;
        private string _connectionTest;
        protected readonly SettingsInfraData _settingsInfraData;

        public DatabaseFactory()
        {
            ConfigurarParamentrosConexaoBaseDeDados();

            _settingsInfraData = new SettingsInfraData() { ConnectionString = _connectionTest };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp() { }

        [OneTimeTearDown]
        public void OneTimeTearDown() { }

        private void ConfigurarParamentrosConexaoBaseDeDados()
        {
            if (SettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
            {
                switch (SettingsTest.BancoDeDadosRelacional)
                {
                    case EBancoDadosRelacional.SQLServer:
                        _connectionReal = SettingsTest.ConnectionSQLServerReal;
                        _connectionTest = SettingsTest.ConnectionSQLServerTest;
                        break;

                    case EBancoDadosRelacional.MySQL:
                        _connectionReal = SettingsTest.ConnectionMySqlReal;
                        _connectionTest = SettingsTest.ConnectionMySqlTest;
                        break;

                    case EBancoDadosRelacional.SQLite:
                        _connectionReal = SettingsTest.ConnectionSQLiteReal;
                        _connectionTest = SettingsTest.ConnectionSQLiteTest;
                        break;

                    case EBancoDadosRelacional.PostgreSQL:
                        _connectionReal = SettingsTest.ConnectionPostgreSQLReal;
                        _connectionTest = SettingsTest.ConnectionPostgreSQLTest;
                        break;

                    case EBancoDadosRelacional.Oracle:
                        _connectionReal = SettingsTest.ConnectionOracleReal;
                        _connectionTest = SettingsTest.ConnectionOracleTest;
                        break;
                }
            }
            else if (SettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
            {
                switch (SettingsTest.BancoDeDadosNaoRelacional)
                {
                    case EBancoDadosNaoRelacional.MongoDB:
                        _connectionReal = SettingsTest.ConnectionMongoDBReal;
                        _connectionTest = SettingsTest.ConnectionMongoDBTest;
                        break;
                }
            }
            else
            {
                throw new Exception("Erro ao configurar tipo de base de dados para testes");
            }
        }

        private void RodarScripts(List<string> queries)
        {
            DbContext ctx = new DbContext(EBancoDadosRelacional.SQLServer, _connectionReal);

            foreach (string sql in queries)
            {
                try
                {
                    ctx.SQLServerConexao.Execute(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        protected void CriarBaseDeDadosETabelas()
        {
            if (SettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
            {
                switch (SettingsTest.BancoDeDadosRelacional)
                {
                    case EBancoDadosRelacional.SQLServer:
                        RodarScripts(QueriesSQLServer.QueriesCreate);
                        break;

                    case EBancoDadosRelacional.MySQL:
                        throw new NotImplementedException("Queries MySQL ainda não foram criadas");
                        break;

                    case EBancoDadosRelacional.SQLite:
                        throw new NotImplementedException("Queries SQLite ainda não foram criadas");
                        break;

                    case EBancoDadosRelacional.PostgreSQL:
                        throw new NotImplementedException("Queries PostgreSQL ainda não foram criadas");
                        break;

                    case EBancoDadosRelacional.Oracle:
                        throw new NotImplementedException("Queries Oracle ainda não foram criadas");
                        break;
                }
            }
            else if (SettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
            {
                switch (SettingsTest.BancoDeDadosNaoRelacional)
                {
                    case EBancoDadosNaoRelacional.MongoDB:
                        throw new NotImplementedException("Queries MongoDB ainda não foram criadas");
                        break;
                }
            }
            else
            {
                throw new Exception("Erro ao rodar scripts de criação de base de dados e tabelas");
            }
        }

        protected void DroparBaseDeDados()
        {
            if (SettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
            {
                switch (SettingsTest.BancoDeDadosRelacional)
                {
                    case EBancoDadosRelacional.SQLServer:
                        RodarScripts(QueriesSQLServer.QueriesDrop);
                        break;

                    case EBancoDadosRelacional.MySQL:
                        throw new NotImplementedException("Queries MySQL ainda não foram criadas");
                        break;

                    case EBancoDadosRelacional.SQLite:
                        throw new NotImplementedException("Queries SQLite ainda não foram criadas");
                        break;

                    case EBancoDadosRelacional.PostgreSQL:
                        throw new NotImplementedException("Queries PostgreSQL ainda não foram criadas");
                        break;

                    case EBancoDadosRelacional.Oracle:
                        throw new NotImplementedException("Queries Oracle ainda não foram criadas");
                        break;
                }
            }
            else if (SettingsTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
            {
                switch (SettingsTest.BancoDeDadosNaoRelacional)
                {
                    case EBancoDadosNaoRelacional.MongoDB:
                        throw new NotImplementedException("Queries MongoDB ainda não foram criadas");
                        break;
                }
            }
            else
            {
                throw new Exception("Erro ao rodar scripts de criação de base de dados e tabelas");
            }
        }
    }
}