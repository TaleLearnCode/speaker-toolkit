MERGE dbo.ContentType AS TARGET
USING (VALUES (1, 1, 'Speaker Title'),
              (2, 1, 'Speaker Biography'))
AS SOURCE (ContentTypeId,
           IsEnabled,
           ContentTypeName)
ON TARGET.ContentTypeId = SOURCE.ContentTypeId
WHEN MATCHED THEN UPDATE SET IsEnabled = SOURCE.IsEnabled,
                             ContentTypeName = SOURCE.ContentTypeName
WHEN NOT MATCHED THEN INSERT (ContentTypeId,
                              IsEnabled,
                              ContentTypeName)
                      VALUES (SOURCE.ContentTypeId,
                              SOURCE.IsEnabled,
                              SOURCE.ContentTypeName);