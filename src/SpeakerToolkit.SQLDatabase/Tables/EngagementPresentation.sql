CREATE TABLE dbo.EngagementPresentation
(
  EngagementPresentationId INT         NOT NULL IDENTITY(1,1),
  EngagementId             INT         NOT NULL,
  PresentationId           INT         NOT NULL,
  StartDateTime            DATETIME2       NULL,
  EndDateTime              DATETIME2       NULL,
  TimeZone                 VARCHAR(10)     NULL,
  Room                     NVARCHAR(50)    NULL,
  CONSTRAINT pkcEngagementPresentation PRIMARY KEY CLUSTERED (EngagementPresentationId),
  CONSTRAINT fkEngagementPresentation_Engagement FOREIGN KEY (EngagementId) REFERENCES dbo.Engagement (EngagementId),
  CONSTRAINT fkEngagementPresentation_Presentation FOREIGN KEY (PresentationId) REFERENCES dbo.Presentation (PresentationId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation',                                                       @value=N'Represents the speaker''s presentations.',                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EngagementPresentationId',              @value=N'Identifier of the EngagementPresentation record.',                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EngagementId',                          @value=N'Identifier of the associated engagement.',                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'PresentationId',                        @value=N'Identifier of the associated presentation.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'StartDateTime',                         @value=N'The starting date and time for the presentation.',                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EndDateTime',                           @value=N'The ending date and time for the presentation.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'Room',                                  @value=N'The room where the presentation is being presented.',                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'pkcEngagementPresentation',             @value=N'Defines the primary key for the EngagementPresentation table using the EngagementPresentationId column.',              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'fkEngagementPresentation_Engagement',   @value=N'Defines the relationship between the EngagementPresentation and Engagement tables using the EngagementId column.',     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'fkEngagementPresentation_Presentation', @value=N'Defines the relationship between the EngagementPresentation and Presentation tables using the PresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO