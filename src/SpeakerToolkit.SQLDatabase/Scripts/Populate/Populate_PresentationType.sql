SET IDENTITY_INSERT dbo.PresentationType ON
GO

MERGE dbo.PresentationType AS TARGET
USING (VALUES ( 1,  1, 'Seminar',               'In-depth presentation on a specific subject. Allows for detailed exploration and discussion.'),
              ( 2,  2, 'Workshop',              'Interactive session with hands-on activities. Participants engage in practical exercises. Facilitator guides the session.'),
              --(3,90,  'Half-day workshop'),
              ( 4,  3, 'Keynote',               'Usually the main presentation at an event. Sets the ttone for the entire event. Delivereed by a prominent speaker.'),
              ( 5,  4, 'Panel Discussion',      'Involves multiple speakers discussing a speciufic topic. Each panelist shares their perspective. Often includes audience Q&A.'),
              ( 6,  5, 'Training Session',      'Educational presentation to teach specific skills. Focuses on practical knowledge and application.'),
              ( 7,  6, 'Ignite Talk',           'Short, fast-paced presentations (usually 5 minutes). Each slide is automatically advanced after a fixed time.'),
              ( 8,  7, 'Lightening Talk',       'Brief, concise presentation (typically 5-10 minutes). Covers a specific topic or idea.'),
              ( 9,  8, 'TED-style Talk',        'Emphasizes storytelling and engaging delivery. Focuses on sharing innovative ideas.'),
              (10,  9, 'Product Demo',          'Showcases a product or service. Demonstrates features and benefits.'),
              (11, 10, 'Roundtable Discussion', 'Informal discussion among participants. Encourages open dialogue and idea sharing.'),
              (12, 11, 'Poster Presentation',   'Visual presentation displayed on a poster board. Presenter discusses the content with attendees.'),
              (13, 12, 'Symposium',             'Conference or meeting for the discussion of a specific topic. Multiple presentations and discussions.'),
              (14, 13, 'Fireside Chat',         'Informal conversation between a moderator and a speaker. Often takes place in a related setting.'),
              (15, 14, 'Town Hall Meeting',     'Gathering where leaders address and interact with a community. Provides updates and allows for Q&A.'))
AS SOURCE (PresentationTypeId,
           SortOrder,
           PresentationTypeName,
           TypeDescription)
ON TARGET.PresentationTypeId = SOURCE.PresentationTypeId
WHEN MATCHED THEN
    UPDATE SET PresentationTypeName = SOURCE.PresentationTypeName
WHEN NOT MATCHED BY TARGET THEN INSERT (PresentationTypeId,
                                        PresentationTypeName,
                                        TypeDescription,
                                        SortOrder)
                                VALUES (SOURCE.PresentationTypeId,
                                        SOURCE.PresentationTypeName,
                                        SOURCE.TypeDescription,
                                        SOURCE.SortOrder)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.PresentationType OFF
GO