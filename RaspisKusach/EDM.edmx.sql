
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/24/2022 19:15:44
-- Generated from EDMX file: C:\Users\pc\source\repos\RaspisKusach\RaspisKusach\EDM.edmx
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

IF OBJECT_ID(N'[dbo].[FK_Routes_Trains]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Routes] DROP CONSTRAINT [FK_Routes_Trains];
GO
IF OBJECT_ID(N'[dbo].[FK_RoutesStations_Routes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoutesStations] DROP CONSTRAINT [FK_RoutesStations_Routes];
GO
IF OBJECT_ID(N'[dbo].[FK_RoutesStations_Station]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoutesStations] DROP CONSTRAINT [FK_RoutesStations_Station];
GO
IF OBJECT_ID(N'[dbo].[FK_Tickets_Carriages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tickets] DROP CONSTRAINT [FK_Tickets_Carriages];
GO
IF OBJECT_ID(N'[dbo].[FK_Tickets_Routes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tickets] DROP CONSTRAINT [FK_Tickets_Routes];
GO
IF OBJECT_ID(N'[dbo].[FK_Tickets_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tickets] DROP CONSTRAINT [FK_Tickets_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Carriages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Carriages];
GO
IF OBJECT_ID(N'[dbo].[Routes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Routes];
GO
IF OBJECT_ID(N'[dbo].[RoutesStations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoutesStations];
GO
IF OBJECT_ID(N'[dbo].[Station]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Station];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Tickets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tickets];
GO
IF OBJECT_ID(N'[dbo].[Trains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trains];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Carriages'
CREATE TABLE [dbo].[Carriages] (
    [IdCarriage] int  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Routes'
CREATE TABLE [dbo].[Routes] (
    [IdRoute] int  NOT NULL,
    [IdTrain] int  NOT NULL,
    [Departure_Date] datetime  NOT NULL,
    [Arrival_Date] datetime  NOT NULL
);
GO

-- Creating table 'RoutesStations'
CREATE TABLE [dbo].[RoutesStations] (
    [IdRouteStation] int  NOT NULL,
    [IdRoute] int  NOT NULL,
    [IdStation] int  NOT NULL
);
GO

-- Creating table 'Station'
CREATE TABLE [dbo].[Station] (
    [IdStation] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Location] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Tickets'
CREATE TABLE [dbo].[Tickets] (
    [IdTicket] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [IdRoute] int  NOT NULL,
    [IdCarriage] int  NOT NULL,
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

-- Creating primary key on [IdCarriage] in table 'Carriages'
ALTER TABLE [dbo].[Carriages]
ADD CONSTRAINT [PK_Carriages]
    PRIMARY KEY CLUSTERED ([IdCarriage] ASC);
GO

-- Creating primary key on [IdRoute] in table 'Routes'
ALTER TABLE [dbo].[Routes]
ADD CONSTRAINT [PK_Routes]
    PRIMARY KEY CLUSTERED ([IdRoute] ASC);
GO

-- Creating primary key on [IdRouteStation] in table 'RoutesStations'
ALTER TABLE [dbo].[RoutesStations]
ADD CONSTRAINT [PK_RoutesStations]
    PRIMARY KEY CLUSTERED ([IdRouteStation] ASC);
GO

-- Creating primary key on [IdStation] in table 'Station'
ALTER TABLE [dbo].[Station]
ADD CONSTRAINT [PK_Station]
    PRIMARY KEY CLUSTERED ([IdStation] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
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

-- Creating primary key on [IdUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdCarriage] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK_Tickets_Carriages]
    FOREIGN KEY ([IdCarriage])
    REFERENCES [dbo].[Carriages]
        ([IdCarriage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tickets_Carriages'
CREATE INDEX [IX_FK_Tickets_Carriages]
ON [dbo].[Tickets]
    ([IdCarriage]);
GO

-- Creating foreign key on [IdTrain] in table 'Routes'
ALTER TABLE [dbo].[Routes]
ADD CONSTRAINT [FK_Routes_Trains]
    FOREIGN KEY ([IdTrain])
    REFERENCES [dbo].[Trains]
        ([IdTrain])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Routes_Trains'
CREATE INDEX [IX_FK_Routes_Trains]
ON [dbo].[Routes]
    ([IdTrain]);
GO

-- Creating foreign key on [IdRoute] in table 'RoutesStations'
ALTER TABLE [dbo].[RoutesStations]
ADD CONSTRAINT [FK_RoutesStations_Routes]
    FOREIGN KEY ([IdRoute])
    REFERENCES [dbo].[Routes]
        ([IdRoute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoutesStations_Routes'
CREATE INDEX [IX_FK_RoutesStations_Routes]
ON [dbo].[RoutesStations]
    ([IdRoute]);
GO

-- Creating foreign key on [IdRoute] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK_Tickets_Routes]
    FOREIGN KEY ([IdRoute])
    REFERENCES [dbo].[Routes]
        ([IdRoute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tickets_Routes'
CREATE INDEX [IX_FK_Tickets_Routes]
ON [dbo].[Tickets]
    ([IdRoute]);
GO

-- Creating foreign key on [IdStation] in table 'RoutesStations'
ALTER TABLE [dbo].[RoutesStations]
ADD CONSTRAINT [FK_RoutesStations_Station]
    FOREIGN KEY ([IdStation])
    REFERENCES [dbo].[Station]
        ([IdStation])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoutesStations_Station'
CREATE INDEX [IX_FK_RoutesStations_Station]
ON [dbo].[RoutesStations]
    ([IdStation]);
GO

-- Creating foreign key on [IdUser] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK_Tickets_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tickets_Users'
CREATE INDEX [IX_FK_Tickets_Users]
ON [dbo].[Tickets]
    ([IdUser]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------