
    BEGIN TRY
      BEGIN TRANSACTION;
  

DECLARE @ExistingResourceId1 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 8:4-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId1 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId1 int = COALESCE(@ExistingResourceId1, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId1 AS ResourceId, 1 AS LanguageId, 'Acts 8:4-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5600 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_004_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_004_1.mp3","webmSizeKb":136,"mp3SizeKb":232},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_004_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_004_2.mp3","webmSizeKb":1727,"mp3SizeKb":2898},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_004_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_004_3.mp3","webmSizeKb":1982,"mp3SizeKb":3353},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_004_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_004_4.mp3","webmSizeKb":1226,"mp3SizeKb":2077},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_004_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_004_5.mp3","webmSizeKb":529,"mp3SizeKb":888}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId1 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045008004 AS StartVerseId, 1045008025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId1 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId1 int = COALESCE(@ExistingPassageId1, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId1 AS PassageId, @ResourceId1 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId2 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 8:26-40' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId2 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId2 int = COALESCE(@ExistingResourceId2, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId2 AS ResourceId, 1 AS LanguageId, 'Acts 8:26-40' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5724 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_026_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_026_1.mp3","webmSizeKb":136,"mp3SizeKb":234},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_026_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_026_2.mp3","webmSizeKb":2133,"mp3SizeKb":3588},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_026_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_026_3.mp3","webmSizeKb":2039,"mp3SizeKb":3432},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_026_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_026_4.mp3","webmSizeKb":1006,"mp3SizeKb":1681},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_008_026_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_008_026_5.mp3","webmSizeKb":410,"mp3SizeKb":689}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId2 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045008026 AS StartVerseId, 1045008040 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId2 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId2 int = COALESCE(@ExistingPassageId2, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId2 AS PassageId, @ResourceId2 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId3 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 9:1-19a' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId3 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId3 int = COALESCE(@ExistingResourceId3, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId3 AS ResourceId, 1 AS LanguageId, 'Acts 9:1-19a' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3565 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_001_1.mp3","webmSizeKb":127,"mp3SizeKb":217},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_001_2.mp3","webmSizeKb":1401,"mp3SizeKb":2333},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_001_3.mp3","webmSizeKb":587,"mp3SizeKb":984},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_001_4.mp3","webmSizeKb":756,"mp3SizeKb":1270},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_001_5.mp3","webmSizeKb":694,"mp3SizeKb":1157}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId3 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045009001 AS StartVerseId, 1045009019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId3 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId3 int = COALESCE(@ExistingPassageId3, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId3 AS PassageId, @ResourceId3 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId4 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 9:19b-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId4 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId4 int = COALESCE(@ExistingResourceId4, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId4 AS ResourceId, 1 AS LanguageId, 'Acts 9:19b-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3035 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_019_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_019_1.mp3","webmSizeKb":118,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_019_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_019_2.mp3","webmSizeKb":1139,"mp3SizeKb":1892},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_019_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_019_3.mp3","webmSizeKb":479,"mp3SizeKb":800},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_019_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_019_4.mp3","webmSizeKb":739,"mp3SizeKb":1236},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_019_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_019_5.mp3","webmSizeKb":560,"mp3SizeKb":929}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId4 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045009019 AS StartVerseId, 1045009031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId4 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId4 int = COALESCE(@ExistingPassageId4, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId4 AS PassageId, @ResourceId4 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId5 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 9:32-35' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId5 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId5 int = COALESCE(@ExistingResourceId5, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId5 AS ResourceId, 1 AS LanguageId, 'Acts 9:32-35' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1750 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_032_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_032_1.mp3","webmSizeKb":123,"mp3SizeKb":209},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_032_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_032_2.mp3","webmSizeKb":512,"mp3SizeKb":856},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_032_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_032_3.mp3","webmSizeKb":312,"mp3SizeKb":524},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_032_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_032_4.mp3","webmSizeKb":447,"mp3SizeKb":749},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_032_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_032_5.mp3","webmSizeKb":356,"mp3SizeKb":591}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId5 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045009032 AS StartVerseId, 1045009035 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId5 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId5 int = COALESCE(@ExistingPassageId5, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId5 AS PassageId, @ResourceId5 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId6 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 9:36-43' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId6 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId6 int = COALESCE(@ExistingResourceId6, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId6 AS ResourceId, 1 AS LanguageId, 'Acts 9:36-43' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2508 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_036_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_036_1.mp3","webmSizeKb":126,"mp3SizeKb":214},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_036_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_036_2.mp3","webmSizeKb":931,"mp3SizeKb":1546},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_036_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_036_3.mp3","webmSizeKb":440,"mp3SizeKb":732},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_036_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_036_4.mp3","webmSizeKb":638,"mp3SizeKb":1059},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_009_036_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_009_036_5.mp3","webmSizeKb":373,"mp3SizeKb":616}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId6 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045009036 AS StartVerseId, 1045009043 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId6 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId6 int = COALESCE(@ExistingPassageId6, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId6 AS PassageId, @ResourceId6 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId7 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 10:1-8' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId7 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId7 int = COALESCE(@ExistingResourceId7, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId7 AS ResourceId, 1 AS LanguageId, 'Acts 10:1-8' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2515 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_001_1.mp3","webmSizeKb":122,"mp3SizeKb":208},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_001_2.mp3","webmSizeKb":875,"mp3SizeKb":1461},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_001_3.mp3","webmSizeKb":379,"mp3SizeKb":637},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_001_4.mp3","webmSizeKb":569,"mp3SizeKb":960},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_001_5.mp3","webmSizeKb":570,"mp3SizeKb":952}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId7 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045010001 AS StartVerseId, 1045010008 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId7 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId7 int = COALESCE(@ExistingPassageId7, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId7 AS PassageId, @ResourceId7 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId8 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 10:9-23a' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId8 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId8 int = COALESCE(@ExistingResourceId8, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId8 AS ResourceId, 1 AS LanguageId, 'Acts 10:9-23a' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2629 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_009_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_009_1.mp3","webmSizeKb":120,"mp3SizeKb":203},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_009_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_009_2.mp3","webmSizeKb":1020,"mp3SizeKb":1701},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_009_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_009_3.mp3","webmSizeKb":481,"mp3SizeKb":805},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_009_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_009_4.mp3","webmSizeKb":617,"mp3SizeKb":1032},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_010_009_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_010_009_5.mp3","webmSizeKb":391,"mp3SizeKb":654}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId8 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045010009 AS StartVerseId, 1045010023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId8 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId8 int = COALESCE(@ExistingPassageId8, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId8 AS PassageId, @ResourceId8 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId9 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 11:1-18' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId9 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId9 int = COALESCE(@ExistingResourceId9, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId9 AS ResourceId, 1 AS LanguageId, 'Acts 11:1-18' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3249 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_001_1.mp3","webmSizeKb":93,"mp3SizeKb":162},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_001_2.mp3","webmSizeKb":885,"mp3SizeKb":1478},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_001_3.mp3","webmSizeKb":861,"mp3SizeKb":1455},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_001_4.mp3","webmSizeKb":810,"mp3SizeKb":1380},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_001_5.mp3","webmSizeKb":600,"mp3SizeKb":1016}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId9 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045011001 AS StartVerseId, 1045011018 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId9 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId9 int = COALESCE(@ExistingPassageId9, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId9 AS PassageId, @ResourceId9 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId10 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 11:19-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId10 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId10 int = COALESCE(@ExistingResourceId10, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId10 AS ResourceId, 1 AS LanguageId, 'Acts 11:19-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3735 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_019_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_019_1.mp3","webmSizeKb":129,"mp3SizeKb":217},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_019_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_019_2.mp3","webmSizeKb":1007,"mp3SizeKb":1670},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_019_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_019_3.mp3","webmSizeKb":929,"mp3SizeKb":1546},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_019_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_019_4.mp3","webmSizeKb":852,"mp3SizeKb":1428},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_019_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_019_5.mp3","webmSizeKb":818,"mp3SizeKb":1384}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId10 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045011019 AS StartVerseId, 1045011026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId10 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId10 int = COALESCE(@ExistingPassageId10, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId10 AS PassageId, @ResourceId10 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId11 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 11:27-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId11 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId11 int = COALESCE(@ExistingResourceId11, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId11 AS ResourceId, 1 AS LanguageId, 'Acts 11:27-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2720 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_027_1.mp3","webmSizeKb":123,"mp3SizeKb":217},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_027_2.mp3","webmSizeKb":850,"mp3SizeKb":1460},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_027_3.mp3","webmSizeKb":653,"mp3SizeKb":1127},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_027_4.mp3","webmSizeKb":595,"mp3SizeKb":1035},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_011_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_011_027_5.mp3","webmSizeKb":499,"mp3SizeKb":858}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId11 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045011027 AS StartVerseId, 1045011030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId11 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId11 int = COALESCE(@ExistingPassageId11, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId11 AS PassageId, @ResourceId11 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId12 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 12:1-5' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId12 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId12 int = COALESCE(@ExistingResourceId12, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId12 AS ResourceId, 1 AS LanguageId, 'Acts 12:1-5' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2616 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_001_1.mp3","webmSizeKb":122,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_001_2.mp3","webmSizeKb":994,"mp3SizeKb":1708},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_001_3.mp3","webmSizeKb":524,"mp3SizeKb":872},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_001_4.mp3","webmSizeKb":592,"mp3SizeKb":1043},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_001_5.mp3","webmSizeKb":384,"mp3SizeKb":648}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId12 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045012001 AS StartVerseId, 1045012005 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId12 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId12 int = COALESCE(@ExistingPassageId12, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId12 AS PassageId, @ResourceId12 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId13 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 12:6-19' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId13 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId13 int = COALESCE(@ExistingResourceId13, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId13 AS ResourceId, 1 AS LanguageId, 'Acts 12:6-19' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4892 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_006_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_006_1.mp3","webmSizeKb":115,"mp3SizeKb":204},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_006_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_006_2.mp3","webmSizeKb":1445,"mp3SizeKb":2471},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_006_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_006_3.mp3","webmSizeKb":1170,"mp3SizeKb":1966},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_006_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_006_4.mp3","webmSizeKb":1116,"mp3SizeKb":1883},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_006_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_006_5.mp3","webmSizeKb":1046,"mp3SizeKb":1746}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId13 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045012006 AS StartVerseId, 1045012019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId13 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId13 int = COALESCE(@ExistingPassageId13, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId13 AS PassageId, @ResourceId13 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId14 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 12:20-24' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId14 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId14 int = COALESCE(@ExistingResourceId14, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId14 AS ResourceId, 1 AS LanguageId, 'Acts 12:20-24' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3467 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_020_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_020_1.mp3","webmSizeKb":117,"mp3SizeKb":199},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_020_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_020_2.mp3","webmSizeKb":1193,"mp3SizeKb":1978},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_020_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_020_3.mp3","webmSizeKb":812,"mp3SizeKb":1388},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_020_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_020_4.mp3","webmSizeKb":667,"mp3SizeKb":1150},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_020_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_020_5.mp3","webmSizeKb":678,"mp3SizeKb":1157}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId14 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045012020 AS StartVerseId, 1045012024 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId14 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId14 int = COALESCE(@ExistingPassageId14, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId14 AS PassageId, @ResourceId14 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId15 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 12:25-13:3' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId15 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId15 int = COALESCE(@ExistingResourceId15, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId15 AS ResourceId, 1 AS LanguageId, 'Acts 12:25-13:3' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3375 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_025_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_025_1.mp3","webmSizeKb":120,"mp3SizeKb":202},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_025_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_025_2.mp3","webmSizeKb":1365,"mp3SizeKb":2261},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_025_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_025_3.mp3","webmSizeKb":585,"mp3SizeKb":978},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_025_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_025_4.mp3","webmSizeKb":618,"mp3SizeKb":1041},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_012_025_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_012_025_5.mp3","webmSizeKb":687,"mp3SizeKb":1150}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId15 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045012025 AS StartVerseId, 1045013003 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId15 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId15 int = COALESCE(@ExistingPassageId15, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId15 AS PassageId, @ResourceId15 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId16 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 13:4-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId16 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId16 int = COALESCE(@ExistingResourceId16, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId16 AS ResourceId, 1 AS LanguageId, 'Acts 13:4-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4504 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_004_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_004_1.mp3","webmSizeKb":124,"mp3SizeKb":210},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_004_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_004_2.mp3","webmSizeKb":1508,"mp3SizeKb":2497},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_004_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_004_3.mp3","webmSizeKb":1152,"mp3SizeKb":1953},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_004_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_004_4.mp3","webmSizeKb":841,"mp3SizeKb":1443},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_004_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_004_5.mp3","webmSizeKb":879,"mp3SizeKb":1496}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId16 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045013004 AS StartVerseId, 1045013012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId16 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId16 int = COALESCE(@ExistingPassageId16, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId16 AS PassageId, @ResourceId16 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId17 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 13:13-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId17 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId17 int = COALESCE(@ExistingResourceId17, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId17 AS ResourceId, 1 AS LanguageId, 'Acts 13:13-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4342 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_013_1.mp3","webmSizeKb":123,"mp3SizeKb":211},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_013_2.mp3","webmSizeKb":1337,"mp3SizeKb":2225},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_013_3.mp3","webmSizeKb":1011,"mp3SizeKb":1733},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_013_4.mp3","webmSizeKb":961,"mp3SizeKb":1651},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_013_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_013_013_5.mp3","webmSizeKb":910,"mp3SizeKb":1545}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId17 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045013013 AS StartVerseId, 1045013022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId17 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId17 int = COALESCE(@ExistingPassageId17, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId17 AS PassageId, @ResourceId17 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId18 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 14:8-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId18 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId18 int = COALESCE(@ExistingResourceId18, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId18 AS ResourceId, 1 AS LanguageId, 'Acts 14:8-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2567 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_014_008_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_014_008_1.mp3","webmSizeKb":128,"mp3SizeKb":230},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_014_008_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_014_008_2.mp3","webmSizeKb":544,"mp3SizeKb":944},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_014_008_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_014_008_3.mp3","webmSizeKb":692,"mp3SizeKb":1198},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_014_008_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_014_008_4.mp3","webmSizeKb":672,"mp3SizeKb":1182},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_014_008_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_014_008_5.mp3","webmSizeKb":531,"mp3SizeKb":923}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId18 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045014008 AS StartVerseId, 1045014020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId18 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId18 int = COALESCE(@ExistingPassageId18, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId18 AS PassageId, @ResourceId18 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId19 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 17:1-9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId19 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId19 int = COALESCE(@ExistingResourceId19, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId19 AS ResourceId, 1 AS LanguageId, 'Acts 17:1-9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4989 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_001_1.mp3","webmSizeKb":175,"mp3SizeKb":307},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_001_2.mp3","webmSizeKb":1633,"mp3SizeKb":2779},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_001_3.mp3","webmSizeKb":1423,"mp3SizeKb":2431},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_001_4.mp3","webmSizeKb":1122,"mp3SizeKb":1938},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_001_5.mp3","webmSizeKb":636,"mp3SizeKb":1083}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId19 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045017001 AS StartVerseId, 1045017009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId19 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId19 int = COALESCE(@ExistingPassageId19, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId19 AS PassageId, @ResourceId19 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId20 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 17:10-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId20 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId20 int = COALESCE(@ExistingResourceId20, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId20 AS ResourceId, 1 AS LanguageId, 'Acts 17:10-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3943 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_010_1.mp3","webmSizeKb":164,"mp3SizeKb":283},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_010_2.mp3","webmSizeKb":993,"mp3SizeKb":1670},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_010_3.mp3","webmSizeKb":1254,"mp3SizeKb":2113},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_010_4.mp3","webmSizeKb":965,"mp3SizeKb":1633},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_010_5.mp3","webmSizeKb":567,"mp3SizeKb":950}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId20 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045017010 AS StartVerseId, 1045017015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId20 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId20 int = COALESCE(@ExistingPassageId20, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId20 AS PassageId, @ResourceId20 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId21 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 17:16-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId21 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId21 int = COALESCE(@ExistingResourceId21, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId21 AS ResourceId, 1 AS LanguageId, 'Acts 17:16-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3482 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_016_1.mp3","webmSizeKb":159,"mp3SizeKb":276},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_016_2.mp3","webmSizeKb":1096,"mp3SizeKb":1856},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_016_3.mp3","webmSizeKb":1013,"mp3SizeKb":1723},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_016_4.mp3","webmSizeKb":637,"mp3SizeKb":1086},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_016_5.mp3","webmSizeKb":577,"mp3SizeKb":978}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId21 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045017016 AS StartVerseId, 1045017021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId21 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId21 int = COALESCE(@ExistingPassageId21, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId21 AS PassageId, @ResourceId21 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId22 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 17:22-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId22 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId22 int = COALESCE(@ExistingResourceId22, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId22 AS ResourceId, 1 AS LanguageId, 'Acts 17:22-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4072 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_022_1.mp3","webmSizeKb":172,"mp3SizeKb":299},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_022_2.mp3","webmSizeKb":1132,"mp3SizeKb":1914},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_022_3.mp3","webmSizeKb":816,"mp3SizeKb":1386},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_022_4.mp3","webmSizeKb":1068,"mp3SizeKb":1821},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_017_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_017_022_5.mp3","webmSizeKb":884,"mp3SizeKb":1499}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId22 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045017022 AS StartVerseId, 1045017034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId22 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId22 int = COALESCE(@ExistingPassageId22, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId22 AS PassageId, @ResourceId22 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId23 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 18:18-23' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId23 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId23 int = COALESCE(@ExistingResourceId23, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId23 AS ResourceId, 1 AS LanguageId, 'Acts 18:18-23' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3496 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_018_1.mp3","webmSizeKb":157,"mp3SizeKb":268},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_018_2.mp3","webmSizeKb":980,"mp3SizeKb":1654},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_018_3.mp3","webmSizeKb":1324,"mp3SizeKb":2232},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_018_4.mp3","webmSizeKb":777,"mp3SizeKb":1319},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_018_5.mp3","webmSizeKb":258,"mp3SizeKb":443}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId23 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045018018 AS StartVerseId, 1045018023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId23 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId23 int = COALESCE(@ExistingPassageId23, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId23 AS PassageId, @ResourceId23 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId24 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 18:24-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId24 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId24 int = COALESCE(@ExistingResourceId24, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId24 AS ResourceId, 1 AS LanguageId, 'Acts 18:24-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3350 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_024_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_024_1.mp3","webmSizeKb":145,"mp3SizeKb":250},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_024_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_024_2.mp3","webmSizeKb":1031,"mp3SizeKb":1739},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_024_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_024_3.mp3","webmSizeKb":964,"mp3SizeKb":1612},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_024_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_024_4.mp3","webmSizeKb":856,"mp3SizeKb":1444},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_018_024_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_018_024_5.mp3","webmSizeKb":354,"mp3SizeKb":599}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId24 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045018024 AS StartVerseId, 1045018028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId24 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId24 int = COALESCE(@ExistingPassageId24, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId24 AS PassageId, @ResourceId24 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId25 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 19:1-7' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId25 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId25 int = COALESCE(@ExistingResourceId25, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId25 AS ResourceId, 1 AS LanguageId, 'Acts 19:1-7' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2404 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_001_1.mp3","webmSizeKb":96,"mp3SizeKb":173},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_001_2.mp3","webmSizeKb":738,"mp3SizeKb":1279},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_001_3.mp3","webmSizeKb":505,"mp3SizeKb":874},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_001_4.mp3","webmSizeKb":563,"mp3SizeKb":980},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_001_5.mp3","webmSizeKb":502,"mp3SizeKb":871}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId25 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045019001 AS StartVerseId, 1045019007 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId25 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId25 int = COALESCE(@ExistingPassageId25, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId25 AS PassageId, @ResourceId25 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId26 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 19:8-10' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId26 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId26 int = COALESCE(@ExistingResourceId26, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId26 AS ResourceId, 1 AS LanguageId, 'Acts 19:8-10' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2005 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_008_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_008_1.mp3","webmSizeKb":81,"mp3SizeKb":142},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_008_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_008_2.mp3","webmSizeKb":455,"mp3SizeKb":797},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_008_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_008_3.mp3","webmSizeKb":574,"mp3SizeKb":1005},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_008_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_008_4.mp3","webmSizeKb":587,"mp3SizeKb":1027},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_008_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_008_5.mp3","webmSizeKb":308,"mp3SizeKb":535}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId26 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045019008 AS StartVerseId, 1045019010 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId26 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId26 int = COALESCE(@ExistingPassageId26, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId26 AS PassageId, @ResourceId26 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId27 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 19:11-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId27 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId27 int = COALESCE(@ExistingResourceId27, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId27 AS ResourceId, 1 AS LanguageId, 'Acts 19:11-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2864 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_011_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_011_1.mp3","webmSizeKb":83,"mp3SizeKb":145},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_011_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_011_2.mp3","webmSizeKb":729,"mp3SizeKb":1318},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_011_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_011_3.mp3","webmSizeKb":611,"mp3SizeKb":1041},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_011_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_011_4.mp3","webmSizeKb":1023,"mp3SizeKb":1847},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_011_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_011_5.mp3","webmSizeKb":418,"mp3SizeKb":717}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId27 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045019011 AS StartVerseId, 1045019020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId27 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId27 int = COALESCE(@ExistingPassageId27, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId27 AS PassageId, @ResourceId27 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId28 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 19:21-41' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId28 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId28 int = COALESCE(@ExistingResourceId28, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId28 AS ResourceId, 1 AS LanguageId, 'Acts 19:21-41' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4140 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_021_1.mp3","webmSizeKb":89,"mp3SizeKb":157},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_021_2.mp3","webmSizeKb":1193,"mp3SizeKb":2096},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_021_3.mp3","webmSizeKb":920,"mp3SizeKb":1727},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_021_4.mp3","webmSizeKb":1367,"mp3SizeKb":2420},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_019_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_019_021_5.mp3","webmSizeKb":571,"mp3SizeKb":1032}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId28 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045019021 AS StartVerseId, 1045019041 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId28 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId28 int = COALESCE(@ExistingPassageId28, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId28 AS PassageId, @ResourceId28 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId29 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 20:1-6' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId29 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId29 int = COALESCE(@ExistingResourceId29, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId29 AS ResourceId, 1 AS LanguageId, 'Acts 20:1-6' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2829 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_001_1.mp3","webmSizeKb":90,"mp3SizeKb":160},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_001_2.mp3","webmSizeKb":1231,"mp3SizeKb":2111},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_001_3.mp3","webmSizeKb":685,"mp3SizeKb":1225},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_001_4.mp3","webmSizeKb":606,"mp3SizeKb":1043},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_001_5.mp3","webmSizeKb":217,"mp3SizeKb":377}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId29 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045020001 AS StartVerseId, 1045020006 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId29 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId29 int = COALESCE(@ExistingPassageId29, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId29 AS PassageId, @ResourceId29 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId30 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 20:7-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId30 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId30 int = COALESCE(@ExistingResourceId30, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId30 AS ResourceId, 1 AS LanguageId, 'Acts 20:7-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2907 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_007_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_007_1.mp3","webmSizeKb":87,"mp3SizeKb":157},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_007_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_007_2.mp3","webmSizeKb":878,"mp3SizeKb":1719},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_007_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_007_3.mp3","webmSizeKb":690,"mp3SizeKb":1307},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_007_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_007_4.mp3","webmSizeKb":936,"mp3SizeKb":1746},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_007_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_007_5.mp3","webmSizeKb":316,"mp3SizeKb":606}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId30 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045020007 AS StartVerseId, 1045020012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId30 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId30 int = COALESCE(@ExistingPassageId30, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId30 AS PassageId, @ResourceId30 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId31 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 20:13-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId31 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId31 int = COALESCE(@ExistingResourceId31, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId31 AS ResourceId, 1 AS LanguageId, 'Acts 20:13-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3144 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_013_1.mp3","webmSizeKb":76,"mp3SizeKb":144},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_013_2.mp3","webmSizeKb":1177,"mp3SizeKb":2087},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_013_3.mp3","webmSizeKb":959,"mp3SizeKb":1748},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_013_4.mp3","webmSizeKb":740,"mp3SizeKb":1351},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_013_5.mp3","webmSizeKb":192,"mp3SizeKb":379}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId31 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045020013 AS StartVerseId, 1045020017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId31 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId31 int = COALESCE(@ExistingPassageId31, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId31 AS PassageId, @ResourceId31 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId32 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 20:18-38' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId32 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId32 int = COALESCE(@ExistingResourceId32, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId32 AS ResourceId, 1 AS LanguageId, 'Acts 20:18-38' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4746 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_018_1.mp3","webmSizeKb":74,"mp3SizeKb":148},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_018_2.mp3","webmSizeKb":1581,"mp3SizeKb":2777},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_018_3.mp3","webmSizeKb":1132,"mp3SizeKb":2014},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_018_4.mp3","webmSizeKb":1314,"mp3SizeKb":2326},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_020_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_020_018_5.mp3","webmSizeKb":645,"mp3SizeKb":1156}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId32 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045020018 AS StartVerseId, 1045020038 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId32 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId32 int = COALESCE(@ExistingPassageId32, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId32 AS PassageId, @ResourceId32 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId33 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 21:1-9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId33 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId33 int = COALESCE(@ExistingResourceId33, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId33 AS ResourceId, 1 AS LanguageId, 'Acts 21:1-9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2397 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_001_1.mp3","webmSizeKb":136,"mp3SizeKb":238},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_001_2.mp3","webmSizeKb":707,"mp3SizeKb":1204},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_001_3.mp3","webmSizeKb":631,"mp3SizeKb":1086},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_001_4.mp3","webmSizeKb":513,"mp3SizeKb":888},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_001_5.mp3","webmSizeKb":410,"mp3SizeKb":707}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId33 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045021001 AS StartVerseId, 1045021009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId33 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId33 int = COALESCE(@ExistingPassageId33, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId33 AS PassageId, @ResourceId33 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId34 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 21:10-14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId34 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId34 int = COALESCE(@ExistingResourceId34, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId34 AS ResourceId, 1 AS LanguageId, 'Acts 21:10-14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2032 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_010_1.mp3","webmSizeKb":133,"mp3SizeKb":227},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_010_2.mp3","webmSizeKb":805,"mp3SizeKb":1367},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_010_3.mp3","webmSizeKb":380,"mp3SizeKb":649},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_010_4.mp3","webmSizeKb":420,"mp3SizeKb":715},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_010_5.mp3","webmSizeKb":294,"mp3SizeKb":500}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId34 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045021010 AS StartVerseId, 1045021014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId34 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId34 int = COALESCE(@ExistingPassageId34, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId34 AS PassageId, @ResourceId34 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId35 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 21:15-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId35 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId35 int = COALESCE(@ExistingResourceId35, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId35 AS ResourceId, 1 AS LanguageId, 'Acts 21:15-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2743 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_015_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_015_1.mp3","webmSizeKb":139,"mp3SizeKb":240},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_015_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_015_2.mp3","webmSizeKb":1071,"mp3SizeKb":1816},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_015_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_015_3.mp3","webmSizeKb":539,"mp3SizeKb":919},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_015_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_015_4.mp3","webmSizeKb":605,"mp3SizeKb":1031},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_015_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_015_5.mp3","webmSizeKb":389,"mp3SizeKb":659}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId35 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045021015 AS StartVerseId, 1045021026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId35 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId35 int = COALESCE(@ExistingPassageId35, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId35 AS PassageId, @ResourceId35 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId36 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Acts 21:27-36' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId36 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId36 int = COALESCE(@ExistingResourceId36, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId36 AS ResourceId, 1 AS LanguageId, 'Acts 21:27-36' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2511 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_027_1.mp3","webmSizeKb":133,"mp3SizeKb":228},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_027_2.mp3","webmSizeKb":677,"mp3SizeKb":1148},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_027_3.mp3","webmSizeKb":518,"mp3SizeKb":883},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_027_4.mp3","webmSizeKb":769,"mp3SizeKb":1320},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_45_021_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_45_021_027_5.mp3","webmSizeKb":414,"mp3SizeKb":707}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId36 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1045021027 AS StartVerseId, 1045021036 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId36 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId36 int = COALESCE(@ExistingPassageId36, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId36 AS PassageId, @ResourceId36 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId37 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 1:1-2:3' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId37 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId37 int = COALESCE(@ExistingResourceId37, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId37 AS ResourceId, 1 AS LanguageId, 'Genesis 1:1-2:3' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3671 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_001_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_001_001_1.mp3","webmSizeKb":141,"mp3SizeKb":238},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_001_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_001_001_2.mp3","webmSizeKb":141,"mp3SizeKb":238},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_001_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_001_001_3.mp3","webmSizeKb":968,"mp3SizeKb":1606},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_001_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_001_001_4.mp3","webmSizeKb":921,"mp3SizeKb":1526},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_001_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_001_001_5.mp3","webmSizeKb":1263,"mp3SizeKb":2088},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_001_001_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_001_001_6.mp3","webmSizeKb":237,"mp3SizeKb":394}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId37 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001001001 AS StartVerseId, 1001002003 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId37 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId37 int = COALESCE(@ExistingPassageId37, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId37 AS PassageId, @ResourceId37 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId38 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 8:20-9:17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId38 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId38 int = COALESCE(@ExistingResourceId38, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId38 AS ResourceId, 1 AS LanguageId, 'Genesis 8:20-9:17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4501 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_020_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_020_1.mp3","webmSizeKb":111,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_020_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_020_2.mp3","webmSizeKb":1207,"mp3SizeKb":2094},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_020_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_020_3.mp3","webmSizeKb":968,"mp3SizeKb":1680},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_020_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_020_4.mp3","webmSizeKb":1119,"mp3SizeKb":1973},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_020_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_020_5.mp3","webmSizeKb":1096,"mp3SizeKb":1893}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId38 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001008020 AS StartVerseId, 1001009017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId38 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId38 int = COALESCE(@ExistingPassageId38, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId38 AS PassageId, @ResourceId38 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId39 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 9:18-29' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId39 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId39 int = COALESCE(@ExistingResourceId39, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId39 AS ResourceId, 1 AS LanguageId, 'Genesis 9:18-29' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3349 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_009_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_009_018_1.mp3","webmSizeKb":101,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_009_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_009_018_2.mp3","webmSizeKb":896,"mp3SizeKb":1583},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_009_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_009_018_3.mp3","webmSizeKb":705,"mp3SizeKb":1257},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_009_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_009_018_4.mp3","webmSizeKb":945,"mp3SizeKb":1701},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_009_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_009_018_5.mp3","webmSizeKb":702,"mp3SizeKb":1236}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId39 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001009018 AS StartVerseId, 1001009029 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId39 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId39 int = COALESCE(@ExistingPassageId39, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId39 AS PassageId, @ResourceId39 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId40 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 10:1-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId40 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId40 int = COALESCE(@ExistingResourceId40, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId40 AS ResourceId, 1 AS LanguageId, 'Genesis 10:1-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2810 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_010_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_010_001_1.mp3","webmSizeKb":105,"mp3SizeKb":189},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_010_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_010_001_2.mp3","webmSizeKb":805,"mp3SizeKb":1420},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_010_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_010_001_3.mp3","webmSizeKb":733,"mp3SizeKb":1312},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_010_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_010_001_4.mp3","webmSizeKb":837,"mp3SizeKb":1497},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_010_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_010_001_5.mp3","webmSizeKb":330,"mp3SizeKb":576}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId40 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001010001 AS StartVerseId, 1001010032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId40 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId40 int = COALESCE(@ExistingPassageId40, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId40 AS PassageId, @ResourceId40 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId41 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 11:1-9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId41 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId41 int = COALESCE(@ExistingResourceId41, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId41 AS ResourceId, 1 AS LanguageId, 'Genesis 11:1-9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2897 as ContentSizeKb, '{"steps":[{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_001_2.mp3","webmSizeKb":817,"mp3SizeKb":1393},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_001_3.mp3","webmSizeKb":830,"mp3SizeKb":1403},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_001_4.mp3","webmSizeKb":747,"mp3SizeKb":1268},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_001_5.mp3","webmSizeKb":503,"mp3SizeKb":851}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId41 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001011001 AS StartVerseId, 1001011009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId41 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId41 int = COALESCE(@ExistingPassageId41, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId41 AS PassageId, @ResourceId41 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId42 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 11:10-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId42 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId42 int = COALESCE(@ExistingResourceId42, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId42 AS ResourceId, 1 AS LanguageId, 'Genesis 11:10-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2277 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_010_1.mp3","webmSizeKb":115,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_010_2.mp3","webmSizeKb":742,"mp3SizeKb":1265},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_010_3.mp3","webmSizeKb":705,"mp3SizeKb":1198},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_010_4.mp3","webmSizeKb":466,"mp3SizeKb":792},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_010_5.mp3","webmSizeKb":249,"mp3SizeKb":420}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId42 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001011010 AS StartVerseId, 1001011026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId42 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId42 int = COALESCE(@ExistingPassageId42, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId42 AS PassageId, @ResourceId42 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId43 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 11:27-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId43 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId43 int = COALESCE(@ExistingResourceId43, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId43 AS ResourceId, 1 AS LanguageId, 'Genesis 11:27-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2508 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_027_1.mp3","webmSizeKb":115,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_027_2.mp3","webmSizeKb":785,"mp3SizeKb":1345},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_027_3.mp3","webmSizeKb":557,"mp3SizeKb":954},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_027_4.mp3","webmSizeKb":524,"mp3SizeKb":898},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_011_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_011_027_5.mp3","webmSizeKb":527,"mp3SizeKb":897}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId43 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001011027 AS StartVerseId, 1001011032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId43 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId43 int = COALESCE(@ExistingPassageId43, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId43 AS PassageId, @ResourceId43 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId44 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 12:1-9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId44 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId44 int = COALESCE(@ExistingResourceId44, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId44 AS ResourceId, 1 AS LanguageId, 'Genesis 12:1-9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4123 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_001_1.mp3","webmSizeKb":110,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_001_2.mp3","webmSizeKb":1385,"mp3SizeKb":2367},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_001_3.mp3","webmSizeKb":1005,"mp3SizeKb":1723},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_001_4.mp3","webmSizeKb":844,"mp3SizeKb":1437},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_001_5.mp3","webmSizeKb":779,"mp3SizeKb":1317}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId44 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001012001 AS StartVerseId, 1001012009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId44 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId44 int = COALESCE(@ExistingPassageId44, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId44 AS PassageId, @ResourceId44 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId45 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 12:10-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId45 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId45 int = COALESCE(@ExistingResourceId45, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId45 AS ResourceId, 1 AS LanguageId, 'Genesis 12:10-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4029 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_010_1.mp3","webmSizeKb":123,"mp3SizeKb":214},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_010_2.mp3","webmSizeKb":1322,"mp3SizeKb":2236},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_010_3.mp3","webmSizeKb":1123,"mp3SizeKb":1893},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_010_4.mp3","webmSizeKb":842,"mp3SizeKb":1431},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_012_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_012_010_5.mp3","webmSizeKb":619,"mp3SizeKb":1041}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId45 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001012010 AS StartVerseId, 1001012020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId45 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId45 int = COALESCE(@ExistingPassageId45, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId45 AS PassageId, @ResourceId45 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId46 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 13:1-18' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId46 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId46 int = COALESCE(@ExistingResourceId46, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId46 AS ResourceId, 1 AS LanguageId, 'Genesis 13:1-18' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3842 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_013_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_013_001_1.mp3","webmSizeKb":105,"mp3SizeKb":182},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_013_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_013_001_2.mp3","webmSizeKb":1259,"mp3SizeKb":2124},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_013_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_013_001_3.mp3","webmSizeKb":1008,"mp3SizeKb":1706},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_013_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_013_001_4.mp3","webmSizeKb":813,"mp3SizeKb":1381},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_013_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_013_001_5.mp3","webmSizeKb":657,"mp3SizeKb":1111}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId46 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001013001 AS StartVerseId, 1001013018 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId46 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId46 int = COALESCE(@ExistingPassageId46, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId46 AS PassageId, @ResourceId46 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId47 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 2:4-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId47 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId47 int = COALESCE(@ExistingResourceId47, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId47 AS ResourceId, 1 AS LanguageId, 'Genesis 2:4-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5103 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_002_004_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_002_004_1.mp3","webmSizeKb":125,"mp3SizeKb":223},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_002_004_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_002_004_2.mp3","webmSizeKb":1301,"mp3SizeKb":2262},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_002_004_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_002_004_3.mp3","webmSizeKb":963,"mp3SizeKb":1683},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_002_004_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_002_004_4.mp3","webmSizeKb":972,"mp3SizeKb":1749},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_002_004_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_002_004_5.mp3","webmSizeKb":1742,"mp3SizeKb":3042}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId47 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001002004 AS StartVerseId, 1001002025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId47 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId47 int = COALESCE(@ExistingPassageId47, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId47 AS PassageId, @ResourceId47 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId48 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 23:1-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId48 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId48 int = COALESCE(@ExistingResourceId48, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId48 AS ResourceId, 1 AS LanguageId, 'Genesis 23:1-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4997 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_023_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_023_001_1.mp3","webmSizeKb":160,"mp3SizeKb":279},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_023_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_023_001_2.mp3","webmSizeKb":1207,"mp3SizeKb":2068},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_023_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_023_001_3.mp3","webmSizeKb":1329,"mp3SizeKb":2297},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_023_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_023_001_4.mp3","webmSizeKb":1571,"mp3SizeKb":2767},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_023_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_023_001_5.mp3","webmSizeKb":730,"mp3SizeKb":1263}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId48 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001023001 AS StartVerseId, 1001023020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId48 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId48 int = COALESCE(@ExistingPassageId48, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId48 AS PassageId, @ResourceId48 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId49 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 24:1-14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId49 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId49 int = COALESCE(@ExistingResourceId49, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId49 AS ResourceId, 1 AS LanguageId, 'Genesis 24:1-14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4047 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_001_1.mp3","webmSizeKb":166,"mp3SizeKb":286},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_001_2.mp3","webmSizeKb":1369,"mp3SizeKb":2328},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_001_3.mp3","webmSizeKb":938,"mp3SizeKb":1597},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_001_4.mp3","webmSizeKb":873,"mp3SizeKb":1504},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_001_5.mp3","webmSizeKb":701,"mp3SizeKb":1193}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId49 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001024001 AS StartVerseId, 1001024014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId49 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId49 int = COALESCE(@ExistingPassageId49, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId49 AS PassageId, @ResourceId49 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId50 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 24:50-61' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId50 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId50 int = COALESCE(@ExistingResourceId50, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId50 AS ResourceId, 1 AS LanguageId, 'Genesis 24:50-61' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3341 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_050_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_050_1.mp3","webmSizeKb":127,"mp3SizeKb":216},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_050_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_050_2.mp3","webmSizeKb":968,"mp3SizeKb":1653},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_050_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_050_3.mp3","webmSizeKb":949,"mp3SizeKb":1620},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_050_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_050_4.mp3","webmSizeKb":918,"mp3SizeKb":1556},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_050_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_050_5.mp3","webmSizeKb":379,"mp3SizeKb":637}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId50 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001024050 AS StartVerseId, 1001024061 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId50 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId50 int = COALESCE(@ExistingPassageId50, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId50 AS PassageId, @ResourceId50 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId51 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 24:62-67' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId51 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId51 int = COALESCE(@ExistingResourceId51, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId51 AS ResourceId, 1 AS LanguageId, 'Genesis 24:62-67' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1991 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_062_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_062_1.mp3","webmSizeKb":122,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_062_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_062_2.mp3","webmSizeKb":658,"mp3SizeKb":1109},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_062_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_062_3.mp3","webmSizeKb":410,"mp3SizeKb":701},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_062_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_062_4.mp3","webmSizeKb":500,"mp3SizeKb":851},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_024_062_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_024_062_5.mp3","webmSizeKb":301,"mp3SizeKb":503}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId51 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001024062 AS StartVerseId, 1001024067 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId51 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId51 int = COALESCE(@ExistingPassageId51, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId51 AS PassageId, @ResourceId51 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId52 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 25:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId52 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId52 int = COALESCE(@ExistingResourceId52, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId52 AS ResourceId, 1 AS LanguageId, 'Genesis 25:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2669 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_001_1.mp3","webmSizeKb":98,"mp3SizeKb":168},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_001_2.mp3","webmSizeKb":993,"mp3SizeKb":1681},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_001_3.mp3","webmSizeKb":633,"mp3SizeKb":1065},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_001_4.mp3","webmSizeKb":508,"mp3SizeKb":861},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_001_5.mp3","webmSizeKb":437,"mp3SizeKb":734}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId52 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001025001 AS StartVerseId, 1001025011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId52 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId52 int = COALESCE(@ExistingPassageId52, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId52 AS PassageId, @ResourceId52 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId53 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 25:12-18' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId53 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId53 int = COALESCE(@ExistingResourceId53, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId53 AS ResourceId, 1 AS LanguageId, 'Genesis 25:12-18' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2207 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_012_1.mp3","webmSizeKb":107,"mp3SizeKb":183},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_012_2.mp3","webmSizeKb":714,"mp3SizeKb":1197},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_012_3.mp3","webmSizeKb":346,"mp3SizeKb":585},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_012_4.mp3","webmSizeKb":421,"mp3SizeKb":707},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_012_5.mp3","webmSizeKb":619,"mp3SizeKb":1039}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId53 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001025012 AS StartVerseId, 1001025018 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId53 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId53 int = COALESCE(@ExistingPassageId53, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId53 AS PassageId, @ResourceId53 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId54 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 25:19-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId54 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId54 int = COALESCE(@ExistingResourceId54, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId54 AS ResourceId, 1 AS LanguageId, 'Genesis 25:19-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4390 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_019_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_019_1.mp3","webmSizeKb":112,"mp3SizeKb":191},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_019_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_019_2.mp3","webmSizeKb":1185,"mp3SizeKb":1993},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_019_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_019_3.mp3","webmSizeKb":1150,"mp3SizeKb":1932},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_019_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_019_4.mp3","webmSizeKb":893,"mp3SizeKb":1504},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_025_019_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_025_019_5.mp3","webmSizeKb":1050,"mp3SizeKb":1756}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId54 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001025019 AS StartVerseId, 1001025034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId54 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId54 int = COALESCE(@ExistingPassageId54, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId54 AS PassageId, @ResourceId54 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId55 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 26:1-33' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId55 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId55 int = COALESCE(@ExistingResourceId55, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId55 AS ResourceId, 1 AS LanguageId, 'Genesis 26:1-33' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5008 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_001_1.mp3","webmSizeKb":117,"mp3SizeKb":215},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_001_2.mp3","webmSizeKb":1870,"mp3SizeKb":3281},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_001_3.mp3","webmSizeKb":1052,"mp3SizeKb":1848},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_001_4.mp3","webmSizeKb":1077,"mp3SizeKb":1920},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_001_5.mp3","webmSizeKb":892,"mp3SizeKb":1543}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId55 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001026001 AS StartVerseId, 1001026033 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId55 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId55 int = COALESCE(@ExistingPassageId55, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId55 AS PassageId, @ResourceId55 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId56 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 26:34-27:17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId56 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId56 int = COALESCE(@ExistingResourceId56, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId56 AS ResourceId, 1 AS LanguageId, 'Genesis 26:34-27:17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3020 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_034_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_034_1.mp3","webmSizeKb":112,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_034_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_034_2.mp3","webmSizeKb":1020,"mp3SizeKb":1784},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_034_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_034_3.mp3","webmSizeKb":639,"mp3SizeKb":1126},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_034_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_034_4.mp3","webmSizeKb":887,"mp3SizeKb":1608},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_026_034_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_026_034_5.mp3","webmSizeKb":362,"mp3SizeKb":638}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId56 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001026034 AS StartVerseId, 1001027017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId56 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId56 int = COALESCE(@ExistingPassageId56, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId56 AS PassageId, @ResourceId56 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId57 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 27:18-29' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId57 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId57 int = COALESCE(@ExistingResourceId57, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId57 AS ResourceId, 1 AS LanguageId, 'Genesis 27:18-29' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2843 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_018_1.mp3","webmSizeKb":102,"mp3SizeKb":186},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_018_2.mp3","webmSizeKb":810,"mp3SizeKb":1449},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_018_3.mp3","webmSizeKb":802,"mp3SizeKb":1427},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_018_4.mp3","webmSizeKb":762,"mp3SizeKb":1379},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_018_5.mp3","webmSizeKb":367,"mp3SizeKb":643}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId57 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001027018 AS StartVerseId, 1001027029 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId57 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId57 int = COALESCE(@ExistingPassageId57, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId57 AS PassageId, @ResourceId57 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId58 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 27:30-40' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId58 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId58 int = COALESCE(@ExistingResourceId58, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId58 AS ResourceId, 1 AS LanguageId, 'Genesis 27:30-40' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3207 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_030_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_030_1.mp3","webmSizeKb":110,"mp3SizeKb":194},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_030_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_030_2.mp3","webmSizeKb":1022,"mp3SizeKb":1809},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_030_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_030_3.mp3","webmSizeKb":679,"mp3SizeKb":1197},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_030_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_030_4.mp3","webmSizeKb":990,"mp3SizeKb":1800},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_030_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_030_5.mp3","webmSizeKb":406,"mp3SizeKb":727}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId58 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001027030 AS StartVerseId, 1001027040 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId58 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId58 int = COALESCE(@ExistingPassageId58, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId58 AS PassageId, @ResourceId58 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId59 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 27:41-28:9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId59 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId59 int = COALESCE(@ExistingResourceId59, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId59 AS ResourceId, 1 AS LanguageId, 'Genesis 27:41-28:9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4006 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_041_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_041_1.mp3","webmSizeKb":113,"mp3SizeKb":206},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_041_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_041_2.mp3","webmSizeKb":1351,"mp3SizeKb":2398},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_041_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_041_3.mp3","webmSizeKb":942,"mp3SizeKb":1670},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_041_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_041_4.mp3","webmSizeKb":1002,"mp3SizeKb":1798},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_027_041_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_027_041_5.mp3","webmSizeKb":598,"mp3SizeKb":1047}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId59 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001027041 AS StartVerseId, 1001028009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId59 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId59 int = COALESCE(@ExistingPassageId59, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId59 AS PassageId, @ResourceId59 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId60 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 28:10-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId60 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId60 int = COALESCE(@ExistingResourceId60, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId60 AS ResourceId, 1 AS LanguageId, 'Genesis 28:10-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3613 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_028_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_028_010_1.mp3","webmSizeKb":120,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_028_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_028_010_2.mp3","webmSizeKb":1260,"mp3SizeKb":2211},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_028_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_028_010_3.mp3","webmSizeKb":772,"mp3SizeKb":1380},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_028_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_028_010_4.mp3","webmSizeKb":889,"mp3SizeKb":1614},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_028_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_028_010_5.mp3","webmSizeKb":572,"mp3SizeKb":1007}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId60 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001028010 AS StartVerseId, 1001028022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId60 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId60 int = COALESCE(@ExistingPassageId60, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId60 AS PassageId, @ResourceId60 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId61 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 29:1-14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId61 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId61 int = COALESCE(@ExistingResourceId61, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId61 AS ResourceId, 1 AS LanguageId, 'Genesis 29:1-14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3442 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_001_1.mp3","webmSizeKb":113,"mp3SizeKb":218},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_001_2.mp3","webmSizeKb":1084,"mp3SizeKb":1929},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_001_3.mp3","webmSizeKb":891,"mp3SizeKb":1585},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_001_4.mp3","webmSizeKb":756,"mp3SizeKb":1356},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_001_5.mp3","webmSizeKb":598,"mp3SizeKb":1063}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId61 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001029001 AS StartVerseId, 1001029014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId61 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId61 int = COALESCE(@ExistingPassageId61, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId61 AS PassageId, @ResourceId61 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId62 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 29:15-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId62 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId62 int = COALESCE(@ExistingResourceId62, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId62 AS ResourceId, 1 AS LanguageId, 'Genesis 29:15-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4046 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_015_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_015_1.mp3","webmSizeKb":124,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_015_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_015_2.mp3","webmSizeKb":1229,"mp3SizeKb":2185},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_015_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_015_3.mp3","webmSizeKb":1040,"mp3SizeKb":1848},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_015_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_015_4.mp3","webmSizeKb":1138,"mp3SizeKb":2071},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_015_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_015_5.mp3","webmSizeKb":515,"mp3SizeKb":904}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId62 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001029015 AS StartVerseId, 1001029030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId62 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId62 int = COALESCE(@ExistingPassageId62, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId62 AS PassageId, @ResourceId62 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId63 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 29:31-30:24' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId63 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId63 int = COALESCE(@ExistingResourceId63, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId63 AS ResourceId, 1 AS LanguageId, 'Genesis 29:31-30:24' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3870 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_031_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_031_1.mp3","webmSizeKb":125,"mp3SizeKb":226},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_031_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_031_2.mp3","webmSizeKb":1318,"mp3SizeKb":2319},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_031_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_031_3.mp3","webmSizeKb":807,"mp3SizeKb":1438},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_031_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_031_4.mp3","webmSizeKb":1072,"mp3SizeKb":1916},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_029_031_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_029_031_5.mp3","webmSizeKb":548,"mp3SizeKb":956}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId63 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001029031 AS StartVerseId, 1001030024 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId63 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId63 int = COALESCE(@ExistingPassageId63, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId63 AS PassageId, @ResourceId63 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId64 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 30:25-43' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId64 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId64 int = COALESCE(@ExistingResourceId64, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId64 AS ResourceId, 1 AS LanguageId, 'Genesis 30:25-43' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4065 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_030_025_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_030_025_1.mp3","webmSizeKb":132,"mp3SizeKb":238},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_030_025_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_030_025_2.mp3","webmSizeKb":1399,"mp3SizeKb":2430},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_030_025_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_030_025_3.mp3","webmSizeKb":938,"mp3SizeKb":1628},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_030_025_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_030_025_4.mp3","webmSizeKb":1119,"mp3SizeKb":1956},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_030_025_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_030_025_5.mp3","webmSizeKb":477,"mp3SizeKb":818}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId64 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001030025 AS StartVerseId, 1001030043 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId64 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId64 int = COALESCE(@ExistingPassageId64, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId64 AS PassageId, @ResourceId64 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId65 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 31:1-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId65 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId65 int = COALESCE(@ExistingResourceId65, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId65 AS ResourceId, 1 AS LanguageId, 'Genesis 31:1-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3837 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_001_1.mp3","webmSizeKb":123,"mp3SizeKb":221},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_001_2.mp3","webmSizeKb":1245,"mp3SizeKb":2142},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_001_3.mp3","webmSizeKb":920,"mp3SizeKb":1590},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_001_4.mp3","webmSizeKb":1086,"mp3SizeKb":1899},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_001_5.mp3","webmSizeKb":463,"mp3SizeKb":798}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId65 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001031001 AS StartVerseId, 1001031021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId65 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId65 int = COALESCE(@ExistingPassageId65, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId65 AS PassageId, @ResourceId65 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId66 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 31:22-35' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId66 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId66 int = COALESCE(@ExistingResourceId66, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId66 AS ResourceId, 1 AS LanguageId, 'Genesis 31:22-35' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3931 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_022_1.mp3","webmSizeKb":119,"mp3SizeKb":208},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_022_2.mp3","webmSizeKb":1161,"mp3SizeKb":1994},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_022_3.mp3","webmSizeKb":941,"mp3SizeKb":1617},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_022_4.mp3","webmSizeKb":1082,"mp3SizeKb":1901},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_022_5.mp3","webmSizeKb":628,"mp3SizeKb":1078}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId66 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001031022 AS StartVerseId, 1001031035 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId66 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId66 int = COALESCE(@ExistingPassageId66, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId66 AS PassageId, @ResourceId66 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId67 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 31:36-55' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId67 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId67 int = COALESCE(@ExistingResourceId67, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId67 AS ResourceId, 1 AS LanguageId, 'Genesis 31:36-55' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4469 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_036_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_036_1.mp3","webmSizeKb":95,"mp3SizeKb":165},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_036_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_036_2.mp3","webmSizeKb":1256,"mp3SizeKb":2136},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_036_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_036_3.mp3","webmSizeKb":1081,"mp3SizeKb":1858},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_036_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_036_4.mp3","webmSizeKb":1051,"mp3SizeKb":1847},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_031_036_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_031_036_5.mp3","webmSizeKb":986,"mp3SizeKb":1713}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId67 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001031036 AS StartVerseId, 1001031055 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId67 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId67 int = COALESCE(@ExistingPassageId67, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId67 AS PassageId, @ResourceId67 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId68 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 32:1-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId68 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId68 int = COALESCE(@ExistingResourceId68, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId68 AS ResourceId, 1 AS LanguageId, 'Genesis 32:1-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3498 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_001_1.mp3","webmSizeKb":118,"mp3SizeKb":205},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_001_2.mp3","webmSizeKb":1247,"mp3SizeKb":2109},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_001_3.mp3","webmSizeKb":789,"mp3SizeKb":1341},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_001_4.mp3","webmSizeKb":930,"mp3SizeKb":1594},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_001_5.mp3","webmSizeKb":414,"mp3SizeKb":710}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId68 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001032001 AS StartVerseId, 1001032021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId68 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId68 int = COALESCE(@ExistingPassageId68, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId68 AS PassageId, @ResourceId68 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId69 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 32:22-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId69 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId69 int = COALESCE(@ExistingResourceId69, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId69 AS ResourceId, 1 AS LanguageId, 'Genesis 32:22-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3189 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_022_1.mp3","webmSizeKb":126,"mp3SizeKb":218},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_022_2.mp3","webmSizeKb":1149,"mp3SizeKb":1956},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_022_3.mp3","webmSizeKb":824,"mp3SizeKb":1408},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_022_4.mp3","webmSizeKb":879,"mp3SizeKb":1517},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_032_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_032_022_5.mp3","webmSizeKb":211,"mp3SizeKb":361}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId69 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001032022 AS StartVerseId, 1001032032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId69 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId69 int = COALESCE(@ExistingPassageId69, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId69 AS PassageId, @ResourceId69 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId70 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 33:1-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId70 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId70 int = COALESCE(@ExistingResourceId70, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId70 AS ResourceId, 1 AS LanguageId, 'Genesis 33:1-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3767 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_033_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_033_001_1.mp3","webmSizeKb":119,"mp3SizeKb":210},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_033_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_033_001_2.mp3","webmSizeKb":1181,"mp3SizeKb":2014},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_033_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_033_001_3.mp3","webmSizeKb":875,"mp3SizeKb":1497},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_033_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_033_001_4.mp3","webmSizeKb":971,"mp3SizeKb":1664},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_033_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_033_001_5.mp3","webmSizeKb":621,"mp3SizeKb":1057}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId70 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001033001 AS StartVerseId, 1001033020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId70 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId70 int = COALESCE(@ExistingPassageId70, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId70 AS PassageId, @ResourceId70 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId71 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 5:1-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId71 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId71 int = COALESCE(@ExistingResourceId71, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId71 AS ResourceId, 1 AS LanguageId, 'Genesis 5:1-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2476 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_005_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_005_001_1.mp3","webmSizeKb":125,"mp3SizeKb":208},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_005_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_005_001_2.mp3","webmSizeKb":479,"mp3SizeKb":788},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_005_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_005_001_3.mp3","webmSizeKb":558,"mp3SizeKb":926},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_005_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_005_001_4.mp3","webmSizeKb":749,"mp3SizeKb":1245},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_005_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_005_001_5.mp3","webmSizeKb":326,"mp3SizeKb":538},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_005_001_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_005_001_6.mp3","webmSizeKb":239,"mp3SizeKb":395}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId71 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001005001 AS StartVerseId, 1001005032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId71 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId71 int = COALESCE(@ExistingPassageId71, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId71 AS PassageId, @ResourceId71 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId72 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 34:1-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId72 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId72 int = COALESCE(@ExistingResourceId72, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId72 AS ResourceId, 1 AS LanguageId, 'Genesis 34:1-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3265 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_001_1.mp3","webmSizeKb":107,"mp3SizeKb":187},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_001_2.mp3","webmSizeKb":1203,"mp3SizeKb":2047},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_001_3.mp3","webmSizeKb":682,"mp3SizeKb":1173},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_001_4.mp3","webmSizeKb":730,"mp3SizeKb":1274},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_001_5.mp3","webmSizeKb":543,"mp3SizeKb":928}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId72 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001034001 AS StartVerseId, 1001034017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId72 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId72 int = COALESCE(@ExistingPassageId72, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId72 AS PassageId, @ResourceId72 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId73 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 34:18-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId73 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId73 int = COALESCE(@ExistingResourceId73, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId73 AS ResourceId, 1 AS LanguageId, 'Genesis 34:18-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3193 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_018_1.mp3","webmSizeKb":114,"mp3SizeKb":203},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_018_2.mp3","webmSizeKb":869,"mp3SizeKb":1495},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_018_3.mp3","webmSizeKb":801,"mp3SizeKb":1390},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_018_4.mp3","webmSizeKb":848,"mp3SizeKb":1499},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_034_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_034_018_5.mp3","webmSizeKb":561,"mp3SizeKb":979}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId73 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001034018 AS StartVerseId, 1001034031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId73 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId73 int = COALESCE(@ExistingPassageId73, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId73 AS PassageId, @ResourceId73 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId74 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 35:1-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId74 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId74 int = COALESCE(@ExistingResourceId74, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId74 AS ResourceId, 1 AS LanguageId, 'Genesis 35:1-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3887 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_001_1.mp3","webmSizeKb":108,"mp3SizeKb":195},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_001_2.mp3","webmSizeKb":1296,"mp3SizeKb":2231},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_001_3.mp3","webmSizeKb":1020,"mp3SizeKb":1768},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_001_4.mp3","webmSizeKb":956,"mp3SizeKb":1684},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_001_5.mp3","webmSizeKb":507,"mp3SizeKb":875}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId74 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001035001 AS StartVerseId, 1001035015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId74 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId74 int = COALESCE(@ExistingPassageId74, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId74 AS PassageId, @ResourceId74 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId75 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 35:16-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId75 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId75 int = COALESCE(@ExistingResourceId75, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId75 AS ResourceId, 1 AS LanguageId, 'Genesis 35:16-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2141 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_016_1.mp3","webmSizeKb":111,"mp3SizeKb":197},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_016_2.mp3","webmSizeKb":611,"mp3SizeKb":1045},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_016_3.mp3","webmSizeKb":410,"mp3SizeKb":701},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_016_4.mp3","webmSizeKb":672,"mp3SizeKb":1177},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_016_5.mp3","webmSizeKb":337,"mp3SizeKb":581}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId75 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001035016 AS StartVerseId, 1001035020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId75 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId75 int = COALESCE(@ExistingPassageId75, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId75 AS PassageId, @ResourceId75 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId76 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 35:21-29' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId76 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId76 int = COALESCE(@ExistingResourceId76, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId76 AS ResourceId, 1 AS LanguageId, 'Genesis 35:21-29' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2439 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_021_1.mp3","webmSizeKb":111,"mp3SizeKb":194},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_021_2.mp3","webmSizeKb":886,"mp3SizeKb":1514},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_021_3.mp3","webmSizeKb":601,"mp3SizeKb":1047},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_021_4.mp3","webmSizeKb":615,"mp3SizeKb":1073},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_035_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_035_021_5.mp3","webmSizeKb":226,"mp3SizeKb":395}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId76 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001035021 AS StartVerseId, 1001035029 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId76 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId76 int = COALESCE(@ExistingPassageId76, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId76 AS PassageId, @ResourceId76 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId77 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 36:1-19' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId77 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId77 int = COALESCE(@ExistingResourceId77, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId77 AS ResourceId, 1 AS LanguageId, 'Genesis 36:1-19' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2689 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_001_1.mp3","webmSizeKb":99,"mp3SizeKb":174},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_001_2.mp3","webmSizeKb":816,"mp3SizeKb":1385},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_001_3.mp3","webmSizeKb":713,"mp3SizeKb":1221},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_001_4.mp3","webmSizeKb":774,"mp3SizeKb":1323},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_001_5.mp3","webmSizeKb":287,"mp3SizeKb":492}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId77 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001036001 AS StartVerseId, 1001036019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId77 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId77 int = COALESCE(@ExistingPassageId77, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId77 AS PassageId, @ResourceId77 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId78 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 36:20-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId78 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId78 int = COALESCE(@ExistingResourceId78, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId78 AS ResourceId, 1 AS LanguageId, 'Genesis 36:20-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1775 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_020_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_020_1.mp3","webmSizeKb":106,"mp3SizeKb":186},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_020_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_020_2.mp3","webmSizeKb":548,"mp3SizeKb":930},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_020_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_020_3.mp3","webmSizeKb":369,"mp3SizeKb":624},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_020_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_020_4.mp3","webmSizeKb":395,"mp3SizeKb":676},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_020_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_020_5.mp3","webmSizeKb":357,"mp3SizeKb":603}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId78 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001036020 AS StartVerseId, 1001036030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId78 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId78 int = COALESCE(@ExistingPassageId78, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId78 AS PassageId, @ResourceId78 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId79 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 36:31-43' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId79 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId79 int = COALESCE(@ExistingResourceId79, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId79 AS ResourceId, 1 AS LanguageId, 'Genesis 36:31-43' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1942 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_031_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_031_1.mp3","webmSizeKb":106,"mp3SizeKb":184},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_031_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_031_2.mp3","webmSizeKb":713,"mp3SizeKb":1208},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_031_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_031_3.mp3","webmSizeKb":333,"mp3SizeKb":568},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_031_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_031_4.mp3","webmSizeKb":515,"mp3SizeKb":886},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_036_031_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_036_031_5.mp3","webmSizeKb":275,"mp3SizeKb":468}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId79 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001036031 AS StartVerseId, 1001036043 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId79 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId79 int = COALESCE(@ExistingPassageId79, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId79 AS PassageId, @ResourceId79 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId80 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 37:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId80 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId80 int = COALESCE(@ExistingResourceId80, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId80 AS ResourceId, 1 AS LanguageId, 'Genesis 37:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3529 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_001_1.mp3","webmSizeKb":107,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_001_2.mp3","webmSizeKb":1123,"mp3SizeKb":1922},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_001_3.mp3","webmSizeKb":971,"mp3SizeKb":1677},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_001_4.mp3","webmSizeKb":764,"mp3SizeKb":1341},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_001_5.mp3","webmSizeKb":564,"mp3SizeKb":974}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId80 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001037001 AS StartVerseId, 1001037011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId80 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId80 int = COALESCE(@ExistingPassageId80, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId80 AS PassageId, @ResourceId80 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId81 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 37:12-36' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId81 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId81 int = COALESCE(@ExistingResourceId81, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId81 AS ResourceId, 1 AS LanguageId, 'Genesis 37:12-36' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4104 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_012_1.mp3","webmSizeKb":117,"mp3SizeKb":204},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_012_2.mp3","webmSizeKb":1211,"mp3SizeKb":2066},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_012_3.mp3","webmSizeKb":1080,"mp3SizeKb":1865},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_012_4.mp3","webmSizeKb":975,"mp3SizeKb":1710},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_037_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_037_012_5.mp3","webmSizeKb":721,"mp3SizeKb":1240}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId81 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001037012 AS StartVerseId, 1001037036 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId81 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId81 int = COALESCE(@ExistingPassageId81, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId81 AS PassageId, @ResourceId81 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId82 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 38:1-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId82 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId82 int = COALESCE(@ExistingResourceId82, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId82 AS ResourceId, 1 AS LanguageId, 'Genesis 38:1-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4248 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_038_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_038_001_1.mp3","webmSizeKb":115,"mp3SizeKb":203},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_038_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_038_001_2.mp3","webmSizeKb":1250,"mp3SizeKb":2140},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_038_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_038_001_3.mp3","webmSizeKb":1030,"mp3SizeKb":1775},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_038_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_038_001_4.mp3","webmSizeKb":987,"mp3SizeKb":1719},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_038_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_038_001_5.mp3","webmSizeKb":866,"mp3SizeKb":1468}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId82 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001038001 AS StartVerseId, 1001038030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId82 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId82 int = COALESCE(@ExistingPassageId82, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId82 AS PassageId, @ResourceId82 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId83 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 39:1-23' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId83 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId83 int = COALESCE(@ExistingResourceId83, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId83 AS ResourceId, 1 AS LanguageId, 'Genesis 39:1-23' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4066 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_039_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_039_001_1.mp3","webmSizeKb":121,"mp3SizeKb":218},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_039_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_039_001_2.mp3","webmSizeKb":1229,"mp3SizeKb":2157},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_039_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_039_001_3.mp3","webmSizeKb":1088,"mp3SizeKb":1894},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_039_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_039_001_4.mp3","webmSizeKb":1110,"mp3SizeKb":1996},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_039_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_039_001_5.mp3","webmSizeKb":518,"mp3SizeKb":903}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId83 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001039001 AS StartVerseId, 1001039023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId83 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId83 int = COALESCE(@ExistingPassageId83, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId83 AS PassageId, @ResourceId83 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId84 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 40:1-23' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId84 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId84 int = COALESCE(@ExistingResourceId84, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId84 AS ResourceId, 1 AS LanguageId, 'Genesis 40:1-23' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3931 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_040_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_040_001_1.mp3","webmSizeKb":117,"mp3SizeKb":209},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_040_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_040_001_2.mp3","webmSizeKb":1275,"mp3SizeKb":2187},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_040_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_040_001_3.mp3","webmSizeKb":963,"mp3SizeKb":1681},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_040_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_040_001_4.mp3","webmSizeKb":916,"mp3SizeKb":1609},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_040_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_040_001_5.mp3","webmSizeKb":660,"mp3SizeKb":1131}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId84 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001040001 AS StartVerseId, 1001040023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId84 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId84 int = COALESCE(@ExistingPassageId84, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId84 AS PassageId, @ResourceId84 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId85 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 6:1-8' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId85 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId85 int = COALESCE(@ExistingResourceId85, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId85 AS ResourceId, 1 AS LanguageId, 'Genesis 6:1-8' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2647 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_001_1.mp3","webmSizeKb":119,"mp3SizeKb":200},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_001_2.mp3","webmSizeKb":672,"mp3SizeKb":1108},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_001_3.mp3","webmSizeKb":503,"mp3SizeKb":839},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_001_4.mp3","webmSizeKb":500,"mp3SizeKb":836},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_001_5.mp3","webmSizeKb":620,"mp3SizeKb":1028},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_001_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_001_6.mp3","webmSizeKb":233,"mp3SizeKb":390}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId85 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001006001 AS StartVerseId, 1001006008 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId85 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId85 int = COALESCE(@ExistingPassageId85, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId85 AS PassageId, @ResourceId85 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId86 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 41:1-36' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId86 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId86 int = COALESCE(@ExistingResourceId86, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId86 AS ResourceId, 1 AS LanguageId, 'Genesis 41:1-36' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3923 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_001_1.mp3","webmSizeKb":118,"mp3SizeKb":216},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_001_2.mp3","webmSizeKb":1210,"mp3SizeKb":2078},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_001_3.mp3","webmSizeKb":827,"mp3SizeKb":1421},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_001_4.mp3","webmSizeKb":980,"mp3SizeKb":1702},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_001_5.mp3","webmSizeKb":788,"mp3SizeKb":1354}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId86 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001041001 AS StartVerseId, 1001041036 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId86 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId86 int = COALESCE(@ExistingPassageId86, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId86 AS PassageId, @ResourceId86 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId87 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 41:37-57' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId87 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId87 int = COALESCE(@ExistingResourceId87, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId87 AS ResourceId, 1 AS LanguageId, 'Genesis 41:37-57' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3519 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_037_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_037_1.mp3","webmSizeKb":119,"mp3SizeKb":210},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_037_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_037_2.mp3","webmSizeKb":1175,"mp3SizeKb":2023},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_037_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_037_3.mp3","webmSizeKb":764,"mp3SizeKb":1328},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_037_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_037_4.mp3","webmSizeKb":944,"mp3SizeKb":1649},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_041_037_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_041_037_5.mp3","webmSizeKb":517,"mp3SizeKb":893}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId87 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001041037 AS StartVerseId, 1001041057 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId87 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId87 int = COALESCE(@ExistingPassageId87, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId87 AS PassageId, @ResourceId87 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId88 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 42:1-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId88 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId88 int = COALESCE(@ExistingResourceId88, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId88 AS ResourceId, 1 AS LanguageId, 'Genesis 42:1-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3774 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_001_1.mp3","webmSizeKb":115,"mp3SizeKb":210},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_001_2.mp3","webmSizeKb":1147,"mp3SizeKb":1984},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_001_3.mp3","webmSizeKb":889,"mp3SizeKb":1544},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_001_4.mp3","webmSizeKb":964,"mp3SizeKb":1709},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_001_5.mp3","webmSizeKb":659,"mp3SizeKb":1126}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId88 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001042001 AS StartVerseId, 1001042026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId88 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId88 int = COALESCE(@ExistingPassageId88, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId88 AS PassageId, @ResourceId88 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId89 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 42:27-38' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId89 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId89 int = COALESCE(@ExistingResourceId89, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId89 AS ResourceId, 1 AS LanguageId, 'Genesis 42:27-38' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2628 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_027_1.mp3","webmSizeKb":113,"mp3SizeKb":206},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_027_2.mp3","webmSizeKb":857,"mp3SizeKb":1490},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_027_3.mp3","webmSizeKb":553,"mp3SizeKb":958},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_027_4.mp3","webmSizeKb":867,"mp3SizeKb":1533},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_042_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_042_027_5.mp3","webmSizeKb":238,"mp3SizeKb":421}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId89 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001042027 AS StartVerseId, 1001042038 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId89 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId89 int = COALESCE(@ExistingPassageId89, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId89 AS PassageId, @ResourceId89 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId90 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 43:1-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId90 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId90 int = COALESCE(@ExistingResourceId90, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId90 AS ResourceId, 1 AS LanguageId, 'Genesis 43:1-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4272 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_043_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_043_001_1.mp3","webmSizeKb":116,"mp3SizeKb":209},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_043_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_043_001_2.mp3","webmSizeKb":1281,"mp3SizeKb":2238},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_043_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_043_001_3.mp3","webmSizeKb":1308,"mp3SizeKb":2302},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_043_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_043_001_4.mp3","webmSizeKb":1017,"mp3SizeKb":1816},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_043_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_043_001_5.mp3","webmSizeKb":550,"mp3SizeKb":952}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId90 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001043001 AS StartVerseId, 1001043034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId90 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId90 int = COALESCE(@ExistingPassageId90, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId90 AS PassageId, @ResourceId90 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId91 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 44:1-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId91 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId91 int = COALESCE(@ExistingResourceId91, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId91 AS ResourceId, 1 AS LanguageId, 'Genesis 44:1-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3125 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_001_1.mp3","webmSizeKb":130,"mp3SizeKb":243},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_001_2.mp3","webmSizeKb":1088,"mp3SizeKb":1937},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_001_3.mp3","webmSizeKb":741,"mp3SizeKb":1314},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_001_4.mp3","webmSizeKb":852,"mp3SizeKb":1526},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_001_5.mp3","webmSizeKb":314,"mp3SizeKb":556}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId91 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001044001 AS StartVerseId, 1001044013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId91 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId91 int = COALESCE(@ExistingPassageId91, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId91 AS PassageId, @ResourceId91 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId92 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 44:14-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId92 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId92 int = COALESCE(@ExistingResourceId92, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId92 AS ResourceId, 1 AS LanguageId, 'Genesis 44:14-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3049 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_014_1.mp3","webmSizeKb":122,"mp3SizeKb":225},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_014_2.mp3","webmSizeKb":1007,"mp3SizeKb":1785},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_014_3.mp3","webmSizeKb":733,"mp3SizeKb":1299},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_014_4.mp3","webmSizeKb":752,"mp3SizeKb":1347},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_044_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_044_014_5.mp3","webmSizeKb":435,"mp3SizeKb":771}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId92 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001044014 AS StartVerseId, 1001044034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId92 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId92 int = COALESCE(@ExistingPassageId92, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId92 AS PassageId, @ResourceId92 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId93 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 45:1-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId93 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId93 int = COALESCE(@ExistingResourceId93, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId93 AS ResourceId, 1 AS LanguageId, 'Genesis 45:1-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4083 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_045_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_045_001_1.mp3","webmSizeKb":123,"mp3SizeKb":224},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_045_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_045_001_2.mp3","webmSizeKb":1164,"mp3SizeKb":2047},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_045_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_045_001_3.mp3","webmSizeKb":891,"mp3SizeKb":1567},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_045_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_045_001_4.mp3","webmSizeKb":1153,"mp3SizeKb":2073},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_045_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_045_001_5.mp3","webmSizeKb":752,"mp3SizeKb":1316}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId93 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001045001 AS StartVerseId, 1001045028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId93 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId93 int = COALESCE(@ExistingPassageId93, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId93 AS PassageId, @ResourceId93 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId94 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 46:1-27' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId94 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId94 int = COALESCE(@ExistingResourceId94, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId94 AS ResourceId, 1 AS LanguageId, 'Genesis 46:1-27' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3526 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_001_1.mp3","webmSizeKb":123,"mp3SizeKb":225},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_001_2.mp3","webmSizeKb":1120,"mp3SizeKb":1975},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_001_3.mp3","webmSizeKb":764,"mp3SizeKb":1345},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_001_4.mp3","webmSizeKb":987,"mp3SizeKb":1764},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_001_5.mp3","webmSizeKb":532,"mp3SizeKb":929}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId94 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001046001 AS StartVerseId, 1001046027 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId94 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId94 int = COALESCE(@ExistingPassageId94, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId94 AS PassageId, @ResourceId94 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId95 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 46:28-47:12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId95 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId95 int = COALESCE(@ExistingResourceId95, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId95 AS ResourceId, 1 AS LanguageId, 'Genesis 46:28-47:12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3103 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_028_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_028_1.mp3","webmSizeKb":133,"mp3SizeKb":240},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_028_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_028_2.mp3","webmSizeKb":1021,"mp3SizeKb":1777},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_028_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_028_3.mp3","webmSizeKb":489,"mp3SizeKb":857},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_028_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_028_4.mp3","webmSizeKb":1004,"mp3SizeKb":1794},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_046_028_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_046_028_5.mp3","webmSizeKb":456,"mp3SizeKb":803}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId95 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001046028 AS StartVerseId, 1001047012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId95 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId95 int = COALESCE(@ExistingPassageId95, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId95 AS PassageId, @ResourceId95 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId96 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 47:13-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId96 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId96 int = COALESCE(@ExistingResourceId96, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId96 AS ResourceId, 1 AS LanguageId, 'Genesis 47:13-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2893 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_013_1.mp3","webmSizeKb":120,"mp3SizeKb":224},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_013_2.mp3","webmSizeKb":737,"mp3SizeKb":1301},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_013_3.mp3","webmSizeKb":761,"mp3SizeKb":1341},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_013_4.mp3","webmSizeKb":905,"mp3SizeKb":1628},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_013_5.mp3","webmSizeKb":370,"mp3SizeKb":651}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId96 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001047013 AS StartVerseId, 1001047026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId96 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId96 int = COALESCE(@ExistingPassageId96, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId96 AS PassageId, @ResourceId96 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId97 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 47:27-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId97 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId97 int = COALESCE(@ExistingResourceId97, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId97 AS ResourceId, 1 AS LanguageId, 'Genesis 47:27-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2358 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_027_1.mp3","webmSizeKb":119,"mp3SizeKb":221},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_027_2.mp3","webmSizeKb":871,"mp3SizeKb":1540},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_027_3.mp3","webmSizeKb":513,"mp3SizeKb":922},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_027_4.mp3","webmSizeKb":555,"mp3SizeKb":1013},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_047_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_047_027_5.mp3","webmSizeKb":300,"mp3SizeKb":531}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId97 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001047027 AS StartVerseId, 1001047031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId97 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId97 int = COALESCE(@ExistingPassageId97, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId97 AS PassageId, @ResourceId97 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId98 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 6:9-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId98 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId98 int = COALESCE(@ExistingResourceId98, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId98 AS ResourceId, 1 AS LanguageId, 'Genesis 6:9-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4493 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_009_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_009_1.mp3","webmSizeKb":108,"mp3SizeKb":196},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_009_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_009_2.mp3","webmSizeKb":1408,"mp3SizeKb":2445},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_009_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_009_3.mp3","webmSizeKb":782,"mp3SizeKb":1375},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_009_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_009_4.mp3","webmSizeKb":1042,"mp3SizeKb":1887},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_006_009_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_006_009_5.mp3","webmSizeKb":1153,"mp3SizeKb":2000}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId98 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001006009 AS StartVerseId, 1001006022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId98 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId98 int = COALESCE(@ExistingPassageId98, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId98 AS PassageId, @ResourceId98 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId99 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 48:1-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId99 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId99 int = COALESCE(@ExistingResourceId99, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId99 AS ResourceId, 1 AS LanguageId, 'Genesis 48:1-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5091 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_048_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_048_001_1.mp3","webmSizeKb":121,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_048_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_048_001_2.mp3","webmSizeKb":1543,"mp3SizeKb":2720},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_048_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_048_001_3.mp3","webmSizeKb":1050,"mp3SizeKb":1863},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_048_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_048_001_4.mp3","webmSizeKb":958,"mp3SizeKb":1703},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_048_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_048_001_5.mp3","webmSizeKb":1419,"mp3SizeKb":2515}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId99 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001048001 AS StartVerseId, 1001048022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId99 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId99 int = COALESCE(@ExistingPassageId99, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId99 AS PassageId, @ResourceId99 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId100 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 49:1-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId100 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId100 int = COALESCE(@ExistingResourceId100, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId100 AS ResourceId, 1 AS LanguageId, 'Genesis 49:1-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3530 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_001_1.mp3","webmSizeKb":116,"mp3SizeKb":216},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_001_2.mp3","webmSizeKb":1274,"mp3SizeKb":2286},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_001_3.mp3","webmSizeKb":930,"mp3SizeKb":1660},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_001_4.mp3","webmSizeKb":834,"mp3SizeKb":1503},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_001_5.mp3","webmSizeKb":376,"mp3SizeKb":664}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId100 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001049001 AS StartVerseId, 1001049028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId100 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId100 int = COALESCE(@ExistingPassageId100, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId100 AS PassageId, @ResourceId100 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId101 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 49:29-50:14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId101 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId101 int = COALESCE(@ExistingResourceId101, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId101 AS ResourceId, 1 AS LanguageId, 'Genesis 49:29-50:14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3366 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_029_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_029_1.mp3","webmSizeKb":127,"mp3SizeKb":233},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_029_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_029_2.mp3","webmSizeKb":1229,"mp3SizeKb":2147},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_029_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_029_3.mp3","webmSizeKb":583,"mp3SizeKb":1026},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_029_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_029_4.mp3","webmSizeKb":882,"mp3SizeKb":1585},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_049_029_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_049_029_5.mp3","webmSizeKb":545,"mp3SizeKb":951}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId101 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001049029 AS StartVerseId, 1001050014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId101 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId101 int = COALESCE(@ExistingPassageId101, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId101 AS PassageId, @ResourceId101 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId102 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 50:15-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId102 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId102 int = COALESCE(@ExistingResourceId102, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId102 AS ResourceId, 1 AS LanguageId, 'Genesis 50:15-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3407 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_015_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_015_1.mp3","webmSizeKb":113,"mp3SizeKb":209},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_015_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_015_2.mp3","webmSizeKb":1290,"mp3SizeKb":2278},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_015_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_015_3.mp3","webmSizeKb":730,"mp3SizeKb":1311},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_015_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_015_4.mp3","webmSizeKb":952,"mp3SizeKb":1698},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_015_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_015_5.mp3","webmSizeKb":322,"mp3SizeKb":570}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId102 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001050015 AS StartVerseId, 1001050021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId102 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId102 int = COALESCE(@ExistingPassageId102, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId102 AS PassageId, @ResourceId102 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId103 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 50:22-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId103 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId103 int = COALESCE(@ExistingResourceId103, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId103 AS ResourceId, 1 AS LanguageId, 'Genesis 50:22-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2510 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_022_1.mp3","webmSizeKb":120,"mp3SizeKb":221},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_022_2.mp3","webmSizeKb":834,"mp3SizeKb":1466},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_022_3.mp3","webmSizeKb":509,"mp3SizeKb":898},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_022_4.mp3","webmSizeKb":735,"mp3SizeKb":1303},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_050_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_050_022_5.mp3","webmSizeKb":312,"mp3SizeKb":546}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId103 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001050022 AS StartVerseId, 1001050026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId103 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId103 int = COALESCE(@ExistingPassageId103, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId103 AS PassageId, @ResourceId103 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId104 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 7:1-24' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId104 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId104 int = COALESCE(@ExistingResourceId104, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId104 AS ResourceId, 1 AS LanguageId, 'Genesis 7:1-24' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4130 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_007_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_007_001_1.mp3","webmSizeKb":111,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_007_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_007_001_2.mp3","webmSizeKb":1331,"mp3SizeKb":2321},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_007_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_007_001_3.mp3","webmSizeKb":1079,"mp3SizeKb":1875},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_007_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_007_001_4.mp3","webmSizeKb":961,"mp3SizeKb":1703},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_007_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_007_001_5.mp3","webmSizeKb":648,"mp3SizeKb":1119}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId104 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001007001 AS StartVerseId, 1001007024 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId104 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId104 int = COALESCE(@ExistingPassageId104, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId104 AS PassageId, @ResourceId104 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId105 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Genesis 8:1-19' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId105 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId105 int = COALESCE(@ExistingResourceId105, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId105 AS ResourceId, 1 AS LanguageId, 'Genesis 8:1-19' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3879 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_001_1.mp3","webmSizeKb":117,"mp3SizeKb":211},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_001_2.mp3","webmSizeKb":1288,"mp3SizeKb":2243},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_001_3.mp3","webmSizeKb":1149,"mp3SizeKb":2058},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_001_4.mp3","webmSizeKb":998,"mp3SizeKb":1809},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_01_008_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_01_008_001_5.mp3","webmSizeKb":327,"mp3SizeKb":573}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId105 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1001008001 AS StartVerseId, 1001008019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId105 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId105 int = COALESCE(@ExistingPassageId105, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId105 AS PassageId, @ResourceId105 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId106 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER John 4:1-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId106 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId106 int = COALESCE(@ExistingResourceId106, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId106 AS ResourceId, 1 AS LanguageId, 'John 4:1-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4848 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_001_1.mp3","webmSizeKb":129,"mp3SizeKb":217},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_001_2.mp3","webmSizeKb":1485,"mp3SizeKb":2465},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_001_3.mp3","webmSizeKb":1238,"mp3SizeKb":2063},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_001_4.mp3","webmSizeKb":867,"mp3SizeKb":1440},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_001_5.mp3","webmSizeKb":848,"mp3SizeKb":1408},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_001_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_001_6.mp3","webmSizeKb":281,"mp3SizeKb":468}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId106 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1044004001 AS StartVerseId, 1044004015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId106 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId106 int = COALESCE(@ExistingPassageId106, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId106 AS PassageId, @ResourceId106 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId107 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER John 4:16-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId107 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId107 int = COALESCE(@ExistingResourceId107, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId107 AS ResourceId, 1 AS LanguageId, 'John 4:16-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5450 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_016_1.mp3","webmSizeKb":137,"mp3SizeKb":225},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_016_2.mp3","webmSizeKb":1917,"mp3SizeKb":3139},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_016_3.mp3","webmSizeKb":1329,"mp3SizeKb":2200},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_016_4.mp3","webmSizeKb":838,"mp3SizeKb":1395},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_016_5.mp3","webmSizeKb":969,"mp3SizeKb":1616},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_44_004_016_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_44_004_016_6.mp3","webmSizeKb":260,"mp3SizeKb":433}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId107 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1044004016 AS StartVerseId, 1044004026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId107 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId107 int = COALESCE(@ExistingPassageId107, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId107 AS PassageId, @ResourceId107 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId108 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Job 1:1-5' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId108 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId108 int = COALESCE(@ExistingResourceId108, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId108 AS ResourceId, 1 AS LanguageId, 'Job 1:1-5' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2048 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_001_1.mp3","webmSizeKb":113,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_001_2.mp3","webmSizeKb":541,"mp3SizeKb":911},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_001_3.mp3","webmSizeKb":510,"mp3SizeKb":864},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_001_4.mp3","webmSizeKb":396,"mp3SizeKb":672},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_001_5.mp3","webmSizeKb":488,"mp3SizeKb":825}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId108 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1018001001 AS StartVerseId, 1018001005 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId108 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId108 int = COALESCE(@ExistingPassageId108, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId108 AS PassageId, @ResourceId108 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId109 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Job 1:6-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId109 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId109 int = COALESCE(@ExistingResourceId109, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId109 AS ResourceId, 1 AS LanguageId, 'Job 1:6-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2339 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_006_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_006_1.mp3","webmSizeKb":119,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_006_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_006_2.mp3","webmSizeKb":694,"mp3SizeKb":1160},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_006_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_006_3.mp3","webmSizeKb":468,"mp3SizeKb":787},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_006_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_006_4.mp3","webmSizeKb":389,"mp3SizeKb":656},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_006_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_006_5.mp3","webmSizeKb":669,"mp3SizeKb":1125}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId109 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1018001006 AS StartVerseId, 1018001012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId109 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId109 int = COALESCE(@ExistingPassageId109, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId109 AS PassageId, @ResourceId109 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId110 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Job 1:13-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId110 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId110 int = COALESCE(@ExistingResourceId110, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId110 AS ResourceId, 1 AS LanguageId, 'Job 1:13-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2381 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_013_1.mp3","webmSizeKb":113,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_013_2.mp3","webmSizeKb":840,"mp3SizeKb":1408},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_013_3.mp3","webmSizeKb":612,"mp3SizeKb":1021},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_013_4.mp3","webmSizeKb":440,"mp3SizeKb":740},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_001_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_001_013_5.mp3","webmSizeKb":376,"mp3SizeKb":627}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId110 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1018001013 AS StartVerseId, 1018001022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId110 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId110 int = COALESCE(@ExistingPassageId110, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId110 AS PassageId, @ResourceId110 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId111 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Job 2:1-6' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId111 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId111 int = COALESCE(@ExistingResourceId111, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId111 AS ResourceId, 1 AS LanguageId, 'Job 2:1-6' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2231 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_001_1.mp3","webmSizeKb":107,"mp3SizeKb":182},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_001_2.mp3","webmSizeKb":866,"mp3SizeKb":1439},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_001_3.mp3","webmSizeKb":420,"mp3SizeKb":704},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_001_4.mp3","webmSizeKb":363,"mp3SizeKb":608},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_001_5.mp3","webmSizeKb":475,"mp3SizeKb":790}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId111 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1018002001 AS StartVerseId, 1018002006 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId111 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId111 int = COALESCE(@ExistingPassageId111, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId111 AS PassageId, @ResourceId111 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId112 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Job 2:7-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId112 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId112 int = COALESCE(@ExistingResourceId112, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId112 AS ResourceId, 1 AS LanguageId, 'Job 2:7-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2514 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_007_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_007_1.mp3","webmSizeKb":113,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_007_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_007_2.mp3","webmSizeKb":804,"mp3SizeKb":1339},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_007_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_007_3.mp3","webmSizeKb":576,"mp3SizeKb":959},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_007_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_007_4.mp3","webmSizeKb":481,"mp3SizeKb":803},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_18_002_007_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_18_002_007_5.mp3","webmSizeKb":540,"mp3SizeKb":900}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId112 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1018002007 AS StartVerseId, 1018002013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId112 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId112 int = COALESCE(@ExistingPassageId112, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId112 AS PassageId, @ResourceId112 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId113 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 1:1-4' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId113 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId113 int = COALESCE(@ExistingResourceId113, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId113 AS ResourceId, 1 AS LanguageId, 'Luke 1:1-4' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1146 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_001_1.mp3","webmSizeKb":104,"mp3SizeKb":183},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_001_2.mp3","webmSizeKb":269,"mp3SizeKb":457},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_001_3.mp3","webmSizeKb":290,"mp3SizeKb":489},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_001_4.mp3","webmSizeKb":324,"mp3SizeKb":557},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_001_5.mp3","webmSizeKb":159,"mp3SizeKb":266}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId113 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043001001 AS StartVerseId, 1043001004 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId113 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId113 int = COALESCE(@ExistingPassageId113, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId113 AS PassageId, @ResourceId113 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId114 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 3:15-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId114 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId114 int = COALESCE(@ExistingResourceId114, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId114 AS ResourceId, 1 AS LanguageId, 'Luke 3:15-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2444 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_015_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_015_1.mp3","webmSizeKb":106,"mp3SizeKb":180},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_015_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_015_2.mp3","webmSizeKb":812,"mp3SizeKb":1359},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_015_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_015_3.mp3","webmSizeKb":355,"mp3SizeKb":597},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_015_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_015_4.mp3","webmSizeKb":397,"mp3SizeKb":671},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_015_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_015_5.mp3","webmSizeKb":774,"mp3SizeKb":1294}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId114 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043003015 AS StartVerseId, 1043003022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId114 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId114 int = COALESCE(@ExistingPassageId114, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId114 AS PassageId, @ResourceId114 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId115 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 3:23-38' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId115 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId115 int = COALESCE(@ExistingResourceId115, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId115 AS ResourceId, 1 AS LanguageId, 'Luke 3:23-38' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1660 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_023_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_023_1.mp3","webmSizeKb":154,"mp3SizeKb":280},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_023_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_023_2.mp3","webmSizeKb":399,"mp3SizeKb":676},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_023_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_023_3.mp3","webmSizeKb":355,"mp3SizeKb":603},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_023_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_023_4.mp3","webmSizeKb":405,"mp3SizeKb":690},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_023_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_023_5.mp3","webmSizeKb":347,"mp3SizeKb":588}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId115 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043003023 AS StartVerseId, 1043003038 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId115 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId115 int = COALESCE(@ExistingPassageId115, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId115 AS PassageId, @ResourceId115 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId116 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 4:1-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId116 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId116 int = COALESCE(@ExistingResourceId116, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId116 AS ResourceId, 1 AS LanguageId, 'Luke 4:1-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3044 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_001_1.mp3","webmSizeKb":155,"mp3SizeKb":283},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_001_2.mp3","webmSizeKb":1073,"mp3SizeKb":1810},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_001_3.mp3","webmSizeKb":528,"mp3SizeKb":898},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_001_4.mp3","webmSizeKb":545,"mp3SizeKb":926},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_001_5.mp3","webmSizeKb":743,"mp3SizeKb":1250}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId116 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043004001 AS StartVerseId, 1043004013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId116 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId116 int = COALESCE(@ExistingPassageId116, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId116 AS PassageId, @ResourceId116 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId117 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 4:14-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId117 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId117 int = COALESCE(@ExistingResourceId117, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId117 AS ResourceId, 1 AS LanguageId, 'Luke 4:14-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3291 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_014_1.mp3","webmSizeKb":154,"mp3SizeKb":282},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_014_2.mp3","webmSizeKb":949,"mp3SizeKb":1592},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_014_3.mp3","webmSizeKb":587,"mp3SizeKb":1015},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_014_4.mp3","webmSizeKb":610,"mp3SizeKb":1030},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_014_5.mp3","webmSizeKb":991,"mp3SizeKb":1659}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId117 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043004014 AS StartVerseId, 1043004030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId117 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId117 int = COALESCE(@ExistingPassageId117, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId117 AS PassageId, @ResourceId117 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId118 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 4:31-44' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId118 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId118 int = COALESCE(@ExistingResourceId118, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId118 AS ResourceId, 1 AS LanguageId, 'Luke 4:31-44' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2896 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_031_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_031_1.mp3","webmSizeKb":154,"mp3SizeKb":281},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_031_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_031_2.mp3","webmSizeKb":906,"mp3SizeKb":1519},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_031_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_031_3.mp3","webmSizeKb":592,"mp3SizeKb":992},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_031_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_031_4.mp3","webmSizeKb":513,"mp3SizeKb":862},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_004_031_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_004_031_5.mp3","webmSizeKb":731,"mp3SizeKb":1223}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId118 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043004031 AS StartVerseId, 1043004044 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId118 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId118 int = COALESCE(@ExistingPassageId118, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId118 AS PassageId, @ResourceId118 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId119 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 5:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId119 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId119 int = COALESCE(@ExistingResourceId119, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId119 AS ResourceId, 1 AS LanguageId, 'Luke 5:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2494 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_001_1.mp3","webmSizeKb":153,"mp3SizeKb":280},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_001_2.mp3","webmSizeKb":863,"mp3SizeKb":1441},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_001_3.mp3","webmSizeKb":474,"mp3SizeKb":799},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_001_4.mp3","webmSizeKb":495,"mp3SizeKb":833},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_001_5.mp3","webmSizeKb":509,"mp3SizeKb":851}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId119 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043005001 AS StartVerseId, 1043005011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId119 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId119 int = COALESCE(@ExistingPassageId119, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId119 AS PassageId, @ResourceId119 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId120 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 5:12-16' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId120 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId120 int = COALESCE(@ExistingResourceId120, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId120 AS ResourceId, 1 AS LanguageId, 'Luke 5:12-16' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1713 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_012_1.mp3","webmSizeKb":112,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_012_2.mp3","webmSizeKb":561,"mp3SizeKb":935},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_012_3.mp3","webmSizeKb":346,"mp3SizeKb":580},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_012_4.mp3","webmSizeKb":352,"mp3SizeKb":595},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_012_5.mp3","webmSizeKb":342,"mp3SizeKb":572}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId120 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043005012 AS StartVerseId, 1043005016 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId120 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId120 int = COALESCE(@ExistingPassageId120, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId120 AS PassageId, @ResourceId120 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId121 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 5:17-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId121 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId121 int = COALESCE(@ExistingResourceId121, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId121 AS ResourceId, 1 AS LanguageId, 'Luke 5:17-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3298 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_017_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_017_1.mp3","webmSizeKb":117,"mp3SizeKb":204},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_017_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_017_2.mp3","webmSizeKb":1148,"mp3SizeKb":1955},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_017_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_017_3.mp3","webmSizeKb":515,"mp3SizeKb":871},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_017_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_017_4.mp3","webmSizeKb":737,"mp3SizeKb":1254},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_017_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_017_5.mp3","webmSizeKb":781,"mp3SizeKb":1314}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId121 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043005017 AS StartVerseId, 1043005026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId121 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId121 int = COALESCE(@ExistingPassageId121, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId121 AS PassageId, @ResourceId121 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId122 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 5:27-39' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId122 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId122 int = COALESCE(@ExistingResourceId122, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId122 AS ResourceId, 1 AS LanguageId, 'Luke 5:27-39' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3861 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_027_1.mp3","webmSizeKb":108,"mp3SizeKb":184},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_027_2.mp3","webmSizeKb":1216,"mp3SizeKb":2049},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_027_3.mp3","webmSizeKb":787,"mp3SizeKb":1316},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_027_4.mp3","webmSizeKb":819,"mp3SizeKb":1369},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_005_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_005_027_5.mp3","webmSizeKb":931,"mp3SizeKb":1553}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId122 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043005027 AS StartVerseId, 1043005039 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId122 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId122 int = COALESCE(@ExistingPassageId122, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId122 AS PassageId, @ResourceId122 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId123 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId123 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId123 int = COALESCE(@ExistingResourceId123, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId123 AS ResourceId, 1 AS LanguageId, 'Luke 6:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3014 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_001_1.mp3","webmSizeKb":92,"mp3SizeKb":156},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_001_2.mp3","webmSizeKb":948,"mp3SizeKb":1575},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_001_3.mp3","webmSizeKb":543,"mp3SizeKb":901},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_001_4.mp3","webmSizeKb":513,"mp3SizeKb":854},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_001_5.mp3","webmSizeKb":918,"mp3SizeKb":1515}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId123 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006001 AS StartVerseId, 1043006011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId123 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId123 int = COALESCE(@ExistingPassageId123, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId123 AS PassageId, @ResourceId123 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId124 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 1:5-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId124 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId124 int = COALESCE(@ExistingResourceId124, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId124 AS ResourceId, 1 AS LanguageId, 'Luke 1:5-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4304 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_005_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_005_1.mp3","webmSizeKb":121,"mp3SizeKb":216},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_005_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_005_2.mp3","webmSizeKb":1062,"mp3SizeKb":1833},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_005_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_005_3.mp3","webmSizeKb":666,"mp3SizeKb":1146},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_005_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_005_4.mp3","webmSizeKb":831,"mp3SizeKb":1450},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_005_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_005_5.mp3","webmSizeKb":1624,"mp3SizeKb":2780}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId124 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043001005 AS StartVerseId, 1043001025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId124 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId124 int = COALESCE(@ExistingPassageId124, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId124 AS PassageId, @ResourceId124 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId125 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:12-16' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId125 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId125 int = COALESCE(@ExistingResourceId125, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId125 AS ResourceId, 1 AS LanguageId, 'Luke 6:12-16' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1708 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_012_1.mp3","webmSizeKb":94,"mp3SizeKb":160},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_012_2.mp3","webmSizeKb":584,"mp3SizeKb":976},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_012_3.mp3","webmSizeKb":369,"mp3SizeKb":618},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_012_4.mp3","webmSizeKb":375,"mp3SizeKb":631},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_012_5.mp3","webmSizeKb":286,"mp3SizeKb":475}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId125 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006012 AS StartVerseId, 1043006016 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId125 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId125 int = COALESCE(@ExistingPassageId125, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId125 AS PassageId, @ResourceId125 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId126 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:17-19' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId126 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId126 int = COALESCE(@ExistingResourceId126, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId126 AS ResourceId, 1 AS LanguageId, 'Luke 6:17-19' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1454 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_017_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_017_1.mp3","webmSizeKb":102,"mp3SizeKb":172},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_017_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_017_2.mp3","webmSizeKb":433,"mp3SizeKb":722},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_017_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_017_3.mp3","webmSizeKb":274,"mp3SizeKb":457},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_017_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_017_4.mp3","webmSizeKb":325,"mp3SizeKb":542},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_017_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_017_5.mp3","webmSizeKb":320,"mp3SizeKb":530}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId126 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006017 AS StartVerseId, 1043006019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId126 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId126 int = COALESCE(@ExistingPassageId126, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId126 AS PassageId, @ResourceId126 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId127 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:20-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId127 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId127 int = COALESCE(@ExistingResourceId127, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId127 AS ResourceId, 1 AS LanguageId, 'Luke 6:20-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2225 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_020_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_020_1.mp3","webmSizeKb":98,"mp3SizeKb":163},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_020_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_020_2.mp3","webmSizeKb":803,"mp3SizeKb":1333},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_020_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_020_3.mp3","webmSizeKb":322,"mp3SizeKb":535},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_020_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_020_4.mp3","webmSizeKb":409,"mp3SizeKb":685},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_020_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_020_5.mp3","webmSizeKb":593,"mp3SizeKb":984}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId127 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006020 AS StartVerseId, 1043006026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId127 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId127 int = COALESCE(@ExistingPassageId127, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId127 AS PassageId, @ResourceId127 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId128 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:27-36' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId128 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId128 int = COALESCE(@ExistingResourceId128, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId128 AS ResourceId, 1 AS LanguageId, 'Luke 6:27-36' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1868 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_027_1.mp3","webmSizeKb":98,"mp3SizeKb":167},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_027_2.mp3","webmSizeKb":486,"mp3SizeKb":808},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_027_3.mp3","webmSizeKb":425,"mp3SizeKb":717},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_027_4.mp3","webmSizeKb":482,"mp3SizeKb":800},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_027_5.mp3","webmSizeKb":377,"mp3SizeKb":624}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId128 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006027 AS StartVerseId, 1043006036 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId128 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId128 int = COALESCE(@ExistingPassageId128, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId128 AS PassageId, @ResourceId128 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId129 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:37-42' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId129 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId129 int = COALESCE(@ExistingResourceId129, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId129 AS ResourceId, 1 AS LanguageId, 'Luke 6:37-42' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1756 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_037_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_037_1.mp3","webmSizeKb":99,"mp3SizeKb":169},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_037_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_037_2.mp3","webmSizeKb":467,"mp3SizeKb":791},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_037_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_037_3.mp3","webmSizeKb":397,"mp3SizeKb":671},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_037_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_037_4.mp3","webmSizeKb":378,"mp3SizeKb":639},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_037_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_037_5.mp3","webmSizeKb":415,"mp3SizeKb":693}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId129 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006037 AS StartVerseId, 1043006042 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId129 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId129 int = COALESCE(@ExistingPassageId129, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId129 AS PassageId, @ResourceId129 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId130 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 6:43-49' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId130 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId130 int = COALESCE(@ExistingResourceId130, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId130 AS ResourceId, 1 AS LanguageId, 'Luke 6:43-49' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1784 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_043_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_043_1.mp3","webmSizeKb":100,"mp3SizeKb":173},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_043_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_043_2.mp3","webmSizeKb":592,"mp3SizeKb":996},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_043_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_043_3.mp3","webmSizeKb":340,"mp3SizeKb":576},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_043_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_043_4.mp3","webmSizeKb":449,"mp3SizeKb":763},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_006_043_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_006_043_5.mp3","webmSizeKb":303,"mp3SizeKb":508}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId130 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043006043 AS StartVerseId, 1043006049 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId130 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId130 int = COALESCE(@ExistingPassageId130, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId130 AS PassageId, @ResourceId130 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId131 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 7:1-10' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId131 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId131 int = COALESCE(@ExistingResourceId131, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId131 AS ResourceId, 1 AS LanguageId, 'Luke 7:1-10' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2572 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_001_1.mp3","webmSizeKb":99,"mp3SizeKb":168},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_001_2.mp3","webmSizeKb":1013,"mp3SizeKb":1685},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_001_3.mp3","webmSizeKb":504,"mp3SizeKb":849},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_001_4.mp3","webmSizeKb":411,"mp3SizeKb":693},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_001_5.mp3","webmSizeKb":545,"mp3SizeKb":918}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId131 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043007001 AS StartVerseId, 1043007010 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId131 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId131 int = COALESCE(@ExistingPassageId131, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId131 AS PassageId, @ResourceId131 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId132 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 7:11-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId132 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId132 int = COALESCE(@ExistingResourceId132, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId132 AS ResourceId, 1 AS LanguageId, 'Luke 7:11-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2198 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_011_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_011_1.mp3","webmSizeKb":97,"mp3SizeKb":164},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_011_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_011_2.mp3","webmSizeKb":724,"mp3SizeKb":1217},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_011_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_011_3.mp3","webmSizeKb":452,"mp3SizeKb":766},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_011_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_011_4.mp3","webmSizeKb":454,"mp3SizeKb":771},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_011_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_011_5.mp3","webmSizeKb":471,"mp3SizeKb":792}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId132 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043007011 AS StartVerseId, 1043007017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId132 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId132 int = COALESCE(@ExistingPassageId132, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId132 AS PassageId, @ResourceId132 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId133 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 7:18-35' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId133 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId133 int = COALESCE(@ExistingResourceId133, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId133 AS ResourceId, 1 AS LanguageId, 'Luke 7:18-35' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3548 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_018_1.mp3","webmSizeKb":92,"mp3SizeKb":156},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_018_2.mp3","webmSizeKb":1534,"mp3SizeKb":2563},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_018_3.mp3","webmSizeKb":713,"mp3SizeKb":1216},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_018_4.mp3","webmSizeKb":755,"mp3SizeKb":1261},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_018_5.mp3","webmSizeKb":454,"mp3SizeKb":757}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId133 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043007018 AS StartVerseId, 1043007035 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId133 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId133 int = COALESCE(@ExistingPassageId133, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId133 AS PassageId, @ResourceId133 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId134 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 7:36-8:3' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId134 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId134 int = COALESCE(@ExistingResourceId134, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId134 AS ResourceId, 1 AS LanguageId, 'Luke 7:36-8:3' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3791 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_036_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_036_1.mp3","webmSizeKb":103,"mp3SizeKb":174},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_036_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_036_2.mp3","webmSizeKb":1539,"mp3SizeKb":2551},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_036_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_036_3.mp3","webmSizeKb":743,"mp3SizeKb":1248},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_036_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_036_4.mp3","webmSizeKb":821,"mp3SizeKb":1404},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_007_036_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_007_036_5.mp3","webmSizeKb":585,"mp3SizeKb":994}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId134 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043007036 AS StartVerseId, 1043008003 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId134 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId134 int = COALESCE(@ExistingPassageId134, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId134 AS PassageId, @ResourceId134 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId135 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 8:4-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId135 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId135 int = COALESCE(@ExistingResourceId135, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId135 AS ResourceId, 1 AS LanguageId, 'Luke 8:4-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3126 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_004_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_004_1.mp3","webmSizeKb":108,"mp3SizeKb":191},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_004_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_004_2.mp3","webmSizeKb":1093,"mp3SizeKb":1843},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_004_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_004_3.mp3","webmSizeKb":448,"mp3SizeKb":766},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_004_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_004_4.mp3","webmSizeKb":1028,"mp3SizeKb":1734},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_004_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_004_5.mp3","webmSizeKb":449,"mp3SizeKb":758}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId135 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043008004 AS StartVerseId, 1043008015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId135 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId135 int = COALESCE(@ExistingPassageId135, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId135 AS PassageId, @ResourceId135 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId136 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 8:16-18' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId136 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId136 int = COALESCE(@ExistingResourceId136, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId136 AS ResourceId, 1 AS LanguageId, 'Luke 8:16-18' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1545 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_016_1.mp3","webmSizeKb":103,"mp3SizeKb":181},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_016_2.mp3","webmSizeKb":415,"mp3SizeKb":692},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_016_3.mp3","webmSizeKb":278,"mp3SizeKb":477},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_016_4.mp3","webmSizeKb":442,"mp3SizeKb":762},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_016_5.mp3","webmSizeKb":307,"mp3SizeKb":510}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId136 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043008016 AS StartVerseId, 1043008018 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId136 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId136 int = COALESCE(@ExistingPassageId136, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId136 AS PassageId, @ResourceId136 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId137 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 8:19-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId137 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId137 int = COALESCE(@ExistingResourceId137, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId137 AS ResourceId, 1 AS LanguageId, 'Luke 8:19-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1304 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_019_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_019_1.mp3","webmSizeKb":100,"mp3SizeKb":170},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_019_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_019_2.mp3","webmSizeKb":429,"mp3SizeKb":714},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_019_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_019_3.mp3","webmSizeKb":288,"mp3SizeKb":481},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_019_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_019_4.mp3","webmSizeKb":305,"mp3SizeKb":511},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_019_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_019_5.mp3","webmSizeKb":182,"mp3SizeKb":302}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId137 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043008019 AS StartVerseId, 1043008021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId137 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId137 int = COALESCE(@ExistingPassageId137, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId137 AS PassageId, @ResourceId137 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId138 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 8:22-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId138 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId138 int = COALESCE(@ExistingResourceId138, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId138 AS ResourceId, 1 AS LanguageId, 'Luke 8:22-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1802 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_022_1.mp3","webmSizeKb":104,"mp3SizeKb":174},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_022_2.mp3","webmSizeKb":605,"mp3SizeKb":1006},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_022_3.mp3","webmSizeKb":227,"mp3SizeKb":378},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_022_4.mp3","webmSizeKb":372,"mp3SizeKb":621},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_008_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_008_022_5.mp3","webmSizeKb":494,"mp3SizeKb":819}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId138 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043008022 AS StartVerseId, 1043008025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId138 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId138 int = COALESCE(@ExistingPassageId138, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId138 AS PassageId, @ResourceId138 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId139 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 1:26-38' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId139 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId139 int = COALESCE(@ExistingResourceId139, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId139 AS ResourceId, 1 AS LanguageId, 'Luke 1:26-38' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2850 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_026_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_026_1.mp3","webmSizeKb":124,"mp3SizeKb":217},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_026_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_026_2.mp3","webmSizeKb":804,"mp3SizeKb":1374},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_026_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_026_3.mp3","webmSizeKb":409,"mp3SizeKb":703},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_026_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_026_4.mp3","webmSizeKb":679,"mp3SizeKb":1173},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_026_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_026_5.mp3","webmSizeKb":834,"mp3SizeKb":1422}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId139 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043001026 AS StartVerseId, 1043001038 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId139 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId139 int = COALESCE(@ExistingPassageId139, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId139 AS PassageId, @ResourceId139 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId140 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 1:39-56' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId140 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId140 int = COALESCE(@ExistingResourceId140, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId140 AS ResourceId, 1 AS LanguageId, 'Luke 1:39-56' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2383 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_039_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_039_1.mp3","webmSizeKb":113,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_039_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_039_2.mp3","webmSizeKb":790,"mp3SizeKb":1347},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_039_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_039_3.mp3","webmSizeKb":389,"mp3SizeKb":669},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_039_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_039_4.mp3","webmSizeKb":483,"mp3SizeKb":839},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_039_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_039_5.mp3","webmSizeKb":608,"mp3SizeKb":1047}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId140 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043001039 AS StartVerseId, 1043001056 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId140 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId140 int = COALESCE(@ExistingPassageId140, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId140 AS PassageId, @ResourceId140 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId141 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 1:57-80' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId141 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId141 int = COALESCE(@ExistingResourceId141, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId141 AS ResourceId, 1 AS LanguageId, 'Luke 1:57-80' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3958 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_057_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_057_1.mp3","webmSizeKb":116,"mp3SizeKb":205},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_057_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_057_2.mp3","webmSizeKb":1302,"mp3SizeKb":2231},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_057_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_057_3.mp3","webmSizeKb":779,"mp3SizeKb":1341},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_057_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_057_4.mp3","webmSizeKb":823,"mp3SizeKb":1431},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_001_057_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_001_057_5.mp3","webmSizeKb":938,"mp3SizeKb":1593}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId141 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043001057 AS StartVerseId, 1043001080 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId141 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId141 int = COALESCE(@ExistingPassageId141, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId141 AS PassageId, @ResourceId141 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId142 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 16:1-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId142 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId142 int = COALESCE(@ExistingResourceId142, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId142 AS ResourceId, 1 AS LanguageId, 'Luke 16:1-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2877 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_001_1.mp3","webmSizeKb":123,"mp3SizeKb":220},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_001_2.mp3","webmSizeKb":1261,"mp3SizeKb":2244},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_001_3.mp3","webmSizeKb":661,"mp3SizeKb":1195},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_001_4.mp3","webmSizeKb":538,"mp3SizeKb":984},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_001_5.mp3","webmSizeKb":294,"mp3SizeKb":535}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId142 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043016001 AS StartVerseId, 1043016015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId142 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId142 int = COALESCE(@ExistingPassageId142, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId142 AS PassageId, @ResourceId142 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId143 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 16:16-18' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId143 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId143 int = COALESCE(@ExistingResourceId143, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId143 AS ResourceId, 1 AS LanguageId, 'Luke 16:16-18' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1871 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_016_1.mp3","webmSizeKb":127,"mp3SizeKb":226},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_016_2.mp3","webmSizeKb":658,"mp3SizeKb":1152},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_016_3.mp3","webmSizeKb":435,"mp3SizeKb":778},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_016_4.mp3","webmSizeKb":358,"mp3SizeKb":646},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_016_5.mp3","webmSizeKb":293,"mp3SizeKb":514}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId143 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043016016 AS StartVerseId, 1043016018 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId143 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId143 int = COALESCE(@ExistingPassageId143, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId143 AS PassageId, @ResourceId143 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId144 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 16:19-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId144 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId144 int = COALESCE(@ExistingResourceId144, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId144 AS ResourceId, 1 AS LanguageId, 'Luke 16:19-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3421 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_019_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_019_1.mp3","webmSizeKb":130,"mp3SizeKb":231},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_019_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_019_2.mp3","webmSizeKb":1475,"mp3SizeKb":2609},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_019_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_019_3.mp3","webmSizeKb":770,"mp3SizeKb":1376},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_019_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_019_4.mp3","webmSizeKb":617,"mp3SizeKb":1108},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_016_019_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_016_019_5.mp3","webmSizeKb":429,"mp3SizeKb":761}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId144 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043016019 AS StartVerseId, 1043016031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId144 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId144 int = COALESCE(@ExistingPassageId144, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId144 AS PassageId, @ResourceId144 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId145 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 17:1-10' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId145 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId145 int = COALESCE(@ExistingResourceId145, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId145 AS ResourceId, 1 AS LanguageId, 'Luke 17:1-10' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2929 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_001_1.mp3","webmSizeKb":127,"mp3SizeKb":223},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_001_2.mp3","webmSizeKb":954,"mp3SizeKb":1665},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_001_3.mp3","webmSizeKb":785,"mp3SizeKb":1384},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_001_4.mp3","webmSizeKb":481,"mp3SizeKb":854},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_001_5.mp3","webmSizeKb":582,"mp3SizeKb":1013}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId145 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043017001 AS StartVerseId, 1043017010 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId145 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId145 int = COALESCE(@ExistingPassageId145, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId145 AS PassageId, @ResourceId145 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId146 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 17:11-19' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId146 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId146 int = COALESCE(@ExistingResourceId146, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId146 AS ResourceId, 1 AS LanguageId, 'Luke 17:11-19' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2369 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_011_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_011_1.mp3","webmSizeKb":126,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_011_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_011_2.mp3","webmSizeKb":967,"mp3SizeKb":1668},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_011_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_011_3.mp3","webmSizeKb":480,"mp3SizeKb":847},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_011_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_011_4.mp3","webmSizeKb":428,"mp3SizeKb":773},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_011_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_011_5.mp3","webmSizeKb":368,"mp3SizeKb":656}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId146 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043017011 AS StartVerseId, 1043017019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId146 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId146 int = COALESCE(@ExistingPassageId146, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId146 AS PassageId, @ResourceId146 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId147 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 17:20-37' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId147 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId147 int = COALESCE(@ExistingResourceId147, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId147 AS ResourceId, 1 AS LanguageId, 'Luke 17:20-37' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3291 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_020_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_020_1.mp3","webmSizeKb":127,"mp3SizeKb":225},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_020_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_020_2.mp3","webmSizeKb":1343,"mp3SizeKb":2354},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_020_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_020_3.mp3","webmSizeKb":898,"mp3SizeKb":1589},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_020_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_020_4.mp3","webmSizeKb":525,"mp3SizeKb":949},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_017_020_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_017_020_5.mp3","webmSizeKb":398,"mp3SizeKb":701}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId147 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043017020 AS StartVerseId, 1043017037 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId147 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId147 int = COALESCE(@ExistingPassageId147, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId147 AS PassageId, @ResourceId147 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId148 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 2:1-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId148 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId148 int = COALESCE(@ExistingResourceId148, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId148 AS ResourceId, 1 AS LanguageId, 'Luke 2:1-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3400 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_001_1.mp3","webmSizeKb":158,"mp3SizeKb":288},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_001_2.mp3","webmSizeKb":1135,"mp3SizeKb":1907},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_001_3.mp3","webmSizeKb":582,"mp3SizeKb":980},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_001_4.mp3","webmSizeKb":607,"mp3SizeKb":1024},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_001_5.mp3","webmSizeKb":918,"mp3SizeKb":1535}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId148 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043002001 AS StartVerseId, 1043002021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId148 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId148 int = COALESCE(@ExistingPassageId148, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId148 AS PassageId, @ResourceId148 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId149 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 18:1-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId149 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId149 int = COALESCE(@ExistingResourceId149, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId149 AS ResourceId, 1 AS LanguageId, 'Luke 18:1-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3819 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_001_1.mp3","webmSizeKb":125,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_001_2.mp3","webmSizeKb":1321,"mp3SizeKb":2314},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_001_3.mp3","webmSizeKb":931,"mp3SizeKb":1644},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_001_4.mp3","webmSizeKb":749,"mp3SizeKb":1338},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_001_5.mp3","webmSizeKb":693,"mp3SizeKb":1221}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId149 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043018001 AS StartVerseId, 1043018017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId149 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId149 int = COALESCE(@ExistingPassageId149, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId149 AS PassageId, @ResourceId149 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId150 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 18:18-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId150 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId150 int = COALESCE(@ExistingResourceId150, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId150 AS ResourceId, 1 AS LanguageId, 'Luke 18:18-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2665 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_018_1.mp3","webmSizeKb":124,"mp3SizeKb":218},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_018_2.mp3","webmSizeKb":836,"mp3SizeKb":1451},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_018_3.mp3","webmSizeKb":647,"mp3SizeKb":1135},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_018_4.mp3","webmSizeKb":591,"mp3SizeKb":1053},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_018_5.mp3","webmSizeKb":467,"mp3SizeKb":822}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId150 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043018018 AS StartVerseId, 1043018030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId150 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId150 int = COALESCE(@ExistingPassageId150, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId150 AS PassageId, @ResourceId150 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId151 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 18:31-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId151 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId151 int = COALESCE(@ExistingResourceId151, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId151 AS ResourceId, 1 AS LanguageId, 'Luke 18:31-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1883 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_031_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_031_1.mp3","webmSizeKb":129,"mp3SizeKb":228},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_031_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_031_2.mp3","webmSizeKb":624,"mp3SizeKb":1090},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_031_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_031_3.mp3","webmSizeKb":546,"mp3SizeKb":977},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_031_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_031_4.mp3","webmSizeKb":299,"mp3SizeKb":531},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_018_031_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_018_031_5.mp3","webmSizeKb":285,"mp3SizeKb":498}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId151 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043018031 AS StartVerseId, 1043018034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId151 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId151 int = COALESCE(@ExistingPassageId151, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId151 AS PassageId, @ResourceId151 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId152 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 2:22-40' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId152 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId152 int = COALESCE(@ExistingResourceId152, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId152 AS ResourceId, 1 AS LanguageId, 'Luke 2:22-40' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3871 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_022_1.mp3","webmSizeKb":108,"mp3SizeKb":184},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_022_2.mp3","webmSizeKb":843,"mp3SizeKb":1426},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_022_3.mp3","webmSizeKb":916,"mp3SizeKb":1569},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_022_4.mp3","webmSizeKb":680,"mp3SizeKb":1173},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_022_5.mp3","webmSizeKb":1324,"mp3SizeKb":2244}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId152 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043002022 AS StartVerseId, 1043002040 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId152 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId152 int = COALESCE(@ExistingPassageId152, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId152 AS PassageId, @ResourceId152 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId153 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 2:41-52' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId153 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId153 int = COALESCE(@ExistingResourceId153, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId153 AS ResourceId, 1 AS LanguageId, 'Luke 2:41-52' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2538 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_041_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_041_1.mp3","webmSizeKb":160,"mp3SizeKb":292},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_041_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_041_2.mp3","webmSizeKb":702,"mp3SizeKb":1169},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_041_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_041_3.mp3","webmSizeKb":662,"mp3SizeKb":1118},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_041_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_041_4.mp3","webmSizeKb":663,"mp3SizeKb":1126},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_002_041_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_002_041_5.mp3","webmSizeKb":351,"mp3SizeKb":589}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId153 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043002041 AS StartVerseId, 1043002052 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId153 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId153 int = COALESCE(@ExistingPassageId153, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId153 AS PassageId, @ResourceId153 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId154 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Luke 3:1-14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId154 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId154 int = COALESCE(@ExistingResourceId154, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId154 AS ResourceId, 1 AS LanguageId, 'Luke 3:1-14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3712 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_001_1.mp3","webmSizeKb":106,"mp3SizeKb":182},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_001_2.mp3","webmSizeKb":1286,"mp3SizeKb":2155},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_001_3.mp3","webmSizeKb":654,"mp3SizeKb":1102},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_001_4.mp3","webmSizeKb":583,"mp3SizeKb":989},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_43_003_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_43_003_001_5.mp3","webmSizeKb":1083,"mp3SizeKb":1816}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId154 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1043003001 AS StartVerseId, 1043003014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId154 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId154 int = COALESCE(@ExistingPassageId154, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId154 AS PassageId, @ResourceId154 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId155 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 1:1-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId155 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId155 int = COALESCE(@ExistingResourceId155, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId155 AS ResourceId, 1 AS LanguageId, 'Matthew 1:1-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5566 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_001_1.mp3","webmSizeKb":149,"mp3SizeKb":256},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_001_2.mp3","webmSizeKb":1722,"mp3SizeKb":2955},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_001_3.mp3","webmSizeKb":908,"mp3SizeKb":1569},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_001_4.mp3","webmSizeKb":2069,"mp3SizeKb":3591},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_001_5.mp3","webmSizeKb":718,"mp3SizeKb":1232}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId155 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041001001 AS StartVerseId, 1041001017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId155 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId155 int = COALESCE(@ExistingPassageId155, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId155 AS PassageId, @ResourceId155 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId156 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 27:32-44' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId156 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId156 int = COALESCE(@ExistingResourceId156, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId156 AS ResourceId, 1 AS LanguageId, 'Matthew 27:32-44' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2789 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_032_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_032_1.mp3","webmSizeKb":104,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_032_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_032_2.mp3","webmSizeKb":966,"mp3SizeKb":1710},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_032_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_032_3.mp3","webmSizeKb":652,"mp3SizeKb":1149},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_032_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_032_4.mp3","webmSizeKb":681,"mp3SizeKb":1238},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_032_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_032_5.mp3","webmSizeKb":386,"mp3SizeKb":679}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId156 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041027032 AS StartVerseId, 1041027044 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId156 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId156 int = COALESCE(@ExistingPassageId156, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId156 AS PassageId, @ResourceId156 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId157 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 27:45-56' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId157 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId157 int = COALESCE(@ExistingResourceId157, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId157 AS ResourceId, 1 AS LanguageId, 'Matthew 27:45-56' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3204 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_045_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_045_1.mp3","webmSizeKb":104,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_045_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_045_2.mp3","webmSizeKb":1164,"mp3SizeKb":2015},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_045_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_045_3.mp3","webmSizeKb":775,"mp3SizeKb":1373},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_045_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_045_4.mp3","webmSizeKb":663,"mp3SizeKb":1202},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_045_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_045_5.mp3","webmSizeKb":498,"mp3SizeKb":867}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId157 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041027045 AS StartVerseId, 1041027056 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId157 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId157 int = COALESCE(@ExistingPassageId157, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId157 AS PassageId, @ResourceId157 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId158 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 27:57-66' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId158 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId158 int = COALESCE(@ExistingResourceId158, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId158 AS ResourceId, 1 AS LanguageId, 'Matthew 27:57-66' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2287 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_057_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_057_1.mp3","webmSizeKb":108,"mp3SizeKb":206},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_057_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_057_2.mp3","webmSizeKb":842,"mp3SizeKb":1475},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_057_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_057_3.mp3","webmSizeKb":535,"mp3SizeKb":945},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_057_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_057_4.mp3","webmSizeKb":496,"mp3SizeKb":902},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_057_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_057_5.mp3","webmSizeKb":306,"mp3SizeKb":536}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId158 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041027057 AS StartVerseId, 1041027066 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId158 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId158 int = COALESCE(@ExistingPassageId158, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId158 AS PassageId, @ResourceId158 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId159 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 28:1-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId159 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId159 int = COALESCE(@ExistingResourceId159, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId159 AS ResourceId, 1 AS LanguageId, 'Matthew 28:1-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2875 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_001_1.mp3","webmSizeKb":93,"mp3SizeKb":170},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_001_2.mp3","webmSizeKb":860,"mp3SizeKb":1507},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_001_3.mp3","webmSizeKb":743,"mp3SizeKb":1302},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_001_4.mp3","webmSizeKb":603,"mp3SizeKb":1069},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_001_5.mp3","webmSizeKb":576,"mp3SizeKb":1004}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId159 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041028001 AS StartVerseId, 1041028015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId159 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId159 int = COALESCE(@ExistingPassageId159, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId159 AS PassageId, @ResourceId159 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId160 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 28:16-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId160 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId160 int = COALESCE(@ExistingResourceId160, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId160 AS ResourceId, 1 AS LanguageId, 'Matthew 28:16-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2098 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_016_1.mp3","webmSizeKb":103,"mp3SizeKb":190},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_016_2.mp3","webmSizeKb":857,"mp3SizeKb":1497},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_016_3.mp3","webmSizeKb":368,"mp3SizeKb":645},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_016_4.mp3","webmSizeKb":390,"mp3SizeKb":699},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_028_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_028_016_5.mp3","webmSizeKb":380,"mp3SizeKb":659}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId160 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041028016 AS StartVerseId, 1041028020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId160 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId160 int = COALESCE(@ExistingPassageId160, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId160 AS PassageId, @ResourceId160 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId161 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 5:27-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId161 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId161 int = COALESCE(@ExistingResourceId161, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId161 AS ResourceId, 1 AS LanguageId, 'Matthew 5:27-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3240 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_027_1.mp3","webmSizeKb":133,"mp3SizeKb":225},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_027_2.mp3","webmSizeKb":1061,"mp3SizeKb":1778},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_027_3.mp3","webmSizeKb":662,"mp3SizeKb":1109},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_027_4.mp3","webmSizeKb":805,"mp3SizeKb":1346},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_027_5.mp3","webmSizeKb":579,"mp3SizeKb":973}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId161 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041005027 AS StartVerseId, 1041005032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId161 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId161 int = COALESCE(@ExistingPassageId161, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId161 AS PassageId, @ResourceId161 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId162 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 5:33-42' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId162 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId162 int = COALESCE(@ExistingResourceId162, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId162 AS ResourceId, 1 AS LanguageId, 'Matthew 5:33-42' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3622 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_033_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_033_1.mp3","webmSizeKb":123,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_033_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_033_2.mp3","webmSizeKb":1176,"mp3SizeKb":1971},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_033_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_033_3.mp3","webmSizeKb":719,"mp3SizeKb":1204},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_033_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_033_4.mp3","webmSizeKb":956,"mp3SizeKb":1599},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_033_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_033_5.mp3","webmSizeKb":648,"mp3SizeKb":1088}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId162 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041005033 AS StartVerseId, 1041005042 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId162 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId162 int = COALESCE(@ExistingPassageId162, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId162 AS PassageId, @ResourceId162 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId163 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 5:43-48' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId163 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId163 int = COALESCE(@ExistingResourceId163, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId163 AS ResourceId, 1 AS LanguageId, 'Matthew 5:43-48' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3778 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_043_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_043_1.mp3","webmSizeKb":128,"mp3SizeKb":220},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_043_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_043_2.mp3","webmSizeKb":982,"mp3SizeKb":1655},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_043_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_043_3.mp3","webmSizeKb":880,"mp3SizeKb":1477},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_043_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_043_4.mp3","webmSizeKb":931,"mp3SizeKb":1565},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_043_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_043_5.mp3","webmSizeKb":857,"mp3SizeKb":1436}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId163 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041005043 AS StartVerseId, 1041005048 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId163 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId163 int = COALESCE(@ExistingPassageId163, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId163 AS PassageId, @ResourceId163 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId164 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 6:1-8' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId164 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId164 int = COALESCE(@ExistingResourceId164, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId164 AS ResourceId, 1 AS LanguageId, 'Matthew 6:1-8' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4080 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_001_1.mp3","webmSizeKb":116,"mp3SizeKb":199},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_001_2.mp3","webmSizeKb":1185,"mp3SizeKb":1966},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_001_3.mp3","webmSizeKb":953,"mp3SizeKb":1589},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_001_4.mp3","webmSizeKb":1063,"mp3SizeKb":1776},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_001_5.mp3","webmSizeKb":763,"mp3SizeKb":1263}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId164 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041006001 AS StartVerseId, 1041006008 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId164 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId164 int = COALESCE(@ExistingPassageId164, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId164 AS PassageId, @ResourceId164 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId165 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 6:9-18' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId165 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId165 int = COALESCE(@ExistingResourceId165, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId165 AS ResourceId, 1 AS LanguageId, 'Matthew 6:9-18' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4013 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_009_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_009_1.mp3","webmSizeKb":119,"mp3SizeKb":202},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_009_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_009_2.mp3","webmSizeKb":1290,"mp3SizeKb":2161},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_009_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_009_3.mp3","webmSizeKb":836,"mp3SizeKb":1386},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_009_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_009_4.mp3","webmSizeKb":1018,"mp3SizeKb":1699},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_009_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_009_5.mp3","webmSizeKb":750,"mp3SizeKb":1247}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId165 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041006009 AS StartVerseId, 1041006018 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId165 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId165 int = COALESCE(@ExistingPassageId165, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId165 AS PassageId, @ResourceId165 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId166 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 6:19-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId166 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId166 int = COALESCE(@ExistingResourceId166, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId166 AS ResourceId, 1 AS LanguageId, 'Matthew 6:19-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4617 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_019_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_019_1.mp3","webmSizeKb":122,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_019_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_019_2.mp3","webmSizeKb":1481,"mp3SizeKb":2466},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_019_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_019_3.mp3","webmSizeKb":1241,"mp3SizeKb":2058},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_019_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_019_4.mp3","webmSizeKb":1075,"mp3SizeKb":1798},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_006_019_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_006_019_5.mp3","webmSizeKb":698,"mp3SizeKb":1163}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId166 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041006019 AS StartVerseId, 1041006034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId166 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId166 int = COALESCE(@ExistingPassageId166, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId166 AS PassageId, @ResourceId166 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId167 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 7:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId167 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId167 int = COALESCE(@ExistingResourceId167, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId167 AS ResourceId, 1 AS LanguageId, 'Matthew 7:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4747 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_001_1.mp3","webmSizeKb":115,"mp3SizeKb":196},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_001_2.mp3","webmSizeKb":1362,"mp3SizeKb":2267},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_001_3.mp3","webmSizeKb":1394,"mp3SizeKb":2299},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_001_4.mp3","webmSizeKb":1132,"mp3SizeKb":1883},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_001_5.mp3","webmSizeKb":744,"mp3SizeKb":1224}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId167 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041007001 AS StartVerseId, 1041007012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId167 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId167 int = COALESCE(@ExistingPassageId167, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId167 AS PassageId, @ResourceId167 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId168 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 7:13-29' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId168 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId168 int = COALESCE(@ExistingResourceId168, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId168 AS ResourceId, 1 AS LanguageId, 'Matthew 7:13-29' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4382 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_013_1.mp3","webmSizeKb":109,"mp3SizeKb":183},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_013_2.mp3","webmSizeKb":1275,"mp3SizeKb":2118},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_013_3.mp3","webmSizeKb":978,"mp3SizeKb":1625},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_013_4.mp3","webmSizeKb":1033,"mp3SizeKb":1718},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_007_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_007_013_5.mp3","webmSizeKb":987,"mp3SizeKb":1649}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId168 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041007013 AS StartVerseId, 1041007029 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId168 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId168 int = COALESCE(@ExistingPassageId168, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId168 AS PassageId, @ResourceId168 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId169 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 1:18-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId169 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId169 int = COALESCE(@ExistingResourceId169, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId169 AS ResourceId, 1 AS LanguageId, 'Matthew 1:18-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3162 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_018_1.mp3","webmSizeKb":122,"mp3SizeKb":215},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_018_2.mp3","webmSizeKb":1059,"mp3SizeKb":1839},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_018_3.mp3","webmSizeKb":434,"mp3SizeKb":746},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_018_4.mp3","webmSizeKb":582,"mp3SizeKb":1007},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_001_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_001_018_5.mp3","webmSizeKb":965,"mp3SizeKb":1664}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId169 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041001018 AS StartVerseId, 1041001025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId169 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId169 int = COALESCE(@ExistingPassageId169, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId169 AS PassageId, @ResourceId169 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId170 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 10:1-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId170 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId170 int = COALESCE(@ExistingResourceId170, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId170 AS ResourceId, 1 AS LanguageId, 'Matthew 10:1-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3833 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_010_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_010_001_1.mp3","webmSizeKb":132,"mp3SizeKb":229},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_010_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_010_001_2.mp3","webmSizeKb":1417,"mp3SizeKb":2414},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_010_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_010_001_3.mp3","webmSizeKb":525,"mp3SizeKb":904},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_010_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_010_001_4.mp3","webmSizeKb":1014,"mp3SizeKb":1738},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_010_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_010_001_5.mp3","webmSizeKb":745,"mp3SizeKb":1258}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId170 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041010001 AS StartVerseId, 1041010015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId170 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId170 int = COALESCE(@ExistingPassageId170, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId170 AS PassageId, @ResourceId170 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId171 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:1-9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId171 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId171 int = COALESCE(@ExistingResourceId171, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId171 AS ResourceId, 1 AS LanguageId, 'Matthew 13:1-9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2386 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_001_1.mp3","webmSizeKb":105,"mp3SizeKb":180},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_001_2.mp3","webmSizeKb":908,"mp3SizeKb":1539},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_001_3.mp3","webmSizeKb":502,"mp3SizeKb":860},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_001_4.mp3","webmSizeKb":591,"mp3SizeKb":1023},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_001_5.mp3","webmSizeKb":280,"mp3SizeKb":480}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId171 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013001 AS StartVerseId, 1041013009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId171 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId171 int = COALESCE(@ExistingPassageId171, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId171 AS PassageId, @ResourceId171 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId172 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:10-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId172 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId172 int = COALESCE(@ExistingResourceId172, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId172 AS ResourceId, 1 AS LanguageId, 'Matthew 13:10-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2578 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_010_1.mp3","webmSizeKb":129,"mp3SizeKb":227},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_010_2.mp3","webmSizeKb":931,"mp3SizeKb":1573},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_010_3.mp3","webmSizeKb":395,"mp3SizeKb":671},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_010_4.mp3","webmSizeKb":450,"mp3SizeKb":762},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_010_5.mp3","webmSizeKb":673,"mp3SizeKb":1134}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId172 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013010 AS StartVerseId, 1041013017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId172 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId172 int = COALESCE(@ExistingPassageId172, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId172 AS PassageId, @ResourceId172 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId173 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:18-23' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId173 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId173 int = COALESCE(@ExistingResourceId173, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId173 AS ResourceId, 1 AS LanguageId, 'Matthew 13:18-23' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2042 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_018_1.mp3","webmSizeKb":129,"mp3SizeKb":227},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_018_2.mp3","webmSizeKb":608,"mp3SizeKb":1024},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_018_3.mp3","webmSizeKb":415,"mp3SizeKb":706},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_018_4.mp3","webmSizeKb":518,"mp3SizeKb":884},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_018_5.mp3","webmSizeKb":372,"mp3SizeKb":632}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId173 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013018 AS StartVerseId, 1041013023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId173 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId173 int = COALESCE(@ExistingPassageId173, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId173 AS PassageId, @ResourceId173 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId174 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:24-35' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId174 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId174 int = COALESCE(@ExistingResourceId174, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId174 AS ResourceId, 1 AS LanguageId, 'Matthew 13:24-35' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2556 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_024_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_024_1.mp3","webmSizeKb":129,"mp3SizeKb":227},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_024_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_024_2.mp3","webmSizeKb":892,"mp3SizeKb":1540},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_024_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_024_3.mp3","webmSizeKb":564,"mp3SizeKb":991},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_024_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_024_4.mp3","webmSizeKb":564,"mp3SizeKb":1017},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_024_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_024_5.mp3","webmSizeKb":407,"mp3SizeKb":713}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId174 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013024 AS StartVerseId, 1041013035 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId174 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId174 int = COALESCE(@ExistingPassageId174, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId174 AS PassageId, @ResourceId174 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId175 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:36-43' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId175 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId175 int = COALESCE(@ExistingResourceId175, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId175 AS ResourceId, 1 AS LanguageId, 'Matthew 13:36-43' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2277 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_036_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_036_1.mp3","webmSizeKb":132,"mp3SizeKb":233},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_036_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_036_2.mp3","webmSizeKb":906,"mp3SizeKb":1572},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_036_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_036_3.mp3","webmSizeKb":326,"mp3SizeKb":574},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_036_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_036_4.mp3","webmSizeKb":422,"mp3SizeKb":750},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_036_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_036_5.mp3","webmSizeKb":491,"mp3SizeKb":855}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId175 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013036 AS StartVerseId, 1041013043 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId175 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId175 int = COALESCE(@ExistingPassageId175, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId175 AS PassageId, @ResourceId175 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId176 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:44-53' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId176 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId176 int = COALESCE(@ExistingResourceId176, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId176 AS ResourceId, 1 AS LanguageId, 'Matthew 13:44-53' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2353 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_044_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_044_1.mp3","webmSizeKb":128,"mp3SizeKb":228},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_044_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_044_2.mp3","webmSizeKb":855,"mp3SizeKb":1481},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_044_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_044_3.mp3","webmSizeKb":488,"mp3SizeKb":863},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_044_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_044_4.mp3","webmSizeKb":560,"mp3SizeKb":1008},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_044_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_044_5.mp3","webmSizeKb":322,"mp3SizeKb":561}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId176 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013044 AS StartVerseId, 1041013053 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId176 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId176 int = COALESCE(@ExistingPassageId176, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId176 AS PassageId, @ResourceId176 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId177 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 13:54-58' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId177 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId177 int = COALESCE(@ExistingResourceId177, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId177 AS ResourceId, 1 AS LanguageId, 'Matthew 13:54-58' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1741 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_054_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_054_1.mp3","webmSizeKb":128,"mp3SizeKb":226},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_054_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_054_2.mp3","webmSizeKb":594,"mp3SizeKb":1041},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_054_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_054_3.mp3","webmSizeKb":354,"mp3SizeKb":617},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_054_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_054_4.mp3","webmSizeKb":396,"mp3SizeKb":713},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_013_054_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_013_054_5.mp3","webmSizeKb":269,"mp3SizeKb":471}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId177 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041013054 AS StartVerseId, 1041013058 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId177 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId177 int = COALESCE(@ExistingPassageId177, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId177 AS PassageId, @ResourceId177 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId178 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 14:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId178 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId178 int = COALESCE(@ExistingResourceId178, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId178 AS ResourceId, 1 AS LanguageId, 'Matthew 14:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2872 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_001_1.mp3","webmSizeKb":122,"mp3SizeKb":217},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_001_2.mp3","webmSizeKb":1095,"mp3SizeKb":1906},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_001_3.mp3","webmSizeKb":815,"mp3SizeKb":1437},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_001_4.mp3","webmSizeKb":522,"mp3SizeKb":934},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_001_5.mp3","webmSizeKb":318,"mp3SizeKb":559}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId178 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041014001 AS StartVerseId, 1041014012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId178 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId178 int = COALESCE(@ExistingPassageId178, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId178 AS PassageId, @ResourceId178 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId179 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 14:13-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId179 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId179 int = COALESCE(@ExistingResourceId179, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId179 AS ResourceId, 1 AS LanguageId, 'Matthew 14:13-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2356 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_013_1.mp3","webmSizeKb":126,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_013_2.mp3","webmSizeKb":809,"mp3SizeKb":1414},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_013_3.mp3","webmSizeKb":646,"mp3SizeKb":1162},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_013_4.mp3","webmSizeKb":483,"mp3SizeKb":868},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_014_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_014_013_5.mp3","webmSizeKb":292,"mp3SizeKb":517}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId179 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041014013 AS StartVerseId, 1041014021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId179 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId179 int = COALESCE(@ExistingPassageId179, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId179 AS PassageId, @ResourceId179 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId180 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 15:21-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId180 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId180 int = COALESCE(@ExistingResourceId180, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId180 AS ResourceId, 1 AS LanguageId, 'Matthew 15:21-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3126 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_021_1.mp3","webmSizeKb":109,"mp3SizeKb":189},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_021_2.mp3","webmSizeKb":957,"mp3SizeKb":1635},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_021_3.mp3","webmSizeKb":562,"mp3SizeKb":964},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_021_4.mp3","webmSizeKb":1125,"mp3SizeKb":1939},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_021_5.mp3","webmSizeKb":373,"mp3SizeKb":643}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId180 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041015021 AS StartVerseId, 1041015028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId180 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId180 int = COALESCE(@ExistingPassageId180, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId180 AS PassageId, @ResourceId180 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId181 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 15:29-39' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId181 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId181 int = COALESCE(@ExistingResourceId181, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId181 AS ResourceId, 1 AS LanguageId, 'Matthew 15:29-39' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3288 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_029_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_029_1.mp3","webmSizeKb":111,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_029_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_029_2.mp3","webmSizeKb":799,"mp3SizeKb":1367},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_029_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_029_3.mp3","webmSizeKb":667,"mp3SizeKb":1146},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_029_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_029_4.mp3","webmSizeKb":1182,"mp3SizeKb":2046},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_015_029_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_015_029_5.mp3","webmSizeKb":529,"mp3SizeKb":902}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId181 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041015029 AS StartVerseId, 1041015039 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId181 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId181 int = COALESCE(@ExistingPassageId181, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId181 AS PassageId, @ResourceId181 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId182 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 16:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId182 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId182 int = COALESCE(@ExistingResourceId182, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId182 AS ResourceId, 1 AS LanguageId, 'Matthew 16:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3334 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_001_1.mp3","webmSizeKb":101,"mp3SizeKb":175},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_001_2.mp3","webmSizeKb":1201,"mp3SizeKb":2040},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_001_3.mp3","webmSizeKb":595,"mp3SizeKb":1016},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_001_4.mp3","webmSizeKb":746,"mp3SizeKb":1289},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_001_5.mp3","webmSizeKb":691,"mp3SizeKb":1178}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId182 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041016001 AS StartVerseId, 1041016012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId182 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId182 int = COALESCE(@ExistingPassageId182, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId182 AS PassageId, @ResourceId182 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId183 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 16:13-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId183 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId183 int = COALESCE(@ExistingResourceId183, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId183 AS ResourceId, 1 AS LanguageId, 'Matthew 16:13-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3053 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_013_1.mp3","webmSizeKb":114,"mp3SizeKb":199},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_013_2.mp3","webmSizeKb":1301,"mp3SizeKb":2222},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_013_3.mp3","webmSizeKb":349,"mp3SizeKb":591},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_013_4.mp3","webmSizeKb":436,"mp3SizeKb":752},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_016_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_016_013_5.mp3","webmSizeKb":853,"mp3SizeKb":1457}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId183 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041016013 AS StartVerseId, 1041016020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId183 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId183 int = COALESCE(@ExistingPassageId183, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId183 AS PassageId, @ResourceId183 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId184 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 17:1-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId184 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId184 int = COALESCE(@ExistingResourceId184, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId184 AS ResourceId, 1 AS LanguageId, 'Matthew 17:1-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2472 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_001_1.mp3","webmSizeKb":111,"mp3SizeKb":189},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_001_2.mp3","webmSizeKb":741,"mp3SizeKb":1238},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_001_3.mp3","webmSizeKb":539,"mp3SizeKb":905},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_001_4.mp3","webmSizeKb":675,"mp3SizeKb":1137},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_001_5.mp3","webmSizeKb":406,"mp3SizeKb":681}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId184 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041017001 AS StartVerseId, 1041017013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId184 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId184 int = COALESCE(@ExistingPassageId184, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId184 AS PassageId, @ResourceId184 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId185 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 17:14-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId185 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId185 int = COALESCE(@ExistingResourceId185, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId185 AS ResourceId, 1 AS LanguageId, 'Matthew 17:14-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2544 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_014_1.mp3","webmSizeKb":114,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_014_2.mp3","webmSizeKb":702,"mp3SizeKb":1173},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_014_3.mp3","webmSizeKb":655,"mp3SizeKb":1095},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_014_4.mp3","webmSizeKb":701,"mp3SizeKb":1172},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_014_5.mp3","webmSizeKb":372,"mp3SizeKb":624}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId185 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041017014 AS StartVerseId, 1041017021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId185 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId185 int = COALESCE(@ExistingPassageId185, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId185 AS PassageId, @ResourceId185 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId186 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 17:22-27' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId186 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId186 int = COALESCE(@ExistingResourceId186, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId186 AS ResourceId, 1 AS LanguageId, 'Matthew 17:22-27' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1951 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_022_1.mp3","webmSizeKb":108,"mp3SizeKb":181},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_022_2.mp3","webmSizeKb":566,"mp3SizeKb":944},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_022_3.mp3","webmSizeKb":554,"mp3SizeKb":921},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_022_4.mp3","webmSizeKb":497,"mp3SizeKb":829},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_017_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_017_022_5.mp3","webmSizeKb":226,"mp3SizeKb":375}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId186 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041017022 AS StartVerseId, 1041017027 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId186 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId186 int = COALESCE(@ExistingPassageId186, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId186 AS PassageId, @ResourceId186 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId187 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 4:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId187 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId187 int = COALESCE(@ExistingResourceId187, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId187 AS ResourceId, 1 AS LanguageId, 'Matthew 4:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3159 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_001_1.mp3","webmSizeKb":93,"mp3SizeKb":155},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_001_2.mp3","webmSizeKb":1075,"mp3SizeKb":1808},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_001_3.mp3","webmSizeKb":674,"mp3SizeKb":1136},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_001_4.mp3","webmSizeKb":757,"mp3SizeKb":1289},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_001_5.mp3","webmSizeKb":560,"mp3SizeKb":947}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId187 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041004001 AS StartVerseId, 1041004011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId187 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId187 int = COALESCE(@ExistingPassageId187, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId187 AS PassageId, @ResourceId187 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId188 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 18:1-9' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId188 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId188 int = COALESCE(@ExistingResourceId188, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId188 AS ResourceId, 1 AS LanguageId, 'Matthew 18:1-9' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2233 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_001_1.mp3","webmSizeKb":108,"mp3SizeKb":181},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_001_2.mp3","webmSizeKb":636,"mp3SizeKb":1062},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_001_3.mp3","webmSizeKb":683,"mp3SizeKb":1141},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_001_4.mp3","webmSizeKb":456,"mp3SizeKb":762},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_001_5.mp3","webmSizeKb":350,"mp3SizeKb":584}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId188 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041018001 AS StartVerseId, 1041018009 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId188 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId188 int = COALESCE(@ExistingPassageId188, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId188 AS PassageId, @ResourceId188 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId189 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 18:10-14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId189 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId189 int = COALESCE(@ExistingResourceId189, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId189 AS ResourceId, 1 AS LanguageId, 'Matthew 18:10-14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1859 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_010_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_010_1.mp3","webmSizeKb":104,"mp3SizeKb":178},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_010_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_010_2.mp3","webmSizeKb":566,"mp3SizeKb":942},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_010_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_010_3.mp3","webmSizeKb":411,"mp3SizeKb":686},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_010_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_010_4.mp3","webmSizeKb":470,"mp3SizeKb":792},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_010_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_010_5.mp3","webmSizeKb":308,"mp3SizeKb":517}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId189 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041018010 AS StartVerseId, 1041018014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId189 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId189 int = COALESCE(@ExistingPassageId189, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId189 AS PassageId, @ResourceId189 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId190 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 18:21-35' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId190 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId190 int = COALESCE(@ExistingResourceId190, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId190 AS ResourceId, 1 AS LanguageId, 'Matthew 18:21-35' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3241 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_021_1.mp3","webmSizeKb":103,"mp3SizeKb":173},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_021_2.mp3","webmSizeKb":924,"mp3SizeKb":1538},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_021_3.mp3","webmSizeKb":744,"mp3SizeKb":1239},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_021_4.mp3","webmSizeKb":1120,"mp3SizeKb":1872},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_018_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_018_021_5.mp3","webmSizeKb":350,"mp3SizeKb":582}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId190 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041018021 AS StartVerseId, 1041018035 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId190 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId190 int = COALESCE(@ExistingPassageId190, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId190 AS PassageId, @ResourceId190 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId191 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 19:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId191 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId191 int = COALESCE(@ExistingResourceId191, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId191 AS ResourceId, 1 AS LanguageId, 'Matthew 19:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3944 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_001_1.mp3","webmSizeKb":139,"mp3SizeKb":258},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_001_2.mp3","webmSizeKb":1406,"mp3SizeKb":2525},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_001_3.mp3","webmSizeKb":803,"mp3SizeKb":1426},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_001_4.mp3","webmSizeKb":1136,"mp3SizeKb":2036},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_001_5.mp3","webmSizeKb":460,"mp3SizeKb":826}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId191 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041019001 AS StartVerseId, 1041019012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId191 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId191 int = COALESCE(@ExistingPassageId191, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId191 AS PassageId, @ResourceId191 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId192 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 19:13-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId192 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId192 int = COALESCE(@ExistingResourceId192, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId192 AS ResourceId, 1 AS LanguageId, 'Matthew 19:13-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3778 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_013_1.mp3","webmSizeKb":139,"mp3SizeKb":252},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_013_2.mp3","webmSizeKb":1184,"mp3SizeKb":2124},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_013_3.mp3","webmSizeKb":945,"mp3SizeKb":1682},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_013_4.mp3","webmSizeKb":995,"mp3SizeKb":1768},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_019_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_019_013_5.mp3","webmSizeKb":515,"mp3SizeKb":919}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId192 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041019013 AS StartVerseId, 1041019030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId192 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId192 int = COALESCE(@ExistingPassageId192, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId192 AS PassageId, @ResourceId192 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId193 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 20:1-16' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId193 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId193 int = COALESCE(@ExistingResourceId193, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId193 AS ResourceId, 1 AS LanguageId, 'Matthew 20:1-16' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2804 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_001_1.mp3","webmSizeKb":125,"mp3SizeKb":227},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_001_2.mp3","webmSizeKb":795,"mp3SizeKb":1424},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_001_3.mp3","webmSizeKb":710,"mp3SizeKb":1272},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_001_4.mp3","webmSizeKb":957,"mp3SizeKb":1723},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_001_5.mp3","webmSizeKb":217,"mp3SizeKb":396}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId193 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041020001 AS StartVerseId, 1041020016 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId193 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId193 int = COALESCE(@ExistingPassageId193, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId193 AS PassageId, @ResourceId193 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId194 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 20:17-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId194 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId194 int = COALESCE(@ExistingResourceId194, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId194 AS ResourceId, 1 AS LanguageId, 'Matthew 20:17-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3122 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_017_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_017_1.mp3","webmSizeKb":116,"mp3SizeKb":203},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_017_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_017_2.mp3","webmSizeKb":858,"mp3SizeKb":1530},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_017_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_017_3.mp3","webmSizeKb":797,"mp3SizeKb":1387},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_017_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_017_4.mp3","webmSizeKb":873,"mp3SizeKb":1524},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_017_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_017_5.mp3","webmSizeKb":478,"mp3SizeKb":860}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId194 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041020017 AS StartVerseId, 1041020028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId194 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId194 int = COALESCE(@ExistingPassageId194, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId194 AS PassageId, @ResourceId194 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId195 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 20:29-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId195 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId195 int = COALESCE(@ExistingResourceId195, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId195 AS ResourceId, 1 AS LanguageId, 'Matthew 20:29-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1949 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_029_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_029_1.mp3","webmSizeKb":121,"mp3SizeKb":218},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_029_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_029_2.mp3","webmSizeKb":699,"mp3SizeKb":1246},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_029_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_029_3.mp3","webmSizeKb":310,"mp3SizeKb":550},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_029_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_029_4.mp3","webmSizeKb":624,"mp3SizeKb":1102},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_020_029_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_020_029_5.mp3","webmSizeKb":195,"mp3SizeKb":340}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId195 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041020029 AS StartVerseId, 1041020034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId195 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId195 int = COALESCE(@ExistingPassageId195, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId195 AS PassageId, @ResourceId195 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId196 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 4:12-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId196 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId196 int = COALESCE(@ExistingResourceId196, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId196 AS ResourceId, 1 AS LanguageId, 'Matthew 4:12-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2569 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_012_1.mp3","webmSizeKb":104,"mp3SizeKb":179},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_012_2.mp3","webmSizeKb":329,"mp3SizeKb":559},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_012_3.mp3","webmSizeKb":704,"mp3SizeKb":1205},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_012_4.mp3","webmSizeKb":623,"mp3SizeKb":1067},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_004_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_004_012_5.mp3","webmSizeKb":809,"mp3SizeKb":1377}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId196 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041004012 AS StartVerseId, 1041004025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId196 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId196 int = COALESCE(@ExistingPassageId196, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId196 AS PassageId, @ResourceId196 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId197 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 21:12-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId197 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId197 int = COALESCE(@ExistingResourceId197, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId197 AS ResourceId, 1 AS LanguageId, 'Matthew 21:12-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4572 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_012_1.mp3","webmSizeKb":109,"mp3SizeKb":186},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_012_2.mp3","webmSizeKb":1494,"mp3SizeKb":2507},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_012_3.mp3","webmSizeKb":915,"mp3SizeKb":1543},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_012_4.mp3","webmSizeKb":926,"mp3SizeKb":1559},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_012_5.mp3","webmSizeKb":1128,"mp3SizeKb":1896}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId197 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041021012 AS StartVerseId, 1041021022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId197 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId197 int = COALESCE(@ExistingPassageId197, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId197 AS PassageId, @ResourceId197 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId198 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 21:23-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId198 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId198 int = COALESCE(@ExistingResourceId198, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId198 AS ResourceId, 1 AS LanguageId, 'Matthew 21:23-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3752 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_023_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_023_1.mp3","webmSizeKb":112,"mp3SizeKb":190},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_023_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_023_2.mp3","webmSizeKb":1147,"mp3SizeKb":1923},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_023_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_023_3.mp3","webmSizeKb":1060,"mp3SizeKb":1774},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_023_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_023_4.mp3","webmSizeKb":726,"mp3SizeKb":1221},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_021_023_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_021_023_5.mp3","webmSizeKb":707,"mp3SizeKb":1184}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId198 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041021023 AS StartVerseId, 1041021032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId198 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId198 int = COALESCE(@ExistingPassageId198, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId198 AS PassageId, @ResourceId198 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId199 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 23:23-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId199 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId199 int = COALESCE(@ExistingResourceId199, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId199 AS ResourceId, 1 AS LanguageId, 'Matthew 23:23-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3142 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_023_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_023_1.mp3","webmSizeKb":116,"mp3SizeKb":195},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_023_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_023_2.mp3","webmSizeKb":904,"mp3SizeKb":1496},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_023_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_023_3.mp3","webmSizeKb":751,"mp3SizeKb":1246},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_023_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_023_4.mp3","webmSizeKb":607,"mp3SizeKb":1010},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_023_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_023_5.mp3","webmSizeKb":529,"mp3SizeKb":879},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_023_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_023_6.mp3","webmSizeKb":235,"mp3SizeKb":389}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId199 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041023023 AS StartVerseId, 1041023028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId199 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId199 int = COALESCE(@ExistingPassageId199, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId199 AS PassageId, @ResourceId199 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId200 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 5:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId200 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId200 int = COALESCE(@ExistingResourceId200, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId200 AS ResourceId, 1 AS LanguageId, 'Matthew 5:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3332 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_001_1.mp3","webmSizeKb":111,"mp3SizeKb":188},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_001_2.mp3","webmSizeKb":1041,"mp3SizeKb":1742},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_001_3.mp3","webmSizeKb":842,"mp3SizeKb":1409},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_001_4.mp3","webmSizeKb":779,"mp3SizeKb":1307},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_001_5.mp3","webmSizeKb":559,"mp3SizeKb":938}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId200 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041005001 AS StartVerseId, 1041005012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId200 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId200 int = COALESCE(@ExistingPassageId200, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId200 AS PassageId, @ResourceId200 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId201 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 23:37-24:2' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId201 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId201 int = COALESCE(@ExistingResourceId201, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId201 AS ResourceId, 1 AS LanguageId, 'Matthew 23:37-24:2' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2199 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_037_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_037_1.mp3","webmSizeKb":99,"mp3SizeKb":165},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_037_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_037_2.mp3","webmSizeKb":843,"mp3SizeKb":1406},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_037_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_037_3.mp3","webmSizeKb":350,"mp3SizeKb":583},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_037_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_037_4.mp3","webmSizeKb":540,"mp3SizeKb":902},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_023_037_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_023_037_5.mp3","webmSizeKb":367,"mp3SizeKb":612}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId201 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041023037 AS StartVerseId, 1041024002 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId201 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId201 int = COALESCE(@ExistingPassageId201, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId201 AS PassageId, @ResourceId201 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId202 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 24:3-14' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId202 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId202 int = COALESCE(@ExistingResourceId202, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId202 AS ResourceId, 1 AS LanguageId, 'Matthew 24:3-14' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4388 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_003_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_003_1.mp3","webmSizeKb":133,"mp3SizeKb":233},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_003_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_003_2.mp3","webmSizeKb":1590,"mp3SizeKb":2715},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_003_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_003_3.mp3","webmSizeKb":945,"mp3SizeKb":1621},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_003_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_003_4.mp3","webmSizeKb":708,"mp3SizeKb":1221},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_003_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_003_5.mp3","webmSizeKb":1012,"mp3SizeKb":1731}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId202 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041024003 AS StartVerseId, 1041024014 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId202 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId202 int = COALESCE(@ExistingPassageId202, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId202 AS PassageId, @ResourceId202 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId203 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 24:15-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId203 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId203 int = COALESCE(@ExistingResourceId203, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId203 AS ResourceId, 1 AS LanguageId, 'Matthew 24:15-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 6190 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_015_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_015_1.mp3","webmSizeKb":174,"mp3SizeKb":301},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_015_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_015_2.mp3","webmSizeKb":2336,"mp3SizeKb":4274},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_015_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_015_3.mp3","webmSizeKb":1364,"mp3SizeKb":2328},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_015_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_015_4.mp3","webmSizeKb":1132,"mp3SizeKb":1935},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_015_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_015_5.mp3","webmSizeKb":1184,"mp3SizeKb":2018}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId203 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041024015 AS StartVerseId, 1041024028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId203 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId203 int = COALESCE(@ExistingPassageId203, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId203 AS PassageId, @ResourceId203 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId204 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 24:29-36' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId204 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId204 int = COALESCE(@ExistingResourceId204, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId204 AS ResourceId, 1 AS LanguageId, 'Matthew 24:29-36' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3533 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_029_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_029_1.mp3","webmSizeKb":129,"mp3SizeKb":219},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_029_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_029_2.mp3","webmSizeKb":1196,"mp3SizeKb":1984},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_029_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_029_3.mp3","webmSizeKb":667,"mp3SizeKb":1110},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_029_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_029_4.mp3","webmSizeKb":850,"mp3SizeKb":1420},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_029_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_029_5.mp3","webmSizeKb":691,"mp3SizeKb":1149}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId204 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041024029 AS StartVerseId, 1041024036 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId204 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId204 int = COALESCE(@ExistingPassageId204, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId204 AS PassageId, @ResourceId204 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId205 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 24:37-44' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId205 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId205 int = COALESCE(@ExistingResourceId205, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId205 AS ResourceId, 1 AS LanguageId, 'Matthew 24:37-44' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3233 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_037_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_037_1.mp3","webmSizeKb":115,"mp3SizeKb":195},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_037_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_037_2.mp3","webmSizeKb":1044,"mp3SizeKb":1737},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_037_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_037_3.mp3","webmSizeKb":800,"mp3SizeKb":1324},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_037_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_037_4.mp3","webmSizeKb":834,"mp3SizeKb":1393},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_037_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_037_5.mp3","webmSizeKb":440,"mp3SizeKb":733}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId205 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041024037 AS StartVerseId, 1041024044 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId205 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId205 int = COALESCE(@ExistingPassageId205, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId205 AS PassageId, @ResourceId205 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId206 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 24:45-51' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId206 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId206 int = COALESCE(@ExistingResourceId206, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId206 AS ResourceId, 1 AS LanguageId, 'Matthew 24:45-51' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3247 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_045_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_045_1.mp3","webmSizeKb":131,"mp3SizeKb":222},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_045_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_045_2.mp3","webmSizeKb":1076,"mp3SizeKb":1785},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_045_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_045_3.mp3","webmSizeKb":829,"mp3SizeKb":1383},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_045_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_045_4.mp3","webmSizeKb":749,"mp3SizeKb":1252},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_024_045_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_024_045_5.mp3","webmSizeKb":462,"mp3SizeKb":774}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId206 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041024045 AS StartVerseId, 1041024051 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId206 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId206 int = COALESCE(@ExistingPassageId206, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId206 AS PassageId, @ResourceId206 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId207 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 5:13-16' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId207 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId207 int = COALESCE(@ExistingResourceId207, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId207 AS ResourceId, 1 AS LanguageId, 'Matthew 5:13-16' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2846 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_013_1.mp3","webmSizeKb":118,"mp3SizeKb":200},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_013_2.mp3","webmSizeKb":765,"mp3SizeKb":1289},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_013_3.mp3","webmSizeKb":566,"mp3SizeKb":952},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_013_4.mp3","webmSizeKb":748,"mp3SizeKb":1264},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_005_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_005_013_5.mp3","webmSizeKb":649,"mp3SizeKb":1092}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId207 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041005013 AS StartVerseId, 1041005016 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId207 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId207 int = COALESCE(@ExistingPassageId207, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId207 AS PassageId, @ResourceId207 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId208 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 26:57-68' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId208 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId208 int = COALESCE(@ExistingResourceId208, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId208 AS ResourceId, 1 AS LanguageId, 'Matthew 26:57-68' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4030 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_026_057_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_026_057_1.mp3","webmSizeKb":113,"mp3SizeKb":202},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_026_057_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_026_057_2.mp3","webmSizeKb":1368,"mp3SizeKb":2333},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_026_057_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_026_057_3.mp3","webmSizeKb":1164,"mp3SizeKb":2010},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_026_057_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_026_057_4.mp3","webmSizeKb":740,"mp3SizeKb":1294},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_026_057_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_026_057_5.mp3","webmSizeKb":645,"mp3SizeKb":1104}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId208 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041026057 AS StartVerseId, 1041026068 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId208 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId208 int = COALESCE(@ExistingPassageId208, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId208 AS PassageId, @ResourceId208 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId209 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 27:1-10' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId209 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId209 int = COALESCE(@ExistingResourceId209, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId209 AS ResourceId, 1 AS LanguageId, 'Matthew 27:1-10' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2408 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_001_1.mp3","webmSizeKb":125,"mp3SizeKb":221},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_001_2.mp3","webmSizeKb":890,"mp3SizeKb":1534},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_001_3.mp3","webmSizeKb":564,"mp3SizeKb":981},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_001_4.mp3","webmSizeKb":431,"mp3SizeKb":754},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_001_5.mp3","webmSizeKb":398,"mp3SizeKb":690}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId209 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041027001 AS StartVerseId, 1041027010 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId209 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId209 int = COALESCE(@ExistingPassageId209, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId209 AS PassageId, @ResourceId209 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId210 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 27:11-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId210 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId210 int = COALESCE(@ExistingResourceId210, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId210 AS ResourceId, 1 AS LanguageId, 'Matthew 27:11-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3295 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_011_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_011_1.mp3","webmSizeKb":108,"mp3SizeKb":194},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_011_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_011_2.mp3","webmSizeKb":972,"mp3SizeKb":1674},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_011_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_011_3.mp3","webmSizeKb":939,"mp3SizeKb":1634},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_011_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_011_4.mp3","webmSizeKb":685,"mp3SizeKb":1207},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_011_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_011_5.mp3","webmSizeKb":591,"mp3SizeKb":1010}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId210 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041027011 AS StartVerseId, 1041027026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId210 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId210 int = COALESCE(@ExistingPassageId210, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId210 AS PassageId, @ResourceId210 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId211 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Matthew 27:27-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId211 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId211 int = COALESCE(@ExistingResourceId211, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId211 AS ResourceId, 1 AS LanguageId, 'Matthew 27:27-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1649 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_027_1.mp3","webmSizeKb":109,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_027_2.mp3","webmSizeKb":468,"mp3SizeKb":830},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_027_3.mp3","webmSizeKb":438,"mp3SizeKb":780},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_027_4.mp3","webmSizeKb":359,"mp3SizeKb":635},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_41_027_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_41_027_027_5.mp3","webmSizeKb":275,"mp3SizeKb":492}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId211 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1041027027 AS StartVerseId, 1041027031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId211 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId211 int = COALESCE(@ExistingPassageId211, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId211 AS PassageId, @ResourceId211 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId212 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 1:1-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId212 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId212 int = COALESCE(@ExistingResourceId212, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId212 AS ResourceId, 1 AS LanguageId, 'Mark 1:1-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2768 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_001_1.mp3","webmSizeKb":117,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_001_2.mp3","webmSizeKb":713,"mp3SizeKb":1175},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_001_3.mp3","webmSizeKb":177,"mp3SizeKb":291},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_001_4.mp3","webmSizeKb":346,"mp3SizeKb":577},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_001_5.mp3","webmSizeKb":1415,"mp3SizeKb":2343}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId212 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042001001 AS StartVerseId, 1042001013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId212 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId212 int = COALESCE(@ExistingPassageId212, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId212 AS PassageId, @ResourceId212 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId213 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 2:23-3:6' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId213 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId213 int = COALESCE(@ExistingResourceId213, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId213 AS ResourceId, 1 AS LanguageId, 'Mark 2:23-3:6' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3178 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_023_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_023_1.mp3","webmSizeKb":120,"mp3SizeKb":202},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_023_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_023_2.mp3","webmSizeKb":784,"mp3SizeKb":1294},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_023_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_023_3.mp3","webmSizeKb":590,"mp3SizeKb":1003},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_023_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_023_4.mp3","webmSizeKb":657,"mp3SizeKb":1122},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_023_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_023_5.mp3","webmSizeKb":1027,"mp3SizeKb":1709}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId213 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042002023 AS StartVerseId, 1042003006 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId213 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId213 int = COALESCE(@ExistingPassageId213, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId213 AS PassageId, @ResourceId213 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId214 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 3:7-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId214 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId214 int = COALESCE(@ExistingResourceId214, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId214 AS ResourceId, 1 AS LanguageId, 'Mark 3:7-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2424 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_007_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_007_1.mp3","webmSizeKb":111,"mp3SizeKb":185},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_007_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_007_2.mp3","webmSizeKb":532,"mp3SizeKb":870},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_007_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_007_3.mp3","webmSizeKb":436,"mp3SizeKb":736},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_007_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_007_4.mp3","webmSizeKb":873,"mp3SizeKb":1475},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_007_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_007_5.mp3","webmSizeKb":472,"mp3SizeKb":775}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId214 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042003007 AS StartVerseId, 1042003012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId214 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId214 int = COALESCE(@ExistingPassageId214, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId214 AS PassageId, @ResourceId214 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId215 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 3:13-19' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId215 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId215 int = COALESCE(@ExistingResourceId215, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId215 AS ResourceId, 1 AS LanguageId, 'Mark 3:13-19' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1521 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_013_1.mp3","webmSizeKb":116,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_013_2.mp3","webmSizeKb":400,"mp3SizeKb":656},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_013_3.mp3","webmSizeKb":196,"mp3SizeKb":342},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_013_4.mp3","webmSizeKb":163,"mp3SizeKb":273},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_013_5.mp3","webmSizeKb":646,"mp3SizeKb":1071}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId215 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042003013 AS StartVerseId, 1042003019 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId215 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId215 int = COALESCE(@ExistingPassageId215, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId215 AS PassageId, @ResourceId215 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId216 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 3:20-35' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId216 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId216 int = COALESCE(@ExistingResourceId216, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId216 AS ResourceId, 1 AS LanguageId, 'Mark 3:20-35' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3143 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_020_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_020_1.mp3","webmSizeKb":120,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_020_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_020_2.mp3","webmSizeKb":610,"mp3SizeKb":1000},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_020_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_020_3.mp3","webmSizeKb":651,"mp3SizeKb":1115},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_020_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_020_4.mp3","webmSizeKb":720,"mp3SizeKb":1240},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_003_020_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_003_020_5.mp3","webmSizeKb":1042,"mp3SizeKb":1731}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId216 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042003020 AS StartVerseId, 1042003035 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId216 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId216 int = COALESCE(@ExistingPassageId216, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId216 AS PassageId, @ResourceId216 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId217 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 4:1-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId217 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId217 int = COALESCE(@ExistingResourceId217, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId217 AS ResourceId, 1 AS LanguageId, 'Mark 4:1-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 5007 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_001_1.mp3","webmSizeKb":120,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_001_2.mp3","webmSizeKb":910,"mp3SizeKb":1490},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_001_3.mp3","webmSizeKb":1381,"mp3SizeKb":2344},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_001_4.mp3","webmSizeKb":1320,"mp3SizeKb":2244},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_001_5.mp3","webmSizeKb":1276,"mp3SizeKb":2111}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId217 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042004001 AS StartVerseId, 1042004020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId217 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId217 int = COALESCE(@ExistingPassageId217, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId217 AS PassageId, @ResourceId217 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId218 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 4:21-25' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId218 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId218 int = COALESCE(@ExistingResourceId218, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId218 AS ResourceId, 1 AS LanguageId, 'Mark 4:21-25' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2043 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_021_1.mp3","webmSizeKb":119,"mp3SizeKb":194},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_021_2.mp3","webmSizeKb":620,"mp3SizeKb":1019},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_021_3.mp3","webmSizeKb":248,"mp3SizeKb":419},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_021_4.mp3","webmSizeKb":527,"mp3SizeKb":895},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_021_5.mp3","webmSizeKb":529,"mp3SizeKb":876}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId218 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042004021 AS StartVerseId, 1042004025 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId218 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId218 int = COALESCE(@ExistingPassageId218, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId218 AS PassageId, @ResourceId218 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId219 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 4:26-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId219 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId219 int = COALESCE(@ExistingResourceId219, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId219 AS ResourceId, 1 AS LanguageId, 'Mark 4:26-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2651 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_026_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_026_1.mp3","webmSizeKb":117,"mp3SizeKb":196},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_026_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_026_2.mp3","webmSizeKb":627,"mp3SizeKb":1025},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_026_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_026_3.mp3","webmSizeKb":389,"mp3SizeKb":664},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_026_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_026_4.mp3","webmSizeKb":820,"mp3SizeKb":1405},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_026_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_026_5.mp3","webmSizeKb":698,"mp3SizeKb":1152}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId219 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042004026 AS StartVerseId, 1042004034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId219 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId219 int = COALESCE(@ExistingPassageId219, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId219 AS PassageId, @ResourceId219 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId220 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 4:35-41' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId220 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId220 int = COALESCE(@ExistingResourceId220, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId220 AS ResourceId, 1 AS LanguageId, 'Mark 4:35-41' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3710 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_035_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_035_1.mp3","webmSizeKb":145,"mp3SizeKb":241},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_035_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_035_2.mp3","webmSizeKb":543,"mp3SizeKb":895},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_035_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_035_3.mp3","webmSizeKb":734,"mp3SizeKb":1277},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_035_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_035_4.mp3","webmSizeKb":1211,"mp3SizeKb":2112},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_004_035_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_004_035_5.mp3","webmSizeKb":1077,"mp3SizeKb":1778}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId220 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042004035 AS StartVerseId, 1042004041 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId220 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId220 int = COALESCE(@ExistingPassageId220, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId220 AS PassageId, @ResourceId220 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId221 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 5:1-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId221 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId221 int = COALESCE(@ExistingResourceId221, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId221 AS ResourceId, 1 AS LanguageId, 'Mark 5:1-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4558 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_001_1.mp3","webmSizeKb":124,"mp3SizeKb":205},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_001_2.mp3","webmSizeKb":730,"mp3SizeKb":1199},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_001_3.mp3","webmSizeKb":1176,"mp3SizeKb":2033},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_001_4.mp3","webmSizeKb":1618,"mp3SizeKb":2791},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_001_5.mp3","webmSizeKb":910,"mp3SizeKb":1497}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId221 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042005001 AS StartVerseId, 1042005020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId221 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId221 int = COALESCE(@ExistingPassageId221, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId221 AS PassageId, @ResourceId221 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId222 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 5:21-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId222 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId222 int = COALESCE(@ExistingResourceId222, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId222 AS ResourceId, 1 AS LanguageId, 'Mark 5:21-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3561 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_021_1.mp3","webmSizeKb":116,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_021_2.mp3","webmSizeKb":750,"mp3SizeKb":1228},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_021_3.mp3","webmSizeKb":816,"mp3SizeKb":1409},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_021_4.mp3","webmSizeKb":1103,"mp3SizeKb":1895},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_021_5.mp3","webmSizeKb":776,"mp3SizeKb":1282}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId222 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042005021 AS StartVerseId, 1042005034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId222 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId222 int = COALESCE(@ExistingPassageId222, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId222 AS PassageId, @ResourceId222 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId223 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 5:35-43' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId223 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId223 int = COALESCE(@ExistingResourceId223, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId223 AS ResourceId, 1 AS LanguageId, 'Mark 5:35-43' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3256 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_035_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_035_1.mp3","webmSizeKb":109,"mp3SizeKb":179},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_035_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_035_2.mp3","webmSizeKb":642,"mp3SizeKb":1047},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_035_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_035_3.mp3","webmSizeKb":737,"mp3SizeKb":1258},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_035_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_035_4.mp3","webmSizeKb":1112,"mp3SizeKb":1935},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_005_035_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_005_035_5.mp3","webmSizeKb":656,"mp3SizeKb":1085}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId223 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042005035 AS StartVerseId, 1042005043 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId223 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId223 int = COALESCE(@ExistingPassageId223, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId223 AS PassageId, @ResourceId223 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId224 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 1:14-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId224 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId224 int = COALESCE(@ExistingResourceId224, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId224 AS ResourceId, 1 AS LanguageId, 'Mark 1:14-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2377 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_014_1.mp3","webmSizeKb":116,"mp3SizeKb":194},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_014_2.mp3","webmSizeKb":400,"mp3SizeKb":660},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_014_3.mp3","webmSizeKb":276,"mp3SizeKb":474},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_014_4.mp3","webmSizeKb":531,"mp3SizeKb":922},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_014_5.mp3","webmSizeKb":1054,"mp3SizeKb":1739}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId224 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042001014 AS StartVerseId, 1042001020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId224 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId224 int = COALESCE(@ExistingPassageId224, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId224 AS PassageId, @ResourceId224 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId225 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 6:1-6a' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId225 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId225 int = COALESCE(@ExistingResourceId225, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId225 AS ResourceId, 1 AS LanguageId, 'Mark 6:1-6a' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2552 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_001_1.mp3","webmSizeKb":126,"mp3SizeKb":211},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_001_2.mp3","webmSizeKb":376,"mp3SizeKb":621},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_001_3.mp3","webmSizeKb":519,"mp3SizeKb":903},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_001_4.mp3","webmSizeKb":941,"mp3SizeKb":1645},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_001_5.mp3","webmSizeKb":590,"mp3SizeKb":973}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId225 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042006001 AS StartVerseId, 1042006006 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId225 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId225 int = COALESCE(@ExistingPassageId225, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId225 AS PassageId, @ResourceId225 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId226 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 6:6b-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId226 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId226 int = COALESCE(@ExistingResourceId226, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId226 AS ResourceId, 1 AS LanguageId, 'Mark 6:6b-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2312 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_006_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_006_1.mp3","webmSizeKb":126,"mp3SizeKb":211},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_006_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_006_2.mp3","webmSizeKb":462,"mp3SizeKb":764},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_006_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_006_3.mp3","webmSizeKb":446,"mp3SizeKb":773},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_006_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_006_4.mp3","webmSizeKb":738,"mp3SizeKb":1265},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_006_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_006_5.mp3","webmSizeKb":540,"mp3SizeKb":891}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId226 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042006006 AS StartVerseId, 1042006013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId226 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId226 int = COALESCE(@ExistingPassageId226, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId226 AS PassageId, @ResourceId226 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId227 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 6:14-29' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId227 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId227 int = COALESCE(@ExistingResourceId227, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId227 AS ResourceId, 1 AS LanguageId, 'Mark 6:14-29' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3884 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_014_1.mp3","webmSizeKb":119,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_014_2.mp3","webmSizeKb":630,"mp3SizeKb":1034},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_014_3.mp3","webmSizeKb":1149,"mp3SizeKb":1959},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_014_4.mp3","webmSizeKb":1119,"mp3SizeKb":1915},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_014_5.mp3","webmSizeKb":867,"mp3SizeKb":1439}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId227 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042006014 AS StartVerseId, 1042006029 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId227 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId227 int = COALESCE(@ExistingPassageId227, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId227 AS PassageId, @ResourceId227 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId228 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 6:30-44' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId228 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId228 int = COALESCE(@ExistingResourceId228, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId228 AS ResourceId, 1 AS LanguageId, 'Mark 6:30-44' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3051 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_030_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_030_1.mp3","webmSizeKb":129,"mp3SizeKb":216},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_030_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_030_2.mp3","webmSizeKb":648,"mp3SizeKb":1067},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_030_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_030_3.mp3","webmSizeKb":776,"mp3SizeKb":1329},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_030_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_030_4.mp3","webmSizeKb":888,"mp3SizeKb":1516},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_030_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_030_5.mp3","webmSizeKb":610,"mp3SizeKb":1007}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId228 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042006030 AS StartVerseId, 1042006044 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId228 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId228 int = COALESCE(@ExistingPassageId228, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId228 AS PassageId, @ResourceId228 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId229 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 6:45-56' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId229 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId229 int = COALESCE(@ExistingResourceId229, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId229 AS ResourceId, 1 AS LanguageId, 'Mark 6:45-56' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2887 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_045_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_045_1.mp3","webmSizeKb":125,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_045_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_045_2.mp3","webmSizeKb":663,"mp3SizeKb":1095},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_045_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_045_3.mp3","webmSizeKb":740,"mp3SizeKb":1267},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_045_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_045_4.mp3","webmSizeKb":668,"mp3SizeKb":1143},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_006_045_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_006_045_5.mp3","webmSizeKb":691,"mp3SizeKb":1139}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId229 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042006045 AS StartVerseId, 1042006056 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId229 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId229 int = COALESCE(@ExistingPassageId229, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId229 AS PassageId, @ResourceId229 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId230 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 7:1-8' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId230 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId230 int = COALESCE(@ExistingResourceId230, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId230 AS ResourceId, 1 AS LanguageId, 'Mark 7:1-8' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2611 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_001_1.mp3","webmSizeKb":115,"mp3SizeKb":191},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_001_2.mp3","webmSizeKb":687,"mp3SizeKb":1125},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_001_3.mp3","webmSizeKb":473,"mp3SizeKb":812},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_001_4.mp3","webmSizeKb":759,"mp3SizeKb":1294},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_001_5.mp3","webmSizeKb":577,"mp3SizeKb":955}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId230 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042007001 AS StartVerseId, 1042007008 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId230 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId230 int = COALESCE(@ExistingPassageId230, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId230 AS PassageId, @ResourceId230 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId231 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 7:9-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId231 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId231 int = COALESCE(@ExistingResourceId231, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId231 AS ResourceId, 1 AS LanguageId, 'Mark 7:9-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1511 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_009_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_009_1.mp3","webmSizeKb":108,"mp3SizeKb":178},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_009_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_009_2.mp3","webmSizeKb":282,"mp3SizeKb":460},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_009_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_009_3.mp3","webmSizeKb":432,"mp3SizeKb":742},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_009_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_009_4.mp3","webmSizeKb":515,"mp3SizeKb":874},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_009_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_009_5.mp3","webmSizeKb":174,"mp3SizeKb":284}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId231 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042007009 AS StartVerseId, 1042007013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId231 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId231 int = COALESCE(@ExistingPassageId231, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId231 AS PassageId, @ResourceId231 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId232 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 7:14-23' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId232 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId232 int = COALESCE(@ExistingResourceId232, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId232 AS ResourceId, 1 AS LanguageId, 'Mark 7:14-23' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2218 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_014_1.mp3","webmSizeKb":119,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_014_2.mp3","webmSizeKb":493,"mp3SizeKb":810},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_014_3.mp3","webmSizeKb":516,"mp3SizeKb":880},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_014_4.mp3","webmSizeKb":592,"mp3SizeKb":1014},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_014_5.mp3","webmSizeKb":498,"mp3SizeKb":820}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId232 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042007014 AS StartVerseId, 1042007023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId232 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId232 int = COALESCE(@ExistingPassageId232, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId232 AS PassageId, @ResourceId232 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId233 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 7:24-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId233 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId233 int = COALESCE(@ExistingResourceId233, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId233 AS ResourceId, 1 AS LanguageId, 'Mark 7:24-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2378 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_024_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_024_1.mp3","webmSizeKb":124,"mp3SizeKb":207},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_024_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_024_2.mp3","webmSizeKb":652,"mp3SizeKb":1070},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_024_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_024_3.mp3","webmSizeKb":526,"mp3SizeKb":914},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_024_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_024_4.mp3","webmSizeKb":550,"mp3SizeKb":941},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_024_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_024_5.mp3","webmSizeKb":526,"mp3SizeKb":869}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId233 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042007024 AS StartVerseId, 1042007030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId233 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId233 int = COALESCE(@ExistingPassageId233, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId233 AS PassageId, @ResourceId233 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId234 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 7:31-37' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId234 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId234 int = COALESCE(@ExistingResourceId234, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId234 AS ResourceId, 1 AS LanguageId, 'Mark 7:31-37' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2281 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_031_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_031_1.mp3","webmSizeKb":126,"mp3SizeKb":210},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_031_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_031_2.mp3","webmSizeKb":543,"mp3SizeKb":890},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_031_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_031_3.mp3","webmSizeKb":564,"mp3SizeKb":965},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_031_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_031_4.mp3","webmSizeKb":700,"mp3SizeKb":1204},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_007_031_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_007_031_5.mp3","webmSizeKb":348,"mp3SizeKb":572}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId234 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042007031 AS StartVerseId, 1042007037 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId234 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId234 int = COALESCE(@ExistingPassageId234, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId234 AS PassageId, @ResourceId234 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId235 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 8:1-10' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId235 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId235 int = COALESCE(@ExistingResourceId235, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId235 AS ResourceId, 1 AS LanguageId, 'Mark 8:1-10' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2140 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_001_1.mp3","webmSizeKb":117,"mp3SizeKb":195},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_001_2.mp3","webmSizeKb":319,"mp3SizeKb":527},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_001_3.mp3","webmSizeKb":576,"mp3SizeKb":993},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_001_4.mp3","webmSizeKb":732,"mp3SizeKb":1249},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_001_5.mp3","webmSizeKb":396,"mp3SizeKb":653}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId235 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042008001 AS StartVerseId, 1042008010 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId235 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId235 int = COALESCE(@ExistingPassageId235, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId235 AS PassageId, @ResourceId235 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId236 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 8:11-21' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId236 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId236 int = COALESCE(@ExistingResourceId236, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId236 AS ResourceId, 1 AS LanguageId, 'Mark 8:11-21' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2987 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_011_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_011_1.mp3","webmSizeKb":118,"mp3SizeKb":196},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_011_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_011_2.mp3","webmSizeKb":560,"mp3SizeKb":917},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_011_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_011_3.mp3","webmSizeKb":891,"mp3SizeKb":1527},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_011_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_011_4.mp3","webmSizeKb":848,"mp3SizeKb":1441},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_011_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_011_5.mp3","webmSizeKb":570,"mp3SizeKb":944}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId236 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042008011 AS StartVerseId, 1042008021 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId236 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId236 int = COALESCE(@ExistingPassageId236, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId236 AS PassageId, @ResourceId236 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId237 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 8:22-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId237 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId237 int = COALESCE(@ExistingResourceId237, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId237 AS ResourceId, 1 AS LanguageId, 'Mark 8:22-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1906 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_022_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_022_1.mp3","webmSizeKb":111,"mp3SizeKb":184},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_022_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_022_2.mp3","webmSizeKb":478,"mp3SizeKb":786},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_022_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_022_3.mp3","webmSizeKb":487,"mp3SizeKb":831},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_022_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_022_4.mp3","webmSizeKb":638,"mp3SizeKb":1085},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_022_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_022_5.mp3","webmSizeKb":192,"mp3SizeKb":319}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId237 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042008022 AS StartVerseId, 1042008026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId237 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId237 int = COALESCE(@ExistingPassageId237, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId237 AS PassageId, @ResourceId237 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId238 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 8:27-30' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId238 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId238 int = COALESCE(@ExistingResourceId238, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId238 AS ResourceId, 1 AS LanguageId, 'Mark 8:27-30' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1728 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_027_1.mp3","webmSizeKb":111,"mp3SizeKb":185},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_027_2.mp3","webmSizeKb":491,"mp3SizeKb":805},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_027_3.mp3","webmSizeKb":290,"mp3SizeKb":492},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_027_4.mp3","webmSizeKb":580,"mp3SizeKb":984},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_027_5.mp3","webmSizeKb":256,"mp3SizeKb":423}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId238 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042008027 AS StartVerseId, 1042008030 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId238 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId238 int = COALESCE(@ExistingPassageId238, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId238 AS PassageId, @ResourceId238 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId239 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 8:31-9:1' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId239 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId239 int = COALESCE(@ExistingResourceId239, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId239 AS ResourceId, 1 AS LanguageId, 'Mark 8:31-9:1' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 4460 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_031_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_031_1.mp3","webmSizeKb":110,"mp3SizeKb":183},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_031_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_031_2.mp3","webmSizeKb":858,"mp3SizeKb":1406},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_031_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_031_3.mp3","webmSizeKb":1099,"mp3SizeKb":1907},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_031_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_031_4.mp3","webmSizeKb":1226,"mp3SizeKb":2090},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_008_031_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_008_031_5.mp3","webmSizeKb":1167,"mp3SizeKb":1925}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId239 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042008031 AS StartVerseId, 1042009001 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId239 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId239 int = COALESCE(@ExistingPassageId239, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId239 AS PassageId, @ResourceId239 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId240 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 1:21-28' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId240 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId240 int = COALESCE(@ExistingResourceId240, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId240 AS ResourceId, 1 AS LanguageId, 'Mark 1:21-28' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2375 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_021_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_021_1.mp3","webmSizeKb":120,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_021_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_021_2.mp3","webmSizeKb":519,"mp3SizeKb":854},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_021_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_021_3.mp3","webmSizeKb":376,"mp3SizeKb":647},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_021_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_021_4.mp3","webmSizeKb":570,"mp3SizeKb":987},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_021_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_021_5.mp3","webmSizeKb":790,"mp3SizeKb":1300}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId240 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042001021 AS StartVerseId, 1042001028 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId240 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId240 int = COALESCE(@ExistingPassageId240, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId240 AS PassageId, @ResourceId240 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId241 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 9:2-13' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId241 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId241 int = COALESCE(@ExistingResourceId241, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId241 AS ResourceId, 1 AS LanguageId, 'Mark 9:2-13' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3314 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_002_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_002_1.mp3","webmSizeKb":119,"mp3SizeKb":199},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_002_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_002_2.mp3","webmSizeKb":685,"mp3SizeKb":1127},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_002_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_002_3.mp3","webmSizeKb":805,"mp3SizeKb":1380},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_002_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_002_4.mp3","webmSizeKb":1160,"mp3SizeKb":1976},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_002_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_002_5.mp3","webmSizeKb":545,"mp3SizeKb":896}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId241 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042009002 AS StartVerseId, 1042009013 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId241 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId241 int = COALESCE(@ExistingPassageId241, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId241 AS PassageId, @ResourceId241 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId242 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 9:14-29' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId242 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId242 int = COALESCE(@ExistingResourceId242, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId242 AS ResourceId, 1 AS LanguageId, 'Mark 9:14-29' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2807 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_014_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_014_1.mp3","webmSizeKb":109,"mp3SizeKb":182},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_014_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_014_2.mp3","webmSizeKb":788,"mp3SizeKb":1302},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_014_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_014_3.mp3","webmSizeKb":671,"mp3SizeKb":1144},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_014_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_014_4.mp3","webmSizeKb":659,"mp3SizeKb":1125},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_014_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_014_5.mp3","webmSizeKb":580,"mp3SizeKb":959}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId242 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042009014 AS StartVerseId, 1042009029 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId242 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId242 int = COALESCE(@ExistingPassageId242, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId242 AS PassageId, @ResourceId242 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId243 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 9:30-50' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId243 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId243 int = COALESCE(@ExistingResourceId243, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId243 AS ResourceId, 1 AS LanguageId, 'Mark 9:30-50' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3380 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_030_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_030_1.mp3","webmSizeKb":113,"mp3SizeKb":186},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_030_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_030_2.mp3","webmSizeKb":1047,"mp3SizeKb":1712},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_030_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_030_3.mp3","webmSizeKb":571,"mp3SizeKb":965},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_030_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_030_4.mp3","webmSizeKb":603,"mp3SizeKb":1020},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_009_030_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_009_030_5.mp3","webmSizeKb":1046,"mp3SizeKb":1725}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId243 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042009030 AS StartVerseId, 1042009050 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId243 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId243 int = COALESCE(@ExistingPassageId243, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId243 AS PassageId, @ResourceId243 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId244 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 10:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId244 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId244 int = COALESCE(@ExistingResourceId244, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId244 AS ResourceId, 1 AS LanguageId, 'Mark 10:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2566 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_001_1.mp3","webmSizeKb":104,"mp3SizeKb":172},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_001_2.mp3","webmSizeKb":653,"mp3SizeKb":1070},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_001_3.mp3","webmSizeKb":534,"mp3SizeKb":881},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_001_4.mp3","webmSizeKb":821,"mp3SizeKb":1354},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_001_5.mp3","webmSizeKb":454,"mp3SizeKb":748}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId244 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042010001 AS StartVerseId, 1042010012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId244 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId244 int = COALESCE(@ExistingPassageId244, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId244 AS PassageId, @ResourceId244 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId245 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 10:13-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId245 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId245 int = COALESCE(@ExistingResourceId245, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId245 AS ResourceId, 1 AS LanguageId, 'Mark 10:13-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3513 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_013_1.mp3","webmSizeKb":116,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_013_2.mp3","webmSizeKb":1074,"mp3SizeKb":1765},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_013_3.mp3","webmSizeKb":724,"mp3SizeKb":1190},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_013_4.mp3","webmSizeKb":1079,"mp3SizeKb":1776},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_013_5.mp3","webmSizeKb":520,"mp3SizeKb":857}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId245 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042010013 AS StartVerseId, 1042010031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId245 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId245 int = COALESCE(@ExistingPassageId245, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId245 AS PassageId, @ResourceId245 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId246 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 10:32-45' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId246 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId246 int = COALESCE(@ExistingResourceId246, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId246 AS ResourceId, 1 AS LanguageId, 'Mark 10:32-45' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3168 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_032_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_032_1.mp3","webmSizeKb":112,"mp3SizeKb":185},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_032_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_032_2.mp3","webmSizeKb":816,"mp3SizeKb":1337},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_032_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_032_3.mp3","webmSizeKb":554,"mp3SizeKb":910},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_032_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_032_4.mp3","webmSizeKb":812,"mp3SizeKb":1329},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_032_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_032_5.mp3","webmSizeKb":874,"mp3SizeKb":1437}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId246 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042010032 AS StartVerseId, 1042010045 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId246 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId246 int = COALESCE(@ExistingPassageId246, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId246 AS PassageId, @ResourceId246 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId247 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 10:46-52' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId247 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId247 int = COALESCE(@ExistingResourceId247, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId247 AS ResourceId, 1 AS LanguageId, 'Mark 10:46-52' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2425 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_046_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_046_1.mp3","webmSizeKb":139,"mp3SizeKb":232},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_046_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_046_2.mp3","webmSizeKb":583,"mp3SizeKb":958},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_046_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_046_3.mp3","webmSizeKb":470,"mp3SizeKb":787},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_046_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_046_4.mp3","webmSizeKb":502,"mp3SizeKb":839},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_010_046_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_010_046_5.mp3","webmSizeKb":731,"mp3SizeKb":1208}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId247 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042010046 AS StartVerseId, 1042010052 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId247 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId247 int = COALESCE(@ExistingPassageId247, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId247 AS PassageId, @ResourceId247 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId248 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 11:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId248 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId248 int = COALESCE(@ExistingResourceId248, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId248 AS ResourceId, 1 AS LanguageId, 'Mark 11:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3170 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_001_1.mp3","webmSizeKb":117,"mp3SizeKb":194},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_001_2.mp3","webmSizeKb":788,"mp3SizeKb":1296},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_001_3.mp3","webmSizeKb":596,"mp3SizeKb":989},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_001_4.mp3","webmSizeKb":943,"mp3SizeKb":1551},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_001_5.mp3","webmSizeKb":726,"mp3SizeKb":1188}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId248 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042011001 AS StartVerseId, 1042011011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId248 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId248 int = COALESCE(@ExistingPassageId248, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId248 AS PassageId, @ResourceId248 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId249 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 11:12-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId249 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId249 int = COALESCE(@ExistingResourceId249, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId249 AS ResourceId, 1 AS LanguageId, 'Mark 11:12-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3309 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_012_1.mp3","webmSizeKb":112,"mp3SizeKb":183},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_012_2.mp3","webmSizeKb":927,"mp3SizeKb":1509},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_012_3.mp3","webmSizeKb":702,"mp3SizeKb":1159},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_012_4.mp3","webmSizeKb":709,"mp3SizeKb":1178},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_012_5.mp3","webmSizeKb":859,"mp3SizeKb":1418}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId249 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042011012 AS StartVerseId, 1042011026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId249 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId249 int = COALESCE(@ExistingPassageId249, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId249 AS PassageId, @ResourceId249 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId250 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 11:27-33' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId250 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId250 int = COALESCE(@ExistingResourceId250, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId250 AS ResourceId, 1 AS LanguageId, 'Mark 11:27-33' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2289 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_027_1.mp3","webmSizeKb":121,"mp3SizeKb":204},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_027_2.mp3","webmSizeKb":775,"mp3SizeKb":1277},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_027_3.mp3","webmSizeKb":467,"mp3SizeKb":771},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_027_4.mp3","webmSizeKb":531,"mp3SizeKb":878},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_011_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_011_027_5.mp3","webmSizeKb":395,"mp3SizeKb":651}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId250 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042011027 AS StartVerseId, 1042011033 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId250 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId250 int = COALESCE(@ExistingPassageId250, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId250 AS PassageId, @ResourceId250 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId251 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 12:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId251 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId251 int = COALESCE(@ExistingResourceId251, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId251 AS ResourceId, 1 AS LanguageId, 'Mark 12:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3202 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_001_1.mp3","webmSizeKb":114,"mp3SizeKb":192},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_001_2.mp3","webmSizeKb":1081,"mp3SizeKb":1777},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_001_3.mp3","webmSizeKb":665,"mp3SizeKb":1101},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_001_4.mp3","webmSizeKb":725,"mp3SizeKb":1202},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_001_5.mp3","webmSizeKb":617,"mp3SizeKb":1013}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId251 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042012001 AS StartVerseId, 1042012012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId251 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId251 int = COALESCE(@ExistingPassageId251, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId251 AS PassageId, @ResourceId251 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId252 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 1:29-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId252 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId252 int = COALESCE(@ExistingResourceId252, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId252 AS ResourceId, 1 AS LanguageId, 'Mark 1:29-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1710 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_029_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_029_1.mp3","webmSizeKb":121,"mp3SizeKb":201},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_029_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_029_2.mp3","webmSizeKb":399,"mp3SizeKb":656},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_029_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_029_3.mp3","webmSizeKb":211,"mp3SizeKb":357},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_029_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_029_4.mp3","webmSizeKb":524,"mp3SizeKb":907},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_029_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_029_5.mp3","webmSizeKb":455,"mp3SizeKb":752}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId252 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042001029 AS StartVerseId, 1042001034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId252 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId252 int = COALESCE(@ExistingPassageId252, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId252 AS PassageId, @ResourceId252 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId253 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 12:13-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId253 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId253 int = COALESCE(@ExistingResourceId253, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId253 AS ResourceId, 1 AS LanguageId, 'Mark 12:13-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2534 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_013_1.mp3","webmSizeKb":124,"mp3SizeKb":208},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_013_2.mp3","webmSizeKb":933,"mp3SizeKb":1530},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_013_3.mp3","webmSizeKb":506,"mp3SizeKb":835},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_013_4.mp3","webmSizeKb":490,"mp3SizeKb":813},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_013_5.mp3","webmSizeKb":481,"mp3SizeKb":794}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId253 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042012013 AS StartVerseId, 1042012017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId253 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId253 int = COALESCE(@ExistingPassageId253, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId253 AS PassageId, @ResourceId253 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId254 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 12:18-27' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId254 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId254 int = COALESCE(@ExistingResourceId254, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId254 AS ResourceId, 1 AS LanguageId, 'Mark 12:18-27' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2771 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_018_1.mp3","webmSizeKb":123,"mp3SizeKb":204},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_018_2.mp3","webmSizeKb":832,"mp3SizeKb":1363},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_018_3.mp3","webmSizeKb":807,"mp3SizeKb":1337},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_018_4.mp3","webmSizeKb":610,"mp3SizeKb":1007},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_018_5.mp3","webmSizeKb":399,"mp3SizeKb":659}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId254 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042012018 AS StartVerseId, 1042012027 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId254 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId254 int = COALESCE(@ExistingPassageId254, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId254 AS PassageId, @ResourceId254 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId255 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 12:28-34' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId255 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId255 int = COALESCE(@ExistingResourceId255, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId255 AS ResourceId, 1 AS LanguageId, 'Mark 12:28-34' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2390 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_028_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_028_1.mp3","webmSizeKb":116,"mp3SizeKb":191},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_028_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_028_2.mp3","webmSizeKb":713,"mp3SizeKb":1169},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_028_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_028_3.mp3","webmSizeKb":409,"mp3SizeKb":674},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_028_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_028_4.mp3","webmSizeKb":520,"mp3SizeKb":855},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_028_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_028_5.mp3","webmSizeKb":632,"mp3SizeKb":1042}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId255 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042012028 AS StartVerseId, 1042012034 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId255 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId255 int = COALESCE(@ExistingPassageId255, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId255 AS PassageId, @ResourceId255 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId256 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 12:35-37' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId256 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId256 int = COALESCE(@ExistingResourceId256, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId256 AS ResourceId, 1 AS LanguageId, 'Mark 12:35-37' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1851 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_035_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_035_1.mp3","webmSizeKb":126,"mp3SizeKb":210},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_035_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_035_2.mp3","webmSizeKb":566,"mp3SizeKb":933},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_035_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_035_3.mp3","webmSizeKb":390,"mp3SizeKb":643},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_035_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_035_4.mp3","webmSizeKb":354,"mp3SizeKb":584},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_035_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_035_5.mp3","webmSizeKb":415,"mp3SizeKb":685}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId256 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042012035 AS StartVerseId, 1042012037 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId256 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId256 int = COALESCE(@ExistingPassageId256, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId256 AS PassageId, @ResourceId256 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId257 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 12:38-44' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId257 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId257 int = COALESCE(@ExistingResourceId257, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId257 AS ResourceId, 1 AS LanguageId, 'Mark 12:38-44' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1789 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_038_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_038_1.mp3","webmSizeKb":117,"mp3SizeKb":198},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_038_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_038_2.mp3","webmSizeKb":542,"mp3SizeKb":895},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_038_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_038_3.mp3","webmSizeKb":331,"mp3SizeKb":544},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_038_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_038_4.mp3","webmSizeKb":465,"mp3SizeKb":767},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_012_038_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_012_038_5.mp3","webmSizeKb":334,"mp3SizeKb":549}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId257 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042012038 AS StartVerseId, 1042012044 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId257 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId257 int = COALESCE(@ExistingPassageId257, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId257 AS PassageId, @ResourceId257 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId258 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 13:1-8' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId258 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId258 int = COALESCE(@ExistingResourceId258, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId258 AS ResourceId, 1 AS LanguageId, 'Mark 13:1-8' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1891 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_001_1.mp3","webmSizeKb":105,"mp3SizeKb":175},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_001_2.mp3","webmSizeKb":689,"mp3SizeKb":1126},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_001_3.mp3","webmSizeKb":398,"mp3SizeKb":653},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_001_4.mp3","webmSizeKb":383,"mp3SizeKb":629},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_001_5.mp3","webmSizeKb":316,"mp3SizeKb":519}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId258 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042013001 AS StartVerseId, 1042013008 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId258 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId258 int = COALESCE(@ExistingPassageId258, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId258 AS PassageId, @ResourceId258 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId259 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 13:9-23' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId259 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId259 int = COALESCE(@ExistingResourceId259, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId259 AS ResourceId, 1 AS LanguageId, 'Mark 13:9-23' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3255 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_009_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_009_1.mp3","webmSizeKb":117,"mp3SizeKb":195},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_009_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_009_2.mp3","webmSizeKb":1095,"mp3SizeKb":1794},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_009_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_009_3.mp3","webmSizeKb":710,"mp3SizeKb":1166},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_009_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_009_4.mp3","webmSizeKb":627,"mp3SizeKb":1032},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_009_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_009_5.mp3","webmSizeKb":706,"mp3SizeKb":1155}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId259 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042013009 AS StartVerseId, 1042013023 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId259 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId259 int = COALESCE(@ExistingPassageId259, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId259 AS PassageId, @ResourceId259 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId260 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 13:24-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId260 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId260 int = COALESCE(@ExistingResourceId260, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId260 AS ResourceId, 1 AS LanguageId, 'Mark 13:24-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2629 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_024_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_024_1.mp3","webmSizeKb":111,"mp3SizeKb":184},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_024_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_024_2.mp3","webmSizeKb":968,"mp3SizeKb":1588},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_024_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_024_3.mp3","webmSizeKb":583,"mp3SizeKb":955},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_024_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_024_4.mp3","webmSizeKb":407,"mp3SizeKb":671},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_024_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_024_5.mp3","webmSizeKb":560,"mp3SizeKb":925}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId260 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042013024 AS StartVerseId, 1042013031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId260 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId260 int = COALESCE(@ExistingPassageId260, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId260 AS PassageId, @ResourceId260 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId261 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 13:32-37' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId261 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId261 int = COALESCE(@ExistingResourceId261, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId261 AS ResourceId, 1 AS LanguageId, 'Mark 13:32-37' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2000 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_032_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_032_1.mp3","webmSizeKb":103,"mp3SizeKb":169},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_032_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_032_2.mp3","webmSizeKb":668,"mp3SizeKb":1092},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_032_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_032_3.mp3","webmSizeKb":432,"mp3SizeKb":710},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_032_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_032_4.mp3","webmSizeKb":456,"mp3SizeKb":754},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_013_032_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_013_032_5.mp3","webmSizeKb":341,"mp3SizeKb":561}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId261 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042013032 AS StartVerseId, 1042013037 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId261 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId261 int = COALESCE(@ExistingPassageId261, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId261 AS PassageId, @ResourceId261 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId262 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:1-11' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId262 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId262 int = COALESCE(@ExistingResourceId262, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId262 AS ResourceId, 1 AS LanguageId, 'Mark 14:1-11' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3209 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_001_1.mp3","webmSizeKb":105,"mp3SizeKb":173},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_001_2.mp3","webmSizeKb":1190,"mp3SizeKb":1953},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_001_3.mp3","webmSizeKb":607,"mp3SizeKb":997},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_001_4.mp3","webmSizeKb":637,"mp3SizeKb":1047},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_001_5.mp3","webmSizeKb":670,"mp3SizeKb":1093}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId262 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014001 AS StartVerseId, 1042014011 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId262 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId262 int = COALESCE(@ExistingPassageId262, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId262 AS PassageId, @ResourceId262 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId263 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:12-26' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId263 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId263 int = COALESCE(@ExistingResourceId263, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId263 AS ResourceId, 1 AS LanguageId, 'Mark 14:12-26' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3967 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_012_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_012_1.mp3","webmSizeKb":105,"mp3SizeKb":174},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_012_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_012_2.mp3","webmSizeKb":1494,"mp3SizeKb":2455},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_012_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_012_3.mp3","webmSizeKb":866,"mp3SizeKb":1423},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_012_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_012_4.mp3","webmSizeKb":840,"mp3SizeKb":1382},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_012_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_012_5.mp3","webmSizeKb":662,"mp3SizeKb":1090}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId263 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014012 AS StartVerseId, 1042014026 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId263 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId263 int = COALESCE(@ExistingPassageId263, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId263 AS PassageId, @ResourceId263 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId264 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:27-31' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId264 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId264 int = COALESCE(@ExistingResourceId264, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId264 AS ResourceId, 1 AS LanguageId, 'Mark 14:27-31' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1803 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_027_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_027_1.mp3","webmSizeKb":114,"mp3SizeKb":189},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_027_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_027_2.mp3","webmSizeKb":663,"mp3SizeKb":1087},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_027_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_027_3.mp3","webmSizeKb":391,"mp3SizeKb":642},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_027_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_027_4.mp3","webmSizeKb":375,"mp3SizeKb":616},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_027_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_027_5.mp3","webmSizeKb":260,"mp3SizeKb":427}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId264 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014027 AS StartVerseId, 1042014031 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId264 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId264 int = COALESCE(@ExistingPassageId264, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId264 AS PassageId, @ResourceId264 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId265 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:32-42' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId265 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId265 int = COALESCE(@ExistingResourceId265, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId265 AS ResourceId, 1 AS LanguageId, 'Mark 14:32-42' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3432 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_032_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_032_1.mp3","webmSizeKb":110,"mp3SizeKb":182},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_032_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_032_2.mp3","webmSizeKb":920,"mp3SizeKb":1506},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_032_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_032_3.mp3","webmSizeKb":798,"mp3SizeKb":1311},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_032_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_032_4.mp3","webmSizeKb":905,"mp3SizeKb":1495},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_032_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_032_5.mp3","webmSizeKb":699,"mp3SizeKb":1155}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId265 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014032 AS StartVerseId, 1042014042 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId265 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId265 int = COALESCE(@ExistingPassageId265, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId265 AS PassageId, @ResourceId265 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId266 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:43-52' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId266 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId266 int = COALESCE(@ExistingResourceId266, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId266 AS ResourceId, 1 AS LanguageId, 'Mark 14:43-52' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2946 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_043_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_043_1.mp3","webmSizeKb":122,"mp3SizeKb":204},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_043_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_043_2.mp3","webmSizeKb":917,"mp3SizeKb":1504},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_043_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_043_3.mp3","webmSizeKb":727,"mp3SizeKb":1199},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_043_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_043_4.mp3","webmSizeKb":593,"mp3SizeKb":976},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_043_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_043_5.mp3","webmSizeKb":587,"mp3SizeKb":967}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId266 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014043 AS StartVerseId, 1042014052 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId266 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId266 int = COALESCE(@ExistingPassageId266, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId266 AS PassageId, @ResourceId266 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId267 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 1:35-39' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId267 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId267 int = COALESCE(@ExistingResourceId267, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId267 AS ResourceId, 1 AS LanguageId, 'Mark 1:35-39' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1598 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_035_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_035_1.mp3","webmSizeKb":120,"mp3SizeKb":199},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_035_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_035_2.mp3","webmSizeKb":519,"mp3SizeKb":853},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_035_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_035_3.mp3","webmSizeKb":199,"mp3SizeKb":337},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_035_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_035_4.mp3","webmSizeKb":207,"mp3SizeKb":353},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_035_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_035_5.mp3","webmSizeKb":553,"mp3SizeKb":916}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId267 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042001035 AS StartVerseId, 1042001039 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId267 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId267 int = COALESCE(@ExistingPassageId267, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId267 AS PassageId, @ResourceId267 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId268 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:53-65' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId268 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId268 int = COALESCE(@ExistingResourceId268, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId268 AS ResourceId, 1 AS LanguageId, 'Mark 14:53-65' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2907 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_053_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_053_1.mp3","webmSizeKb":115,"mp3SizeKb":191},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_053_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_053_2.mp3","webmSizeKb":1022,"mp3SizeKb":1678},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_053_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_053_3.mp3","webmSizeKb":558,"mp3SizeKb":920},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_053_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_053_4.mp3","webmSizeKb":652,"mp3SizeKb":1072},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_053_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_053_5.mp3","webmSizeKb":560,"mp3SizeKb":921}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId268 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014053 AS StartVerseId, 1042014065 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId268 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId268 int = COALESCE(@ExistingPassageId268, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId268 AS PassageId, @ResourceId268 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId269 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 14:66-72' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId269 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId269 int = COALESCE(@ExistingResourceId269, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId269 AS ResourceId, 1 AS LanguageId, 'Mark 14:66-72' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2547 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_066_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_066_1.mp3","webmSizeKb":108,"mp3SizeKb":180},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_066_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_066_2.mp3","webmSizeKb":916,"mp3SizeKb":1509},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_066_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_066_3.mp3","webmSizeKb":531,"mp3SizeKb":875},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_066_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_066_4.mp3","webmSizeKb":543,"mp3SizeKb":897},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_014_066_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_014_066_5.mp3","webmSizeKb":449,"mp3SizeKb":737}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId269 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042014066 AS StartVerseId, 1042014072 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId269 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId269 int = COALESCE(@ExistingPassageId269, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId269 AS PassageId, @ResourceId269 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId270 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 15:1-15' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId270 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId270 int = COALESCE(@ExistingResourceId270, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId270 AS ResourceId, 1 AS LanguageId, 'Mark 15:1-15' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3087 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_001_1.mp3","webmSizeKb":107,"mp3SizeKb":178},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_001_2.mp3","webmSizeKb":1027,"mp3SizeKb":1688},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_001_3.mp3","webmSizeKb":752,"mp3SizeKb":1242},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_001_4.mp3","webmSizeKb":671,"mp3SizeKb":1112},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_001_5.mp3","webmSizeKb":530,"mp3SizeKb":871}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId270 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042015001 AS StartVerseId, 1042015015 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId270 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId270 int = COALESCE(@ExistingPassageId270, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId270 AS PassageId, @ResourceId270 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId271 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 15:16-32' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId271 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId271 int = COALESCE(@ExistingResourceId271, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId271 AS ResourceId, 1 AS LanguageId, 'Mark 15:16-32' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3865 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_016_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_016_1.mp3","webmSizeKb":109,"mp3SizeKb":181},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_016_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_016_2.mp3","webmSizeKb":1281,"mp3SizeKb":2104},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_016_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_016_3.mp3","webmSizeKb":866,"mp3SizeKb":1429},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_016_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_016_4.mp3","webmSizeKb":961,"mp3SizeKb":1581},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_016_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_016_5.mp3","webmSizeKb":648,"mp3SizeKb":1064}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId271 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042015016 AS StartVerseId, 1042015032 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId271 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId271 int = COALESCE(@ExistingPassageId271, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId271 AS PassageId, @ResourceId271 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId272 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 15:33-39' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId272 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId272 int = COALESCE(@ExistingResourceId272, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId272 AS ResourceId, 1 AS LanguageId, 'Mark 15:33-39' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2666 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_033_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_033_1.mp3","webmSizeKb":115,"mp3SizeKb":190},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_033_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_033_2.mp3","webmSizeKb":1014,"mp3SizeKb":1659},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_033_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_033_3.mp3","webmSizeKb":577,"mp3SizeKb":943},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_033_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_033_4.mp3","webmSizeKb":450,"mp3SizeKb":740},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_033_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_033_5.mp3","webmSizeKb":510,"mp3SizeKb":832}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId272 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042015033 AS StartVerseId, 1042015039 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId272 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId272 int = COALESCE(@ExistingPassageId272, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId272 AS PassageId, @ResourceId272 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId273 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 15:40-47' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId273 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId273 int = COALESCE(@ExistingResourceId273, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId273 AS ResourceId, 1 AS LanguageId, 'Mark 15:40-47' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2690 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_040_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_040_1.mp3","webmSizeKb":126,"mp3SizeKb":216},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_040_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_040_2.mp3","webmSizeKb":1076,"mp3SizeKb":1796},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_040_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_040_3.mp3","webmSizeKb":573,"mp3SizeKb":971},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_040_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_040_4.mp3","webmSizeKb":554,"mp3SizeKb":932},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_015_040_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_015_040_5.mp3","webmSizeKb":361,"mp3SizeKb":612}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId273 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042015040 AS StartVerseId, 1042015047 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId273 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId273 int = COALESCE(@ExistingPassageId273, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId273 AS PassageId, @ResourceId273 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId274 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 16:1-8' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId274 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId274 int = COALESCE(@ExistingResourceId274, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId274 AS ResourceId, 1 AS LanguageId, 'Mark 16:1-8' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2150 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_001_1.mp3","webmSizeKb":140,"mp3SizeKb":242},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_001_2.mp3","webmSizeKb":594,"mp3SizeKb":995},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_001_3.mp3","webmSizeKb":576,"mp3SizeKb":962},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_001_4.mp3","webmSizeKb":449,"mp3SizeKb":755},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_001_5.mp3","webmSizeKb":391,"mp3SizeKb":651}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId274 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042016001 AS StartVerseId, 1042016008 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId274 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId274 int = COALESCE(@ExistingPassageId274, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId274 AS PassageId, @ResourceId274 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId275 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 16:9-20' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId275 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId275 int = COALESCE(@ExistingResourceId275, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId275 AS ResourceId, 1 AS LanguageId, 'Mark 16:9-20' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2877 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_009_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_009_1.mp3","webmSizeKb":106,"mp3SizeKb":175},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_009_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_009_2.mp3","webmSizeKb":788,"mp3SizeKb":1289},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_009_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_009_3.mp3","webmSizeKb":725,"mp3SizeKb":1189},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_009_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_009_4.mp3","webmSizeKb":638,"mp3SizeKb":1049},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_016_009_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_016_009_5.mp3","webmSizeKb":620,"mp3SizeKb":1016}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId275 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042016009 AS StartVerseId, 1042016020 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId275 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId275 int = COALESCE(@ExistingPassageId275, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId275 AS PassageId, @ResourceId275 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId276 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 1:40-45' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId276 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId276 int = COALESCE(@ExistingResourceId276, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId276 AS ResourceId, 1 AS LanguageId, 'Mark 1:40-45' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2055 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_040_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_040_1.mp3","webmSizeKb":120,"mp3SizeKb":200},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_040_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_040_2.mp3","webmSizeKb":484,"mp3SizeKb":798},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_040_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_040_3.mp3","webmSizeKb":369,"mp3SizeKb":625},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_040_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_040_4.mp3","webmSizeKb":515,"mp3SizeKb":879},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_001_040_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_001_040_5.mp3","webmSizeKb":567,"mp3SizeKb":938}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId276 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042001040 AS StartVerseId, 1042001045 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId276 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId276 int = COALESCE(@ExistingPassageId276, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId276 AS PassageId, @ResourceId276 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId277 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 2:1-12' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId277 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId277 int = COALESCE(@ExistingResourceId277, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId277 AS ResourceId, 1 AS LanguageId, 'Mark 2:1-12' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 3534 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_001_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_001_1.mp3","webmSizeKb":112,"mp3SizeKb":187},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_001_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_001_2.mp3","webmSizeKb":799,"mp3SizeKb":1320},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_001_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_001_3.mp3","webmSizeKb":640,"mp3SizeKb":1073},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_001_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_001_4.mp3","webmSizeKb":849,"mp3SizeKb":1440},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_001_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_001_5.mp3","webmSizeKb":896,"mp3SizeKb":1490},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_001_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_001_6.mp3","webmSizeKb":238,"mp3SizeKb":398}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId277 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042002001 AS StartVerseId, 1042002012 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId277 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId277 int = COALESCE(@ExistingPassageId277, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId277 AS PassageId, @ResourceId277 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId278 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 2:13-17' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId278 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId278 int = COALESCE(@ExistingResourceId278, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId278 AS ResourceId, 1 AS LanguageId, 'Mark 2:13-17' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 2701 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_013_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_013_1.mp3","webmSizeKb":123,"mp3SizeKb":206},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_013_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_013_2.mp3","webmSizeKb":626,"mp3SizeKb":1029},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_013_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_013_3.mp3","webmSizeKb":349,"mp3SizeKb":575},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_013_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_013_4.mp3","webmSizeKb":593,"mp3SizeKb":1001},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_013_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_013_5.mp3","webmSizeKb":770,"mp3SizeKb":1275},{"step":6,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_013_6.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_013_6.mp3","webmSizeKb":240,"mp3SizeKb":402}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId278 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042002013 AS StartVerseId, 1042002017 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId278 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId278 int = COALESCE(@ExistingPassageId278, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId278 AS PassageId, @ResourceId278 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


DECLARE @ExistingResourceId279 int;
MERGE INTO [dbo].[Resources] AS Target
USING (SELECT 1 AS Type, 2 AS MediaType, 'CBBT-ER Mark 2:18-22' AS EnglishLabel) AS Source
ON Target.Type = Source.Type AND Target.MediaType = Source.MediaType AND Target.EnglishLabel = Source.EnglishLabel
WHEN MATCHED THEN
UPDATE SET @ExistingResourceId279 = Target.Id
WHEN NOT MATCHED THEN
INSERT (Type, MediaType, EnglishLabel) VALUES (Source.Type, Source.MediaType, Source.EnglishLabel)
OUTPUT Inserted.Id;
DECLARE @ResourceId279 int = COALESCE(@ExistingResourceId279, SCOPE_IDENTITY());

MERGE INTO [dbo].[ResourceContents] AS Target
USING (SELECT @ResourceId279 AS ResourceId, 1 AS LanguageId, 'Mark 2:18-22' AS DisplayName, 1 AS Version, 1 AS Completed, 1 AS Trusted, 1729 as ContentSizeKb, '{"steps":[{"step":1,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_018_1.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_018_1.mp3","webmSizeKb":115,"mp3SizeKb":193},{"step":2,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_018_2.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_018_2.mp3","webmSizeKb":580,"mp3SizeKb":949},{"step":3,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_018_3.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_018_3.mp3","webmSizeKb":290,"mp3SizeKb":476},{"step":4,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_018_4.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_018_4.mp3","webmSizeKb":324,"mp3SizeKb":538},{"step":5,"webmUrl":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/webm/ENG_CBBTER_42_002_018_5.webm","mp3Url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/audio/mp3/ENG_CBBTER_42_002_018_5.mp3","webmSizeKb":420,"mp3SizeKb":693}]}' as Content) AS Source
ON Target.ResourceId = Source.ResourceId AND Target.LanguageId = Source.LanguageId
WHEN NOT MATCHED THEN
INSERT (ResourceId, LanguageId, DisplayName, Content, Version, Completed, Trusted, ContentSizeKb) VALUES (Source.ResourceId, Source.LanguageId, Source.DisplayName, Source.Content, Source.Version, Source.Completed, Source.Trusted, Source.ContentSizeKb);

DECLARE @ExistingPassageId279 int;
MERGE INTO [dbo].[Passages] AS Target
USING (SELECT 1042002018 AS StartVerseId, 1042002022 AS EndVerseId) AS Source
ON Target.StartVerseId = Source.StartVerseId AND Target.EndVerseId = Source.EndVerseId
WHEN MATCHED THEN
UPDATE SET @ExistingPassageId279 = Target.Id
WHEN NOT MATCHED THEN
INSERT (StartVerseId, EndVerseId) VALUES (Source.StartVerseId, Source.EndVerseId)
OUTPUT Inserted.Id;
DECLARE @PassageId279 int = COALESCE(@ExistingPassageId279, SCOPE_IDENTITY());

MERGE INTO [dbo].[PassageResources] AS Target
USING (SELECT @PassageId279 AS PassageId, @ResourceId279 AS ResourceId) AS Source
ON Target.PassageId = Source.PassageId AND Target.ResourceId = Source.ResourceId
WHEN NOT MATCHED THEN
INSERT (PassageId, ResourceId) VALUES (Source.PassageId, Source.ResourceId);


      COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; -- Re-throws the error
    END CATCH
  
