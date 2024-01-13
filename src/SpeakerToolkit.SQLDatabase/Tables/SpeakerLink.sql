CREATE TABLE dbo.SpeakerLink
(
  SpeakerLinkId INT           NOT NULL IDENTITY(1,1),
  SpeakerId     INT           NOT NULL,
  LinkType      NVARCHAR(100) NOT NULL,
  LinkUrl       NVARCHAR(200) NOT NULL,
  CONSTRAINT pkcSpeakerLink PRIMARY KEY CLUSTERED (SpeakerLinkId),
  CONSTRAINT fkcSpeakerLink_SpeakerId FOREIGN KEY (SpeakerId) REFERENCES dbo.Speaker (SpeakerId),
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerLink',                                 @value=N'Represents a link to a website/social media profile.',                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerLink', @level2name=N'SpeakerLinkId',   @value=N'The identifier of the link record.',                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerLink', @level2name=N'SpeakerId',       @value=N'The identifier of the associated speaker.',                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerLink', @level2name=N'LinkType',        @value=N'The type of link being represented.',                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerLink', @level2name=N'LinkUrl',         @value=N'The URL of the link.',                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerLink', @level2name=N'pkcSpeakerLink',  @value=N'Defines the primary key for the Link table using the LinkId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO