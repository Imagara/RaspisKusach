
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/23/2022 00:08:59
-- Generated from EDMX file: C:\Users\milic\source\repos\RaspisKusach\RaspisKusach\EDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RouteScheduleDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Routes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Routes];
GO
IF OBJECT_ID(N'[dbo].[TestTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestTable];
GO
IF OBJECT_ID(N'[dbo].[Tickets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tickets];
GO
IF OBJECT_ID(N'[dbo].[Trains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trains];
GO
IF OBJECT_ID(N'[dbo].[TrainsCarriages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainsCarriages];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Routes'
CREATE TABLE [dbo].[Routes] (
    [IdRoute] int  NOT NULL,
    [IdTrain] int  NOT NULL,
    [Departure_Station] nvarchar(50)  NOT NULL,
    [Arrival_Station] nvarchar(50)  NOT NULL,
    [Departure_Date] datetime  NOT NULL,
    [Arrival_Date] datetime  NOT NULL
);
GO

-- Creating table 'TestTable'
CREATE TABLE [dbo].[TestTable] (
    [IdRoute] int  NOT NULL,
    [IdTrain] int  NOT NULL,
    [Departure] nvarchar(50)  NOT NULL,
    [Arrival] nvarchar(50)  NOT NULL,
    [DepartureDate] datetime  NOT NULL,
    [ArrivalDate] datetime  NOT NULL
);
GO

-- Creating table 'Tickets'
CREATE TABLE [dbo].[Tickets] (
    [IdTicket] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [IdRoute] int  NOT NULL,
    [IdTrainCarriage] int  NOT NULL,
    [PlaceNumber] int  NOT NULL,
    [Category] int  NOT NULL,
    [BuyDate] datetime  NOT NULL
);
GO

-- Creating table 'Trains'
CREATE TABLE [dbo].[Trains] (
    [IdTrain] int  NOT NULL,
    [NameOfTrain] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'TrainsCarriages'
CREATE TABLE [dbo].[TrainsCarriages] (
    [IdCarriage] int  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [IdUser] int  NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Passport] nvarchar(10)  NULL,
    [Surname] nvarchar(50)  NULL,
    [Name] nvarchar(50)  NULL,
    [Patronymic] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdRoute] in table 'Routes'
ALTER TABLE [dbo].[Routes]
ADD CONSTRAINT [PK_Routes]
    PRIMARY KEY CLUSTERED ([IdRoute] ASC);
GO

-- Creating primary key on [IdRoute] in table 'TestTable'
ALTER TABLE [dbo].[TestTable]
ADD CONSTRAINT [PK_TestTable]
    PRIMARY KEY CLUSTERED ([IdRoute] ASC);
GO

-- Creating primary key on [IdTicket] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [PK_Tickets]
    PRIMARY KEY CLUSTERED ([IdTicket] ASC);
GO

-- Creating primary key on [IdTrain] in table 'Trains'
ALTER TABLE [dbo].[Trains]
ADD CONSTRAINT [PK_Trains]
    PRIMARY KEY CLUSTERED ([IdTrain] ASC);
GO

-- Creating primary key on [IdCarriage] in table 'TrainsCarriages'
ALTER TABLE [dbo].[TrainsCarriages]
ADD CONSTRAINT [PK_TrainsCarriages]
    PRIMARY KEY CLUSTERED ([IdCarriage] ASC);
GO

-- Creating primary key on [IdUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------