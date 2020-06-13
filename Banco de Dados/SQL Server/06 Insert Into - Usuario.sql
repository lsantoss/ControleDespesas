USE [ControleDespesas]
GO

INSERT INTO [dbo].[Usuario] ([Login],[Senha],[Privilegio])

VALUES 
('admin', 'admin',1),
('readonly', 'readonly',2)

GO