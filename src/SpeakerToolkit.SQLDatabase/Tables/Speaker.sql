CREATE TABLE dbo.Speaker
(
  SpeakerId           INT          NOT NULL IDENTITY(1,1),
  FirstName           NVARCHAR(50) NOT NULL,
  LastName            NVARCHAR(50) NOT NULL,
  EnablePublicProfile BIT          NOT NULL CONSTRAINT dfSpeaker_EnablePublicProfile DEFAULT(0),
  PublicProfileUrl    VARCHAR(100) NOT NULL,
  CountryCode         CHAR(2)      NOT NULL,
  CountryDivisionCode CHAR(3)          NULL,
  DefaultLanguageCode CHAR(2)      NOT NULL CONSTRAINT dfSpeaker_DefaultLangaugeCode DEFAULT('en'),
  CONSTRAINT pkcSpeaker PRIMARY KEY CLUSTERED (SpeakerId),
  CONSTRAINT fkSpeaker_Country FOREIGN KEY (CountryCode) REFERENCES dbo.Country(CountryCode),
  CONSTRAINT fkSpeaker_CountryDivision FOREIGN KEY (CountryCode, CountryDivisionCode) REFERENCES dbo.CountryDivision(CountryCode, CountryDivisionCode),
  CONSTRAINT fkSpeaker_DefaultLanguage FOREIGN KEY (DefaultLanguageCode) REFERENCES dbo.Language(LanguageCode)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker',                                               @value=N'Details about a speaker.',                                                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'SpeakerId',                     @value=N'The identifier of the speaker.',                                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'FirstName',                     @value=N'The first name of the speaker.',                                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'LastName',                      @value=N'The last name of the speaker.',                                                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'EnablePublicProfile',           @value=N'Flag indicating whether the speaker profile is displayed publicly.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'CountryCode',                   @value=N'Idenfiier of the country where the speaker is located.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'CountryDivisionCode',           @value=N'Identifier of the country division where the speaker is located.',                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'DefaultLanguageCode',           @value=N'The default langauge to use with the speaker.',                                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Speaker', @level2name=N'pkcSpeaker',                    @value=N'Defines the primary key for the Speaker table using the SpeakerId column.',                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'Speaker', @level2name=N'fkSpeaker_Country',             @value = N'Defines the relationship between Speaker and Country tables using the CountryCode column.',                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'Speaker', @level2name=N'fkSpeaker_CountryDivision',     @value = N'Defines the relationship between Speaker and CountryDivision tables using the CountryCode and CountryDivisionCode columns.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'Speaker', @level2name=N'fkSpeaker_DefaultLanguage',     @value = N'Defines the relationship between Speaker and Language tables using the DefaultLanguageCode column.',                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'Speaker', @level2name=N'dfSpeaker_EnablePublicProfile', @value = N'Defines the default value for the EnablePublicProfile as 1 (true).',                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'Speaker', @level2name=N'dfSpeaker_DefaultLangaugeCode', @value = N'Defines the default value for the DefaultLangaugeCode as ''en'' (English).',                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO