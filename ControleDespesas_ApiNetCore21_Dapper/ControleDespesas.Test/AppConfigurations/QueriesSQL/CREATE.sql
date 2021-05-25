--====================================================================================================================================
--========================================== CREATE DATABASE [ControleDespesasTest] ==================================================
--====================================================================================================================================
USE [master]
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ControleDespesasTest')
CREATE DATABASE ControleDespesasTest

GO

--====================================================================================================================================
--================================================= DROP TABLEs & PROCEDURES =========================================================
--====================================================================================================================================
USE [ControleDespesasTest]
IF OBJECT_ID('dbo.ELMAH_Error', 'U') IS NOT NULL 
DROP TABLE dbo.ELMAH_Error

IF OBJECT_ID('dbo.LogRequestResponse', 'U') IS NOT NULL 
DROP TABLE dbo.LogRequestResponse

IF OBJECT_ID('dbo.Pagamento', 'U') IS NOT NULL 
DROP TABLE dbo.Pagamento

IF OBJECT_ID('dbo.Pessoa', 'U') IS NOT NULL 
DROP TABLE dbo.Pessoa

IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL 
DROP TABLE dbo.Usuario

IF OBJECT_ID('dbo.TipoPagamento', 'U') IS NOT NULL 
DROP TABLE dbo.TipoPagamento

IF OBJECT_ID('dbo.Empresa', 'U') IS NOT NULL 
DROP TABLE dbo.Empresa

IF OBJECT_ID('ELMAH_GetErrorXml', 'P') IS NOT NULL
DROP PROCEDURE [dbo].[ELMAH_GetErrorXml]

IF OBJECT_ID('ELMAH_GetErrorsXml', 'P') IS NOT NULL
DROP PROCEDURE [dbo].[ELMAH_GetErrorsXml]

IF OBJECT_ID('ELMAH_LogError', 'P') IS NOT NULL
DROP PROCEDURE [dbo].[ELMAH_LogError]

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[Usuario] ========================================================
--====================================================================================================================================
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
END

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[Empresa] ========================================================
--====================================================================================================================================
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
END

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[TipoPagamento] ==================================================
--====================================================================================================================================
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
END

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[Pessoa] =========================================================
--====================================================================================================================================
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
END

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[Pagamento] ======================================================
--====================================================================================================================================
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
END

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[LogRequestResponse] =============================================
--====================================================================================================================================
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
END

--====================================================================================================================================
--============================================== CREATE TABLE [dbo].[ELMAH_Error] ====================================================
--====================================================================================================================================
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
END

GO

--====================================================================================================================================
--=========================================== CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml] =============================================
--====================================================================================================================================
CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
	@Application NVARCHAR(60),
	@ErrorId UNIQUEIDENTIFIER
)
AS SET NOCOUNT ON

SELECT [AllXml]
FROM [ELMAH_Error]
WHERE [ErrorId] = @ErrorId AND [Application] = @Application

GO

--====================================================================================================================================
--=========================================== CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml] ============================================
--====================================================================================================================================
CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
	@Application NVARCHAR(60),
	@PageIndex INT = 0,
	@PageSize INT = 15,
	@TotalCount INT OUTPUT
)
AS SET NOCOUNT ON

DECLARE @FirstTimeUTC DATETIME
DECLARE @FirstSequence INT
DECLARE @StartRow INT
DECLARE @StartRowIndex INT

SELECT @TotalCount = COUNT(1) 
FROM [ELMAH_Error]
WHERE [Application] = @Application

SET @StartRowIndex = @PageIndex * @PageSize + 1

IF @StartRowIndex <= @TotalCount
	BEGIN
		SET ROWCOUNT @StartRowIndex

		SELECT  
			@FirstTimeUTC = [TimeUtc],
			@FirstSequence = [Sequence]
		FROM 
			[ELMAH_Error]
		WHERE   
			[Application] = @Application
		ORDER BY 
			[TimeUtc] DESC, 
			[Sequence] DESC
	END
ELSE
	BEGIN
		SET @PageSize = 0
	END

SET ROWCOUNT @PageSize

SELECT 
	errorId     = [ErrorId], 
	application = [Application],
	host        = [Host], 
	type        = [Type],
	source      = [Source],
	message     = [Message],
	[user]      = [User],
	statusCode  = [StatusCode], 
	time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
FROM [ELMAH_Error] error
WHERE [Application] = @Application
AND [TimeUtc] <= @FirstTimeUTC
AND [Sequence] <= @FirstSequence
ORDER BY [TimeUtc] DESC, [Sequence] DESC
FOR XML AUTO

GO

--====================================================================================================================================
--============================================= CREATE PROCEDURE [dbo].[ELMAH_LogError] ==============================================
--====================================================================================================================================
CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NVARCHAR(MAX),
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS SET NOCOUNT ON

INSERT INTO [ELMAH_Error]
(
    [ErrorId],
    [Application],
    [Host],
    [Type],
    [Source],
    [Message],
    [User],
    [AllXml],
    [StatusCode],
    [TimeUtc]
)
VALUES
(
    @ErrorId,
    @Application,
    @Host,
    @Type,
    @Source,
    @Message,
    @User,
    @AllXml,
    @StatusCode,
    @TimeUtc
)