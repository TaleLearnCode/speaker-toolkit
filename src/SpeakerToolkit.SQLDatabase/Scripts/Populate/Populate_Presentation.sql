SET IDENTITY_INSERT dbo.Presentation ON
GO

MERGE dbo.Presentation AS TARGET
USING (VALUES ( 1, 1, 0, 1, 'building-event-driven-microservices',                                      'https://github.com/TaleLearnCode/BuildingEventDrivenMicroservices'),
              ( 2, 1, 0, 1, 'time-traveling-data',                                                      'https://github.com/TaleLearnCode/TimeTravellingData'),
              ( 3, 1, 0, 1, 'building-microservice-rest-apis-using-azure-functions',                    'https://github.com/TaleLearnCode/BuildingMicroserviceRESTAPIsUsingAzureFunctions'),
              ( 4, 1, 0, 1, 'the-taming-of-the-api',                                                    'https://github.com/TaleLearnCode/TheTamingOfTheAPI'),
              ( 5, 2, 0, 1, 'advanced-serverless-workshop',                                             'https://github.com/TaleLearnCode/AdvancedServerlessWorkshop'),
              ( 6, 1, 0, 1, 'build-a-fully-managed-and-documented-api-from-scratch',                    'https://github.com/TaleLearnCode/FileNew-APIManagement'),
              ( 7, 1, 0, 1, 'build-a-serverless-microservice-from-scratch',                             'https://github.com/TaleLearnCode/FileNew-ServerlessMicroservices'),
              ( 8, 1, 0, 1, 'build-an-event-driven-architected-microservice-from-scratch',              'https://github.com/TaleLearnCode/FileNew-EventDrivenArchitectedMicroservice'),
              ( 9, 1, 0, 1, 'secrets-of-conflict-resolution',                                           'https://github.com/TaleLearnCode/SecretsOfConflictResolution'),
              (10, 1, 0, 1, 'building-great-libraries',                                                 'https://github.com/TaleLearnCode/BuildingGreatLibraries'),
              (11, 1, 1, 0, 'developing-resilient-serverless-solutions',                                'https://github.com/TaleLearnCode/DevelopingResilientServerlessSolution'),
              (12, 2, 0, 1, 'design-and-develop-a-serverless-event-driven-microservice-based-solution', 'https://github.com/TaleLearnCode/Serverless2DayWorkshop'),
              (13, 1, 0, 1, 'getting-deeper-into-azure-functions',                                      'https://github.com/TaleLearnCode/BeyondHelloWorld_GettingDeeperIntoAzureFunctions'),
              (14, 1, 0, 1, 'going-schemaless',                                                         'https://github.com/TaleLearnCode/GoingSchemaless'),
              (15, 1, 0, 1, 'technical-debt-is-not-free',                                               'https://github.com/TaleLearnCode/TechnicalDebtIsNotFree'),
              (16, 1, 0, 1, 'ch-ch-ch-changes',                                                         'https://github.com/TaleLearnCode/Ch-ch-ch-changes-Tracing-Changes-in-Azure-Cosmos-DB'),
              (17, 1, 0, 1, 'whats-new-for-csharp-developers',                                          'https://github.com/TaleLearnCode/WhatsNewForCSharpDevelopers'),
              (18, 1, 0, 1, 'software-craftsmanship-for-new-developers',                                'https://github.com/TaleLearnCode/SoftwareCraftsmanshipForNewDevelopers'),
              (19, 1, 1, 1, 'serverless-in-action',                                                     'https://github.com/TaleLearnCode/ServerlessInAction'),
              (20, 1, 1, 1, 'event-driven-architecture-in-the-cloud',                                   'https://github.com/TaleLearnCode/EventDrivenArchitectureInTheCloud'),
              (21, 1, 0, 1, 'hitchhikers-guide-to-cosmos',                                              'https://github.com/TaleLearnCode/HitchhikersGuideToCosmos'),
              (22, 1, 1, 1, 'serverless-microservices-without-containers',                              'https://github.com/TaleLearnCode/ServerlessMicroservices'),
              (23, 1, 0, 1, 'leading-with-strength',                                                    'https://github.com/TaleLearnCode/LeadingWithStrength'),
              (24, 1, 1, 1, 'intro-to-azure-communication-services',                                    'https://github.com/TaleLearnCode/IntroToAzureCommunicationServices'),
              (25, 1, 1, 1, 'building-a-dotnet-application-using-azure-cosmos-db',                      'https://github.com/TaleLearnCode/BuildingDotNetAppUsingCosmosDB'),
              (26, 1, 1, 1, 'graphing-your-way-through-the-cosmos',                                     'https://github.com/TaleLearnCode/GraphingYourWayThroughTheCosmos'),
              (27, 1, 0, 1, 'which-microsoft-framework-am-i-supposed-to-use',                            NULL),
              (28, 1, 1, 1, 'chad-and-eds-excellent-adventure',                                          NULL),
              (29, 1, 1, 1, 'dotnet-conf-keynote-review',                                                NULL),
              (30, 1, 1, 1, 'building-great-libraries-with-net-standard',                                NULL),
              (31, 1, 1, 1, 'building-an-ultra-scalable-api-using-azure-functions',                      NULL),
              (32, 1, 1, 1, 'delivering-real-time-data-with-azure',                                      NULL),
              (33, 1, 0, 1, 'azure-durable-functions-for-serverless-dotnet-orchestration',               NULL),
              (34, 1, 1, 1, 'azure-services-every-developer-needs-to-know',                              NULL),
              (35, 1, 1, 1, 'building-hyper-scaled-event-processing-solutions-in-azure',                 NULL),
              (36, 2, 1, 1, 'getting-gremlins-to-improve-your-data',                                     NULL),
              (37, 1, 1, 1, 'getting-started-with-azure-sql-database',                                   NULL),
              (38, 1, 1, 1, 'getting-started-with-azure-devops',                                         NULL),
              (39, 4, 0, 1, 'other-duties-as-assigned',                                                  'https://github.com/TaleLearnCode/OtherDutiesAsAssigned'),
              (40, 1, 0, 1, 'database-driven-static-websites',                                           'https://github.com/TaleLearnCode/database-driven-static-websites'),
              (41, 1, 1, 1, 'ignite-2022-roundup',                                                       NULL),
              (42, 5, 1, 1, 'how-can-i-be-a-speaker-like-you',                                           NULL),
              (43, 1, 1, 1, 'common-data-problems-solved-with-graphs',                                   NULL),
              (44, 1, 0, 1, 'automation-using-azure-event-grid',                                         'https://github.com/TaleLearnCode/AutomationUsingAzureEventGrid'),
              (45, 1, 0, 1, 'azure-unleasing-power-of-cloud-computing',                                  'https://github.com/TaleLearnCode/Azure-Unleashing-the-Power-of-Cloud-Computing'),
              (51, 1, 0, 1, 'unleashing-extreme-scalability-with-azure-functions',                       'https://github.com/TaleLearnCode/UnleashingExtremeScalabilityWithAzureFunctions'),
              (46, 1, 0, 1, 'cloud-nirvana-awaits',                                                      'https://github.com/TaleLearnCode/CloudNirvana'),
              (47, 1, 0, 1, 'crack-the-code-to-scalability',                                             'https://github.com/TaleLearnCode/CrackTheCodeToScalability'),
              (48, 1, 0, 1, 'new-horizons-for-csharp-developers',                                        'https://github.com/TaleLearnCode/new-horizons-for-csharp-developers'),
              (49, 1, 0, 1, 'navigating-the-database-landscape',                                         'https://github.com/TaleLearnCode/navigating-the-database-landscape'),
              (50, 1, 0, 1, 'reevaluating-software-design-patterns',                                     'https://github.com/TaleLearnCode/ReevaluatingSoftwareDesignPatterns')
              
              --(PresentationId, PresentationTypeId, IsArchived, IncludeInPublicProfile, Permalink, RepoLink)
             )
AS SOURCE (PresentationId,
           PresentationTypeId,
           IsArchived,
           IncludeInPublicProfile,
           Permalink,
           RepoLink)
ON TARGET.PresentationId = SOURCE.PresentationId
WHEN MATCHED THEN UPDATE SET TARGET.PresentationTypeId     = SOURCE.PresentationTypeId,
                             TARGET.IsArchived             = SOURCE.IsArchived,
                             TARGET.IncludeInPublicProfile = SOURCE.IncludeInPublicProfile,
                             TARGET.Permalink              = SOURCE.Permalink,
                             TARGET.RepoLink               = SOURCE.RepoLink
WHEN NOT MATCHED THEN INSERT (PresentationId,
                              PresentationTypeId,
                              IsArchived,
                              IncludeInPublicProfile,
                              Permalink,
                              RepoLink)
                      VALUES (SOURCE.PresentationId,
                              SOURCE.PresentationTypeId,
                              SOURCE.IsArchived,
                              SOURCE.IncludeInPublicProfile,
                              SOURCE.Permalink,
                              SOURCE.RepoLink)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.Presentation OFF
GO