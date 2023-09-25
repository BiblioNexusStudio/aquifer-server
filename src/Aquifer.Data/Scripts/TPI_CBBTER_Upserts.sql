
BEGIN TRY
BEGIN TRANSACTION;
DECLARE @AudioResourceId INT;
DECLARE @TextResourceId INT;
DECLARE @AudioResourceContentId INT;
DECLARE @TextResourceContentId INT;
DECLARE @LanguageId INT;
DECLARE @PassageId INT;

SELECT @LanguageId = Id FROM Languages WHERE UPPER(ISO6393Code) = 'TPI'
SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042001001 AND [EndVerseId] = 1042001013;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042001001, 1042001013);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:1-13' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:1-13', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 1:1-13', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_1.webm","size":153317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_1.mp3","size":251308}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_2.webm","size":1227619},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_2.mp3","size":2017708}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_3.webm","size":333942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_3.mp3","size":544108}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_4.webm","size":683704},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_4.mp3","size":1122700}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_5.webm","size":1896776},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_5.mp3","size":3114604}}]}', 4295358);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:1-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_1.webm","size":153317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_1.mp3","size":251308}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_2.webm","size":1227619},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_2.mp3","size":2017708}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_3.webm","size":333942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_3.mp3","size":544108}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_4.webm","size":683704},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_4.mp3","size":1122700}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_001_5.webm","size":1896776},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_001_5.mp3","size":3114604}}]}', [ContentSize] = 4295358, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:1-13' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:1-13', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 1:1-13', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_001.json"}', 19481);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:1-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_001.json"}', [ContentSize] = 19481, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042002023 AND [EndVerseId] = 1042003006;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042002023, 1042003006);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:23-3:6' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:23-3:6', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 2:23-3:6', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_1.webm","size":194700},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_1.mp3","size":317036}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_2.webm","size":1296679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_2.mp3","size":2131148}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_3.webm","size":984723},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_3.mp3","size":1622444}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_4.webm","size":1163572},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_4.mp3","size":1911212}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_5.webm","size":1440835},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_5.mp3","size":2369516}}]}', 5080509);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:23-3:6', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_1.webm","size":194700},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_1.mp3","size":317036}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_2.webm","size":1296679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_2.mp3","size":2131148}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_3.webm","size":984723},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_3.mp3","size":1622444}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_4.webm","size":1163572},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_4.mp3","size":1911212}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_023_5.webm","size":1440835},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_023_5.mp3","size":2369516}}]}', [ContentSize] = 5080509, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:23-3:6' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:23-3:6', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 2:23-3:6', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_023.json"}', 22189);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:23-3:6', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_023.json"}', [ContentSize] = 22189, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042003007 AND [EndVerseId] = 1042003012;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042003007, 1042003012);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 3:7-12' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 3:7-12', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 3:7-12', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_1.webm","size":163427},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_1.mp3","size":267020}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_2.webm","size":907284},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_2.mp3","size":1489388}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_3.webm","size":416741},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_3.mp3","size":680972}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_4.webm","size":951470},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_4.mp3","size":1556300}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_5.webm","size":739923},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_5.mp3","size":1213580}}]}', 3178845);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 3:7-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_1.webm","size":163427},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_1.mp3","size":267020}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_2.webm","size":907284},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_2.mp3","size":1489388}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_3.webm","size":416741},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_3.mp3","size":680972}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_4.webm","size":951470},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_4.mp3","size":1556300}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_007_5.webm","size":739923},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_007_5.mp3","size":1213580}}]}', [ContentSize] = 3178845, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 3:7-12' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 3:7-12', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 3:7-12', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_003_007.json"}', 14300);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 3:7-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_003_007.json"}', [ContentSize] = 14300, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042003013 AND [EndVerseId] = 1042003019;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042003013, 1042003019);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 3:13-19' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 3:13-19', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 3:13-19', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_1.webm","size":173366},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_1.mp3","size":283148}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_2.webm","size":693265},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_2.mp3","size":1128332}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_3.webm","size":330268},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_3.mp3","size":536972}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_4.webm","size":388814},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_4.mp3","size":637580}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_5.webm","size":758662},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_5.mp3","size":1237676}}]}', 2344375);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 3:13-19', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_1.webm","size":173366},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_1.mp3","size":283148}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_2.webm","size":693265},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_2.mp3","size":1128332}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_3.webm","size":330268},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_3.mp3","size":536972}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_4.webm","size":388814},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_4.mp3","size":637580}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_013_5.webm","size":758662},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_013_5.mp3","size":1237676}}]}', [ContentSize] = 2344375, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 3:13-19' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 3:13-19', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 3:13-19', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_003_013.json"}', 10950);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 3:13-19', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_003_013.json"}', [ContentSize] = 10950, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042003020 AND [EndVerseId] = 1042003035;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042003020, 1042003035);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 3:20-35' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 3:20-35', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 3:20-35', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_1.webm","size":168777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_1.mp3","size":277580}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_2.webm","size":947291},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_2.mp3","size":1550540}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_3.webm","size":867288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_3.mp3","size":1421804}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_4.webm","size":1005264},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_4.mp3","size":1644812}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_5.webm","size":1177755},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_5.mp3","size":1908908}}]}', 4166375);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 3:20-35', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_1.webm","size":168777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_1.mp3","size":277580}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_2.webm","size":947291},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_2.mp3","size":1550540}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_3.webm","size":867288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_3.mp3","size":1421804}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_4.webm","size":1005264},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_4.mp3","size":1644812}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_003_020_5.webm","size":1177755},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_003_020_5.mp3","size":1908908}}]}', [ContentSize] = 4166375, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 3:20-35' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 3:20-35', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 3:20-35', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_003_020.json"}', 19569);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 3:20-35', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_003_020.json"}', [ContentSize] = 19569, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042004001 AND [EndVerseId] = 1042004020;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042004001, 1042004020);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:1-20' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:1-20', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 4:1-20', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_1.webm","size":174237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_1.mp3","size":286412}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_2.webm","size":1612051},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_2.mp3","size":2631116}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_3.webm","size":1421671},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_3.mp3","size":2321804}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_4.webm","size":1539469},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_4.mp3","size":2506892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_5.webm","size":2034130},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_5.mp3","size":3301196}}]}', 6781558);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:1-20', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_1.webm","size":174237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_1.mp3","size":286412}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_2.webm","size":1612051},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_2.mp3","size":2631116}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_3.webm","size":1421671},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_3.mp3","size":2321804}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_4.webm","size":1539469},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_4.mp3","size":2506892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_001_5.webm","size":2034130},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_001_5.mp3","size":3301196}}]}', [ContentSize] = 6781558, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:1-20' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:1-20', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 4:1-20', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_001.json"}', 30801);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:1-20', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_001.json"}', [ContentSize] = 30801, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042004021 AND [EndVerseId] = 1042004025;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042004021, 1042004025);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:21-25' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:21-25', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 4:21-25', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_1.webm","size":156311},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_1.mp3","size":258092}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_2.webm","size":933560},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_2.mp3","size":1533164}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_3.webm","size":381159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_3.mp3","size":624620}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_4.webm","size":644541},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_4.mp3","size":1053836}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_5.webm","size":707931},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_5.mp3","size":1156268}}]}', 2823502);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:21-25', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_1.webm","size":156311},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_1.mp3","size":258092}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_2.webm","size":933560},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_2.mp3","size":1533164}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_3.webm","size":381159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_3.mp3","size":624620}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_4.webm","size":644541},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_4.mp3","size":1053836}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_021_5.webm","size":707931},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_021_5.mp3","size":1156268}}]}', [ContentSize] = 2823502, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:21-25' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:21-25', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 4:21-25', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_021.json"}', 13093);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:21-25', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_021.json"}', [ContentSize] = 13093, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042004026 AND [EndVerseId] = 1042004034;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042004026, 1042004034);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:26-34' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:26-34', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 4:26-34', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_1.webm","size":153482},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_1.mp3","size":251372}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_2.webm","size":1011170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_2.mp3","size":1644716}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_3.webm","size":492836},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_3.mp3","size":805292}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_4.webm","size":868735},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_4.mp3","size":1414892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_5.webm","size":844632},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_5.mp3","size":1381580}}]}', 3370855);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:26-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_1.webm","size":153482},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_1.mp3","size":251372}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_2.webm","size":1011170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_2.mp3","size":1644716}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_3.webm","size":492836},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_3.mp3","size":805292}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_4.webm","size":868735},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_4.mp3","size":1414892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_026_5.webm","size":844632},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_026_5.mp3","size":1381580}}]}', [ContentSize] = 3370855, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:26-34' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:26-34', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 4:26-34', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_026.json"}', 14915);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:26-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_026.json"}', [ContentSize] = 14915, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042004035 AND [EndVerseId] = 1042004041;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042004035, 1042004041);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:35-41' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:35-41', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 4:35-41', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_1.webm","size":166617},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_1.mp3","size":273740}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_2.webm","size":738064},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_2.mp3","size":1190732}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_3.webm","size":694758},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_3.mp3","size":1128716}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_4.webm","size":1071042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_4.mp3","size":1749836}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_5.webm","size":1245817},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_5.mp3","size":2030636}}]}', 3916298);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:35-41', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_1.webm","size":166617},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_1.mp3","size":273740}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_2.webm","size":738064},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_2.mp3","size":1190732}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_3.webm","size":694758},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_3.mp3","size":1128716}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_4.webm","size":1071042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_4.mp3","size":1749836}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_004_035_5.webm","size":1245817},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_004_035_5.mp3","size":2030636}}]}', [ContentSize] = 3916298, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 4:35-41' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 4:35-41', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 4:35-41', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_035.json"}', 17797);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 4:35-41', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_004_035.json"}', [ContentSize] = 17797, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042005001 AND [EndVerseId] = 1042005020;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042005001, 1042005020);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 5:1-20' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 5:1-20', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 5:1-20', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_1.webm","size":162928},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_1.mp3","size":269644}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_2.webm","size":806944},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_2.mp3","size":1330444}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_3.webm","size":1180150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_3.mp3","size":1933612}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_4.webm","size":1094701},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_4.mp3","size":1800364}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_5.webm","size":1632976},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_5.mp3","size":2655820}}]}', 4877699);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 5:1-20', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_1.webm","size":162928},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_1.mp3","size":269644}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_2.webm","size":806944},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_2.mp3","size":1330444}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_3.webm","size":1180150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_3.mp3","size":1933612}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_4.webm","size":1094701},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_4.mp3","size":1800364}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_001_5.webm","size":1632976},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_001_5.mp3","size":2655820}}]}', [ContentSize] = 4877699, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 5:1-20' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 5:1-20', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 5:1-20', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_005_001.json"}', 23256);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 5:1-20', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_005_001.json"}', [ContentSize] = 23256, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042005021 AND [EndVerseId] = 1042005034;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042005021, 1042005034);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 5:21-34' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 5:21-34', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 5:21-34', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_1.webm","size":168481},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_1.mp3","size":278540}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_2.webm","size":1370956},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_2.mp3","size":2247500}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_3.webm","size":801940},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_3.mp3","size":1324844}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_4.webm","size":1237027},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_4.mp3","size":2019212}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_5.webm","size":1318379},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_5.mp3","size":2163596}}]}', 4896783);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 5:21-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_1.webm","size":168481},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_1.mp3","size":278540}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_2.webm","size":1370956},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_2.mp3","size":2247500}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_3.webm","size":801940},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_3.mp3","size":1324844}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_4.webm","size":1237027},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_4.mp3","size":2019212}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_021_5.webm","size":1318379},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_021_5.mp3","size":2163596}}]}', [ContentSize] = 4896783, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 5:21-34' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 5:21-34', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 5:21-34', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_005_021.json"}', 22354);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 5:21-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_005_021.json"}', [ContentSize] = 22354, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042005035 AND [EndVerseId] = 1042005043;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042005035, 1042005043);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 5:35-43' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 5:35-43', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 5:35-43', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_1.webm","size":161810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_1.mp3","size":265484}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_2.webm","size":1165007},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_2.mp3","size":1896908}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_3.webm","size":754747},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_3.mp3","size":1233260}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_4.webm","size":1393274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_4.mp3","size":2279276}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_5.webm","size":957675},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_5.mp3","size":1560812}}]}', 4432513);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 5:35-43', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_1.webm","size":161810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_1.mp3","size":265484}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_2.webm","size":1165007},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_2.mp3","size":1896908}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_3.webm","size":754747},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_3.mp3","size":1233260}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_4.webm","size":1393274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_4.mp3","size":2279276}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_005_035_5.webm","size":957675},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_005_035_5.mp3","size":1560812}}]}', [ContentSize] = 4432513, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 5:35-43' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 5:35-43', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 5:35-43', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_005_035.json"}', 20330);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 5:35-43', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_005_035.json"}', [ContentSize] = 20330, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042001014 AND [EndVerseId] = 1042001020;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042001014, 1042001020);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:14-20' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:14-20', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 1:14-20', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_1.webm","size":158077},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_1.mp3","size":259852}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_2.webm","size":669869},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_2.mp3","size":1098700}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_3.webm","size":437953},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_3.mp3","size":715372}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_4.webm","size":672881},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_4.mp3","size":1100428}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_5.webm","size":1393515},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_5.mp3","size":2265004}}]}', 3332295);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:14-20', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_1.webm","size":158077},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_1.mp3","size":259852}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_2.webm","size":669869},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_2.mp3","size":1098700}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_3.webm","size":437953},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_3.mp3","size":715372}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_4.webm","size":672881},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_4.mp3","size":1100428}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_014_5.webm","size":1393515},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_014_5.mp3","size":2265004}}]}', [ContentSize] = 3332295, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:14-20' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:14-20', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 1:14-20', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_014.json"}', 15258);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:14-20', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_014.json"}', [ContentSize] = 15258, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042006001 AND [EndVerseId] = 1042006006;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042006001, 1042006006);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:1-6a' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:1-6a', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 6:1-6a', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_1.webm","size":157777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_1.mp3","size":259628}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_2.webm","size":653351},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_2.mp3","size":1067756}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_3.webm","size":628651},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_3.mp3","size":1024556}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_4.webm","size":663309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_4.mp3","size":1074956}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_5.webm","size":714417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_5.mp3","size":1161260}}]}', 2817505);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:1-6a', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_1.webm","size":157777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_1.mp3","size":259628}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_2.webm","size":653351},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_2.mp3","size":1067756}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_3.webm","size":628651},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_3.mp3","size":1024556}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_4.webm","size":663309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_4.mp3","size":1074956}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_001_5.webm","size":714417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_001_5.mp3","size":1161260}}]}', [ContentSize] = 2817505, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:1-6a' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:1-6a', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 6:1-6a', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_001.json"}', 12710);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:1-6a', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_001.json"}', [ContentSize] = 12710, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042006006 AND [EndVerseId] = 1042006013;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042006006, 1042006013);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:6b-13' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:6b-13', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 6:6b-13', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_1.webm","size":160326},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_1.mp3","size":262892}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_2.webm","size":789697},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_2.mp3","size":1280204}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_3.webm","size":661353},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_3.mp3","size":1079852}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_4.webm","size":754532},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_4.mp3","size":1231436}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_5.webm","size":892516},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_5.mp3","size":1462316}}]}', 3258424);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:6b-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_1.webm","size":160326},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_1.mp3","size":262892}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_2.webm","size":789697},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_2.mp3","size":1280204}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_3.webm","size":661353},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_3.mp3","size":1079852}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_4.webm","size":754532},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_4.mp3","size":1231436}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_006_5.webm","size":892516},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_006_5.mp3","size":1462316}}]}', [ContentSize] = 3258424, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:6b-13' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:6b-13', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 6:6b-13', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_006.json"}', 14873);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:6b-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_006.json"}', [ContentSize] = 14873, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042006014 AND [EndVerseId] = 1042006029;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042006014, 1042006029);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:14-29' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:14-29', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 6:14-29', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_1.webm","size":163841},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_1.mp3","size":268844}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_2.webm","size":1073608},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_2.mp3","size":1754348}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_3.webm","size":1225467},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_3.mp3","size":1998188}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_4.webm","size":1263471},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_4.mp3","size":2063564}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_5.webm","size":1373170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_5.mp3","size":2241356}}]}', 5099557);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:14-29', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_1.webm","size":163841},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_1.mp3","size":268844}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_2.webm","size":1073608},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_2.mp3","size":1754348}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_3.webm","size":1225467},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_3.mp3","size":1998188}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_4.webm","size":1263471},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_4.mp3","size":2063564}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_014_5.webm","size":1373170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_014_5.mp3","size":2241356}}]}', [ContentSize] = 5099557, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:14-29' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:14-29', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 6:14-29', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_014.json"}', 22097);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:14-29', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_014.json"}', [ContentSize] = 22097, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042006030 AND [EndVerseId] = 1042006044;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042006030, 1042006044);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:30-44' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:30-44', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 6:30-44', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_1.webm","size":164339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_1.mp3","size":269132}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_2.webm","size":1250317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_2.mp3","size":2035148}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_3.webm","size":978570},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_3.mp3","size":1590956}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_4.webm","size":1209887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_4.mp3","size":1963244}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_5.webm","size":864522},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_5.mp3","size":1406156}}]}', 4467635);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:30-44', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_1.webm","size":164339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_1.mp3","size":269132}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_2.webm","size":1250317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_2.mp3","size":2035148}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_3.webm","size":978570},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_3.mp3","size":1590956}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_4.webm","size":1209887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_4.mp3","size":1963244}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_030_5.webm","size":864522},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_030_5.mp3","size":1406156}}]}', [ContentSize] = 4467635, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:30-44' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:30-44', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 6:30-44', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_030.json"}', 18913);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:30-44', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_030.json"}', [ContentSize] = 18913, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042006045 AND [EndVerseId] = 1042006056;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042006045, 1042006056);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:45-56' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:45-56', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 6:45-56', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_1.webm","size":169455},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_1.mp3","size":278348}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_2.webm","size":1314313},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_2.mp3","size":2142668}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_3.webm","size":970563},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_3.mp3","size":1573004}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_4.webm","size":870339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_4.mp3","size":1414124}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_5.webm","size":936368},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_5.mp3","size":1519820}}]}', 4261038);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:45-56', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_1.webm","size":169455},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_1.mp3","size":278348}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_2.webm","size":1314313},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_2.mp3","size":2142668}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_3.webm","size":970563},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_3.mp3","size":1573004}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_4.webm","size":870339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_4.mp3","size":1414124}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_006_045_5.webm","size":936368},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_006_045_5.mp3","size":1519820}}]}', [ContentSize] = 4261038, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 6:45-56' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 6:45-56', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 6:45-56', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_045.json"}', 18016);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 6:45-56', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_006_045.json"}', [ContentSize] = 18016, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042007001 AND [EndVerseId] = 1042007008;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042007001, 1042007008);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:1-8' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:1-8', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 7:1-8', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_1.webm","size":152439},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_1.mp3","size":250604}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_2.webm","size":1239073},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_2.mp3","size":2036012}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_3.webm","size":629975},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_3.mp3","size":1036268}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_4.webm","size":891395},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_4.mp3","size":1469132}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_5.webm","size":867026},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_5.mp3","size":1431692}}]}', 3779908);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:1-8', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_1.webm","size":152439},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_1.mp3","size":250604}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_2.webm","size":1239073},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_2.mp3","size":2036012}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_3.webm","size":629975},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_3.mp3","size":1036268}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_4.webm","size":891395},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_4.mp3","size":1469132}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_001_5.webm","size":867026},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_001_5.mp3","size":1431692}}]}', [ContentSize] = 3779908, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:1-8' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:1-8', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 7:1-8', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_001.json"}', 18043);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:1-8', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_001.json"}', [ContentSize] = 18043, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042007009 AND [EndVerseId] = 1042007013;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042007009, 1042007013);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:9-13' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:9-13', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 7:9-13', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_1.webm","size":149823},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_1.mp3","size":245324}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_2.webm","size":470177},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_2.mp3","size":775820}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_3.webm","size":502973},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_3.mp3","size":832556}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_4.webm","size":610978},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_4.mp3","size":1013804}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_5.webm","size":259042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_5.mp3","size":427532}}]}', 1992993);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:9-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_1.webm","size":149823},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_1.mp3","size":245324}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_2.webm","size":470177},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_2.mp3","size":775820}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_3.webm","size":502973},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_3.mp3","size":832556}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_4.webm","size":610978},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_4.mp3","size":1013804}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_009_5.webm","size":259042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_009_5.mp3","size":427532}}]}', [ContentSize] = 1992993, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:9-13' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:9-13', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 7:9-13', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_009.json"}', 9411);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:9-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_009.json"}', [ContentSize] = 9411, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042007014 AND [EndVerseId] = 1042007023;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042007014, 1042007023);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:14-23' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:14-23', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 7:14-23', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_1.webm","size":153283},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_1.mp3","size":252140}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_2.webm","size":906679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_2.mp3","size":1501676}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_3.webm","size":589274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_3.mp3","size":962924}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_4.webm","size":683220},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_4.mp3","size":1123724}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_5.webm","size":748830},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_5.mp3","size":1234508}}]}', 3081286);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:14-23', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_1.webm","size":153283},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_1.mp3","size":252140}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_2.webm","size":906679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_2.mp3","size":1501676}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_3.webm","size":589274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_3.mp3","size":962924}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_4.webm","size":683220},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_4.mp3","size":1123724}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_014_5.webm","size":748830},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_014_5.mp3","size":1234508}}]}', [ContentSize] = 3081286, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:14-23' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:14-23', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 7:14-23', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_014.json"}', 15159);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:14-23', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_014.json"}', [ContentSize] = 15159, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042007024 AND [EndVerseId] = 1042007030;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042007024, 1042007030);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:24-30' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:24-30', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 7:24-30', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_1.webm","size":147180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_1.mp3","size":241868}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_2.webm","size":1154712},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_2.mp3","size":1914188}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_3.webm","size":654497},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_3.mp3","size":1088300}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_4.webm","size":758809},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_4.mp3","size":1250156}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_5.webm","size":880429},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_5.mp3","size":1443596}}]}', 3595627);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:24-30', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_1.webm","size":147180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_1.mp3","size":241868}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_2.webm","size":1154712},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_2.mp3","size":1914188}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_3.webm","size":654497},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_3.mp3","size":1088300}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_4.webm","size":758809},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_4.mp3","size":1250156}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_007_024_5.webm","size":880429},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_007_024_5.mp3","size":1443596}}]}', [ContentSize] = 3595627, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 7:24-30' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 7:24-30', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 7:24-30', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_024.json"}', 16824);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 7:24-30', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_007_024.json"}', [ContentSize] = 16824, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042008001 AND [EndVerseId] = 1042008010;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042008001, 1042008010);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:1-10' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:1-10', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 8:1-10', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_1.webm","size":146245},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_1.mp3","size":240236}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_2.webm","size":525165},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_2.mp3","size":858764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_3.webm","size":644874},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_3.mp3","size":1051916}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_4.webm","size":819665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_4.mp3","size":1331564}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_5.webm","size":599261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_5.mp3","size":962348}}]}', 2735210);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:1-10', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_1.webm","size":146245},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_1.mp3","size":240236}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_2.webm","size":525165},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_2.mp3","size":858764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_3.webm","size":644874},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_3.mp3","size":1051916}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_4.webm","size":819665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_4.mp3","size":1331564}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_001_5.webm","size":599261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_001_5.mp3","size":962348}}]}', [ContentSize] = 2735210, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:1-10' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:1-10', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 8:1-10', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_001.json"}', 12889);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:1-10', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_001.json"}', [ContentSize] = 12889, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042008011 AND [EndVerseId] = 1042008021;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042008011, 1042008021);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:11-21' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:11-21', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 8:11-21', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_1.webm","size":149376},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_1.mp3","size":245516}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_2.webm","size":979115},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_2.mp3","size":1587500}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_3.webm","size":965864},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_3.mp3","size":1562156}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_4.webm","size":951769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_4.mp3","size":1539788}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_5.webm","size":939986},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_5.mp3","size":1521740}}]}', 3986110);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:11-21', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_1.webm","size":149376},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_1.mp3","size":245516}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_2.webm","size":979115},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_2.mp3","size":1587500}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_3.webm","size":965864},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_3.mp3","size":1562156}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_4.webm","size":951769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_4.mp3","size":1539788}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_011_5.webm","size":939986},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_011_5.mp3","size":1521740}}]}', [ContentSize] = 3986110, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:11-21' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:11-21', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 8:11-21', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_011.json"}', 17207);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:11-21', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_011.json"}', [ContentSize] = 17207, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042008022 AND [EndVerseId] = 1042008026;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042008022, 1042008026);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:22-26' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:22-26', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 8:22-26', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_1.webm","size":151540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_1.mp3","size":249644}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_2.webm","size":899416},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_2.mp3","size":1446764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_3.webm","size":526113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_3.mp3","size":853292}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_4.webm","size":792049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_4.mp3","size":1287020}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_5.webm","size":284596},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_5.mp3","size":462188}}]}', 2653714);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:22-26', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_1.webm","size":151540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_1.mp3","size":249644}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_2.webm","size":899416},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_2.mp3","size":1446764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_3.webm","size":526113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_3.mp3","size":853292}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_4.webm","size":792049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_4.mp3","size":1287020}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_022_5.webm","size":284596},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_022_5.mp3","size":462188}}]}', [ContentSize] = 2653714, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:22-26' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:22-26', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 8:22-26', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_022.json"}', 12460);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:22-26', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_022.json"}', [ContentSize] = 12460, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042008027 AND [EndVerseId] = 1042008030;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042008027, 1042008030);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:27-30' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:27-30', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 8:27-30', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_1.webm","size":147110},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_1.mp3","size":241772}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_2.webm","size":861569},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_2.mp3","size":1391468}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_3.webm","size":506963},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_3.mp3","size":826028}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_4.webm","size":759595},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_4.mp3","size":1222412}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_5.webm","size":401799},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_5.mp3","size":650444}}]}', 2677036);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:27-30', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_1.webm","size":147110},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_1.mp3","size":241772}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_2.webm","size":861569},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_2.mp3","size":1391468}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_3.webm","size":506963},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_3.mp3","size":826028}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_4.webm","size":759595},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_4.mp3","size":1222412}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_027_5.webm","size":401799},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_027_5.mp3","size":650444}}]}', [ContentSize] = 2677036, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:27-30' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:27-30', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 8:27-30', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_027.json"}', 12561);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:27-30', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_027.json"}', [ContentSize] = 12561, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042008031 AND [EndVerseId] = 1042009001;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042008031, 1042009001);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:31-9:1' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:31-9:1', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 8:31-9:1', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_1.webm","size":152774},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_1.mp3","size":250604}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_2.webm","size":1542237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_2.mp3","size":2499212}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_3.webm","size":977957},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_3.mp3","size":1597388}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_4.webm","size":1257436},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_4.mp3","size":2059820}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_5.webm","size":1783102},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_5.mp3","size":2903852}}]}', 5713506);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:31-9:1', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_1.webm","size":152774},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_1.mp3","size":250604}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_2.webm","size":1542237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_2.mp3","size":2499212}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_3.webm","size":977957},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_3.mp3","size":1597388}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_4.webm","size":1257436},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_4.mp3","size":2059820}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_008_031_5.webm","size":1783102},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_008_031_5.mp3","size":2903852}}]}', [ContentSize] = 5713506, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 8:31-9:1' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 8:31-9:1', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 8:31-9:1', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_031.json"}', 27056);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 8:31-9:1', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_008_031.json"}', [ContentSize] = 27056, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042001021 AND [EndVerseId] = 1042001028;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042001021, 1042001028);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:21-28' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:21-28', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 1:21-28', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_1.webm","size":176711},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_1.mp3","size":288460}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_2.webm","size":900646},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_2.mp3","size":1465324}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_3.webm","size":526440},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_3.mp3","size":855244}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_4.webm","size":650641},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_4.mp3","size":1058764}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_5.webm","size":1180709},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_5.mp3","size":1915564}}]}', 3435147);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:21-28', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_1.webm","size":176711},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_1.mp3","size":288460}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_2.webm","size":900646},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_2.mp3","size":1465324}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_3.webm","size":526440},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_3.mp3","size":855244}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_4.webm","size":650641},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_4.mp3","size":1058764}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_021_5.webm","size":1180709},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_021_5.mp3","size":1915564}}]}', [ContentSize] = 3435147, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:21-28' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:21-28', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 1:21-28', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_021.json"}', 16179);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:21-28', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_021.json"}', [ContentSize] = 16179, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042009002 AND [EndVerseId] = 1042009013;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042009002, 1042009013);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 9:2-13' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 9:2-13', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 9:2-13', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_1.webm","size":145687},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_1.mp3","size":239084}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_2.webm","size":1145346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_2.mp3","size":1887692}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_3.webm","size":876487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_3.mp3","size":1440428}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_4.webm","size":1355537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_4.mp3","size":2233676}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_5.webm","size":903408},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_5.mp3","size":1484972}}]}', 4426465);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 9:2-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_1.webm","size":145687},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_1.mp3","size":239084}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_2.webm","size":1145346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_2.mp3","size":1887692}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_3.webm","size":876487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_3.mp3","size":1440428}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_4.webm","size":1355537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_4.mp3","size":2233676}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_002_5.webm","size":903408},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_002_5.mp3","size":1484972}}]}', [ContentSize] = 4426465, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 9:2-13' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 9:2-13', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 9:2-13', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_009_002.json"}', 20436);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 9:2-13', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_009_002.json"}', [ContentSize] = 20436, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042009014 AND [EndVerseId] = 1042009029;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042009014, 1042009029);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 9:14-29' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 9:14-29', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 9:14-29', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_1.webm","size":149080},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_1.mp3","size":243884}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_2.webm","size":1272261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_2.mp3","size":2081612}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_3.webm","size":1300438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_3.mp3","size":2128748}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_4.webm","size":1219484},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_4.mp3","size":2007500}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_5.webm","size":882769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_5.mp3","size":1448108}}]}', 4824032);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 9:14-29', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_1.webm","size":149080},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_1.mp3","size":243884}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_2.webm","size":1272261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_2.mp3","size":2081612}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_3.webm","size":1300438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_3.mp3","size":2128748}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_4.webm","size":1219484},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_4.mp3","size":2007500}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_014_5.webm","size":882769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_014_5.mp3","size":1448108}}]}', [ContentSize] = 4824032, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 9:14-29' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 9:14-29', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 9:14-29', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_009_014.json"}', 22477);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 9:14-29', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_009_014.json"}', [ContentSize] = 22477, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042009030 AND [EndVerseId] = 1042009050;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042009030, 1042009050);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 9:30-50' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 9:30-50', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 9:30-50', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_1.webm","size":145571},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_1.mp3","size":238412}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_2.webm","size":1753845},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_2.mp3","size":2881196}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_3.webm","size":1021072},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_3.mp3","size":1679660}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_4.webm","size":1152405},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_4.mp3","size":1898156}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_5.webm","size":1464182},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_5.mp3","size":2412044}}]}', 5537075);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 9:30-50', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_1.webm","size":145571},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_1.mp3","size":238412}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_2.webm","size":1753845},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_2.mp3","size":2881196}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_3.webm","size":1021072},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_3.mp3","size":1679660}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_4.webm","size":1152405},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_4.mp3","size":1898156}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_009_030_5.webm","size":1464182},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_009_030_5.mp3","size":2412044}}]}', [ContentSize] = 5537075, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 9:30-50' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 9:30-50', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 9:30-50', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_009_030.json"}', 25897);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 9:30-50', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_009_030.json"}', [ContentSize] = 25897, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042010001 AND [EndVerseId] = 1042010012;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042010001, 1042010012);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:1-12' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:1-12', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 10:1-12', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_1.webm","size":139821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_1.mp3","size":229477}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_2.webm","size":1058718},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_2.mp3","size":1738414}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_3.webm","size":776113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_3.mp3","size":1274688}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_4.webm","size":1295049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_4.mp3","size":2134639}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_5.webm","size":722525},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_5.mp3","size":1192664}}]}', 3992226);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:1-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_1.webm","size":139821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_1.mp3","size":229477}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_2.webm","size":1058718},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_2.mp3","size":1738414}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_3.webm","size":776113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_3.mp3","size":1274688}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_4.webm","size":1295049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_4.mp3","size":2134639}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_001_5.webm","size":722525},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_001_5.mp3","size":1192664}}]}', [ContentSize] = 3992226, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:1-12' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:1-12', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 10:1-12', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_001.json"}', 20336);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:1-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_001.json"}', [ContentSize] = 20336, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042010013 AND [EndVerseId] = 1042010031;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042010013, 1042010031);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:13-31' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:13-31', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 10:13-31', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_1.webm","size":141082},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_1.mp3","size":230835}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_2.webm","size":1641915},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_2.mp3","size":2711005}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_3.webm","size":824827},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_3.mp3","size":1356190}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_4.webm","size":1602503},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_4.mp3","size":2641310}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_5.webm","size":757299},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_5.mp3","size":1247521}}]}', 4967626);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:13-31', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_1.webm","size":141082},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_1.mp3","size":230835}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_2.webm","size":1641915},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_2.mp3","size":2711005}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_3.webm","size":824827},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_3.mp3","size":1356190}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_4.webm","size":1602503},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_4.mp3","size":2641310}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_013_5.webm","size":757299},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_013_5.mp3","size":1247521}}]}', [ContentSize] = 4967626, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:13-31' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:13-31', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 10:13-31', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_013.json"}', 25476);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:13-31', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_013.json"}', [ContentSize] = 25476, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042010032 AND [EndVerseId] = 1042010045;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042010032, 1042010045);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:32-45' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:32-45', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 10:32-45', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_1.webm","size":138229},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_1.mp3","size":226446}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_2.webm","size":1290093},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_2.mp3","size":2120428}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_3.webm","size":821169},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_3.mp3","size":1350861}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_4.webm","size":1186150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_4.mp3","size":1954499}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_5.webm","size":1341145},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_5.mp3","size":2215828}}]}', 4776786);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:32-45', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_1.webm","size":138229},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_1.mp3","size":226446}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_2.webm","size":1290093},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_2.mp3","size":2120428}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_3.webm","size":821169},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_3.mp3","size":1350861}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_4.webm","size":1186150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_4.mp3","size":1954499}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_032_5.webm","size":1341145},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_032_5.mp3","size":2215828}}]}', [ContentSize] = 4776786, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:32-45' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:32-45', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 10:32-45', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_032.json"}', 24445);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:32-45', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_032.json"}', [ContentSize] = 24445, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042010046 AND [EndVerseId] = 1042010052;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042010046, 1042010052);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:46-52' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:46-52', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 10:46-52', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_1.webm","size":145357},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_1.mp3","size":239612}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_2.webm","size":890091},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_2.mp3","size":1468412}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_3.webm","size":674855},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_3.mp3","size":1113774}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_4.webm","size":646724},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_4.mp3","size":1069157}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_5.webm","size":766057},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_5.mp3","size":1265597}}]}', 3123084);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:46-52', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_1.webm","size":145357},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_1.mp3","size":239612}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_2.webm","size":890091},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_2.mp3","size":1468412}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_3.webm","size":674855},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_3.mp3","size":1113774}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_4.webm","size":646724},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_4.mp3","size":1069157}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_010_046_5.webm","size":766057},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_010_046_5.mp3","size":1265597}}]}', [ContentSize] = 3123084, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 10:46-52' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 10:46-52', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 10:46-52', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_046.json"}', 16655);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 10:46-52', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_010_046.json"}', [ContentSize] = 16655, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042011001 AND [EndVerseId] = 1042011011;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042011001, 1042011011);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 11:1-11' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 11:1-11', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 11:1-11', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_1.webm","size":138437},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_1.mp3","size":227178}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_2.webm","size":1267042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_2.mp3","size":2089082}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_3.webm","size":935248},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_3.mp3","size":1538734}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_4.webm","size":1541378},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_4.mp3","size":2526371}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_5.webm","size":1117880},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_5.mp3","size":1835589}}]}', 4999985);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 11:1-11', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_1.webm","size":138437},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_1.mp3","size":227178}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_2.webm","size":1267042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_2.mp3","size":2089082}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_3.webm","size":935248},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_3.mp3","size":1538734}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_4.webm","size":1541378},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_4.mp3","size":2526371}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_001_5.webm","size":1117880},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_001_5.mp3","size":1835589}}]}', [ContentSize] = 4999985, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 11:1-11' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 11:1-11', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 11:1-11', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_011_001.json"}', 25508);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 11:1-11', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_011_001.json"}', [ContentSize] = 25508, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042011012 AND [EndVerseId] = 1042011026;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042011012, 1042011026);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 11:12-26' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 11:12-26', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 11:12-26', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_1.webm","size":141438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_1.mp3","size":231984}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_2.webm","size":1359821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_2.mp3","size":2237979}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_3.webm","size":1067194},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_3.mp3","size":1759521}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_4.webm","size":1001643},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_4.mp3","size":1633297}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_5.webm","size":1362577},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_5.mp3","size":2231815}}]}', 4932673);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 11:12-26', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_1.webm","size":141438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_1.mp3","size":231984}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_2.webm","size":1359821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_2.mp3","size":2237979}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_3.webm","size":1067194},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_3.mp3","size":1759521}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_4.webm","size":1001643},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_4.mp3","size":1633297}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_012_5.webm","size":1362577},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_012_5.mp3","size":2231815}}]}', [ContentSize] = 4932673, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 11:12-26' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 11:12-26', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 11:12-26', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_011_012.json"}', 25382);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 11:12-26', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_011_012.json"}', [ContentSize] = 25382, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042011027 AND [EndVerseId] = 1042011033;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042011027, 1042011033);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 11:27-33' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 11:27-33', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 11:27-33', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_1.webm","size":142417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_1.mp3","size":233343}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_2.webm","size":1197539},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_2.mp3","size":1966202}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_3.webm","size":670895},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_3.mp3","size":1098727}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_4.webm","size":809346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_4.mp3","size":1324112}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_5.webm","size":597930},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_5.mp3","size":978773}}]}', 3418127);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 11:27-33', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_1.webm","size":142417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_1.mp3","size":233343}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_2.webm","size":1197539},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_2.mp3","size":1966202}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_3.webm","size":670895},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_3.mp3","size":1098727}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_4.webm","size":809346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_4.mp3","size":1324112}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_011_027_5.webm","size":597930},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_011_027_5.mp3","size":978773}}]}', [ContentSize] = 3418127, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 11:27-33' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 11:27-33', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 11:27-33', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_011_027.json"}', 17423);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 11:27-33', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_011_027.json"}', [ContentSize] = 17423, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042012001 AND [EndVerseId] = 1042012012;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042012001, 1042012012);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:1-12' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:1-12', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 12:1-12', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_1.webm","size":139665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_1.mp3","size":228850}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_2.webm","size":1636288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_2.mp3","size":2673911}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_3.webm","size":924912},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_3.mp3","size":1504252}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_4.webm","size":1013278},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_4.mp3","size":1653359}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_5.webm","size":898951},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_5.mp3","size":1465800}}]}', 4613094);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:1-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_1.webm","size":139665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_1.mp3","size":228850}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_2.webm","size":1636288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_2.mp3","size":2673911}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_3.webm","size":924912},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_3.mp3","size":1504252}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_4.webm","size":1013278},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_4.mp3","size":1653359}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_001_5.webm","size":898951},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_001_5.mp3","size":1465800}}]}', [ContentSize] = 4613094, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:1-12' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:1-12', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 12:1-12', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_001.json"}', 24157);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:1-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_001.json"}', [ContentSize] = 24157, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042001029 AND [EndVerseId] = 1042001034;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042001029, 1042001034);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:29-34' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:29-34', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 1:29-34', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_1.webm","size":188033},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_1.mp3","size":308140}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_2.webm","size":635396},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_2.mp3","size":1036972}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_3.webm","size":390153},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_3.mp3","size":633004}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_4.webm","size":723582},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_4.mp3","size":1178956}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_5.webm","size":586136},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_5.mp3","size":955660}}]}', 2523300);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:29-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_1.webm","size":188033},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_1.mp3","size":308140}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_2.webm","size":635396},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_2.mp3","size":1036972}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_3.webm","size":390153},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_3.mp3","size":633004}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_4.webm","size":723582},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_4.mp3","size":1178956}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_029_5.webm","size":586136},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_029_5.mp3","size":955660}}]}', [ContentSize] = 2523300, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:29-34' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:29-34', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 1:29-34', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_029.json"}', 11139);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:29-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_029.json"}', [ContentSize] = 11139, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042012013 AND [EndVerseId] = 1042012017;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042012013, 1042012017);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:13-17' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:13-17', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 12:13-17', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_1.webm","size":137644},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_1.mp3","size":226446}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_2.webm","size":1309540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_2.mp3","size":2162329}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_3.webm","size":717924},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_3.mp3","size":1180229}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_4.webm","size":661887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_4.mp3","size":1091831}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_5.webm","size":693214},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_5.mp3","size":1141359}}]}', 3520209);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:13-17', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_1.webm","size":137644},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_1.mp3","size":226446}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_2.webm","size":1309540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_2.mp3","size":2162329}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_3.webm","size":717924},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_3.mp3","size":1180229}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_4.webm","size":661887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_4.mp3","size":1091831}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_013_5.webm","size":693214},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_013_5.mp3","size":1141359}}]}', [ContentSize] = 3520209, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:13-17' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:13-17', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 12:13-17', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_013.json"}', 17886);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:13-17', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_013.json"}', [ContentSize] = 17886, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042012018 AND [EndVerseId] = 1042012027;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042012018, 1042012027);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:18-27' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:18-27', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 12:18-27', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_1.webm","size":136751},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_1.mp3","size":224252}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_2.webm","size":1173530},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_2.mp3","size":1925764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_3.webm","size":1102159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_3.mp3","size":1804033}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_4.webm","size":847464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_4.mp3","size":1388373}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_5.webm","size":545806},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_5.mp3","size":893614}}]}', 3805710);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:18-27', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_1.webm","size":136751},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_1.mp3","size":224252}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_2.webm","size":1173530},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_2.mp3","size":1925764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_3.webm","size":1102159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_3.mp3","size":1804033}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_4.webm","size":847464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_4.mp3","size":1388373}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_018_5.webm","size":545806},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_018_5.mp3","size":893614}}]}', [ContentSize] = 3805710, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:18-27' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:18-27', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 12:18-27', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_018.json"}', 19028);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:18-27', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_018.json"}', [ContentSize] = 19028, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042012028 AND [EndVerseId] = 1042012034;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042012028, 1042012034);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:28-34' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:28-34', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 12:28-34', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_1.webm","size":135861},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_1.mp3","size":222894}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_2.webm","size":960202},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_2.mp3","size":1579276}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_3.webm","size":571235},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_3.mp3","size":939380}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_4.webm","size":705335},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_4.mp3","size":1160794}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_5.webm","size":888309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_5.mp3","size":1466949}}]}', 3260942);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:28-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_1.webm","size":135861},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_1.mp3","size":222894}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_2.webm","size":960202},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_2.mp3","size":1579276}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_3.webm","size":571235},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_3.mp3","size":939380}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_4.webm","size":705335},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_4.mp3","size":1160794}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_028_5.webm","size":888309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_028_5.mp3","size":1466949}}]}', [ContentSize] = 3260942, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:28-34' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:28-34', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 12:28-34', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_028.json"}', 16775);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:28-34', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_028.json"}', [ContentSize] = 16775, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042012035 AND [EndVerseId] = 1042012037;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042012035, 1042012037);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:35-37' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:35-37', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 12:35-37', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_1.webm","size":140079},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_1.mp3","size":230626}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_2.webm","size":790399},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_2.mp3","size":1302900}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_3.webm","size":519132},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_3.mp3","size":857042}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_4.webm","size":491668},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_4.mp3","size":811067}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_5.webm","size":562487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_5.mp3","size":927259}}]}', 2503765);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:35-37', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_1.webm","size":140079},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_1.mp3","size":230626}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_2.webm","size":790399},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_2.mp3","size":1302900}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_3.webm","size":519132},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_3.mp3","size":857042}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_4.webm","size":491668},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_4.mp3","size":811067}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_035_5.webm","size":562487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_035_5.mp3","size":927259}}]}', [ContentSize] = 2503765, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:35-37' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:35-37', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 12:35-37', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_035.json"}', 13129);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:35-37', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_035.json"}', [ContentSize] = 13129, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042012038 AND [EndVerseId] = 1042012044;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042012038, 1042012044);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:38-44' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:38-44', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 12:38-44', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_1.webm","size":141988},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_1.mp3","size":232507}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_2.webm","size":815479},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_2.mp3","size":1347413}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_3.webm","size":490020},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_3.mp3","size":809082}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_4.webm","size":706014},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_4.mp3","size":1163302}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_5.webm","size":514810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_5.mp3","size":851400}}]}', 2668311);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:38-44', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_1.webm","size":141988},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_1.mp3","size":232507}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_2.webm","size":815479},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_2.mp3","size":1347413}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_3.webm","size":490020},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_3.mp3","size":809082}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_4.webm","size":706014},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_4.mp3","size":1163302}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_012_038_5.webm","size":514810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_012_038_5.mp3","size":851400}}]}', [ContentSize] = 2668311, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 12:38-44' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 12:38-44', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 12:38-44', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_038.json"}', 13893);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 12:38-44', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_012_038.json"}', [ContentSize] = 13893, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042001035 AND [EndVerseId] = 1042001039;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042001035, 1042001039);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:35-39' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:35-39', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 1:35-39', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_1.webm","size":175939},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_1.mp3","size":286988}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_2.webm","size":897333},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_2.mp3","size":1453868}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_3.webm","size":377942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_3.mp3","size":613580}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_4.webm","size":409537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_4.mp3","size":661580}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_5.webm","size":816309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_5.mp3","size":1337516}}]}', 2677060);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:35-39', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_1.webm","size":175939},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_1.mp3","size":286988}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_2.webm","size":897333},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_2.mp3","size":1453868}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_3.webm","size":377942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_3.mp3","size":613580}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_4.webm","size":409537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_4.mp3","size":661580}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_035_5.webm","size":816309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_035_5.mp3","size":1337516}}]}', [ContentSize] = 2677060, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:35-39' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:35-39', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 1:35-39', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_035.json"}', 11090);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:35-39', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_035.json"}', [ContentSize] = 11090, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042001040 AND [EndVerseId] = 1042001045;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042001040, 1042001045);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:40-45' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:40-45', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 1:40-45', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_1.webm","size":189742},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_1.mp3","size":310444}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_2.webm","size":906109},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_2.mp3","size":1472620}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_3.webm","size":558464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_3.mp3","size":902572}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_4.webm","size":918409},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_4.mp3","size":1502092}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_5.webm","size":822168},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_5.mp3","size":1344076}}]}', 3394892);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:40-45', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_1.webm","size":189742},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_1.mp3","size":310444}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_2.webm","size":906109},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_2.mp3","size":1472620}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_3.webm","size":558464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_3.mp3","size":902572}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_4.webm","size":918409},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_4.mp3","size":1502092}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_001_040_5.webm","size":822168},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_001_040_5.mp3","size":1344076}}]}', [ContentSize] = 3394892, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 1:40-45' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 1:40-45', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 1:40-45', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_040.json"}', 14491);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 1:40-45', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_001_040.json"}', [ContentSize] = 14491, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042002001 AND [EndVerseId] = 1042002012;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042002001, 1042002012);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:1-12' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:1-12', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 2:1-12', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_1.mp3","size":276620}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_2.webm","size":1359926},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_2.mp3","size":2225708}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_3.webm","size":732303},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_3.mp3","size":1199276}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_4.webm","size":1008897},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_4.mp3","size":1652780}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_5.webm","size":1181036},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_5.mp3","size":1936748}}]}', 4451935);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:1-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_1.mp3","size":276620}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_2.webm","size":1359926},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_2.mp3","size":2225708}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_3.webm","size":732303},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_3.mp3","size":1199276}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_4.webm","size":1008897},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_4.mp3","size":1652780}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_001_5.webm","size":1181036},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_001_5.mp3","size":1936748}}]}', [ContentSize] = 4451935, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:1-12' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:1-12', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 2:1-12', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_001.json"}', 20931);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:1-12', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_001.json"}', [ContentSize] = 20931, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042002013 AND [EndVerseId] = 1042002017;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042002013, 1042002017);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:13-17' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:13-17', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 2:13-17', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_1.webm","size":155180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_1.mp3","size":255212}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_2.webm","size":1190512},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_2.mp3","size":1955180}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_3.webm","size":524217},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_3.mp3","size":860972}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_4.webm","size":780943},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_4.mp3","size":1283564}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_5.webm","size":1058511},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_5.mp3","size":1734956}}]}', 3709363);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:13-17', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_1.webm","size":155180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_1.mp3","size":255212}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_2.webm","size":1190512},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_2.mp3","size":1955180}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_3.webm","size":524217},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_3.mp3","size":860972}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_4.webm","size":780943},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_4.mp3","size":1283564}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_013_5.webm","size":1058511},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_013_5.mp3","size":1734956}}]}', [ContentSize] = 3709363, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:13-17' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:13-17', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 2:13-17', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_013.json"}', 17448);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:13-17', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_013.json"}', [ContentSize] = 17448, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

SELECT @PassageId = NULL;
SELECT @PassageId = Id FROM Passages WHERE [StartVerseId] = 1042002018 AND [EndVerseId] = 1042002022;
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages([StartVerseId], [EndVerseId]) VALUES (1042002018, 1042002022);
SELECT @PassageId = @@IDENTITY;
END

SELECT @AudioResourceId = NULL;
SELECT @AudioResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:18-22' AND [MediaType] = 2;
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:18-22', 2);
SELECT @AudioResourceId = @@IDENTITY;
END

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @AudioResourceId AND [LanguageId] = @LanguageId;
IF @AudioResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@AudioResourceId, @LanguageId, N'Mak 2:18-22', 1, 1, 1, N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_1.mp3","size":276620}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_2.webm","size":1070920},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_2.mp3","size":1754924}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_3.webm","size":584263},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_3.mp3","size":950924}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_4.webm","size":683780},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_4.mp3","size":1113260}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_5.webm","size":715621},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_5.mp3","size":1169228}}]}', 3224357);
SELECT @AudioResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:18-22', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_1.mp3","size":276620}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_2.webm","size":1070920},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_2.mp3","size":1754924}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_3.webm","size":584263},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_3.mp3","size":950924}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_4.webm","size":683780},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_4.mp3","size":1113260}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_042_MRK_002_018_5.webm","size":715621},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_042_MRK_002_018_5.mp3","size":1169228}}]}', [ContentSize] = 3224357, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @AudioResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @AudioResourceId);
END

SELECT @TextResourceId = NULL;
SELECT @TextResourceId = Id FROM Resources WHERE [Type] = 1 AND [EnglishLabel] = N'CBBT-ER Mark 2:18-22' AND [MediaType] = 1;
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources([Type], [EnglishLabel], [MediaType]) VALUES (1, N'CBBT-ER Mark 2:18-22', 1);
SELECT @TextResourceId = @@IDENTITY;
END

SELECT @TextResourceContentId = NULL;
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE [ResourceId] = @TextResourceId AND [LanguageId] = @LanguageId;
IF @TextResourceContentId IS NULL
BEGIN
INSERT INTO ResourceContents([ResourceId], [LanguageId], [DisplayName], [Version], [Completed], [Trusted], [Content], [ContentSize]) VALUES (@TextResourceId, @LanguageId, N'Mak 2:18-22', 1, 1, 1, N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_018.json"}', 14396);
SELECT @TextResourceContentId = @@IDENTITY;
END
ELSE
UPDATE ResourceContents SET [DisplayName] = N'Mak 2:18-22', [Version] = 1, [Completed] = 1, [Trusted] = 1, [Content] = N'{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_042_MRK_002_018.json"}', [ContentSize] = 14396, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId;

IF NOT EXISTS (SELECT 1 FROM PassageResources WHERE [PassageId] = @PassageId AND [ResourceId] = @TextResourceId)
BEGIN
INSERT INTO PassageResources([PassageId], [ResourceId]) VALUES (@PassageId, @TextResourceId);
END

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION;
THROW;
END CATCH
