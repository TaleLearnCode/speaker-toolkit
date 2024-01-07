CREATE TABLE dbo.EngagementPresentationDownload
(
  EngagementPresentationDownloadId INT           NOT NULL IDENTITY(1,1),
  EngagementPresentationId         INT           NOT NULL,
  DownloadName                     NVARCHAR(50)  NOT NULL,
  DownloadUrl                      NVARCHAR(500)     NULL,
  CONSTRAINT pkcEngagementPresentationDownload PRIMARY KEY CLUSTERED (EngagementPresentationDownloadId),
  CONSTRAINT fkEngagementPresentationDownload_EngagementPresentation FOREIGN KEY (EngagementPresentationId) REFERENCES dbo.EngagementPresentation (EngagementPresentationId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload',                                                                         @value=N'Represents a download associated with a engagement presentation.',                                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'EngagementPresentationDownloadId',                        @value=N'Identifier of the EngagementPresentationDownload record.',                                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'EngagementPresentationId',                                @value=N'Identifier of the associated engagement presentation.',                                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'DownloadUrl',                                             @value=N'The link to the download.',                                                                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'pkcEngagementPresentationDownload',                       @value=N'Defines the primary key for the EngagementPresentationDownload table using the EngagementPresentationDownloadId column.',                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'fkEngagementPresentationDownload_EngagementPresentation', @value=N'Defines the relationship between the EngagementPresentationDownload and EngagementPresentation tables using the EngagementPresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO