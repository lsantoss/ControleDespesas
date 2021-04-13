USE [master] 

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ControleDespesas')
BEGIN
CREATE DATABASE ControleDespesas
END