-- ============================================================================
-- ApiToolKit - Google Business Profile APIs
-- Database: ApiToolKit_BusinessProfile
-- Tables: BusinessProfile_Tokens (OAuth tokens per account key), BusinessProfile_ApiLogs (every outbound Google call)
-- ============================================================================
IF DB_ID(N'ApiToolKit_BusinessProfile') IS NULL
    CREATE DATABASE [ApiToolKit_BusinessProfile];
GO

USE [ApiToolKit_BusinessProfile];
GO

IF OBJECT_ID(N'dbo.BusinessProfile_Tokens', N'U') IS NULL
CREATE TABLE dbo.BusinessProfile_Tokens
(
    Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BusinessProfile_Tokens PRIMARY KEY,
    AccountKey   NVARCHAR(200)     NOT NULL CONSTRAINT UQ_BusinessProfile_Tokens_AccountKey UNIQUE,
    AccessToken  NVARCHAR(MAX)     NOT NULL,
    RefreshToken NVARCHAR(MAX)     NULL,
    ExpiresAt    DATETIME2         NOT NULL,
    Scope        NVARCHAR(MAX)     NULL,
    CreatedAt    DATETIME2         NOT NULL CONSTRAINT DF_BusinessProfile_Tokens_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt    DATETIME2         NOT NULL CONSTRAINT DF_BusinessProfile_Tokens_UpdatedAt DEFAULT SYSUTCDATETIME()
);
GO

IF OBJECT_ID(N'dbo.BusinessProfile_ApiLogs', N'U') IS NULL
CREATE TABLE dbo.BusinessProfile_ApiLogs
(
    Id           BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BusinessProfile_ApiLogs PRIMARY KEY,
    AccountKey   NVARCHAR(200)        NULL,
    ApiName      NVARCHAR(200)        NOT NULL,
    HttpMethod   NVARCHAR(10)         NOT NULL,
    Url          NVARCHAR(2000)       NOT NULL,
    RequestBody  NVARCHAR(MAX)        NULL,
    ResponseBody NVARCHAR(MAX)        NULL,
    IsSuccess    BIT                  NOT NULL,
    StatusCode   INT                  NOT NULL,
    CreatedAt    DATETIME2            NOT NULL CONSTRAINT DF_BusinessProfile_ApiLogs_CreatedAt DEFAULT SYSUTCDATETIME()
);
GO

CREATE OR ALTER PROCEDURE dbo.sp_BusinessProfile_GetToken
    @AccountKey NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, AccountKey, AccessToken, RefreshToken, ExpiresAt, Scope, CreatedAt, UpdatedAt
    FROM dbo.BusinessProfile_Tokens
    WHERE AccountKey = @AccountKey;
END
GO

-- Upsert; Google only returns a refresh token on first consent, so keep the stored one when none is sent.
CREATE OR ALTER PROCEDURE dbo.sp_BusinessProfile_SaveToken
    @AccountKey   NVARCHAR(200),
    @AccessToken  NVARCHAR(MAX),
    @RefreshToken NVARCHAR(MAX) = NULL,
    @ExpiresAt    DATETIME2,
    @Scope        NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.BusinessProfile_Tokens WHERE AccountKey = @AccountKey)
        UPDATE dbo.BusinessProfile_Tokens
        SET AccessToken  = @AccessToken,
            RefreshToken = COALESCE(@RefreshToken, RefreshToken),
            ExpiresAt    = @ExpiresAt,
            Scope        = COALESCE(@Scope, Scope),
            UpdatedAt    = SYSUTCDATETIME()
        WHERE AccountKey = @AccountKey;
    ELSE
        INSERT INTO dbo.BusinessProfile_Tokens (AccountKey, AccessToken, RefreshToken, ExpiresAt, Scope)
        VALUES (@AccountKey, @AccessToken, @RefreshToken, @ExpiresAt, @Scope);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_BusinessProfile_SaveApiLog
    @AccountKey   NVARCHAR(200) = NULL,
    @ApiName      NVARCHAR(200),
    @HttpMethod   NVARCHAR(10),
    @Url          NVARCHAR(2000),
    @RequestBody  NVARCHAR(MAX) = NULL,
    @ResponseBody NVARCHAR(MAX) = NULL,
    @IsSuccess    BIT,
    @StatusCode   INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.BusinessProfile_ApiLogs (AccountKey, ApiName, HttpMethod, Url, RequestBody, ResponseBody, IsSuccess, StatusCode)
    VALUES (@AccountKey, @ApiName, @HttpMethod, LEFT(@Url, 2000), @RequestBody, @ResponseBody, @IsSuccess, @StatusCode);
END
GO
