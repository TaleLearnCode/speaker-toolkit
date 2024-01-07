CREATE TABLE dbo.EngagementType
(
  EngagementTypeId   INT           NOT NULL IDENTITY(1,1),
  EngagementTypeName NVARCHAR(100) NOT NULL,
  CONSTRAINT pkcEngagementType PRIMARY KEY CLUSTERED (EngagementTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType',                                    @value=N'Represents a type of engagement.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'EngagementTypeId',   @value=N'The identifier of the engagement type record.',                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'EngagementTypeName', @value=N'The name of the engagement type.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'pkcEngagementType',  @value=N'Defines the primary key for the EngagementType table using the EngagementTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO