CREATE TABLE dbo.SpeakerBiography
(
  SpeakerBiographyId INT           NOT NULL IDENTITY(1,1),
  SpeakerId          INT           NOT NULL,
  LanguageCode       CHAR(2)       NOT NULL,
  Title              NVARCHAR(160) NOT NULL,
  Biography          NVARCHAR(4000) NOT NULL,
  CONSTRAINT pkcSpeakerBiography PRIMARY KEY CLUSTERED (SpeakerBiographyId),
  CONSTRAINT fkSpeakerBiography_Speaker FOREIGN KEY (SpeakerId) REFERENCES dbo.Speaker(SpeakerId),
  CONSTRAINT fkSpeakerBiography_Language FOREIGN KEY (LanguageCode) REFERENCES dbo.Language(LanguageCode),
  CONSTRAINT ucSpeakerBiography_Speaker_Language UNIQUE (SpeakerId, LanguageCode)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography',                                                     @value=N'Contains the title and bio of a speaker in a specified langauge.',                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'SpeakerBiographyId',                  @value=N'The identifier of the speaker bio record.',                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'SpeakerId',                           @value=N'The identifier of the speaker.',                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'LanguageCode',                        @value=N'Code of the associated language.',                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'Title',                               @value=N'The title for the speaker.',                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'Biography',                           @value=N'The biography for the speaker.',                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'pkcSpeakerBiography',                 @value=N'Defines the primary key for the SpeakerBiography table using the SpeakerBiographyId column.',           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'fkSpeakerBiography_Speaker',          @value = N'Defines the relationship between SpeakerBiograhy and Speaker tables using the SpeakerId column.',     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'fkSpeakerBiography_Language',         @value = N'Defines the relationship between SpeakerBiograhy and Language tables using the LanguageCode column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'SpeakerBiography', @level2name=N'ucSpeakerBiography_Speaker_Language', @value = N'Defines a unique constraint based upon the SpeakerId and LanguageCode columns.',                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO