﻿SET IDENTITY_INSERT dbo.PresentationSpeaker ON
GO

MERGE dbo.PresentationSpeaker AS TARGET
USING (VALUES ( 1,  1,  1, 1),
              ( 2,  2,  1, 1),
              ( 3,  3,  1, 1),
              ( 4,  4,  1, 1),
              ( 5,  5,  1, 1),
              ( 6,  6,  1, 1),
              ( 7,  7,  1, 1),
              ( 8,  8,  1, 1),
              ( 9,  9,  1, 1),
              (10, 10,  1, 1),
              (11, 11,  1, 1),
              (12, 12,  1, 1),
              (13, 13,  1, 1),
              (14, 14,  1, 1),
              (15, 15,  1, 1),
              (16, 16,  1, 1),
              (17, 17,  1, 1),
              (18, 18,  1, 1),
              (19, 19,  1, 1),
              (20, 20,  1, 1),
              (21, 21,  1, 1),
              (22, 22,  1, 1),
              (23, 23,  1, 1),
              (24, 24,  1, 1),
              (25, 25,  1, 1),
              (26, 26,  1, 1),
              (27, 27,  1, 1),
              (28, 28,  1, 1),
              (29, 29,  1, 1),
              (30, 30,  1, 1),
              (31, 31,  1, 1),
              (32, 32,  1, 1),
              (33, 33,  1, 1),
              (34, 34,  1, 1),
              (35, 35,  1, 1),
              (36, 36,  1, 1),
              (37, 37,  1, 1),
              (38, 38,  1, 1),
              (39, 39,  1, 1),
              (40, 40,  1, 1),
              (40, 40,  1, 1),
              (41, 41,  1, 1),
              (42, 42,  1, 1),
              (43, 43,  1, 1),
              (44, 44,  1, 1),
              (45, 45,  1, 1),
              (46, 46,  1, 1),
              (47, 47,  1, 1),
              (48, 48,  1, 1),
              (49, 49,  1, 1),
              (50, 50,  1, 1))
AS SOURCE (PresentationSpeakerId,
           PresentationId,
           SpeakerId,
           IsPrimary)
ON TARGET.PresentationSpeakerId = SOURCE.PresentationSpeakerId
WHEN MATCHED THEN UPDATE SET PresentationId = SOURCE.PresentationId,
                             SpeakerId      = SOURCE.SpeakerId,
                             IsPrimary      = SOURCE.IsPrimary
WHEN NOT MATCHED BY TARGET THEN INSERT (PresentationSpeakerId,
                                        PresentationId,
                                        SpeakerId,
                                        IsPrimary)
                                VALUES (SOURCE.PresentationSpeakerId,
                                        SOURCE.PresentationId,
                                        SOURCE.SpeakerId,
                                        SOURCE.IsPrimary)
WHEN NOT MATCHED BY SOURCE THEN DELETE;

SET IDENTITY_INSERT dbo.PresentationSpeaker OFF
GO