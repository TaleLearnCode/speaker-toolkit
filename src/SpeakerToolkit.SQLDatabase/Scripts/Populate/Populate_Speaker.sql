SET IDENTITY_INSERT dbo.Speaker ON
GO

MERGE dbo.Speaker AS TARGET
USING (VALUES (1, 1, 'US', 'KY', 'Chad', 'Green', 'chad-green'))
AS SOURCE (SpeakerId,
           EnablePublicProfile,
           CountryCode,
           CountryDivisionCode,
           FirstName,
           LastName,
           PublicProfileUrl)
ON TARGET.SpeakerId = SOURCE.SpeakerId
WHEN MATCHED THEN
    UPDATE SET EnablePublicProfile = SOURCE.EnablePublicProfile,
               CountryCode         = SOURCE.CountryCode,
               CountryDivisionCode = SOURCE.CountryDivisionCode,
               FirstName           = SOURCE.FirstName,
               LastName            = SOURCE.LastName,
               PublicProfileUrl    = SOURCE.PublicProfileUrl
WHEN NOT MATCHED BY TARGET THEN INSERT (SpeakerId,
                                        EnablePublicProfile,
                                        CountryCode,
                                        CountryDivisionCode,
                                        FirstName,
                                        LastName,
                                        PublicProfileUrl)
                                VALUES (SOURCE.SpeakerId,
                                        SOURCE.EnablePublicProfile,
                                        SOURCE.CountryCode,
                                        SOURCE.CountryDivisionCode,
                                        SOURCE.FirstName,
                                        SOURCE.LastName,
                                        SOURCE.PublicProfileUrl)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.Speaker OFF
GO