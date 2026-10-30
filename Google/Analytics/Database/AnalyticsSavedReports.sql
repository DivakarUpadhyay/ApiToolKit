-- ============================================================================
-- ApiToolKit - Google Analytics (GA4)
-- Adds the saved-report table and procedure that AnalyticsReportRepository uses.
-- Missing from ApiToolKit_Analytics_20260711.bak; safe to run more than once.
-- ============================================================================
USE [ApiToolKit_Analytics];
GO

IF OBJECT_ID(N'dbo.AnalyticsSavedReports', N'U') IS NULL
CREATE TABLE dbo.AnalyticsSavedReports
(
    Id           BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AnalyticsSavedReports PRIMARY KEY,
    PropertyId   NVARCHAR(100)        NOT NULL,
    ReportName   NVARCHAR(200)        NOT NULL,
    Query        NVARCHAR(MAX)        NULL,
    ResponseJson NVARCHAR(MAX)        NULL,
    [RowCount]   INT                  NOT NULL CONSTRAINT DF_AnalyticsSavedReports_RowCount DEFAULT 0,
    CreatedAt    DATETIME2            NOT NULL CONSTRAINT DF_AnalyticsSavedReports_CreatedAt DEFAULT SYSUTCDATETIME()
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_AnalyticsSavedReports_PropertyId')
    CREATE INDEX IX_AnalyticsSavedReports_PropertyId ON dbo.AnalyticsSavedReports (PropertyId, CreatedAt DESC);
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
