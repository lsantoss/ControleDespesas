using System.Collections.Generic;

namespace ControleDespesas.Test.AppConfigurations.QueriesSQL
{
    public static class QueriesSQLServer
    {
        private static string CreateDataBase { get; } = @"USE [master] 
                                                          IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ControleDespesasTest')
                                                          BEGIN
                                                          CREATE DATABASE ControleDespesasTest
                                                          END";

        private static string CreateTableEmpresa { get; } = @"USE [ControleDespesasTest] 
                                                              CREATE TABLE [dbo].[Empresa](
	                                                            [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                            [Nome] [nvarchar](100) NOT NULL,
	                                                            [Logo] [text] NOT NULL,
                                                                CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
                                                              (
	                                                             [Id] ASC
                                                              )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";

        private static string CreateTablePessoa { get; } = @"USE [ControleDespesasTest] 
                                                             CREATE TABLE [dbo].[Pessoa](
	                                                            [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                            [Nome] [nvarchar](100) NOT NULL,
	                                                            [ImagemPerfil] [text] NOT NULL,
                                                              CONSTRAINT [PK_Pessoa] PRIMARY KEY CLUSTERED 
                                                             (
	                                                             [Id] ASC
                                                             )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                             ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";

        private static string CreateTableTipoPagamento { get; } = @"USE [ControleDespesasTest] 
                                                                    CREATE TABLE [dbo].[TipoPagamento](
	                                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                                    [Descricao] [nvarchar](250) NOT NULL,
                                                                    CONSTRAINT [PK_TipoPagamento] PRIMARY KEY CLUSTERED 
                                                                    (
	                                                                    [Id] ASC
                                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                    ) ON [PRIMARY]";

        private static string CreateTablePagamento { get; } = @"USE [ControleDespesasTest] 
                                                                CREATE TABLE [dbo].[Pagamento](
	                                                                [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                                [IdTipoPagamento] [int] NOT NULL,
	                                                                [IdEmpresa] [int] NOT NULL,
	                                                                [IdPessoa] [int] NOT NULL,
	                                                                [Descricao] [nvarchar](250) NOT NULL,
	                                                                [Valor] [money] NOT NULL,
	                                                                [DataVencimento] [smalldatetime] NOT NULL,
	                                                                [DataPagamento] [smalldatetime] NULL,
                                                                    CONSTRAINT [PK_Pagamento] PRIMARY KEY CLUSTERED 
                                                                (
	                                                                [Id] ASC
                                                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                ) ON [PRIMARY]

                                                                ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_Empresa] FOREIGN KEY([IdEmpresa])
                                                                REFERENCES [dbo].[Empresa] ([Id])

                                                                ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_Empresa]

                                                                ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_Pessoa] FOREIGN KEY([IdPessoa])
                                                                REFERENCES [dbo].[Pessoa] ([Id])

                                                                ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_Pessoa]

                                                                ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_TipoPagamento] FOREIGN KEY([IdTipoPagamento])
                                                                REFERENCES [dbo].[TipoPagamento] ([Id])

                                                                ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_TipoPagamento]";

        private static string CreateTableUsuario { get; } = @"USE [ControleDespesasTest] 
                                                                CREATE TABLE [dbo].[Usuario](
	                                                                [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                                [Login] [nvarchar](50) NOT NULL,
	                                                                [Senha] [nvarchar](15) NOT NULL,
	                                                                [Privilegio] [int] NOT NULL,
                                                                    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
                                                                (
	                                                                [Id] ASC
                                                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                ) ON [PRIMARY]";

        private static string DropTableUsuario { get; } = @"USE [ControleDespesasTest]
                                                            DROP TABLE IF EXISTS Usuario";

        private static string DropTablePagamento { get; } = @"USE [ControleDespesasTest]
                                                              DROP TABLE IF EXISTS Pagamento";

        private static string DropTableTipoPagamento { get; } = @"USE [ControleDespesasTest]
                                                                  DROP TABLE IF EXISTS TipoPagamento";

        private static string DropTablePessoa { get; } = @"USE [ControleDespesasTest]
                                                           DROP TABLE IF EXISTS Pessoa";

        private static string DropTableEmpresa { get; } = @"USE [ControleDespesasTest]
                                                           DROP TABLE IF EXISTS Empresa";

        private static string DropDataBase { get; } = @"USE [master] DROP DATABASE IF EXISTS [ControleDespesasTest]";

        private static string MatarSessoes { get; } = @"DECLARE @kill varchar(8000) = ''
                                                        SELECT @kill = sys.dm_exec_sessions.session_id
                                                        FROM sys.dm_exec_sessions
                                                        WHERE database_id = db_id('ControleDespesasTest')
                                                        EXEC('kill ' + @kill)";

        public static List<string> QueriesCreate { get; } = new List<string>()
        {
            CreateDataBase,
            CreateTableEmpresa,
            CreateTablePessoa,
            CreateTableTipoPagamento,
            CreateTablePagamento,
            CreateTableUsuario
        };

        public static List<string> QueriesDrop { get; } = new List<string>()
        {
            DropTableUsuario,
            DropTablePagamento,
            DropTableTipoPagamento,
            DropTablePessoa,
            DropTableEmpresa
        };
    }
}