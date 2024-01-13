SET IDENTITY_INSERT dbo.SpeakerBiography ON
GO

MERGE dbo.SpeakerBiography AS TARGET
USING (VALUES (1, 1, 'EN', 'Sr. Software Architect at Jasper Engines | Cloud Tech Leader | Microsoft MVP | Speaker & Innovator | USMC Vet | Lego Builder #Tech #Cloud #Community', 'Chad Green, a seasoned Senior Software Architect at Jasper Engines & Transmissions, boasts a distinguished three-decades-long career in building customer-centric solutions. Currently leading the migration of legacy applications to cloud-native technologies, Chad''s impactful career includes roles in the senior living, education, healthcare, government, financial, chemical, safety, and consumer goods industries. Beyond his professional pursuits, Chad is a dedicated community leader, having founded and led Code PaLOUsa for 13 years. Recognized as a Microsoft Most Valuable Professional (MVP) in Developer Technologies and Azure, Chad''s global influence extends to his active role in organizing meetups and speaking at national and international events. A proud father, loving husband, and a United States Marine Corps veteran, Chad channels his diverse interests into building intricate Lego sets and a detailed Lego city, showcasing his passion for innovation and attention to detail.'))
AS SOURCE (SpeakerBiographyId,
           SpeakerId,
           LanguageCode,
           Title,
           Biography)
ON TARGET.SpeakerBiographyId = SOURCE.SpeakerBiographyId
WHEN MATCHED THEN UPDATE SET SpeakerId    = SOURCE.SpeakerId,
                             LanguageCode = SOURCE.LanguageCode,
                             Title        = SOURCE.Title,
                             Biography    = SOURCE.Biography
WHEN NOT MATCHED BY TARGET THEN INSERT (SpeakerBiographyId,
                                        SpeakerId,
                                        LanguageCode,
                                        Title,
                                        Biography)
                                VALUES (SOURCE.SpeakerBiographyId,
                                        SOURCE.SpeakerId,
                                        SOURCE.LanguageCode,
                                        SOURCE.Title,
                                        SOURCE.Biography)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.SpeakerBiography OFF
GO