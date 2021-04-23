USE [ControleDespesas]

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='LogRequestResponse') 
BEGIN
	CREATE TABLE [dbo].[LogRequestResponse](
		[LogRequestResponseId] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
		[MachineName] [varchar](250) NOT NULL,
		[DataRequest] [datetime] NOT NULL,
		[DataResponse] [datetime] NOT NULL,
		[EndPoint] [varchar](250) NOT NULL,
		[Request] [nvarchar](max) NOT NULL,
		[Response] [nvarchar](max) NOT NULL,
		[TempoDuracao] [bigint] NOT NULL,
	 CONSTRAINT [PK_LogRequestResponse] PRIMARY KEY CLUSTERED 
	(
		[LogRequestResponseId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END