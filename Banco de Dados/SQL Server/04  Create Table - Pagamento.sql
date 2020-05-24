USE [ControleDespesas]
GO

/****** Object:  Table [dbo].[Pagamento]    Script Date: 23/05/2020 22:15:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pagamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPagamento] [int] NOT NULL,
	[IdEmpresa] [int] NOT NULL,
	[IdPessoa] [int] NOT NULL,
	[Descricao] [nvarchar](250) NOT NULL,
	[Valor] [money] NOT NULL,
	[DataPagamento] [smalldatetime] NOT NULL,
	[DataVencimento] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Pagamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([Id])
GO

ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_Empresa]
GO

ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_Pessoa] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([Id])
GO

ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_Pessoa]
GO

ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_TipoPagamento] FOREIGN KEY([IdTipoPagamento])
REFERENCES [dbo].[TipoPagamento] ([Id])
GO

ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_TipoPagamento]
GO