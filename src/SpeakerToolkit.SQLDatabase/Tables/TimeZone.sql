CREATE TABLE dbo.TimeZone
(
  TimeZoneId       VARCHAR(100) NOT NULL,
  UTCOffsetId      CHAR(1)      NOT NULL,
  STDAbbreviation  CHAR(4)      NOT NULL,
  STDOffsetName    CHAR(6)      NOT NULL,
  STDOffsetMinutes SMALLINT     NOT NULL,
  DSTAbbreviation  CHAR(4)          NULL,
  DSTOffsetName    CHAR(6)          NULL,
  DSTOffsetMinutes SMALLINT         NULL,
  CONSTRAINT pkcTimeZone PRIMARY KEY CLUSTERED (TimeZoneId),
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone',                                   @value=N'Represents the list of time zones as defined by the IANA.',                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'TimeZoneId',        @value=N'The identifier of the time zone as defined by the IANA.',                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'UTCOffsetId',       @value=N'The letter designation for the UTC time offset.',                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'STDAbbreviation',   @value=N'The IANA abbreviation for the time zone when in standard time.',              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'DSTAbbreviation',   @value=N'The IANA abbreviation for the time zone when in daylight savings time.',      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'STDOffsetName',     @value=N'The name of the offset when in standard time.',                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'STDOffsetMinutes',  @value=N'The number of minutes offset from UTC when in standard time.',                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'DSTOffsetName',     @value=N'THe name of the offset when in daylight savings time.',                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO                                                                                                                                                                                                
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'DSTOffsetMinutes',  @value=N'The number of minutes offset from UTC when in daylight savings time.',        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'TimeZone', @level2name=N'pkcTimeZone',       @value=N'Defines the primary key for the TimeZone table using the TimeZoneId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO