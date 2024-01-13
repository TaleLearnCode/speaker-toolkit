﻿MERGE dbo.WorldRegion AS TARGET
USING (VALUES 
              ('150', 1, NULL, 'Europe'),
              ('039', 1, '150', 'Southern Europe'),
              ('142', 1, NULL, 'Asia'),
              ('145', 1, '142', 'Western Asia'),
              ('034', 1, '142', 'Southern Asia'),
              ('019', 1, NULL, 'Americas'),
              ('029', 1, '019', 'Caribbean'),
              ('002', 1, NULL, 'Africa'),
              ('017', 1, '002', 'Middle Africa'),
              ('010', 1, NULL, 'Antarctica'),
              ('005', 1, '019', 'South America'),
              ('009', 1, NULL, 'Oceania'),
              ('016', 1, '009', 'Polynesia'),
              ('155', 1, '150', 'Western Europe'),
              ('036', 1, '009', 'Australia and New Zealand'),
              ('248', 1, '150', 'Northern Europe'),
              ('050', 1, '142', 'Southern Asia'),
              ('011', 1, '002', 'Western Africa'),
              ('100', 1, '150', 'Eastern Europe'),
              ('014', 1, '002', 'Eastern Africa'),
              ('021', 1, '019', 'Northern America'),
              ('035', 1, '142', 'South-Eastern Asia'),
              ('064', 1, '142', 'Southern Asia'),
              ('018', 1, '002', 'Southern Africa'),
              ('151', 1, '150', 'Eastern Europe'),
              ('013', 1, '019', 'Central America'),
              ('053', 1, '009', 'Australia and New Zealand'),
              ('184', 1, '009', 'Polynesia'),
              ('030', 1, '142', 'Eastern Asia'),
              ('154', 1, '150', 'Northern Europe'),
              ('015', 1, '002', 'Northern Africa'),
              ('054', 1, '009', 'Melanesia'),
              ('057', 1, '009', 'Micronesia'),
              ('143', 1, '142', 'Central Asia'),
              ('574', 1, '009', 'Australia and New Zealand'),
              ('061', 1, '009', 'Polynesia'))
AS SOURCE (WorldRegionCode,
           IsEnabled,
           ParentId,
           WorldRegionName)
ON TARGET.WorldRegionCode = SOURCE.WorldRegionCode
WHEN MATCHED THEN UPDATE SET TARGET.WorldRegionName = SOURCE.WorldRegionName,
                             TARGET.ParentId        = SOURCE.ParentId,
                             TARGET.IsEnabled       = SOURCE.IsEnabled
WHEN NOT MATCHED THEN INSERT (WorldRegionCode,
                              WorldRegionName,
                              ParentId,
                              IsEnabled)
                      VALUES (SOURCE.WorldRegionCode,
                              SOURCE.WorldRegionName,
                              SOURCE.ParentId,
                              SOURCE.IsEnabled);