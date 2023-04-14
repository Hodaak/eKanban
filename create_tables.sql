/*
* FILE          : create_tables.cs
* PROJECT       : SENG3070 - Project Kanban
* PROGRAMMER    : Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-24
* DESCRIPTION:
*    This file is to create tables inside eKanban database
*/

-- Assuming you already have a 'eKanban' database created prior to running this script
use "eKanban"
go
-- Drop tables if exist
if exists (select * from sysobjects where id = object_id('dbo.FogLamp') and sysstat & 0xf = 3)
	drop table "dbo"."FogLamp"
GO
if exists (select * from sysobjects where id = object_id('dbo.TestTray') and sysstat & 0xf = 3)
	drop table "dbo"."TestTray"
GO
if exists (select * from sysobjects where id = object_id('dbo.Workstation') and sysstat & 0xf = 3)
	drop table "dbo"."Workstation"
GO
if exists (select * from sysobjects where id = object_id('dbo.Employee') and sysstat & 0xf = 3)
	drop table "dbo"."Employee"
GO
if exists (select * from sysobjects where id = object_id('dbo.ConfigurationTable') and sysstat & 0xf = 3)
	drop table "dbo"."ConfigurationTable"
GO

-- Create Tables
CREATE TABLE "Employee" (
	"ID" int Primary Key IDENTITY (1, 1) NOT NULL,
	"FullName" nvarchar(25) NOT NULL,
	"SkillLevel" nvarchar(25) NOT NULL,
	"IsAvailable" int NOT NULL
)
GO

CREATE TABLE "Workstation" (
	"ID" int Primary Key IDENTITY (1, 1) NOT NULL,
	"Name" nvarchar(25) NOT NULL,
	"WorkerID" int FOREIGN KEY REFERENCES Employee(ID) NULL,
	"NumberOfHarnessLeft" int NOT NULL,
	"NumberOfReflectorLeft" int NOT NULL,
	"NumberOfHousingLeft" int NOT NULL,
	"NumberOfLensLeft" int NOT NULL,
	"NumberOfBulbLeft" int NOT NULL,
	"NumberOfBezelLeft" int NOT NULL,
	"IsNeedToReplenish" int NOT NULL,		--boolean
	"IsAvailable" int NOT NULL,
	"IsAndonRunning" int NOT NULL,
	"NumberOfProductsProduced" int NOT NULL
)
GO

CREATE TABLE "TestTray" (
	"ID" int Primary Key IDENTITY (1, 1) NOT NULL ,
	"WorkstationID" int FOREIGN KEY REFERENCES Workstation(ID) Not NULL,
	"ProductNumber" int NOT NULL,
	"NumberOfPassedFogLamp" int,
	"NumberOfFailedFogLamp" int
)
GO

CREATE TABLE "FogLamp" (
	"ID" int Primary Key IDENTITY (1, 1) NOT NULL,
	"TestTrayID" int FOREIGN KEY REFERENCES TestTray(ID) Not NULL,
	"UnitTestID" nvarchar (15) NULL
)
GO

CREATE TABLE [dbo].[ConfigurationTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[configName] [varchar](50) NOT NULL,
	[configValue] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ConfigurationTable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO