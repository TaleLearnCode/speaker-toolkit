SET IDENTITY_INSERT dbo.SpeakerAward ON
GO

MERGE dbo.SpeakerAward AS TARGET
USING (VALUES (1, 1, 9, 'Developer Technologies', 2019, 'https://mvp.microsoft.com/en-US/MVP/profile/e716be70-76af-e911-a98b-000d3a137780'),
              (2, 1, 9, 'Developer Technologies', 2020, 'https://mvp.microsoft.com/en-US/MVP/profile/e716be70-76af-e911-a98b-000d3a137780'),
              (3, 1, 9, 'Developer Technologies', 2021, 'https://mvp.microsoft.com/en-US/MVP/profile/e716be70-76af-e911-a98b-000d3a137780'),
              (4, 1, 9, 'Developer Technologies', 2022, 'https://mvp.microsoft.com/en-US/MVP/profile/e716be70-76af-e911-a98b-000d3a137780'),
              (5, 1, 9, 'Developer Technologies', 2023, 'https://mvp.microsoft.com/en-US/MVP/profile/e716be70-76af-e911-a98b-000d3a137780'),
              (6, 1, 9, 'Azure',                  2023, 'https://mvp.microsoft.com/en-US/MVP/profile/e716be70-76af-e911-a98b-000d3a137780'))
AS SOURCE (SpeakerAwardId,
           SpeakerId,
           SpeakerAwardTypeId,
           AwardCategory,
           AwardYear,
           AwardProfileUrl)
ON TARGET.SpeakerAwardId = SOURCE.SpeakerAwardId
WHEN MATCHED THEN UPDATE SET SpeakerId          = SOURCE.SpeakerId,
                             SpeakerAwardTypeId = SOURCE.SpeakerAwardTypeId,
                             AwardCategory      = SOURCE.AwardCategory,
                             AwardYear          = SOURCE.AwardYear,
                             AwardProfileUrl    = SOURCE.AwardProfileUrl
WHEN NOT MATCHED THEN INSERT (SpeakerAwardId,
                              SpeakerId,
                              SpeakerAwardTypeId,
                              AwardCategory,
                              AwardYear,
                              AwardProfileUrl)
                      VALUES (SOURCE.SpeakerAwardId,
                              SOURCE.SpeakerId,
                              SOURCE.SpeakerAwardTypeId,
                              SOURCE.AwardCategory,
                              SOURCE.AwardYear,
                              SOURCE.AwardProfileUrl);
GO

SET IDENTITY_INSERT dbo.SpeakerAward OFF
GO