CREATE TABLE dbo.PresentationRelated
(
  PresentationRelatedId INT NOT NULL IDENTITY(1,1),
  PrimaryPresentationId INT NOT NULL,
  RelatedPresentationId INT NOT NULL,
  SortOrder INT NOT NULL,
  CONSTRAINT pkcPresentationRelated PRIMARY KEY CLUSTERED (PresentationRelatedId),
  CONSTRAINT fkPresentationRelated_PresentationId FOREIGN KEY (PrimaryPresentationId) REFERENCES dbo.Presentation (PresentationId),
  CONSTRAINT fkPresentationRelated_RelatedPresentationId FOREIGN KEY (RelatedPresentationId) REFERENCES dbo.Presentation (PresentationId),
  CONSTRAINT unqPresentationRelated_PresentationId_RelatedPresentationId UNIQUE (PrimaryPresentationId, RelatedPresentationId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated',                                                             @value=N'Links two related presentations togethers.',                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'PresentationRelatedId',                       @value=N'The identifier of the releated presentation object.',                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'PrimaryPresentationId',                       @value=N'The identifier of the primary presentation.',                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'RelatedPresentationId',                       @value=N'The identifier of the related presentation.',                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'SortOrder',                                   @value=N'The sorting order of the related presentation.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'pkcPresentationRelated',                      @value=N'Defines the primary key for the PresentationRelated table using the PresentationRelatedId column.',                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'fkPresentationRelated_PresentationId',        @value=N'Defines the relationship between the PresentationRelated and Presentation tables using the PresentationId column.',        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationRelated', @level2name=N'fkPresentationRelated_RelatedPresentationId', @value=N'Defines the relationship between the PresentationRelated and Presentation tables using the RelatedPresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO