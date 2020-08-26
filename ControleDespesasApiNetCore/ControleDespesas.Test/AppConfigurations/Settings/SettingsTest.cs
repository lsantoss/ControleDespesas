using LSCode.ConexoesBD.Enums;
using System;

namespace ControleDespesas.Test.AppConfigurations.Settings
{
    public static class SettingsTest
    {
        public static string ControleDespesasAPINetCore { get; } = @"https://localhost:44323/";
        public static string ChaveAPI { get; } = @"3150112e-5285-43a8-bc71-b100fe7233d5";
        public static string ChaveJWT { get; } = @"(B3]U5N{ho+KdXxB%>Q,Fblh9E;O:NE*O8!ke?b@eM,7kk8Ph{DnRp+}u!Hs?LbE+CpP[*}X^&^fR4w0u0E$H{621%Mr[nw#qC}";

        public static Type TipoBancoDeDdos { get; } = typeof(EBancoDadosRelacional);
        public static EBancoDadosRelacional BancoDeDadosRelacional { get; } = EBancoDadosRelacional.SQLServer;
        public static EBancoDadosNaoRelacional BancoDeDadosNaoRelacional { get; } = EBancoDadosNaoRelacional.MongoDB;

        public static string ConnectionSQLServerReal { get; } = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesas;Data Source=SANTOS-PC\SQLEXPRESS;";
        public static string ConnectionSQLServerTest { get; } = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesasTest;Data Source=SANTOS-PC\SQLEXPRESS;";

        public static string ConnectionMySqlReal { get; } = @"";
        public static string ConnectionMySqlTest { get; } = @"";

        public static string ConnectionSQLiteReal { get; } = @"";
        public static string ConnectionSQLiteTest { get; } = @"";

        public static string ConnectionPostgreSQLReal { get; } = @"";
        public static string ConnectionPostgreSQLTest { get; } = @"";

        public static string ConnectionOracleReal { get; } = @"";
        public static string ConnectionOracleTest { get; } = @"";

        public static string ConnectionMongoDBReal { get; } = @"";
        public static string ConnectionMongoDBTest { get; } = @"";
    }
}