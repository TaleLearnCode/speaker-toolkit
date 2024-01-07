/*
Post-Deployment Script Template              
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.    
 Use SQLCMD syntax to include a file in the post-deployment script.      
 Example:      :r .\myfile.sql                
 Use SQLCMD syntax to reference a variable in the post-deployment script.    
 Example:      :setvar TableName MyTable              
               SELECT * FROM [$(TableName)]          
--------------------------------------------------------------------------------------
*/
:r .\Populate\Populate_PresentationType.sql
:r .\Populate\Populate_Presentation.sql
:r .\Populate\Populate_Tag.sql
:r .\Populate\Populate_PresentationTag.sql
:r .\Populate\Populate_PresentationRelated.sql
:r .\Populate\Populate_LearningObjective.sql
:r .\Populate\Populate_EngagementStatus.sql
:r .\Populate\Populate_EngagementType.sql
:r .\Populate\Populate_Engagement.sql
:r .\Populate\Populate_EngagementPresentation.sql
:r .\Populate\Populate_EngagementPresentationDownload.sql