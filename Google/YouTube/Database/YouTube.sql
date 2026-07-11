-- ============================================================
-- ApiToolKit - YouTube API Database Schema
-- Database: ApiToolKit
-- ============================================================

USE ApiToolKit;
GO

-- ============================================================
-- TABLES
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YouTube_Config')
CREATE TABLE YouTube_Config (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    ClientId    NVARCHAR(256)   NOT NULL,
    ClientSecret NVARCHAR(256)  NOT NULL,
    RedirectUri NVARCHAR(512)   NOT NULL,
    ApiKey      NVARCHAR(256)   NOT NULL DEFAULT '',
    IsActive    BIT             NOT NULL DEFAULT 1,
    CreatedAt   DATETIME2       NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt   DATETIME2       NOT NULL DEFAULT GETUTCDATE()
);
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YouTube_Tokens')
CREATE TABLE YouTube_Tokens (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    UserId       NVARCHAR(128)  NOT NULL,
    AccessToken  NVARCHAR(2048) NOT NULL,
    RefreshToken NVARCHAR(2048) NOT NULL DEFAULT '',
    ExpiresAt    DATETIME2      NOT NULL,
    Scope        NVARCHAR(1024) NOT NULL DEFAULT '',
    CreatedAt    DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt    DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_YouTube_Tokens_UserId UNIQUE (UserId)
);
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YouTube_ApiLogs')
CREATE TABLE YouTube_ApiLogs (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    UserId       NVARCHAR(128)  NOT NULL,
    ApiName      NVARCHAR(256)  NOT NULL,
    RequestBody  NVARCHAR(MAX)  NULL,
    ResponseBody NVARCHAR(MAX)  NULL,
    IsSuccess    BIT            NOT NULL DEFAULT 0,
    StatusCode   INT            NOT NULL DEFAULT 0,
    CreatedAt    DATETIME2      NOT NULL DEFAULT GETUTCDATE()
);
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YouTube_Channels')
CREATE TABLE YouTube_Channels (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    ChannelId       NVARCHAR(64)   NOT NULL,
    UserId          NVARCHAR(128)  NOT NULL,
    Title           NVARCHAR(256)  NOT NULL DEFAULT '',
    Description     NVARCHAR(MAX)  NOT NULL DEFAULT '',
    CustomUrl       NVARCHAR(256)  NOT NULL DEFAULT '',
    PublishedAt     DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    Country         NVARCHAR(8)    NOT NULL DEFAULT '',
    ViewCount       BIGINT         NOT NULL DEFAULT 0,
    SubscriberCount BIGINT         NOT NULL DEFAULT 0,
    VideoCount      BIGINT         NOT NULL DEFAULT 0,
    ThumbnailUrl    NVARCHAR(512)  NOT NULL DEFAULT '',
    CreatedAt       DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_YouTube_Channels_ChannelId UNIQUE (ChannelId)
);
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YouTube_Videos')
CREATE TABLE YouTube_Videos (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    VideoId      NVARCHAR(64)   NOT NULL,
    ChannelId    NVARCHAR(64)   NOT NULL DEFAULT '',
    UserId       NVARCHAR(128)  NOT NULL DEFAULT '',
    Title        NVARCHAR(512)  NOT NULL DEFAULT '',
    Description  NVARCHAR(MAX)  NOT NULL DEFAULT '',
    Status       NVARCHAR(32)   NOT NULL DEFAULT '',
    PublishedAt  DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    ViewCount    BIGINT         NOT NULL DEFAULT 0,
    LikeCount    BIGINT         NOT NULL DEFAULT 0,
    CommentCount BIGINT         NOT NULL DEFAULT 0,
    Duration     NVARCHAR(32)   NOT NULL DEFAULT '',
    ThumbnailUrl NVARCHAR(512)  NOT NULL DEFAULT '',
    CreatedAt    DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_YouTube_Videos_VideoId UNIQUE (VideoId)
);
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YouTube_Playlists')
CREATE TABLE YouTube_Playlists (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    PlaylistId  NVARCHAR(64)   NOT NULL,
    ChannelId   NVARCHAR(64)   NOT NULL DEFAULT '',
    UserId      NVARCHAR(128)  NOT NULL DEFAULT '',
    Title       NVARCHAR(512)  NOT NULL DEFAULT '',
    Description NVARCHAR(MAX)  NOT NULL DEFAULT '',
    Status      NVARCHAR(32)   NOT NULL DEFAULT '',
    ItemCount   INT            NOT NULL DEFAULT 0,
    PublishedAt DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    ThumbnailUrl NVARCHAR(512) NOT NULL DEFAULT '',
    CreatedAt   DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_YouTube_Playlists_PlaylistId UNIQUE (PlaylistId)
);
GO

-- ============================================================
-- STORED PROCEDURES - Auth
-- ============================================================

CREATE OR ALTER PROCEDURE sp_YouTube_GetConfig
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM YouTube_Config WHERE IsActive = 1 ORDER BY Id;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_GetTokenByUserId
    @UserId NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM YouTube_Tokens WHERE UserId = @UserId;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_SaveToken
    @UserId       NVARCHAR(128),
    @AccessToken  NVARCHAR(2048),
    @RefreshToken NVARCHAR(2048),
    @ExpiresAt    DATETIME2,
    @Scope        NVARCHAR(1024)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO YouTube_Tokens (UserId, AccessToken, RefreshToken, ExpiresAt, Scope, CreatedAt, UpdatedAt)
    VALUES (@UserId, @AccessToken, @RefreshToken, @ExpiresAt, @Scope, GETUTCDATE(), GETUTCDATE());
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_UpdateToken
    @UserId       NVARCHAR(128),
    @AccessToken  NVARCHAR(2048),
    @RefreshToken NVARCHAR(2048),
    @ExpiresAt    DATETIME2,
    @Scope        NVARCHAR(1024)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE YouTube_Tokens
    SET AccessToken  = @AccessToken,
        RefreshToken = @RefreshToken,
        ExpiresAt    = @ExpiresAt,
        Scope        = @Scope,
        UpdatedAt    = GETUTCDATE()
    WHERE UserId = @UserId;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_SaveApiLog
    @UserId       NVARCHAR(128),
    @ApiName      NVARCHAR(256),
    @RequestBody  NVARCHAR(MAX),
    @ResponseBody NVARCHAR(MAX),
    @IsSuccess    BIT,
    @StatusCode   INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO YouTube_ApiLogs (UserId, ApiName, RequestBody, ResponseBody, IsSuccess, StatusCode, CreatedAt)
    VALUES (@UserId, @ApiName, @RequestBody, @ResponseBody, @IsSuccess, @StatusCode, GETUTCDATE());
END;
GO

-- ============================================================
-- STORED PROCEDURES - Channel
-- ============================================================

CREATE OR ALTER PROCEDURE sp_YouTube_SaveChannel
    @ChannelId       NVARCHAR(64),
    @UserId          NVARCHAR(128),
    @Title           NVARCHAR(256),
    @Description     NVARCHAR(MAX),
    @CustomUrl       NVARCHAR(256),
    @PublishedAt     DATETIME2,
    @Country         NVARCHAR(8),
    @ViewCount       BIGINT,
    @SubscriberCount BIGINT,
    @VideoCount      BIGINT,
    @ThumbnailUrl    NVARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM YouTube_Channels WHERE ChannelId = @ChannelId)
    BEGIN
        UPDATE YouTube_Channels
        SET Title           = @Title,
            Description     = @Description,
            CustomUrl       = @CustomUrl,
            Country         = @Country,
            ViewCount       = @ViewCount,
            SubscriberCount = @SubscriberCount,
            VideoCount      = @VideoCount,
            ThumbnailUrl    = @ThumbnailUrl
        WHERE ChannelId = @ChannelId;
    END
    ELSE
    BEGIN
        INSERT INTO YouTube_Channels
            (ChannelId, UserId, Title, Description, CustomUrl, PublishedAt, Country, ViewCount, SubscriberCount, VideoCount, ThumbnailUrl, CreatedAt)
        VALUES
            (@ChannelId, @UserId, @Title, @Description, @CustomUrl, @PublishedAt, @Country, @ViewCount, @SubscriberCount, @VideoCount, @ThumbnailUrl, GETUTCDATE());
    END
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_UpdateChannel
    @ChannelId       NVARCHAR(64),
    @Title           NVARCHAR(256),
    @Description     NVARCHAR(MAX),
    @CustomUrl       NVARCHAR(256),
    @Country         NVARCHAR(8),
    @ViewCount       BIGINT,
    @SubscriberCount BIGINT,
    @VideoCount      BIGINT,
    @ThumbnailUrl    NVARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE YouTube_Channels
    SET Title           = @Title,
        Description     = @Description,
        CustomUrl       = @CustomUrl,
        Country         = @Country,
        ViewCount       = @ViewCount,
        SubscriberCount = @SubscriberCount,
        VideoCount      = @VideoCount,
        ThumbnailUrl    = @ThumbnailUrl
    WHERE ChannelId = @ChannelId;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_GetChannelByUserId
    @UserId NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM YouTube_Channels WHERE UserId = @UserId ORDER BY CreatedAt DESC;
END;
GO

-- ============================================================
-- STORED PROCEDURES - Video
-- ============================================================

CREATE OR ALTER PROCEDURE sp_YouTube_SaveVideo
    @VideoId     NVARCHAR(64),
    @ChannelId   NVARCHAR(64),
    @UserId      NVARCHAR(128),
    @Title       NVARCHAR(512),
    @Description NVARCHAR(MAX),
    @Status      NVARCHAR(32),
    @PublishedAt DATETIME2,
    @ViewCount   BIGINT,
    @LikeCount   BIGINT,
    @CommentCount BIGINT,
    @Duration    NVARCHAR(32),
    @ThumbnailUrl NVARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM YouTube_Videos WHERE VideoId = @VideoId)
    BEGIN
        UPDATE YouTube_Videos
        SET Title        = @Title,
            Description  = @Description,
            Status       = @Status,
            ViewCount    = @ViewCount,
            LikeCount    = @LikeCount,
            CommentCount = @CommentCount,
            Duration     = @Duration,
            ThumbnailUrl = @ThumbnailUrl
        WHERE VideoId = @VideoId;
    END
    ELSE
    BEGIN
        INSERT INTO YouTube_Videos
            (VideoId, ChannelId, UserId, Title, Description, Status, PublishedAt, ViewCount, LikeCount, CommentCount, Duration, ThumbnailUrl, CreatedAt)
        VALUES
            (@VideoId, @ChannelId, @UserId, @Title, @Description, @Status, @PublishedAt, @ViewCount, @LikeCount, @CommentCount, @Duration, @ThumbnailUrl, GETUTCDATE());
    END
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_UpdateVideo
    @VideoId     NVARCHAR(64),
    @Title       NVARCHAR(512),
    @Description NVARCHAR(MAX),
    @Status      NVARCHAR(32),
    @ViewCount   BIGINT,
    @LikeCount   BIGINT,
    @CommentCount BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE YouTube_Videos
    SET Title        = @Title,
        Description  = @Description,
        Status       = @Status,
        ViewCount    = @ViewCount,
        LikeCount    = @LikeCount,
        CommentCount = @CommentCount
    WHERE VideoId = @VideoId;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_DeleteVideo
    @VideoId NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM YouTube_Videos WHERE VideoId = @VideoId;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_GetVideosByChannelId
    @ChannelId NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM YouTube_Videos WHERE ChannelId = @ChannelId ORDER BY PublishedAt DESC;
END;
GO

-- ============================================================
-- STORED PROCEDURES - Playlist
-- ============================================================

CREATE OR ALTER PROCEDURE sp_YouTube_SavePlaylist
    @PlaylistId  NVARCHAR(64),
    @ChannelId   NVARCHAR(64),
    @UserId      NVARCHAR(128),
    @Title       NVARCHAR(512),
    @Description NVARCHAR(MAX),
    @Status      NVARCHAR(32),
    @ItemCount   INT,
    @PublishedAt DATETIME2,
    @ThumbnailUrl NVARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM YouTube_Playlists WHERE PlaylistId = @PlaylistId)
    BEGIN
        UPDATE YouTube_Playlists
        SET Title        = @Title,
            Description  = @Description,
            Status       = @Status,
            ItemCount    = @ItemCount,
            ThumbnailUrl = @ThumbnailUrl
        WHERE PlaylistId = @PlaylistId;
    END
    ELSE
    BEGIN
        INSERT INTO YouTube_Playlists
            (PlaylistId, ChannelId, UserId, Title, Description, Status, ItemCount, PublishedAt, ThumbnailUrl, CreatedAt)
        VALUES
            (@PlaylistId, @ChannelId, @UserId, @Title, @Description, @Status, @ItemCount, @PublishedAt, @ThumbnailUrl, GETUTCDATE());
    END
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_UpdatePlaylist
    @PlaylistId  NVARCHAR(64),
    @Title       NVARCHAR(512),
    @Description NVARCHAR(MAX),
    @Status      NVARCHAR(32),
    @ItemCount   INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE YouTube_Playlists
    SET Title       = @Title,
        Description = @Description,
        Status      = @Status,
        ItemCount   = @ItemCount
    WHERE PlaylistId = @PlaylistId;
END;
GO

CREATE OR ALTER PROCEDURE sp_YouTube_DeletePlaylist
    @PlaylistId NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM YouTube_Playlists WHERE PlaylistId = @PlaylistId;
END;
GO
