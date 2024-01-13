CREATE TABLE dbo.ContentType
(
	ContentTypeId   INT          NOT NULL,
	ContentTypeName VARCHAR(100) NOT NULL,
	IsEnabled       BIT          NOT NULL,
	CONSTRAINT pkcContentType PRIMARY KEY (ContentTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'ContentType',                                 @value=N'Defines the types of Content Types supported by the system.',                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'ContentType', @level2name=N'ContentTypeId',   @value=N'Identifier of the Content Type record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'ContentType', @level2name=N'ContentTypeName', @value=N'The name of the Content Type.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'ContentType', @level2name=N'IsEnabled',       @value=N'Flag indicating whether the Content Type is enabled.',                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'ContentType', @level2name=N'pkcContentType',  @value=N'Defines the primary key for the ContentType table using the ContentTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO