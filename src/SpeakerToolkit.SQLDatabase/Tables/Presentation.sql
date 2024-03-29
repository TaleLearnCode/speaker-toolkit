﻿CREATE TABLE dbo.Presentation
(
  PresentationId         INT           NOT NULL IDENTITY(1,1),
  PresentationTypeId     INT           NOT NULL,
  RepoLink               VARCHAR(200)      NULL,
  Permalink              VARCHAR(200)  NOT NULL,
  IsArchived             BIT           NOT NULL CONSTRAINT dfPresentation_IsArchived DEFAULT 0,
  IncludeInPublicProfile BIT           NOT NULL CONSTRAINT dfPresentation_IncludeInPublicProfile DEFAULT 1,
  DefaultLanguageCode    CHAR(2)       NOT NULL CONSTRAINT dfPresentation_DefaultLanguageCode DEFAULT 'en',
  CONSTRAINT pkcPresentation PRIMARY KEY CLUSTERED (PresentationId),
  CONSTRAINT fkPresentation_PresentationType FOREIGN KEY (PresentationTypeId) REFERENCES dbo.PresentationType (PresentationTypeId),
  CONSTRAINT fkPresentation_Language FOREIGN KEY (DefaultLanguageCode) REFERENCES dbo.Language (LanguageCode),
  CONSTRAINT unqPresentation_Permalink UNIQUE (Permalink)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation',                                                       @value=N'Represents the speaker''s presentations.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'PresentationId',                        @value=N'The identifier of the presentation record.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'PresentationTypeId',                    @value=N'Identifier of the type of presentation is represented.',                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'IsArchived',                            @value=N'Flag indicating whether the presentation has been archived.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'IncludeInPublicProfile',                @value=N'Flag indicating whether the presentation is to be include in the public profile.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'DefaultLanguageCode',                   @value=N'The default language to use for the presentation.',                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'pkcPresentation',                       @value=N'Defines the primary key for the Presentation table using the PresentationId column.',                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'fkPresentation_PresentationType',       @value=N'Defines the relationship between the Presentation and PresentationType tables using the PresentationTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'fkPresentation_Language',               @value=N'Defines the relationship between the Presentation and Langauge tables using the DefaultLanguage column.',            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'dfPresentation_IsArchived',             @value=N'Defines the default value for the IsArchived column as 0 (false).',                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'dfPresentation_IncludeInPublicProfile', @value=N'Defines the default value for the IncludeInPublicProfile column as 1 (true).',                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'dfPresentation_DefaultLanguageCode',    @value=N'Defines the default value for the DefaultLanguageCode column as en (English).',                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Presentation', @level2name=N'unqPresentation_Permalink',             @value=N'Defines a constraint for the Presentation table ensuring that the Permalink column is not duplicated.',              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO