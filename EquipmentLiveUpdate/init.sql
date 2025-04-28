USE [master]
GO

IF DB_ID('EquipmentLiveUpdate') IS NOT NULL
  set noexec on 

CREATE DATABASE [EquipmentLiveUpdate];
GO

USE [EquipmentLiveUpdate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE LOGIN [appuser] WITH PASSWORD = 'Passsword!123'
GO

CREATE USER [appuser] FOR LOGIN [appuser]
GO

EXEC sp_addrolemember N'db_owner', N'appuser'
GO