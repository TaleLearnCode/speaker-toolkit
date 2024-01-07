SET IDENTITY_INSERT dbo.EngagementType ON
GO

MERGE dbo.EngagementType AS TARGET
USING (VALUES ( 1, 'Conference'),
              ( 2, 'User Group'),
              ( 3, 'Code Camp'),
              ( 4, 'Interview'),
              ( 5, 'Workshop'),
              ( 6, 'Private Event'))
AS SOURCE (EngagementTypeId,
           EngagementTypeName)
ON TARGET.EngagementTypeId = SOURCE.EngagementTypeId
WHEN MATCHED THEN UPDATE SET TARGET.EngagementTypeName = SOURCE.EngagementTypeName
WHEN NOT MATCHED THEN INSERT (EngagementTypeId,
                              EngagementTypeName)
                      VALUES (SOURCE.EngagementTypeId,
                              SOURCE.EngagementTypeName)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.EngagementType OFF
GO