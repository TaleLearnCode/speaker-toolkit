CREATE TABLE dbo.Engagement
(
  EngagementId          INT            NOT NULL IDENTITY(1,1),
  EngagementTypeId      INT            NOT NULL,
  EngagementStatusId    INT            NOT NULL,
  EngagementName        NVARCHAR(200)  NOT NULL,
  OverviewLocation      NVARCHAR(300)      NULL,
  ListingLocation       NVARCHAR(100)  NOT NULL,
  StartDate             DATE           NOT NULL,
  EndDate               DATE           NOT NULL,
  StartingCost          NVARCHAR(20)       NULL,
  EndingCost            NVARCHAR(20)       NULL,
  EngagementDescription NVARCHAR(2000)     NULL,
  EngagementSummary     NVARCHAR(140)      NULL,
  EngagementUrl         NVARCHAR(200)      NULL,
  Permalink             NVARCHAR(200)  NOT NULL,
  CONSTRAINT pkcEngagement PRIMARY KEY CLUSTERED (EngagementId),
  CONSTRAINT fkEngagement_EngagementType FOREIGN KEY (EngagementTypeId) REFERENCES dbo.EngagementType (EngagementTypeId),
  CONSTRAINT fkEngagement_EngagementStatus FOREIGN KEY (EngagementStatusId) REFERENCES dbo.EngagementStatus (EngagementStatusId),
  CONSTRAINT unqEngagement_Permalink UNIQUE (Permalink)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement',                                               @value=N'Represents an event that the speaker participates in.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementId',                  @value=N'The identifier of the engagement record.',                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementTypeId',              @value=N'Identifier of the associated engagement type.',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementStatusId',            @value=N'Identifier of the associated engagement status.',                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementName',                @value=N'The name of the engagement.',                                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'OverviewLocation',              @value=N'The location of the event to show on the overview.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'ListingLocation',               @value=N'The location of the event to show on the event listing.',                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'StartDate',                     @value=N'The start date of the event.',                                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EndDate',                       @value=N'The end date of the event.',                                                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'StartingCost',                  @value=N'The starting cost for the event.',                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EndingCost',                    @value=N'The ending cost for the event.',                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementDescription',         @value=N'The full description of the event.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementSummary',             @value=N'The summary description of the event.',                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'pkcEngagement',                 @value=N'Defines the primary key for the Engagement table using the EngagementId column.',                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'fkEngagement_EngagementType',   @value=N'Defines the relationship between the Engagement and EngagementType tables using the EngagementTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'fkEngagement_EngagementStatus', @value=N'Defines the relationship between the Engagement and EngagementType tables using the EngagementTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'unqEngagement_Permalink',       @value=N'Defines a constraint for the Engagement table ensuring that the Permalink column is not duplicated.',          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO