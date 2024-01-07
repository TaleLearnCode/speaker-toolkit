SET IDENTITY_INSERT dbo.EngagementStatus ON
GO

MERGE dbo.EngagementStatus AS TARGET
USING (VALUES ( 1, 'Scheduled'),
              ( 2, 'Completed'),
              ( 3, 'Canceled'))
AS SOURCE (EngagementStatusId,
           EngagementStatusName)
ON TARGET.EngagementStatusId = SOURCE.EngagementStatusId
WHEN MATCHED THEN UPDATE SET TARGET.EngagementStatusName = SOURCE.EngagementStatusName
WHEN NOT MATCHED THEN INSERT (EngagementStatusId,
                              EngagementStatusName)
                      VALUES (SOURCE.EngagementStatusId,
                              SOURCE.EngagementStatusName)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.EngagementStatus OFF
GO