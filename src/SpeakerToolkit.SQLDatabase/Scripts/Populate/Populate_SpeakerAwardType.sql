SET IDENTITY_INSERT dbo.SpeakerAwardType ON
GO

MERGE dbo.SpeakerAwardType AS TARGET
USING (VALUES ( 1, 0, 0, 'AWS Hero'),
              ( 2, 0, 0, 'Cloud Native Ambassador'),
              ( 3, 0, 0, 'Docker Captain'),
              ( 4, 0, 0, 'GitHub Stars'),
              ( 5, 0, 0, 'Google Developer Expert'),
              ( 6, 0, 0, 'HashiCorp Ambassador'),
              ( 7, 0, 0, 'HashiCorp Core Contributor'),
              ( 8, 0, 0, 'Java Champion'),
              ( 9, 1, 1, 'Microsoft MVP'),
              (10, 1, 1, 'Microsoft Regional Director'),
              (11, 0, 0, 'Testcontainers Community Champion'),
              (12, 0, 0, 'VMware vExpert'),
              (13, 0, 0, 'Windows Insider MVP'),
              (14, 0, 0, 'Women Techmakers Ambassador'))
AS SOURCE (SpeakerAwardTypeId,
           HasCategories,
           HasAwardYears,
           SpeakerAwardTypeName)
ON (TARGET.SpeakerAwardTypeId = SOURCE.SpeakerAwardTypeId)
WHEN MATCHED THEN UPDATE SET HasCategories        = SOURCE.HasCategories,
                             HasAwardYears        = SOURCE.HasAwardYears,
                             SpeakerAwardTypeName = SOURCE.SpeakerAwardTypeName
WHEN NOT MATCHED THEN INSERT (SpeakerAwardTypeId,
                              HasCategories,
                              HasAwardYears,
                              SpeakerAwardTypeName)
                      VALUES (SOURCE.SpeakerAwardTypeId,
                              SOURCE.HasCategories,
                              SOURCE.HasAwardYears,
                              SOURCE.SpeakerAwardTypeName);
GO

SET IDENTITY_INSERT dbo.SpeakerAwardType OFF
GO