SET IDENTITY_INSERT dbo.SpeakerLink ON
GO

MERGE dbo.SpeakerLink AS TARGET
USING (VALUES (1, 1, 'Twitter',         'https://twitter.com/chadgreen'),
              (2, 1, 'LinkedIn',        'https://www.linkedin.com/in/chadgreen/'),
              (3, 1, 'GitHub',          'https://www.github.com/talelearncode'),
              (4, 1, 'Facebook',        'https://www.facebook.com'),
              (5, 1, 'Blog',            'https://www.chadgreen.com'),
              (6, 1, 'Company Website', 'https://www.jasperengines.com'))
AS SOURCE (SpeakerLinkId,
           SpeakerId,
           LinkType,
           LinkUrl)
ON TARGET.SpeakerLinkId = SOURCE.SpeakerLinkId
WHEN MATCHED THEN UPDATE SET SpeakerId = SOURCE.SpeakerId,
                             LinkType  = SOURCE.LinkType,
                             LinkUrl   = SOURCE.LinkUrl
WHEN NOT MATCHED THEN INSERT (SpeakerLinkId,
                              SpeakerId,
                              LinkType,
                              LinkUrl)
                      VALUES (SOURCE.SpeakerLinkId,
                              SOURCE.SpeakerId,
                              SOURCE.LinkType,
                              SOURCE.LinkUrl);
GO

SET IDENTITY_INSERT dbo.SpeakerLink OFF
GO