using ControleDespesas.Api.Settings;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.QueriesSQL;
using ControleDespesas.Test.AppConfigurations.Settings;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using Microsoft.Extensions.Options;
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
        private  readonly SettingsTest _settingTest;
        protected readonly SettingsInfraData _settingsInfraData;
        protected readonly SettingsAPI _settingsAPI;

        public DatabaseFactory()
        {
            _settingTest = new SettingsTest();

            ConfigurarParamentrosConexaoBaseDeDados();

            _settingsInfraData = new SettingsInfraData() { ConnectionString = _connectionTest };

            _settingsAPI = new SettingsAPI()
            {
                ControleDespesasAPINetCore = _settingTest.ControleDespesasAPINetCore,
                ChaveAPI = _settingTest.ChaveAPI,
                ChaveJWT = _settingTest.ChaveJWT
            };
        }

        [OneTimeSetUp]
        private void OneTimeSetUp() { }

        [OneTimeTearDown]
        private void OneTimeTearDown() { }

        private void ConfigurarParamentrosConexaoBaseDeDados()
        {
            try
            {
                if (_settingTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
                {
                    switch (_settingTest.BancoDeDadosRelacional)
                    {
                        case EBancoDadosRelacional.SQLServer:
                            _connectionReal = _settingTest.ConnectionSQLServerReal;
                            _connectionTest = _settingTest.ConnectionSQLServerTest;
                            break;

                        case EBancoDadosRelacional.MySQL:
                            _connectionReal = _settingTest.ConnectionMySqlReal;
                            _connectionTest = _settingTest.ConnectionMySqlTest;
                            break;

                        case EBancoDadosRelacional.SQLite:
                            _connectionReal = _settingTest.ConnectionSQLiteReal;
                            _connectionTest = _settingTest.ConnectionSQLiteTest;
                            break;

                        case EBancoDadosRelacional.PostgreSQL:
                            _connectionReal = _settingTest.ConnectionPostgreSQLReal;
                            _connectionTest = _settingTest.ConnectionPostgreSQLTest;
                            break;

                        case EBancoDadosRelacional.Oracle:
                            _connectionReal = _settingTest.ConnectionOracleReal;
                            _connectionTest = _settingTest.ConnectionOracleTest;
                            break;
                    }
                }
                else if (_settingTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
                {
                    switch (_settingTest.BancoDeDadosNaoRelacional)
                    {
                        case EBancoDadosNaoRelacional.MongoDB:
                            _connectionReal = _settingTest.ConnectionMongoDBReal;
                            _connectionTest = _settingTest.ConnectionMongoDBTest;
                            break;
                    }
                }
                else
                {
                    throw new Exception("Erro ao configurar tipo de base de dados para testes");
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
            catch (Exception ex)
            {
                throw new Exception($"Erro ao rodar scripts: {ex.Message}");
            }
        }

        protected void CriarBaseDeDadosETabelas()
        {
            try
            {
                if (_settingTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
                {
                    switch (_settingTest.BancoDeDadosRelacional)
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
                else if (_settingTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
                {
                    switch (_settingTest.BancoDeDadosNaoRelacional)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void DroparTabelas()
        {
            try
            {
                if (_settingTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosRelacional).FullName)
                {
                    switch (_settingTest.BancoDeDadosRelacional)
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
                else if (_settingTest.TipoBancoDeDdos.FullName == typeof(EBancoDadosNaoRelacional).FullName)
                {
                    switch (_settingTest.BancoDeDadosNaoRelacional)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}