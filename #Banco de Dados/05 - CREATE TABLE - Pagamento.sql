USE [ControleDespesas] 

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Pagamento') 
BEGIN
CREATE TABLE [dbo].[Pagamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPagamento] [int] NOT NULL,
	[IdEmpresa] [bigint] NOT NULL,
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