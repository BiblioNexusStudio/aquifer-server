
BEGIN TRY
BEGIN TRANSACTION;
DECLARE @LanguageId INT;
DECLARE @ResourceId INT;
DECLARE @PassageId INT;
DECLARE @PassageResourceId INT;
DECLARE @ResourceContentId INT;

SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;
SELECT @LanguageId = Id FROM Languages WHERE UPPER(ISO6393Code) = 'TPI';


SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:1-13' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:1-13', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:1-13', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_1.webm","size":153317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_1.mp3","size":502252}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_2.webm","size":1227619},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_2.mp3","size":4035052}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_3.webm","size":333942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_3.mp3","size":1087852}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_4.webm","size":683704},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_4.mp3","size":2245036}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_5.webm","size":1896776},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_5.mp3","size":6228844}}]}', 4295358)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_1.webm","size":153317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_1.mp3","size":502252}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_2.webm","size":1227619},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_2.mp3","size":4035052}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_3.webm","size":333942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_3.mp3","size":1087852}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_4.webm","size":683704},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_4.mp3","size":2245036}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_001_5.webm","size":1896776},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_001_5.mp3","size":6228844}}]}', ContentSize = 4295358, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001001 AND EndVerseId = 1042001013
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001001, 1042001013)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:23-3:6' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:23-3:6', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:23-3:6', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_1.webm","size":194700},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_1.mp3","size":633836}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_2.webm","size":1296679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_2.mp3","size":4262060}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_3.webm","size":984723},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_3.mp3","size":3244652}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_4.webm","size":1163572},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_4.mp3","size":3822188}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_5.webm","size":1440835},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_5.mp3","size":4738796}}]}', 5080509)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:23-3:6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_1.webm","size":194700},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_1.mp3","size":633836}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_2.webm","size":1296679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_2.mp3","size":4262060}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_3.webm","size":984723},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_3.mp3","size":3244652}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_4.webm","size":1163572},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_4.mp3","size":3822188}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_023_5.webm","size":1440835},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_023_5.mp3","size":4738796}}]}', ContentSize = 5080509, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002023 AND EndVerseId = 1042003006
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002023, 1042003006)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:7-12' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:7-12', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 3:7-12', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_1.webm","size":163427},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_1.mp3","size":533804}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_2.webm","size":907284},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_2.mp3","size":2978540}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_3.webm","size":416741},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_3.mp3","size":1361708}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_4.webm","size":951470},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_4.mp3","size":3112364}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_5.webm","size":739923},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_5.mp3","size":2426924}}]}', 3178845)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 3:7-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_1.webm","size":163427},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_1.mp3","size":533804}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_2.webm","size":907284},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_2.mp3","size":2978540}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_3.webm","size":416741},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_3.mp3","size":1361708}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_4.webm","size":951470},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_4.mp3","size":3112364}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_007_5.webm","size":739923},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_007_5.mp3","size":2426924}}]}', ContentSize = 3178845, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042003007 AND EndVerseId = 1042003012
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042003007, 1042003012)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:13-19' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:13-19', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 3:13-19', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_1.webm","size":173366},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_1.mp3","size":566060}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_2.webm","size":693265},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_2.mp3","size":2256428}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_3.webm","size":330268},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_3.mp3","size":1073708}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_4.webm","size":388814},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_4.mp3","size":1274924}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_5.webm","size":758662},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_5.mp3","size":2475116}}]}', 2344375)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 3:13-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_1.webm","size":173366},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_1.mp3","size":566060}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_2.webm","size":693265},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_2.mp3","size":2256428}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_3.webm","size":330268},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_3.mp3","size":1073708}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_4.webm","size":388814},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_4.mp3","size":1274924}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_013_5.webm","size":758662},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_013_5.mp3","size":2475116}}]}', ContentSize = 2344375, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042003013 AND EndVerseId = 1042003019
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042003013, 1042003019)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:20-35' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:20-35', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 3:20-35', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_1.webm","size":168777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_1.mp3","size":554924}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_2.webm","size":947291},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_2.mp3","size":3100844}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_3.webm","size":867288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_3.mp3","size":2843372}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_4.webm","size":1005264},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_4.mp3","size":3289388}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_5.webm","size":1177755},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_5.mp3","size":3817580}}]}', 4166375)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 3:20-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_1.webm","size":168777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_1.mp3","size":554924}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_2.webm","size":947291},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_2.mp3","size":3100844}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_3.webm","size":867288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_3.mp3","size":2843372}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_4.webm","size":1005264},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_4.mp3","size":3289388}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_003_020_5.webm","size":1177755},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_003_020_5.mp3","size":3817580}}]}', ContentSize = 4166375, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042003020 AND EndVerseId = 1042003035
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042003020, 1042003035)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:1-20' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:1-20', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:1-20', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_1.webm","size":174237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_1.mp3","size":572588}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_2.webm","size":1612051},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_2.mp3","size":5261996}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_3.webm","size":1421671},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_3.mp3","size":4643372}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_4.webm","size":1539469},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_4.mp3","size":5013548}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_5.webm","size":2034130},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_5.mp3","size":6602156}}]}', 6781558)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_1.webm","size":174237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_1.mp3","size":572588}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_2.webm","size":1612051},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_2.mp3","size":5261996}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_3.webm","size":1421671},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_3.mp3","size":4643372}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_4.webm","size":1539469},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_4.mp3","size":5013548}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_001_5.webm","size":2034130},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_001_5.mp3","size":6602156}}]}', ContentSize = 6781558, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004001 AND EndVerseId = 1042004020
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004001, 1042004020)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:21-25' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:21-25', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:21-25', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_1.webm","size":156311},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_1.mp3","size":515948}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_2.webm","size":933560},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_2.mp3","size":3066092}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_3.webm","size":381159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_3.mp3","size":1249004}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_4.webm","size":644541},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_4.mp3","size":2107436}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_5.webm","size":707931},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_5.mp3","size":2312300}}]}', 2823502)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:21-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_1.webm","size":156311},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_1.mp3","size":515948}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_2.webm","size":933560},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_2.mp3","size":3066092}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_3.webm","size":381159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_3.mp3","size":1249004}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_4.webm","size":644541},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_4.mp3","size":2107436}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_021_5.webm","size":707931},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_021_5.mp3","size":2312300}}]}', ContentSize = 2823502, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004021 AND EndVerseId = 1042004025
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004021, 1042004025)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:26-34' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:26-34', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:26-34', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_1.webm","size":153482},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_1.mp3","size":502508}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_2.webm","size":1011170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_2.mp3","size":3289196}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_3.webm","size":492836},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_3.mp3","size":1610348}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_4.webm","size":868735},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_4.mp3","size":2829548}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_5.webm","size":844632},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_5.mp3","size":2762924}}]}', 3370855)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:26-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_1.webm","size":153482},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_1.mp3","size":502508}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_2.webm","size":1011170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_2.mp3","size":3289196}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_3.webm","size":492836},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_3.mp3","size":1610348}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_4.webm","size":868735},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_4.mp3","size":2829548}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_026_5.webm","size":844632},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_026_5.mp3","size":2762924}}]}', ContentSize = 3370855, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004026 AND EndVerseId = 1042004034
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004026, 1042004034)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:35-41' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:35-41', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:35-41', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_1.webm","size":166617},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_1.mp3","size":547244}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_2.webm","size":738064},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_2.mp3","size":2381228}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_3.webm","size":694758},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_3.mp3","size":2257196}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_4.webm","size":1071042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_4.mp3","size":3499436}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_5.webm","size":1245817},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_5.mp3","size":4061036}}]}', 3916298)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:35-41', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_1.webm","size":166617},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_1.mp3","size":547244}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_2.webm","size":738064},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_2.mp3","size":2381228}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_3.webm","size":694758},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_3.mp3","size":2257196}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_4.webm","size":1071042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_4.mp3","size":3499436}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_004_035_5.webm","size":1245817},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_004_035_5.mp3","size":4061036}}]}', ContentSize = 3916298, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004035 AND EndVerseId = 1042004041
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004035, 1042004041)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:1-20' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:1-20', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 5:1-20', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_1.webm","size":162928},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_1.mp3","size":538924}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_2.webm","size":806944},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_2.mp3","size":2660524}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_3.webm","size":1180150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_3.mp3","size":3866860}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_4.webm","size":1094701},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_4.mp3","size":3600364}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_5.webm","size":1632976},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_5.mp3","size":5311276}}]}', 4877699)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 5:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_1.webm","size":162928},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_1.mp3","size":538924}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_2.webm","size":806944},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_2.mp3","size":2660524}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_3.webm","size":1180150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_3.mp3","size":3866860}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_4.webm","size":1094701},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_4.mp3","size":3600364}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_001_5.webm","size":1632976},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_001_5.mp3","size":5311276}}]}', ContentSize = 4877699, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042005001 AND EndVerseId = 1042005020
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042005001, 1042005020)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:21-34' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:21-34', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 5:21-34', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_1.webm","size":168481},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_1.mp3","size":556844}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_2.webm","size":1370956},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_2.mp3","size":4494764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_3.webm","size":801940},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_3.mp3","size":2649452}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_4.webm","size":1237027},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_4.mp3","size":4038188}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_5.webm","size":1318379},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_5.mp3","size":4326956}}]}', 4896783)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 5:21-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_1.webm","size":168481},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_1.mp3","size":556844}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_2.webm","size":1370956},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_2.mp3","size":4494764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_3.webm","size":801940},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_3.mp3","size":2649452}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_4.webm","size":1237027},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_4.mp3","size":4038188}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_021_5.webm","size":1318379},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_021_5.mp3","size":4326956}}]}', ContentSize = 4896783, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042005021 AND EndVerseId = 1042005034
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042005021, 1042005034)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:35-43' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:35-43', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 5:35-43', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_1.webm","size":161810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_1.mp3","size":530732}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_2.webm","size":1165007},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_2.mp3","size":3793580}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_3.webm","size":754747},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_3.mp3","size":2466284}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_4.webm","size":1393274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_4.mp3","size":4558316}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_5.webm","size":957675},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_5.mp3","size":3121388}}]}', 4432513)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 5:35-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_1.webm","size":161810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_1.mp3","size":530732}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_2.webm","size":1165007},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_2.mp3","size":3793580}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_3.webm","size":754747},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_3.mp3","size":2466284}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_4.webm","size":1393274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_4.mp3","size":4558316}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_005_035_5.webm","size":957675},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_005_035_5.mp3","size":3121388}}]}', ContentSize = 4432513, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042005035 AND EndVerseId = 1042005043
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042005035, 1042005043)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:14-20' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:14-20', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:14-20', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_1.webm","size":158077},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_1.mp3","size":519340}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_2.webm","size":669869},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_2.mp3","size":2197036}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_3.webm","size":437953},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_3.mp3","size":1430380}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_4.webm","size":672881},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_4.mp3","size":2200492}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_5.webm","size":1393515},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_5.mp3","size":4529644}}]}', 3332295)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:14-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_1.webm","size":158077},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_1.mp3","size":519340}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_2.webm","size":669869},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_2.mp3","size":2197036}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_3.webm","size":437953},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_3.mp3","size":1430380}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_4.webm","size":672881},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_4.mp3","size":2200492}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_014_5.webm","size":1393515},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_014_5.mp3","size":4529644}}]}', ContentSize = 3332295, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001014 AND EndVerseId = 1042001020
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001014, 1042001020)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:1-6a' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:1-6a', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:1-6a', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_1.webm","size":157777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_1.mp3","size":519020}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_2.webm","size":653351},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_2.mp3","size":2135276}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_3.webm","size":628651},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_3.mp3","size":2048876}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_4.webm","size":663309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_4.mp3","size":2149676}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_5.webm","size":714417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_5.mp3","size":2322284}}]}', 2817505)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:1-6a', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_1.webm","size":157777},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_1.mp3","size":519020}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_2.webm","size":653351},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_2.mp3","size":2135276}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_3.webm","size":628651},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_3.mp3","size":2048876}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_4.webm","size":663309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_4.mp3","size":2149676}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_001_5.webm","size":714417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_001_5.mp3","size":2322284}}]}', ContentSize = 2817505, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006001 AND EndVerseId = 1042006006
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006001, 1042006006)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:6b-13' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:6b-13', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:6b-13', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_1.webm","size":160326},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_1.mp3","size":525548}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_2.webm","size":789697},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_2.mp3","size":2560172}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_3.webm","size":661353},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_3.mp3","size":2159468}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_4.webm","size":754532},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_4.mp3","size":2462636}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_5.webm","size":892516},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_5.mp3","size":2924396}}]}', 3258424)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:6b-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_1.webm","size":160326},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_1.mp3","size":525548}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_2.webm","size":789697},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_2.mp3","size":2560172}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_3.webm","size":661353},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_3.mp3","size":2159468}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_4.webm","size":754532},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_4.mp3","size":2462636}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_006_5.webm","size":892516},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_006_5.mp3","size":2924396}}]}', ContentSize = 3258424, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006006 AND EndVerseId = 1042006013
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006006, 1042006013)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:14-29' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:14-29', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:14-29', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_1.webm","size":163841},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_1.mp3","size":537452}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_2.webm","size":1073608},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_2.mp3","size":3508460}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_3.webm","size":1225467},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_3.mp3","size":3996140}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_4.webm","size":1263471},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_4.mp3","size":4126892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_5.webm","size":1373170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_5.mp3","size":4482476}}]}', 5099557)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:14-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_1.webm","size":163841},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_1.mp3","size":537452}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_2.webm","size":1073608},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_2.mp3","size":3508460}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_3.webm","size":1225467},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_3.mp3","size":3996140}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_4.webm","size":1263471},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_4.mp3","size":4126892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_014_5.webm","size":1373170},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_014_5.mp3","size":4482476}}]}', ContentSize = 5099557, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006014 AND EndVerseId = 1042006029
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006014, 1042006029)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:30-44' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:30-44', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:30-44', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_1.webm","size":164339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_1.mp3","size":538028}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_2.webm","size":1250317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_2.mp3","size":4070060}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_3.webm","size":978570},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_3.mp3","size":3181676}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_4.webm","size":1209887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_4.mp3","size":3926252}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_5.webm","size":864522},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_5.mp3","size":2812076}}]}', 4467635)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:30-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_1.webm","size":164339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_1.mp3","size":538028}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_2.webm","size":1250317},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_2.mp3","size":4070060}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_3.webm","size":978570},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_3.mp3","size":3181676}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_4.webm","size":1209887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_4.mp3","size":3926252}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_030_5.webm","size":864522},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_030_5.mp3","size":2812076}}]}', ContentSize = 4467635, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006030 AND EndVerseId = 1042006044
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006030, 1042006044)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:45-56' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:45-56', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:45-56', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_1.webm","size":169455},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_1.mp3","size":556460}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_2.webm","size":1314313},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_2.mp3","size":4285100}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_3.webm","size":970563},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_3.mp3","size":3145772}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_4.webm","size":870339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_4.mp3","size":2828012}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_5.webm","size":936368},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_5.mp3","size":3039404}}]}', 4261038)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:45-56', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_1.webm","size":169455},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_1.mp3","size":556460}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_2.webm","size":1314313},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_2.mp3","size":4285100}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_3.webm","size":970563},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_3.mp3","size":3145772}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_4.webm","size":870339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_4.mp3","size":2828012}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_006_045_5.webm","size":936368},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_006_045_5.mp3","size":3039404}}]}', ContentSize = 4261038, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006045 AND EndVerseId = 1042006056
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006045, 1042006056)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:1-8' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:1-8', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:1-8', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_1.webm","size":152439},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_1.mp3","size":500972}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_2.webm","size":1239073},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_2.mp3","size":4071788}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_3.webm","size":629975},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_3.mp3","size":2072300}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_4.webm","size":891395},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_4.mp3","size":2938028}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_5.webm","size":867026},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_5.mp3","size":2863148}}]}', 3779908)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_1.webm","size":152439},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_1.mp3","size":500972}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_2.webm","size":1239073},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_2.mp3","size":4071788}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_3.webm","size":629975},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_3.mp3","size":2072300}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_4.webm","size":891395},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_4.mp3","size":2938028}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_001_5.webm","size":867026},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_001_5.mp3","size":2863148}}]}', ContentSize = 3779908, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007001 AND EndVerseId = 1042007008
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007001, 1042007008)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:9-13' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:9-13', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:9-13', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_1.webm","size":149823},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_1.mp3","size":490412}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_2.webm","size":470177},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_2.mp3","size":1551404}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_3.webm","size":502973},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_3.mp3","size":1664876}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_4.webm","size":610978},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_4.mp3","size":2027372}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_5.webm","size":259042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_5.mp3","size":854828}}]}', 1992993)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:9-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_1.webm","size":149823},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_1.mp3","size":490412}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_2.webm","size":470177},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_2.mp3","size":1551404}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_3.webm","size":502973},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_3.mp3","size":1664876}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_4.webm","size":610978},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_4.mp3","size":2027372}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_009_5.webm","size":259042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_009_5.mp3","size":854828}}]}', ContentSize = 1992993, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007009 AND EndVerseId = 1042007013
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007009, 1042007013)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:14-23' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:14-23', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:14-23', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_1.webm","size":153283},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_1.mp3","size":504044}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_2.webm","size":906679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_2.mp3","size":3003116}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_3.webm","size":589274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_3.mp3","size":1925612}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_4.webm","size":683220},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_4.mp3","size":2247212}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_5.webm","size":748830},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_5.mp3","size":2468780}}]}', 3081286)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:14-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_1.webm","size":153283},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_1.mp3","size":504044}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_2.webm","size":906679},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_2.mp3","size":3003116}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_3.webm","size":589274},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_3.mp3","size":1925612}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_4.webm","size":683220},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_4.mp3","size":2247212}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_014_5.webm","size":748830},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_014_5.mp3","size":2468780}}]}', ContentSize = 3081286, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007014 AND EndVerseId = 1042007023
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007014, 1042007023)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:24-30' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:24-30', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:24-30', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_1.webm","size":147180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_1.mp3","size":483500}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_2.webm","size":1154712},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_2.mp3","size":3828140}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_3.webm","size":654497},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_3.mp3","size":2176364}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_4.webm","size":758809},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_4.mp3","size":2500076}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_5.webm","size":880429},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_5.mp3","size":2886956}}]}', 3595627)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:24-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_1.webm","size":147180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_1.mp3","size":483500}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_2.webm","size":1154712},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_2.mp3","size":3828140}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_3.webm","size":654497},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_3.mp3","size":2176364}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_4.webm","size":758809},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_4.mp3","size":2500076}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_007_024_5.webm","size":880429},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_007_024_5.mp3","size":2886956}}]}', ContentSize = 3595627, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007024 AND EndVerseId = 1042007030
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007024, 1042007030)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:1-10' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:1-10', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:1-10', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_1.webm","size":146245},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_1.mp3","size":480236}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_2.webm","size":525165},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_2.mp3","size":1717292}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_3.webm","size":644874},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_3.mp3","size":2103596}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_4.webm","size":819665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_4.mp3","size":2662892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_5.webm","size":599261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_5.mp3","size":1924460}}]}', 2735210)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:1-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_1.webm","size":146245},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_1.mp3","size":480236}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_2.webm","size":525165},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_2.mp3","size":1717292}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_3.webm","size":644874},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_3.mp3","size":2103596}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_4.webm","size":819665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_4.mp3","size":2662892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_001_5.webm","size":599261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_001_5.mp3","size":1924460}}]}', ContentSize = 2735210, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008001 AND EndVerseId = 1042008010
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008001, 1042008010)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:11-21' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:11-21', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:11-21', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_1.webm","size":149376},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_1.mp3","size":490796}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_2.webm","size":979115},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_2.mp3","size":3174764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_3.webm","size":965864},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_3.mp3","size":3124076}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_4.webm","size":951769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_4.mp3","size":3079340}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_5.webm","size":939986},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_5.mp3","size":3043244}}]}', 3986110)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:11-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_1.webm","size":149376},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_1.mp3","size":490796}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_2.webm","size":979115},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_2.mp3","size":3174764}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_3.webm","size":965864},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_3.mp3","size":3124076}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_4.webm","size":951769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_4.mp3","size":3079340}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_011_5.webm","size":939986},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_011_5.mp3","size":3043244}}]}', ContentSize = 3986110, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008011 AND EndVerseId = 1042008021
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008011, 1042008021)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:22-26' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:22-26', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:22-26', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_1.webm","size":151540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_1.mp3","size":499052}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_2.webm","size":899416},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_2.mp3","size":2893292}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_3.webm","size":526113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_3.mp3","size":1706348}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_4.webm","size":792049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_4.mp3","size":2573804}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_5.webm","size":284596},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_5.mp3","size":924140}}]}', 2653714)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:22-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_1.webm","size":151540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_1.mp3","size":499052}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_2.webm","size":899416},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_2.mp3","size":2893292}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_3.webm","size":526113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_3.mp3","size":1706348}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_4.webm","size":792049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_4.mp3","size":2573804}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_022_5.webm","size":284596},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_022_5.mp3","size":924140}}]}', ContentSize = 2653714, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008022 AND EndVerseId = 1042008026
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008022, 1042008026)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:27-30' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:27-30', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:27-30', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_1.webm","size":147110},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_1.mp3","size":483308}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_2.webm","size":861569},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_2.mp3","size":2782700}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_3.webm","size":506963},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_3.mp3","size":1651820}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_4.webm","size":759595},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_4.mp3","size":2444588}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_5.webm","size":401799},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_5.mp3","size":1300652}}]}', 2677036)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:27-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_1.webm","size":147110},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_1.mp3","size":483308}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_2.webm","size":861569},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_2.mp3","size":2782700}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_3.webm","size":506963},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_3.mp3","size":1651820}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_4.webm","size":759595},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_4.mp3","size":2444588}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_027_5.webm","size":401799},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_027_5.mp3","size":1300652}}]}', ContentSize = 2677036, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008027 AND EndVerseId = 1042008030
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008027, 1042008030)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:31-9:1' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:31-9:1', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:31-9:1', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_1.webm","size":152774},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_1.mp3","size":500972}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_2.webm","size":1542237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_2.mp3","size":4998188}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_3.webm","size":977957},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_3.mp3","size":3194540}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_4.webm","size":1257436},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_4.mp3","size":4119404}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_5.webm","size":1783102},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_5.mp3","size":5807468}}]}', 5713506)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:31-9:1', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_1.webm","size":152774},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_1.mp3","size":500972}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_2.webm","size":1542237},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_2.mp3","size":4998188}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_3.webm","size":977957},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_3.mp3","size":3194540}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_4.webm","size":1257436},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_4.mp3","size":4119404}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_008_031_5.webm","size":1783102},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_008_031_5.mp3","size":5807468}}]}', ContentSize = 5713506, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008031 AND EndVerseId = 1042009001
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008031, 1042009001)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:21-28' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:21-28', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:21-28', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_1.webm","size":176711},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_1.mp3","size":576556}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_2.webm","size":900646},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_2.mp3","size":2930284}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_3.webm","size":526440},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_3.mp3","size":1710124}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_4.webm","size":650641},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_4.mp3","size":2117164}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_5.webm","size":1180709},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_5.mp3","size":3830764}}]}', 3435147)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:21-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_1.webm","size":176711},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_1.mp3","size":576556}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_2.webm","size":900646},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_2.mp3","size":2930284}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_3.webm","size":526440},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_3.mp3","size":1710124}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_4.webm","size":650641},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_4.mp3","size":2117164}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_021_5.webm","size":1180709},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_021_5.mp3","size":3830764}}]}', ContentSize = 3435147, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001021 AND EndVerseId = 1042001028
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001021, 1042001028)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:2-13' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:2-13', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 9:2-13', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_1.webm","size":145687},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_1.mp3","size":477932}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_2.webm","size":1145346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_2.mp3","size":3775148}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_3.webm","size":876487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_3.mp3","size":2880620}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_4.webm","size":1355537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_4.mp3","size":4467116}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_5.webm","size":903408},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_5.mp3","size":2969708}}]}', 4426465)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 9:2-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_1.webm","size":145687},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_1.mp3","size":477932}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_2.webm","size":1145346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_2.mp3","size":3775148}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_3.webm","size":876487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_3.mp3","size":2880620}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_4.webm","size":1355537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_4.mp3","size":4467116}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_002_5.webm","size":903408},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_002_5.mp3","size":2969708}}]}', ContentSize = 4426465, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042009002 AND EndVerseId = 1042009013
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042009002, 1042009013)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:14-29' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:14-29', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 9:14-29', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_1.webm","size":149080},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_1.mp3","size":487532}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_2.webm","size":1272261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_2.mp3","size":4162988}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_3.webm","size":1300438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_3.mp3","size":4257260}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_4.webm","size":1219484},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_4.mp3","size":4014764}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_5.webm","size":882769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_5.mp3","size":2895980}}]}', 4824032)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 9:14-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_1.webm","size":149080},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_1.mp3","size":487532}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_2.webm","size":1272261},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_2.mp3","size":4162988}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_3.webm","size":1300438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_3.mp3","size":4257260}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_4.webm","size":1219484},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_4.mp3","size":4014764}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_014_5.webm","size":882769},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_014_5.mp3","size":2895980}}]}', ContentSize = 4824032, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042009014 AND EndVerseId = 1042009029
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042009014, 1042009029)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:30-50' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:30-50', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 9:30-50', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_1.webm","size":145571},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_1.mp3","size":476588}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_2.webm","size":1753845},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_2.mp3","size":5762156}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_3.webm","size":1021072},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_3.mp3","size":3359084}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_4.webm","size":1152405},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_4.mp3","size":3796076}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_5.webm","size":1464182},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_5.mp3","size":4823852}}]}', 5537075)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 9:30-50', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_1.webm","size":145571},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_1.mp3","size":476588}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_2.webm","size":1753845},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_2.mp3","size":5762156}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_3.webm","size":1021072},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_3.mp3","size":3359084}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_4.webm","size":1152405},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_4.mp3","size":3796076}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_009_030_5.webm","size":1464182},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_009_030_5.mp3","size":4823852}}]}', ContentSize = 5537075, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042009030 AND EndVerseId = 1042009050
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042009030, 1042009050)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:1-12' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:1-12', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:1-12', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_1.webm","size":139821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_1.mp3","size":458727}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_2.webm","size":1058718},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_2.mp3","size":3476601}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_3.webm","size":776113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_3.mp3","size":2549150}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_4.webm","size":1295049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_4.mp3","size":4269052}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_5.webm","size":722525},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_5.mp3","size":2385101}}]}', 3992226)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_1.webm","size":139821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_1.mp3","size":458727}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_2.webm","size":1058718},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_2.mp3","size":3476601}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_3.webm","size":776113},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_3.mp3","size":2549150}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_4.webm","size":1295049},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_4.mp3","size":4269052}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_001_5.webm","size":722525},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_001_5.mp3","size":2385101}}]}', ContentSize = 3992226, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010001 AND EndVerseId = 1042010012
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010001, 1042010012)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:13-31' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:13-31', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:13-31', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_1.webm","size":141082},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_1.mp3","size":461443}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_2.webm","size":1641915},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_2.mp3","size":5421783}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_3.webm","size":824827},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_3.mp3","size":2712154}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_4.webm","size":1602503},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_4.mp3","size":5282394}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_5.webm","size":757299},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_5.mp3","size":2494815}}]}', 4967626)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:13-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_1.webm","size":141082},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_1.mp3","size":461443}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_2.webm","size":1641915},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_2.mp3","size":5421783}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_3.webm","size":824827},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_3.mp3","size":2712154}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_4.webm","size":1602503},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_4.mp3","size":5282394}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_013_5.webm","size":757299},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_013_5.mp3","size":2494815}}]}', ContentSize = 4967626, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010013 AND EndVerseId = 1042010031
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010013, 1042010031)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:32-45' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:32-45', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:32-45', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_1.webm","size":138229},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_1.mp3","size":452666}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_2.webm","size":1290093},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_2.mp3","size":4240630}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_3.webm","size":821169},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_3.mp3","size":2701496}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_4.webm","size":1186150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_4.mp3","size":3908771}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_5.webm","size":1341145},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_5.mp3","size":4431429}}]}', 4776786)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:32-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_1.webm","size":138229},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_1.mp3","size":452666}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_2.webm","size":1290093},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_2.mp3","size":4240630}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_3.webm","size":821169},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_3.mp3","size":2701496}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_4.webm","size":1186150},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_4.mp3","size":3908771}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_032_5.webm","size":1341145},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_032_5.mp3","size":4431429}}]}', ContentSize = 4776786, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010032 AND EndVerseId = 1042010045
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010032, 1042010045)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:46-52' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:46-52', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:46-52', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_1.webm","size":145357},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_1.mp3","size":478998}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_2.webm","size":890091},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_2.mp3","size":2936598}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_3.webm","size":674855},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_3.mp3","size":2227321}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_4.webm","size":646724},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_4.mp3","size":2138087}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_5.webm","size":766057},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_5.mp3","size":2530968}}]}', 3123084)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:46-52', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_1.webm","size":145357},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_1.mp3","size":478998}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_2.webm","size":890091},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_2.mp3","size":2936598}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_3.webm","size":674855},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_3.mp3","size":2227321}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_4.webm","size":646724},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_4.mp3","size":2138087}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_010_046_5.webm","size":766057},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_010_046_5.mp3","size":2530968}}]}', ContentSize = 3123084, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010046 AND EndVerseId = 1042010052
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010046, 1042010052)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:1-11' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:1-11', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 11:1-11', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_1.webm","size":138437},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_1.mp3","size":454129}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_2.webm","size":1267042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_2.mp3","size":4177937}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_3.webm","size":935248},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_3.mp3","size":3077241}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_4.webm","size":1541378},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_4.mp3","size":5052516}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_5.webm","size":1117880},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_5.mp3","size":3670952}}]}', 4999985)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 11:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_1.webm","size":138437},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_1.mp3","size":454129}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_2.webm","size":1267042},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_2.mp3","size":4177937}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_3.webm","size":935248},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_3.mp3","size":3077241}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_4.webm","size":1541378},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_4.mp3","size":5052516}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_001_5.webm","size":1117880},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_001_5.mp3","size":3670952}}]}', ContentSize = 4999985, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042011001 AND EndVerseId = 1042011011
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042011001, 1042011011)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:12-26' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:12-26', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 11:12-26', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_1.webm","size":141438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_1.mp3","size":463742}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_2.webm","size":1359821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_2.mp3","size":4475732}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_3.webm","size":1067194},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_3.mp3","size":3518815}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_4.webm","size":1001643},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_4.mp3","size":3266368}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_5.webm","size":1362577},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_5.mp3","size":4463403}}]}', 4932673)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 11:12-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_1.webm","size":141438},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_1.mp3","size":463742}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_2.webm","size":1359821},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_2.mp3","size":4475732}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_3.webm","size":1067194},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_3.mp3","size":3518815}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_4.webm","size":1001643},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_4.mp3","size":3266368}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_012_5.webm","size":1362577},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_012_5.mp3","size":4463403}}]}', ContentSize = 4932673, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042011012 AND EndVerseId = 1042011026
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042011012, 1042011026)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:27-33' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:27-33', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 11:27-33', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_1.webm","size":142417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_1.mp3","size":466459}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_2.webm","size":1197539},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_2.mp3","size":3932177}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_3.webm","size":670895},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_3.mp3","size":2197228}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_4.webm","size":809346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_4.mp3","size":2647997}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_5.webm","size":597930},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_5.mp3","size":1957319}}]}', 3418127)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 11:27-33', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_1.webm","size":142417},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_1.mp3","size":466459}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_2.webm","size":1197539},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_2.mp3","size":3932177}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_3.webm","size":670895},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_3.mp3","size":2197228}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_4.webm","size":809346},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_4.mp3","size":2647997}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_011_027_5.webm","size":597930},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_011_027_5.mp3","size":1957319}}]}', ContentSize = 3418127, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042011027 AND EndVerseId = 1042011033
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042011027, 1042011033)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:1-12' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:1-12', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:1-12', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_1.webm","size":139665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_1.mp3","size":457473}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_2.webm","size":1636288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_2.mp3","size":5347595}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_3.webm","size":924912},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_3.mp3","size":3008278}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_4.webm","size":1013278},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_4.mp3","size":3306492}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_5.webm","size":898951},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_5.mp3","size":2931373}}]}', 4613094)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_1.webm","size":139665},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_1.mp3","size":457473}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_2.webm","size":1636288},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_2.mp3","size":5347595}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_3.webm","size":924912},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_3.mp3","size":3008278}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_4.webm","size":1013278},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_4.mp3","size":3306492}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_001_5.webm","size":898951},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_001_5.mp3","size":2931373}}]}', ContentSize = 4613094, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012001 AND EndVerseId = 1042012012
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012001, 1042012012)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:29-34' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:29-34', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:29-34', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_1.webm","size":188033},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_1.mp3","size":615916}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_2.webm","size":635396},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_2.mp3","size":2073580}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_3.webm","size":390153},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_3.mp3","size":1265644}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_4.webm","size":723582},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_4.mp3","size":2357548}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_5.webm","size":586136},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_5.mp3","size":1910956}}]}', 2523300)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:29-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_1.webm","size":188033},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_1.mp3","size":615916}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_2.webm","size":635396},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_2.mp3","size":2073580}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_3.webm","size":390153},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_3.mp3","size":1265644}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_4.webm","size":723582},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_4.mp3","size":2357548}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_029_5.webm","size":586136},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_029_5.mp3","size":1910956}}]}', ContentSize = 2523300, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001029 AND EndVerseId = 1042001034
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001029, 1042001034)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:13-17' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:13-17', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:13-17', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_1.webm","size":137644},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_1.mp3","size":452666}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_2.webm","size":1309540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_2.mp3","size":4324431}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_3.webm","size":717924},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_3.mp3","size":2360232}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_4.webm","size":661887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_4.mp3","size":2183435}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_5.webm","size":693214},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_5.mp3","size":2282492}}]}', 3520209)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_1.webm","size":137644},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_1.mp3","size":452666}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_2.webm","size":1309540},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_2.mp3","size":4324431}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_3.webm","size":717924},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_3.mp3","size":2360232}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_4.webm","size":661887},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_4.mp3","size":2183435}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_013_5.webm","size":693214},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_013_5.mp3","size":2282492}}]}', ContentSize = 3520209, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012013 AND EndVerseId = 1042012017
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012013, 1042012017)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:18-27' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:18-27', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:18-27', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_1.webm","size":136751},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_1.mp3","size":448278}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_2.webm","size":1173530},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_2.mp3","size":3851301}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_3.webm","size":1102159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_3.mp3","size":3607840}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_4.webm","size":847464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_4.mp3","size":2776519}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_5.webm","size":545806},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_5.mp3","size":1787001}}]}', 3805710)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:18-27', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_1.webm","size":136751},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_1.mp3","size":448278}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_2.webm","size":1173530},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_2.mp3","size":3851301}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_3.webm","size":1102159},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_3.mp3","size":3607840}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_4.webm","size":847464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_4.mp3","size":2776519}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_018_5.webm","size":545806},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_018_5.mp3","size":1787001}}]}', ContentSize = 3805710, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012018 AND EndVerseId = 1042012027
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012018, 1042012027)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:28-34' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:28-34', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:28-34', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_1.webm","size":135861},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_1.mp3","size":445561}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_2.webm","size":960202},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_2.mp3","size":3158325}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_3.webm","size":571235},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_3.mp3","size":1878534}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_4.webm","size":705335},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_4.mp3","size":2321362}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_5.webm","size":888309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_5.mp3","size":2933672}}]}', 3260942)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:28-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_1.webm","size":135861},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_1.mp3","size":445561}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_2.webm","size":960202},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_2.mp3","size":3158325}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_3.webm","size":571235},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_3.mp3","size":1878534}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_4.webm","size":705335},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_4.mp3","size":2321362}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_028_5.webm","size":888309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_028_5.mp3","size":2933672}}]}', ContentSize = 3260942, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012028 AND EndVerseId = 1042012034
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012028, 1042012034)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:35-37' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:35-37', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:35-37', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_1.webm","size":140079},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_1.mp3","size":461026}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_2.webm","size":790399},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_2.mp3","size":2605574}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_3.webm","size":519132},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_3.mp3","size":1713858}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_4.webm","size":491668},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_4.mp3","size":1621907}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_5.webm","size":562487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_5.mp3","size":1854292}}]}', 2503765)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:35-37', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_1.webm","size":140079},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_1.mp3","size":461026}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_2.webm","size":790399},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_2.mp3","size":2605574}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_3.webm","size":519132},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_3.mp3","size":1713858}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_4.webm","size":491668},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_4.mp3","size":1621907}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_035_5.webm","size":562487},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_035_5.mp3","size":1854292}}]}', ContentSize = 2503765, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012035 AND EndVerseId = 1042012037
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012035, 1042012037)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:38-44' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:38-44', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:38-44', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_1.webm","size":141988},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_1.mp3","size":464787}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_2.webm","size":815479},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_2.mp3","size":2694599}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_3.webm","size":490020},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_3.mp3","size":1617937}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_4.webm","size":706014},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_4.mp3","size":2326377}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_5.webm","size":514810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_5.mp3","size":1702573}}]}', 2668311)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:38-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_1.webm","size":141988},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_1.mp3","size":464787}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_2.webm","size":815479},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_2.mp3","size":2694599}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_3.webm","size":490020},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_3.mp3","size":1617937}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_4.webm","size":706014},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_4.mp3","size":2326377}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_012_038_5.webm","size":514810},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_012_038_5.mp3","size":1702573}}]}', ContentSize = 2668311, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012038 AND EndVerseId = 1042012044
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012038, 1042012044)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:35-39' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:35-39', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:35-39', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_1.webm","size":175939},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_1.mp3","size":573740}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_2.webm","size":897333},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_2.mp3","size":2907500}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_3.webm","size":377942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_3.mp3","size":1226924}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_4.webm","size":409537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_4.mp3","size":1322924}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_5.webm","size":816309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_5.mp3","size":2674796}}]}', 2677060)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:35-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_1.webm","size":175939},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_1.mp3","size":573740}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_2.webm","size":897333},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_2.mp3","size":2907500}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_3.webm","size":377942},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_3.mp3","size":1226924}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_4.webm","size":409537},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_4.mp3","size":1322924}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_035_5.webm","size":816309},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_035_5.mp3","size":2674796}}]}', ContentSize = 2677060, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001035 AND EndVerseId = 1042001039
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001035, 1042001039)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:40-45' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:40-45', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:40-45', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_1.webm","size":189742},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_1.mp3","size":620524}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_2.webm","size":906109},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_2.mp3","size":2944876}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_3.webm","size":558464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_3.mp3","size":1804780}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_4.webm","size":918409},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_4.mp3","size":3003820}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_5.webm","size":822168},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_5.mp3","size":2687788}}]}', 3394892)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:40-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_1.webm","size":189742},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_1.mp3","size":620524}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_2.webm","size":906109},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_2.mp3","size":2944876}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_3.webm","size":558464},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_3.mp3","size":1804780}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_4.webm","size":918409},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_4.mp3","size":3003820}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_001_040_5.webm","size":822168},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_001_040_5.mp3","size":2687788}}]}', ContentSize = 3394892, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001040 AND EndVerseId = 1042001045
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001040, 1042001045)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:1-12' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:1-12', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:1-12', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_1.mp3","size":553004}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_2.webm","size":1359926},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_2.mp3","size":4451180}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_3.webm","size":732303},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_3.mp3","size":2398316}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_4.webm","size":1008897},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_4.mp3","size":3305324}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_5.webm","size":1181036},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_5.mp3","size":3873260}}]}', 4451935)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_1.mp3","size":553004}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_2.webm","size":1359926},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_2.mp3","size":4451180}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_3.webm","size":732303},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_3.mp3","size":2398316}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_4.webm","size":1008897},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_4.mp3","size":3305324}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_001_5.webm","size":1181036},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_001_5.mp3","size":3873260}}]}', ContentSize = 4451935, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002001 AND EndVerseId = 1042002012
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002001, 1042002012)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:13-17' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:13-17', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:13-17', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_1.webm","size":155180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_1.mp3","size":510188}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_2.webm","size":1190512},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_2.mp3","size":3910124}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_3.webm","size":524217},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_3.mp3","size":1721708}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_4.webm","size":780943},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_4.mp3","size":2566892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_5.webm","size":1058511},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_5.mp3","size":3469676}}]}', 3709363)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_1.webm","size":155180},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_1.mp3","size":510188}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_2.webm","size":1190512},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_2.mp3","size":3910124}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_3.webm","size":524217},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_3.mp3","size":1721708}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_4.webm","size":780943},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_4.mp3","size":2566892}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_013_5.webm","size":1058511},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_013_5.mp3","size":3469676}}]}', ContentSize = 3709363, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002013 AND EndVerseId = 1042002017
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002013, 1042002017)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



SELECT @PassageId = NULL;
SELECT @ResourceId = NULL;
SELECT @ResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:18-22' AND MediaType = 2
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:18-22', 2)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:18-22', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_1.mp3","size":553004}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_2.webm","size":1070920},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_2.mp3","size":3509612}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_3.webm","size":584263},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_3.mp3","size":1901612}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_4.webm","size":683780},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_4.mp3","size":2226284}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_5.webm","size":715621},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_5.mp3","size":2338220}}]}', 3224357)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:18-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_1.webm","size":169773},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_1.mp3","size":553004}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_2.webm","size":1070920},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_2.mp3","size":3509612}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_3.webm","size":584263},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_3.mp3","size":1901612}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_4.webm","size":683780},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_4.mp3","size":2226284}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/webm/TPI_CBBTER_42_002_018_5.webm","size":715621},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/audio/mp3/TPI_CBBTER_42_002_018_5.mp3","size":2338220}}]}', ContentSize = 3224357, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002018 AND EndVerseId = 1042002022
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002018, 1042002022)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)
SELECT @PassageResourceId = @@IDENTITY
END



COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION;
THROW;
END CATCH

