CREATE TABLE dbo.SpeakerAward
(
  SpeakerAwardId       INT           NOT NULL IDENTITY(1,1),
  SpeakerId            INT           NOT NULL,
  SpeakerAwardTypeId   INT           NOT NULL,
  AwardCategory        NVARCHAR(100)     NULL,
  AwardYear            INT               NULL,
  AwardProfileUrl      VARCHAR(200)  NOT NULL,
  CONSTRAINT pkcSpeakerAward PRIMARY KEY CLUSTERED (SpeakerAwardId),
  CONSTRAINT fkcSpeakerAward_SpeakerId FOREIGN KEY (SpeakerId) REFERENCES dbo.Speaker (SpeakerId),
  CONSTRAINT fkcSpeakerAward_SpeakerAwardTypeId FOREIGN KEY (SpeakerAwardTypeId) REFERENCES dbo.SpeakerAwardType (SpeakerAwardTypeId),
  CONSTRAINT uqcSpeakerAward_SpeakerId_SpeakerAwardTypeId_AwardCategory_AwardYear UNIQUE (SpeakerId, SpeakerAwardTypeId, AwardCategory, AwardYear)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAward',                                      @value=N'Represents an award bestowed to a speaker.',                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAward', @level2name=N'SpeakerAwardId',   @value=N'The identifier of the speaker award record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAward', @level2name=N'SpeakerId',   @value=N'The identifier of the speaker award record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAward', @level2name=N'SpeakerAwardTypeId',   @value=N'The identifier of the speaker award record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAward', @level2name=N'AwardCategory',   @value=N'The identifier of the speaker award record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAward', @level2name=N'AwardYear',   @value=N'The identifier of the speaker award record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
