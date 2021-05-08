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

        private static string CreateTableUsuario { get; } = @"USE [ControleDespesasTest] 
                                                                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Usuario') 
                                                                BEGIN
                                                                CREATE TABLE [dbo].[Usuario](
	                                                                [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                                [Login] [nvarchar](50) NOT NULL,
	                                                                [Senha] [nvarchar](15) NOT NULL,
	                                                                [Privilegio] [int] NOT NULL,
                                                                    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
                                                                (
	                                                                [Id] ASC
                                                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                ) ON [PRIMARY]
                                                                END";

        private static string CreateTableEmpresa { get; } = @"USE [ControleDespesasTest] 
                                                              IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Empresa') 
                                                              BEGIN
                                                                  CREATE TABLE [dbo].[Empresa] (
                                                                      [Id] [int] IDENTITY(1,1) NOT NULL,
                                                                      [Nome] [nvarchar](100) NOT NULL,
                                                                      [Logo] [text] NOT NULL,
                                                                      CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
                                                                  (
	                                                                  [Id] ASC
                                                                  )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                  ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                                              END";

        private static string CreateTableTipoPagamento { get; } = @"USE [ControleDespesasTest] 
                                                                    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='TipoPagamento') 
                                                                    BEGIN
                                                                        CREATE TABLE [dbo].[TipoPagamento](
	                                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                                    [Descricao] [nvarchar](250) NOT NULL,
                                                                        CONSTRAINT [PK_TipoPagamento] PRIMARY KEY CLUSTERED 
                                                                    (
	                                                                    [Id] ASC
                                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                    ) ON [PRIMARY]
                                                                    END";

        private static string CreateTablePessoa { get; } = @"USE [ControleDespesasTest]
                                                             IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Pessoa') 
                                                             BEGIN
                                                             CREATE TABLE [dbo].[Pessoa](
	                                                             [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                             [IdUsuario] [int] NOT NULL,
	                                                             [Nome] [nvarchar](100) NOT NULL,
	                                                             [ImagemPerfil] [text] NOT NULL,
                                                                 CONSTRAINT [PK_Pessoa] PRIMARY KEY CLUSTERED 
                                                             (
	                                                             [Id] ASC
                                                             )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                             ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                                                             ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK_Pessoa_Usuario] FOREIGN KEY([IdUsuario])
                                                             REFERENCES [dbo].[Usuario] ([Id])

                                                             ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK_Pessoa_Usuario]
                                                             END";        

        private static string CreateTablePagamento { get; } = @"USE [ControleDespesasTest] 
                                                                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Pagamento') 
                                                                BEGIN
                                                                CREATE TABLE [dbo].[Pagamento](
	                                                                [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                                [IdTipoPagamento] [int] NOT NULL,
	                                                                [IdEmpresa] [int] NOT NULL,
	                                                                [IdPessoa] [int] NOT NULL,
	                                                                [Descricao] [nvarchar](250) NOT NULL,
	                                                                [Valor] [money] NOT NULL,
	                                                                [DataVencimento] [smalldatetime] NOT NULL,
	                                                                [DataPagamento] [smalldatetime] NULL,
	                                                                [ArquivoPagamento] [text] NULL,
	                                                                [ArquivoComprovante] [text] NULL,
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

                                                                ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_TipoPagamento]
                                                                END";

        private static string CreateTableLogRequestResponse { get; } = @"USE [ControleDespesasTest] 
                                                                         IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='LogRequestResponse') 
                                                                         BEGIN
	                                                                        CREATE TABLE [dbo].[LogRequestResponse](
		                                                                        [Id] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
		                                                                        [MachineName] [varchar](250) NOT NULL,
		                                                                        [DataRequest] [datetime] NOT NULL,
		                                                                        [DataResponse] [datetime] NOT NULL,
		                                                                        [EndPoint] [varchar](250) NOT NULL,
		                                                                        [Request] [nvarchar](max) NOT NULL,
		                                                                        [Response] [nvarchar](max) NOT NULL,
		                                                                        [TempoDuracao] [bigint] NOT NULL,
	                                                                            CONSTRAINT [PK_LogRequestResponse] PRIMARY KEY CLUSTERED 
	                                                                        (
		                                                                        [Id] ASC
	                                                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	                                                                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                                                         END";

        private static string CreateTableELMAH_Error { get; } = @"USE [ControleDespesasTest] 
                                                                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='ELMAH_Error')
                                                                BEGIN
	                                                                CREATE TABLE [dbo].[ELMAH_Error]
	                                                                (
		                                                                [ErrorId]     UNIQUEIDENTIFIER NOT NULL,
		                                                                [Application] NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		                                                                [Host]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		                                                                [Type]        NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		                                                                [Source]      NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		                                                                [Message]     NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		                                                                [User]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		                                                                [StatusCode]  INT NOT NULL,
		                                                                [TimeUtc]     DATETIME NOT NULL,
		                                                                [Sequence]    INT IDENTITY (1, 1) NOT NULL,
		                                                                [AllXml]      NVARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
	                                                                ) 
	                                                                ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	   
	                                                                ALTER TABLE [dbo].[ELMAH_Error] WITH NOCHECK ADD 
		                                                                CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED ([ErrorId]) ON [PRIMARY] 
	
	                                                                ALTER TABLE [dbo].[ELMAH_Error] ADD 
		                                                                CONSTRAINT [DF_ELMAH_Error_ErrorId] DEFAULT (NEWID()) FOR [ErrorId]
	
	                                                                CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error] 
	                                                                (
		                                                                [Application]   ASC,
		                                                                [TimeUtc]       DESC,
		                                                                [Sequence]      DESC
	                                                                ) 
	                                                                ON [PRIMARY]
                                                                END";

        private static string DropTableELMAH_Error { get; } = @"USE [ControleDespesasTest]
                                                                IF OBJECT_ID('dbo.ELMAH_Error', 'U') IS NOT NULL 
                                                                DROP TABLE dbo.ELMAH_Error";

        private static string DropTableLogRequestResponse { get; } = @"USE [ControleDespesasTest]
                                                                       IF OBJECT_ID('dbo.LogRequestResponse', 'U') IS NOT NULL 
                                                                       DROP TABLE dbo.LogRequestResponse";

        private static string DropTablePagamento { get; } = @"USE [ControleDespesasTest]
                                                              IF OBJECT_ID('dbo.Pagamento', 'U') IS NOT NULL 
                                                              DROP TABLE dbo.Pagamento";

        private static string DropTablePessoa { get; } = @"USE [ControleDespesasTest]
                                                           IF OBJECT_ID('dbo.Pessoa', 'U') IS NOT NULL 
                                                           DROP TABLE dbo.Pessoa";

        private static string DropTableUsuario { get; } = @"USE [ControleDespesasTest]
                                                            IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL 
                                                            DROP TABLE dbo.Usuario";

        private static string DropTableTipoPagamento { get; } = @"USE [ControleDespesasTest]
                                                                  IF OBJECT_ID('dbo.TipoPagamento', 'U') IS NOT NULL 
                                                                  DROP TABLE dbo.TipoPagamento";

        private static string DropTableEmpresa { get; } = @"USE [ControleDespesasTest]
                                                            IF OBJECT_ID('dbo.Empresa', 'U') IS NOT NULL 
                                                            DROP TABLE dbo.Empresa";

        private static string DropDataBase { get; } = @"USE [master] DROP DATABASE IF EXISTS [ControleDespesasTest]";

        private static string MatarSessoes { get; } = @"DECLARE @kill varchar(8000) = ''
                                                        SELECT @kill = sys.dm_exec_sessions.session_id
                                                        FROM sys.dm_exec_sessions
                                                        WHERE database_id = db_id('ControleDespesasTest')
                                                        EXEC('kill ' + @kill)";

        public static List<string> QueriesCreate { get; } = new List<string>()
        {
            CreateDataBase,
            CreateTableUsuario,
            CreateTableEmpresa,
            CreateTableTipoPagamento,
            CreateTablePessoa,
            CreateTablePagamento,
            CreateTableLogRequestResponse,
            CreateTableELMAH_Error
        };

        public static List<string> QueriesDrop { get; } = new List<string>()
        {
            DropTableELMAH_Error,
            DropTableLogRequestResponse,
            DropTablePagamento,
            DropTablePessoa,
            DropTableUsuario,
            DropTableTipoPagamento,
            DropTableEmpresa
        };
    }
}