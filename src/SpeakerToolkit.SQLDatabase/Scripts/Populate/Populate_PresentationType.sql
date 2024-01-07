SET IDENTITY_INSERT dbo.PresentationType ON
GO

MERGE dbo.PresentationType AS TARGET
USING (VALUES (1, '45-75 minute session'),
              (2, 'Full-day workshop'),
              (3, 'Half-day workshop'),
              (4, 'Keynote'),
              (5, 'Panel'))
AS SOURCE (PresentationTypeId,
           PresentationTypeName)
ON TARGET.PresentationTypeId = SOURCE.PresentationTypeId
WHEN MATCHED THEN
    UPDATE SET PresentationTypeName = SOURCE.PresentationTypeName
WHEN NOT MATCHED BY TARGET THEN INSERT (PresentationTypeId,
                                        PresentationTypeName)
                                VALUES (SOURCE.PresentationTypeId,
                                        SOURCE.PresentationTypeName)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.PresentationType OFF
GO