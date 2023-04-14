/*
* FILE          : create_eKanban_database.cs
* PROJECT       : SENG3070 - Project Kanban
* PROGRAMMER    : Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-24
* DESCRIPTION:
*    This file is to create eKanban database 
*/

SET NOCOUNT ON
GO

USE master
GO

if exists (select * from sysdatabases where name='eKanban')
		drop database eKanban
GO

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE eKanban
  ON PRIMARY (NAME = N''eKanban'', FILENAME = N''' + @device_directory + N'eKanban.mdf'')
  LOG ON (NAME = N''eKanban_log'',  FILENAME = N''' + @device_directory + N'eKanban.ldf'')')

set quoted_identifier on
GO

SET DATEFORMAT mdy
GO