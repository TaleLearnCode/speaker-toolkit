﻿CREATE TABLE dbo.EngagementStatus
(
  EngagementStatusId INT NOT NULL IDENTITY(1,1),
  EngagementStatusName NVARCHAR(100) NOT NULL,
  CONSTRAINT pkcEngagementStatus PRIMARY KEY CLUSTERED (EngagementStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementStatus',                                      @value=N'Represents a status of a engagement.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementStatus', @level2name=N'EngagementStatusId',   @value=N'The identifier of the engagement status record.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementStatus', @level2name=N'EngagementStatusName', @value=N'The name of the engagement status.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementStatus', @level2name=N'pkcEngagementStatus',  @value=N'Defines the primary key for the EngagementStatus table using the EngagementStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO