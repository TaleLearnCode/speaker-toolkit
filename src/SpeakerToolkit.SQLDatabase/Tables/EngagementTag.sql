CREATE TABLE dbo.EngagementTag
(
  EngagementTagId INT NOT NULL IDENTITY(1,1),
  EngagementId    INT NOT NULL,
  TagId           INT NOT NULL,
  CONSTRAINT pkcEngagementTag PRIMARY KEY CLUSTERED (EngagementTagId),
  CONSTRAINT fkEngagementTag_Engagement FOREIGN KEY (EngagementId) REFERENCES dbo.Engagement (EngagementId),
  CONSTRAINT fkEngagementTag_Tag FOREIGN KEY (TagId) REFERENCES dbo.Tag (TagId),
  CONSTRAINT unqEngagementTag_EngagementId_TagId UNIQUE (EngagementId, TagId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag',                                            @value=N'Represents the association between a engagement and a tag.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag', @level2name=N'EngagementTagId',            @value=N'The identifier of the engagement/tag record.',                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag', @level2name=N'TagId',                      @value=N'Identifier of the associated tag.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag', @level2name=N'EngagementId',               @value=N'Identifier of the associated engagement.',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag', @level2name=N'pkcEngagementTag',           @value=N'Defines the primary key for the EngagementTag table using the EngagementTagId column.',                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag', @level2name=N'fkEngagementTag_Tag',        @value=N'Defines the relationship between the EngagementTag and Tag tables using the TagId column.',               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementTag', @level2name=N'fkEngagementTag_Engagement', @value=N'Defines the relationship between the EngagementTag and Engagement tables using the EngagementId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO