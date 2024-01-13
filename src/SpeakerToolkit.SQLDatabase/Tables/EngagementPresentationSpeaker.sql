CREATE TABLE dbo.EngagementPresentationSpeaker
(
  EngagementPresentationSpeakerId INT         NOT NULL IDENTITY(1,1),
  EngagementPresentationId        INT         NOT NULL,
  SpeakerId                       INT         NOT NULL,
  IsPrimarySpeaker                BIT         NOT NULL CONSTRAINT dfEngagementPresentationSpeaker_IsPrimarySpeaker DEFAULT 0,
  CONSTRAINT pkcEngagementPresentationSpeaker PRIMARY KEY CLUSTERED (EngagementPresentationSpeakerId),
  CONSTRAINT fkEngagementPresentationSpeaker_EngagementPresentation FOREIGN KEY (EngagementPresentationId) REFERENCES dbo.EngagementPresentation (EngagementPresentationId),
  CONSTRAINT fkEngagementPresentationSpeaker_Speaker                FOREIGN KEY (SpeakerId)                REFERENCES dbo.Speaker (SpeakerId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker',                                                                        @value=N'Represents a speaker presenting an engagement presentation.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'EngagementPresentationSpeakerId',                        @value=N'Identifier of the EngagementPresentationSpeaker record.',                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'EngagementPresentationId',                               @value=N'Identifier of the engagement presentation.',                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'SpeakerId',                                              @value=N'Identifier of the assigned speaker.',                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'IsPrimarySpeaker',                                       @value=N'Flag indicating whether the speaker is the primary speaker for the engagement presentation.',             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'pkcEngagementPresentationSpeaker',                       @value=N'Defines the primary key for the EngagementPresentation table using the EngagementPresentationId column.',                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'fkEngagementPresentationSpeaker_EngagementPresentation', @value=N'Defines the relationship between the EngagementPresentationSpeaker and EngagementPresentation tables using the EngagementPresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'fkEngagementPresentationSpeaker_Speaker',                @value=N'Defines the relationship between the EngagementPresentationSpeaker and Speaker tables using the SpeakerId column.',                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationSpeaker', @level2name=N'dfEngagementPresentationSpeaker_IsPrimarySpeaker',       @value=N'Defines the default value for the IsPrimarySpeaker column as 0 (false).',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO