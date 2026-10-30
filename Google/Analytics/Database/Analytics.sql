-- ============================================================================
-- ApiToolKit - Google Analytics (GA4)
-- Database: ApiToolKit_Analytics
-- Generated from the restored backup schema; safe to run more than once
-- (the module also runs it automatically on startup).
-- ============================================================================
IF DB_ID(N'ApiToolKit_Analytics') IS NULL
    CREATE DATABASE [ApiToolKit_Analytics];
GO

USE [ApiToolKit_Analytics];
GO

IF OBJECT_ID(N'dbo.AnalyticsApiLogs', N'U') IS NULL
CREATE TABLE dbo.[AnalyticsApiLogs]
(
    [Id] BIGINT IDENTITY(1,1) NOT NULL,
    [PropertyId] NVARCHAR(100) NOT NULL,
    [ReportType] NVARCHAR(100) NOT NULL,
    [RequestBody] NVARCHAR(MAX) NULL,
    [ResponseBody] NVARCHAR(MAX) NULL,
    [IsSuccess] BIT NOT NULL DEFAULT ((0)),
    [StatusCode] INT NOT NULL DEFAULT ((0)),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.AnalyticsConfig', N'U') IS NULL
CREATE TABLE dbo.[AnalyticsConfig]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ClientId] NVARCHAR(500) NOT NULL,
    [ClientSecret] NVARCHAR(500) NOT NULL,
    [RedirectUri] NVARCHAR(1000) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT ((1)),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.AnalyticsSavedReports', N'U') IS NULL
CREATE TABLE dbo.[AnalyticsSavedReports]
(
    [Id] BIGINT IDENTITY(1,1) NOT NULL,
    [PropertyId] NVARCHAR(100) NOT NULL,
    [ReportName] NVARCHAR(200) NOT NULL,
    [Query] NVARCHAR(MAX) NULL,
    [ResponseJson] NVARCHAR(MAX) NULL,
    [RowCount] INT NOT NULL CONSTRAINT [DF_AnalyticsSavedReports_RowCount] DEFAULT ((0)),
    [CreatedAt] DATETIME2 NOT NULL CONSTRAINT [DF_AnalyticsSavedReports_CreatedAt] DEFAULT (sysutcdatetime()),
    CONSTRAINT [PK_AnalyticsSavedReports] PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.AnalyticsTokens', N'U') IS NULL
CREATE TABLE dbo.[AnalyticsTokens]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [PropertyId] NVARCHAR(100) NOT NULL,
    [AccessToken] NVARCHAR(MAX) NOT NULL,
    [RefreshToken] NVARCHAR(MAX) NULL,
    [ExpiresAt] DATETIME2 NOT NULL,
    [Scope] NVARCHAR(500) NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_GetActiveConfig
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM AnalyticsConfig WHERE IsActive = 1 ORDER BY Id DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_GetReportsByPropertyId
    @PropertyId NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM AnalyticsSavedReports
    WHERE PropertyId = @PropertyId
    ORDER BY CreatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_GetTokenByPropertyId
    @PropertyId NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM AnalyticsTokens WHERE PropertyId = @PropertyId;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_SaveApiLog
    @PropertyId     NVARCHAR(100),
    @ReportType     NVARCHAR(100),
    @RequestBody    NVARCHAR(MAX) = NULL,
    @ResponseBody   NVARCHAR(MAX) = NULL,
    @IsSuccess      BIT,
    @StatusCode     INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AnalyticsApiLogs (PropertyId, ReportType, RequestBody, ResponseBody, IsSuccess, StatusCode)
    VALUES (@PropertyId, @ReportType, @RequestBody, @ResponseBody, @IsSuccess, @StatusCode);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_SaveReport
    @PropertyId   NVARCHAR(100),
    @ReportName   NVARCHAR(200),
    @Query        NVARCHAR(MAX) = NULL,
    @ResponseJson NVARCHAR(MAX) = NULL,
    @RowCount     INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.AnalyticsSavedReports (PropertyId, ReportName, Query, ResponseJson, [RowCount])
    VALUES (@PropertyId, @ReportName, @Query, @ResponseJson, @RowCount);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_SaveToken
    @PropertyId     NVARCHAR(100),
    @AccessToken    NVARCHAR(MAX),
    @RefreshToken   NVARCHAR(MAX) = NULL,
    @ExpiresAt      DATETIME2,
    @Scope          NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AnalyticsTokens (PropertyId, AccessToken, RefreshToken, ExpiresAt, Scope)
    VALUES (@PropertyId, @AccessToken, @RefreshToken, @ExpiresAt, @Scope);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Analytics_UpdateToken
    @Id             INT,
    @AccessToken    NVARCHAR(MAX),
    @RefreshToken   NVARCHAR(MAX) = NULL,
    @ExpiresAt      DATETIME2,
    @Scope          NVARCHAR(500) = NULL,
    @UpdatedAt      DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE AnalyticsTokens
    SET AccessToken = @AccessToken,
        RefreshToken = ISNULL(@RefreshToken, RefreshToken),
        ExpiresAt = @ExpiresAt,
        Scope = ISNULL(@Scope, Scope),
        UpdatedAt = @UpdatedAt
    WHERE Id = @Id;
END
GO
