-- ============================================================================
-- ApiToolKit - Google Ads API
-- Database: GoogleApiToolKit_Ads
-- Generated from the restored backup schema; safe to run more than once
-- (the module also runs it automatically on startup).
-- ============================================================================
IF DB_ID(N'GoogleApiToolKit_Ads') IS NULL
    CREATE DATABASE [GoogleApiToolKit_Ads];
GO

USE [GoogleApiToolKit_Ads];
GO

IF OBJECT_ID(N'dbo.GoogleAds_AdGroups', N'U') IS NULL
CREATE TABLE dbo.[GoogleAds_AdGroups]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ResourceName] NVARCHAR(512) NOT NULL,
    [CustomerId] NVARCHAR(50) NOT NULL,
    [CampaignResourceName] NVARCHAR(512) NOT NULL,
    [Name] NVARCHAR(256) NOT NULL,
    [Status] NVARCHAR(50) NOT NULL DEFAULT (''),
    [Type] NVARCHAR(100) NOT NULL DEFAULT (''),
    [CpcBidMicros] BIGINT NOT NULL DEFAULT ((0)),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.GoogleAds_ApiLogs', N'U') IS NULL
CREATE TABLE dbo.[GoogleAds_ApiLogs]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CustomerId] NVARCHAR(50) NOT NULL DEFAULT (''),
    [ApiName] NVARCHAR(256) NOT NULL,
    [RequestBody] NVARCHAR(MAX) NOT NULL DEFAULT (''),
    [ResponseBody] NVARCHAR(MAX) NOT NULL DEFAULT (''),
    [IsSuccess] BIT NOT NULL DEFAULT ((0)),
    [StatusCode] INT NOT NULL DEFAULT ((0)),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.GoogleAds_Campaigns', N'U') IS NULL
CREATE TABLE dbo.[GoogleAds_Campaigns]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ResourceName] NVARCHAR(512) NOT NULL,
    [CustomerId] NVARCHAR(50) NOT NULL,
    [Name] NVARCHAR(256) NOT NULL,
    [Status] NVARCHAR(50) NOT NULL DEFAULT (''),
    [AdvertisingChannelType] NVARCHAR(50) NOT NULL DEFAULT (''),
    [BiddingStrategyType] NVARCHAR(100) NOT NULL DEFAULT (''),
    [BudgetAmountMicros] BIGINT NOT NULL DEFAULT ((0)),
    [StartDate] NVARCHAR(20) NOT NULL DEFAULT (''),
    [EndDate] NVARCHAR(20) NOT NULL DEFAULT (''),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.GoogleAds_Config', N'U') IS NULL
CREATE TABLE dbo.[GoogleAds_Config]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ClientId] NVARCHAR(256) NOT NULL,
    [ClientSecret] NVARCHAR(512) NOT NULL,
    [DeveloperToken] NVARCHAR(256) NOT NULL,
    [RedirectUri] NVARCHAR(512) NOT NULL,
    [LoginCustomerId] NVARCHAR(50) NOT NULL DEFAULT (''),
    [IsActive] BIT NOT NULL DEFAULT ((1)),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.GoogleAds_Keywords', N'U') IS NULL
CREATE TABLE dbo.[GoogleAds_Keywords]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ResourceName] NVARCHAR(512) NOT NULL,
    [CustomerId] NVARCHAR(50) NOT NULL,
    [AdGroupResourceName] NVARCHAR(512) NOT NULL,
    [Text] NVARCHAR(256) NOT NULL,
    [MatchType] NVARCHAR(50) NOT NULL DEFAULT (''),
    [Status] NVARCHAR(50) NOT NULL DEFAULT (''),
    [CpcBidMicros] BIGINT NOT NULL DEFAULT ((0)),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

IF OBJECT_ID(N'dbo.GoogleAds_Tokens', N'U') IS NULL
CREATE TABLE dbo.[GoogleAds_Tokens]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CustomerId] NVARCHAR(50) NOT NULL,
    [AccessToken] NVARCHAR(MAX) NOT NULL,
    [RefreshToken] NVARCHAR(MAX) NOT NULL,
    [ExpiresAt] DATETIME2 NOT NULL,
    [Scope] NVARCHAR(1024) NOT NULL DEFAULT (''),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT (getutcdate()),
    PRIMARY KEY ([Id])
);
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_GetAdGroupsByCampaign
    @CustomerId             NVARCHAR(50),
    @CampaignResourceName   NVARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM GoogleAds_AdGroups
    WHERE CustomerId = @CustomerId AND CampaignResourceName = @CampaignResourceName
    ORDER BY CreatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_GetCampaignsByCustomer
    @CustomerId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM GoogleAds_Campaigns WHERE CustomerId = @CustomerId ORDER BY CreatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_GetConfig
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM GoogleAds_Config WHERE IsActive = 1 ORDER BY Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_GetKeywordsByAdGroup
    @CustomerId             NVARCHAR(50),
    @AdGroupResourceName    NVARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM GoogleAds_Keywords
    WHERE CustomerId = @CustomerId AND AdGroupResourceName = @AdGroupResourceName
    ORDER BY CreatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_GetToken
    @CustomerId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM GoogleAds_Tokens WHERE CustomerId = @CustomerId ORDER BY UpdatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_SaveAdGroup
    @ResourceName           NVARCHAR(512),
    @CustomerId             NVARCHAR(50),
    @CampaignResourceName   NVARCHAR(512),
    @Name                   NVARCHAR(256),
    @Status                 NVARCHAR(50),
    @Type                   NVARCHAR(100),
    @CpcBidMicros           BIGINT,
    @CreatedAt              DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM GoogleAds_AdGroups WHERE ResourceName = @ResourceName)
    BEGIN
        UPDATE GoogleAds_AdGroups
        SET CustomerId           = @CustomerId,
            CampaignResourceName = @CampaignResourceName,
            Name                 = @Name,
            Status               = @Status,
            Type                 = @Type,
            CpcBidMicros         = @CpcBidMicros
        WHERE ResourceName = @ResourceName;
    END
    ELSE
    BEGIN
        INSERT INTO GoogleAds_AdGroups
            (ResourceName, CustomerId, CampaignResourceName, Name, Status, Type, CpcBidMicros, CreatedAt)
        VALUES
            (@ResourceName, @CustomerId, @CampaignResourceName, @Name, @Status, @Type, @CpcBidMicros, GETUTCDATE());
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_SaveApiLog
    @CustomerId     NVARCHAR(50),
    @ApiName        NVARCHAR(256),
    @RequestBody    NVARCHAR(MAX),
    @ResponseBody   NVARCHAR(MAX),
    @IsSuccess      BIT,
    @StatusCode     INT,
    @CreatedAt      DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO GoogleAds_ApiLogs (CustomerId, ApiName, RequestBody, ResponseBody, IsSuccess, StatusCode, CreatedAt)
    VALUES (@CustomerId, @ApiName, @RequestBody, @ResponseBody, @IsSuccess, @StatusCode, GETUTCDATE());
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_SaveCampaign
    @ResourceName           NVARCHAR(512),
    @CustomerId             NVARCHAR(50),
    @Name                   NVARCHAR(256),
    @Status                 NVARCHAR(50),
    @AdvertisingChannelType NVARCHAR(50),
    @BiddingStrategyType    NVARCHAR(100),
    @BudgetAmountMicros     BIGINT,
    @StartDate              NVARCHAR(20),
    @EndDate                NVARCHAR(20),
    @CreatedAt              DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM GoogleAds_Campaigns WHERE ResourceName = @ResourceName)
    BEGIN
        UPDATE GoogleAds_Campaigns
        SET CustomerId             = @CustomerId,
            Name                   = @Name,
            Status                 = @Status,
            AdvertisingChannelType = @AdvertisingChannelType,
            BiddingStrategyType    = @BiddingStrategyType,
            BudgetAmountMicros     = @BudgetAmountMicros,
            StartDate              = @StartDate,
            EndDate                = @EndDate
        WHERE ResourceName = @ResourceName;
    END
    ELSE
    BEGIN
        INSERT INTO GoogleAds_Campaigns
            (ResourceName, CustomerId, Name, Status, AdvertisingChannelType, BiddingStrategyType, BudgetAmountMicros, StartDate, EndDate, CreatedAt)
        VALUES
            (@ResourceName, @CustomerId, @Name, @Status, @AdvertisingChannelType, @BiddingStrategyType, @BudgetAmountMicros, @StartDate, @EndDate, GETUTCDATE());
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_SaveKeyword
    @ResourceName           NVARCHAR(512),
    @CustomerId             NVARCHAR(50),
    @AdGroupResourceName    NVARCHAR(512),
    @Text                   NVARCHAR(256),
    @MatchType              NVARCHAR(50),
    @Status                 NVARCHAR(50),
    @CpcBidMicros           BIGINT,
    @CreatedAt              DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM GoogleAds_Keywords WHERE ResourceName = @ResourceName)
    BEGIN
        UPDATE GoogleAds_Keywords
        SET CustomerId          = @CustomerId,
            AdGroupResourceName = @AdGroupResourceName,
            Text                = @Text,
            MatchType           = @MatchType,
            Status              = @Status,
            CpcBidMicros        = @CpcBidMicros
        WHERE ResourceName = @ResourceName;
    END
    ELSE
    BEGIN
        INSERT INTO GoogleAds_Keywords
            (ResourceName, CustomerId, AdGroupResourceName, Text, MatchType, Status, CpcBidMicros, CreatedAt)
        VALUES
            (@ResourceName, @CustomerId, @AdGroupResourceName, @Text, @MatchType, @Status, @CpcBidMicros, GETUTCDATE());
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_SaveToken
    @CustomerId     NVARCHAR(50),
    @AccessToken    NVARCHAR(MAX),
    @RefreshToken   NVARCHAR(MAX),
    @ExpiresAt      DATETIME2,
    @Scope          NVARCHAR(1024),
    @CreatedAt      DATETIME2,
    @UpdatedAt      DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM GoogleAds_Tokens WHERE CustomerId = @CustomerId)
    BEGIN
        UPDATE GoogleAds_Tokens
        SET AccessToken  = @AccessToken,
            RefreshToken = @RefreshToken,
            ExpiresAt    = @ExpiresAt,
            Scope        = @Scope,
            UpdatedAt    = GETUTCDATE()
        WHERE CustomerId = @CustomerId;
    END
    ELSE
    BEGIN
        INSERT INTO GoogleAds_Tokens (CustomerId, AccessToken, RefreshToken, ExpiresAt, Scope, CreatedAt, UpdatedAt)
        VALUES (@CustomerId, @AccessToken, @RefreshToken, @ExpiresAt, @Scope, GETUTCDATE(), GETUTCDATE());
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_UpdateAdGroup
    @ResourceName   NVARCHAR(512),
    @CustomerId     NVARCHAR(50),
    @Name           NVARCHAR(256),
    @Status         NVARCHAR(50),
    @CpcBidMicros   BIGINT,
    @UpdatedAt      DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE GoogleAds_AdGroups
    SET Name         = ISNULL(NULLIF(@Name, ''), Name),
        Status       = ISNULL(NULLIF(@Status, ''), Status),
        CpcBidMicros = CASE WHEN @CpcBidMicros > 0 THEN @CpcBidMicros ELSE CpcBidMicros END
    WHERE ResourceName = @ResourceName AND CustomerId = @CustomerId;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_UpdateCampaign
    @ResourceName       NVARCHAR(512),
    @CustomerId         NVARCHAR(50),
    @Name               NVARCHAR(256),
    @Status             NVARCHAR(50),
    @BudgetAmountMicros BIGINT,
    @UpdatedAt          DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE GoogleAds_Campaigns
    SET Name               = ISNULL(NULLIF(@Name, ''), Name),
        Status             = ISNULL(NULLIF(@Status, ''), Status),
        BudgetAmountMicros = CASE WHEN @BudgetAmountMicros > 0 THEN @BudgetAmountMicros ELSE BudgetAmountMicros END
    WHERE ResourceName = @ResourceName AND CustomerId = @CustomerId;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_UpdateKeyword
    @ResourceName   NVARCHAR(512),
    @CustomerId     NVARCHAR(50),
    @Status         NVARCHAR(50),
    @CpcBidMicros   BIGINT,
    @UpdatedAt      DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE GoogleAds_Keywords
    SET Status       = ISNULL(NULLIF(@Status, ''), Status),
        CpcBidMicros = CASE WHEN @CpcBidMicros > 0 THEN @CpcBidMicros ELSE CpcBidMicros END
    WHERE ResourceName = @ResourceName AND CustomerId = @CustomerId;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GoogleAds_UpdateToken
    @CustomerId     NVARCHAR(50),
    @AccessToken    NVARCHAR(MAX),
    @RefreshToken   NVARCHAR(MAX),
    @ExpiresAt      DATETIME2,
    @Scope          NVARCHAR(1024),
    @UpdatedAt      DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE GoogleAds_Tokens
    SET AccessToken  = @AccessToken,
        RefreshToken = @RefreshToken,
        ExpiresAt    = @ExpiresAt,
        Scope        = @Scope,
        UpdatedAt    = GETUTCDATE()
    WHERE CustomerId = @CustomerId;
END
GO
