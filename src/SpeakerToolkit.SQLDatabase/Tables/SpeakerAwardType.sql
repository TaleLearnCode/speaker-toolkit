CREATE TABLE dbo.SpeakerAwardType
(
  SpeakerAwardTypeId   INT           NOT NULL IDENTITY(1,1),
  SpeakerAwardTypeName NVARCHAR(100) NOT NULL,
  HasCategories        BIT           NOT NULL,
  HasAwardYears        BIT           NOT NULL,
  CONSTRAINT pkcSpeakerAwardType PRIMARY KEY CLUSTERED (SpeakerAwardTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAwardType',                                      @value=N'Represents a type of a speaker award.',                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAwardType', @level2name=N'SpeakerAwardTypeId',   @value=N'The identifier of the speaker award type record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAwardType', @level2name=N'SpeakerAwardTypeName', @value=N'The name of the speaker award type name.',                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAwardType', @level2name=N'HasCategories',        @value=N'Flag indicating whether the speaker award has categories.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAwardType', @level2name=N'HasAwardYears',        @value=N'Flag indicating whether the speaker award has award year.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SpeakerAwardType', @level2name=N'pkcSpeakerAwardType',  @value=N'Defines the primary key for the SpeakerAwardType table using the SpeakerAwardTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO