--====================================================================================================================================
--============================================ DROP TABLE [dbo].[ELMAH_Error] ========================================================
--====================================================================================================================================
USE [ControleDespesasTest]
IF OBJECT_ID('dbo.ELMAH_Error', 'U') IS NOT NULL 
DROP TABLE dbo.ELMAH_Error

--====================================================================================================================================
--========================================== DROP TABLE [dbo].[LogRequestResponse] ===================================================
--====================================================================================================================================
IF OBJECT_ID('dbo.LogRequestResponse', 'U') IS NOT NULL 
DROP TABLE dbo.LogRequestResponse

--====================================================================================================================================
--============================================ DROP TABLE [dbo].[Pagamento] ==========================================================
--====================================================================================================================================
IF OBJECT_ID('dbo.Pagamento', 'U') IS NOT NULL 
DROP TABLE dbo.Pagamento

--====================================================================================================================================
--============================================== DROP TABLE [dbo].[Pessoa] ===========================================================
--====================================================================================================================================
IF OBJECT_ID('dbo.Pessoa', 'U') IS NOT NULL 
DROP TABLE dbo.Pessoa

--====================================================================================================================================
--============================================= DROP TABLE [dbo].[Usuario] ===========================================================
--====================================================================================================================================
IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL 
DROP TABLE dbo.Usuario

--====================================================================================================================================
--============================================ DROP TABLE [dbo].[TipoPagamento] ======================================================
--====================================================================================================================================
IF OBJECT_ID('dbo.TipoPagamento', 'U') IS NOT NULL 
DROP TABLE dbo.TipoPagamento

--====================================================================================================================================
--============================================== DROP TABLE [dbo].[Empresa] ==========================================================
--====================================================================================================================================
IF OBJECT_ID('dbo.Empresa', 'U') IS NOT NULL 
DROP TABLE dbo.Empresa

--====================================================================================================================================
--========================================== DROP PROCEDURE [dbo].[ELMAH_GetErrorXml] ================================================
--====================================================================================================================================
IF OBJECT_ID('ELMAH_GetErrorXml', 'P') IS NOT NULL
DROP PROCEDURE [dbo].[ELMAH_GetErrorXml]

--====================================================================================================================================
--========================================== DROP PROCEDURE [dbo].[ELMAH_GetErrorsXml] ================================================
--====================================================================================================================================
IF OBJECT_ID('ELMAH_GetErrorsXml', 'P') IS NOT NULL
DROP PROCEDURE [dbo].[ELMAH_GetErrorsXml]

--====================================================================================================================================
--============================================ DROP PROCEDURE [dbo].[ELMAH_LogError] =================================================
--====================================================================================================================================
IF OBJECT_ID('ELMAH_LogError', 'P') IS NOT NULL
DROP PROCEDURE [dbo].[ELMAH_LogError]