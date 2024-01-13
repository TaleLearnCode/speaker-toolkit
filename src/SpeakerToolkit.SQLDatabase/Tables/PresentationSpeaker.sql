CREATE TABLE dbo.PresentationSpeaker
(
  PresentationSpeakerId INT NOT NULL IDENTITY(1,1),
  PresentationId        INT NOT NULL,
  SpeakerId             INT NOT NULL,
  IsPrimary             BIT NOT NULL CONSTRAINT dfPresentationSpeaker_IsPrimary DEFAULT 0,
  CONSTRAINT pkcPresentationSpeaker             PRIMARY KEY CLUSTERED (PresentationSpeakerId),
  CONSTRAINT fkPresentationSpeaker_Presentation FOREIGN KEY (PresentationId) REFERENCES dbo.Presentation (PresentationId),
  CONSTRAINT fkPresentationSpeaker_Speaker      FOREIGN KEY (SpeakerId) REFERENCES dbo.Speaker (SpeakerId),
  CONSTRAINT unqPresentationSpeaker_PresentationId_SpeakerId UNIQUE (PresentationId, SpeakerId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @value=N'Links a speaker to a presentation.',                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'PresentationSpeakerId',                           @value=N'The identifier of the presentation speaker record.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'PresentationId',                                  @value=N'Identifier of the associated presentation.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'SpeakerId',                                       @value=N'Identifier of the associated speaker.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'IsPrimary',                                       @value=N'Flag indicaitng whether the speaker is the primary speaker for the presentation.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'pkcPresentationSpeaker',                          @value=N'Defines the primary key for the PresentationSpeaker table using the PresentationSpeakerId column.',                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'fkPresentationSpeaker_Presentation',              @value=N'Defines the relationship between the PresentationSpeaker and Presentation tables using the PresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationSpeaker',                 @level2name=N'fkPresentationSpeaker_Speaker',                   @value=N'Defines the relationship between the PresentationSpeaker and Speaker tables using the SpeakerId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentatiPresentationSpeakeronText', @level2name=N'unqPresentationSpeaker_PresentationId_SpeakerId', @value=N'Defines a unique constraint for the table based upon the PresentationId and SpeakerId columns.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO