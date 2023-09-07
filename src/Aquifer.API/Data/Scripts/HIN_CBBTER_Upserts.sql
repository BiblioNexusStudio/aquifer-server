
BEGIN TRY
BEGIN TRANSACTION;
DECLARE @AudioResourceContentId INT;
DECLARE @AudioResourceId INT;
DECLARE @LanguageId INT;
DECLARE @PassageId INT;
DECLARE @PassageResourceId INT;
DECLARE @TextResourceContentId INT;
DECLARE @TextResourceId INT;

SELECT @AudioResourceContentId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @LanguageId = Id FROM Languages WHERE UPPER(ISO6393Code) = 'HIN';
SELECT @PassageId = NULL;
SELECT @PassageResourceId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @TextResourceId = NULL;

SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 11:14-32' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 11:14-32', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 11:14-32' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 11:14-32', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'लूका 11:14-32', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_1.webm","size":161763},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_1.mp3","size":283184}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_2.webm","size":1814364},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_2.mp3","size":3014652}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_3.webm","size":999223},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_3.mp3","size":1630580}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_4.webm","size":1282131},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_4.mp3","size":2082812}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_5.webm","size":1245177},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_5.mp3","size":2019178}}]}', 5502658)
ELSE
UPDATE ResourceContents SET DisplayName = N'लूका 11:14-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_1.webm","size":161763},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_1.mp3","size":283184}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_2.webm","size":1814364},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_2.mp3","size":3014652}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_3.webm","size":999223},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_3.mp3","size":1630580}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_4.webm","size":1282131},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_4.mp3","size":2082812}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_043_LUK_011_014_5.webm","size":1245177},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_043_LUK_011_014_5.mp3","size":2019178}}]}', ContentSize = 5502658, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'लूका 11:14-32', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_043_LUK_011_014.json"}', 59436)
ELSE
UPDATE ResourceContents SET DisplayName = N'लूका 11:14-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_043_LUK_011_014.json"}', ContentSize = 59436, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043011014 AND EndVerseId = 1043011032
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043011014, 1043011032)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:1-13' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:1-13', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:1-13' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:1-13', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 1:1-13', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_1.webm","size":129376},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_1.mp3","size":216552}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_2.webm","size":1023602},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_2.mp3","size":1699944}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_3.webm","size":244094},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_3.mp3","size":409512}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_4.webm","size":534133},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_4.mp3","size":893928}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_5.webm","size":1451195},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_5.mp3","size":2403101}}]}', 3382400)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_1.webm","size":129376},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_1.mp3","size":216552}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_2.webm","size":1023602},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_2.mp3","size":1699944}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_3.webm","size":244094},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_3.mp3","size":409512}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_4.webm","size":534133},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_4.mp3","size":893928}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_001_5.webm","size":1451195},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_001_5.mp3","size":2403101}}]}', ContentSize = 3382400, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 1:1-13', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_001.json"}', 38506)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_001.json"}', ContentSize = 38506, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001001 AND EndVerseId = 1042001013
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001001, 1042001013)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:23-3:6' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:23-3:6', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:23-3:6' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:23-3:6', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 2:23-3:6', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_1.webm","size":118195},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_1.mp3","size":200456}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_2.webm","size":996007},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_2.mp3","size":1657044}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_3.webm","size":812209},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_3.mp3","size":1369070}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_4.webm","size":974951},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_4.mp3","size":1637818}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_5.webm","size":981335},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_5.mp3","size":1671255}}]}', 3882697)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:23-3:6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_1.webm","size":118195},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_1.mp3","size":200456}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_2.webm","size":996007},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_2.mp3","size":1657044}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_3.webm","size":812209},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_3.mp3","size":1369070}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_4.webm","size":974951},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_4.mp3","size":1637818}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_023_5.webm","size":981335},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_023_5.mp3","size":1671255}}]}', ContentSize = 3882697, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 2:23-3:6', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_023.json"}', 45246)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:23-3:6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_023.json"}', ContentSize = 45246, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002023 AND EndVerseId = 1042003006
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002023, 1042003006)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:14-20' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:14-20', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:14-20' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:14-20', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 1:14-20', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_1.webm","size":116168},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_1.mp3","size":192548}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_2.webm","size":570268},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_2.mp3","size":944110}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_3.webm","size":275370},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_3.mp3","size":452799}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_4.webm","size":495789},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_4.mp3","size":816946}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_5.webm","size":1035472},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_5.mp3","size":1716812}}]}', 2493067)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:14-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_1.webm","size":116168},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_1.mp3","size":192548}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_2.webm","size":570268},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_2.mp3","size":944110}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_3.webm","size":275370},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_3.mp3","size":452799}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_4.webm","size":495789},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_4.mp3","size":816946}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_014_5.webm","size":1035472},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_014_5.mp3","size":1716812}}]}', ContentSize = 2493067, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 1:14-20', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_014.json"}', 30376)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:14-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_014.json"}', ContentSize = 30376, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001014 AND EndVerseId = 1042001020
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001014, 1042001020)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:21-28' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:21-28', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:21-28' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:21-28', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 1:21-28', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_1.webm","size":108976},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_1.mp3","size":180840}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_2.webm","size":671472},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_2.mp3","size":1111712}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_3.webm","size":357048},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_3.mp3","size":592293}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_4.webm","size":503797},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_4.mp3","size":833247}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_5.webm","size":872983},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_5.mp3","size":1478053}}]}', 2514276)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:21-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_1.webm","size":108976},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_1.mp3","size":180840}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_2.webm","size":671472},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_2.mp3","size":1111712}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_3.webm","size":357048},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_3.mp3","size":592293}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_4.webm","size":503797},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_4.mp3","size":833247}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_021_5.webm","size":872983},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_021_5.mp3","size":1478053}}]}', ContentSize = 2514276, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 1:21-28', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_021.json"}', 31331)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:21-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_021.json"}', ContentSize = 31331, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001021 AND EndVerseId = 1042001028
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001021, 1042001028)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:29-34' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:29-34', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:29-34' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:29-34', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 1:29-34', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_1.webm","size":110646},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_1.mp3","size":186455}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_2.webm","size":414530},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_2.mp3","size":687170}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_3.webm","size":219929},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_3.mp3","size":363983}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_4.webm","size":430553},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_4.mp3","size":708590}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_5.webm","size":368955},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_5.mp3","size":606399}}]}', 1544613)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:29-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_1.webm","size":110646},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_1.mp3","size":186455}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_2.webm","size":414530},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_2.mp3","size":687170}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_3.webm","size":219929},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_3.mp3","size":363983}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_4.webm","size":430553},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_4.mp3","size":708590}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_029_5.webm","size":368955},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_029_5.mp3","size":606399}}]}', ContentSize = 1544613, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 1:29-34', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_029.json"}', 20528)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:29-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_029.json"}', ContentSize = 20528, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001029 AND EndVerseId = 1042001034
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001029, 1042001034)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:35-39' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:35-39', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:35-39' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:35-39', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 1:35-39', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_1.webm","size":106398},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_1.mp3","size":178632}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_2.webm","size":622721},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_2.mp3","size":1034703}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_3.webm","size":218752},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_3.mp3","size":362184}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_4.webm","size":226991},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_4.mp3","size":375581}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_5.webm","size":560026},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_5.mp3","size":928959}}]}', 1734888)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:35-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_1.webm","size":106398},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_1.mp3","size":178632}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_2.webm","size":622721},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_2.mp3","size":1034703}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_3.webm","size":218752},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_3.mp3","size":362184}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_4.webm","size":226991},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_4.mp3","size":375581}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_035_5.webm","size":560026},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_035_5.mp3","size":928959}}]}', ContentSize = 1734888, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 1:35-39', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_035.json"}', 21754)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:35-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_035.json"}', ContentSize = 21754, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001035 AND EndVerseId = 1042001039
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001035, 1042001039)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:40-45' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:40-45', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:40-45' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:40-45', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 1:40-45', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_1.webm","size":110636},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_1.mp3","size":184365}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_2.webm","size":631528},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_2.mp3","size":1047451}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_3.webm","size":370859},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_3.mp3","size":612878}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_4.webm","size":605004},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_4.mp3","size":1006700}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_5.webm","size":647780},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_5.mp3","size":1098964}}]}', 2365807)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:40-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_1.webm","size":110636},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_1.mp3","size":184365}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_2.webm","size":631528},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_2.mp3","size":1047451}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_3.webm","size":370859},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_3.mp3","size":612878}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_4.webm","size":605004},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_4.mp3","size":1006700}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_001_040_5.webm","size":647780},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_001_040_5.mp3","size":1098964}}]}', ContentSize = 2365807, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 1:40-45', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_040.json"}', 28680)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 1:40-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_001_040.json"}', ContentSize = 28680, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001040 AND EndVerseId = 1042001045
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001040, 1042001045)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:1-12' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:1-12', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:1-12' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:1-12', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 2:1-12', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_1.webm","size":121572},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_1.mp3","size":203173}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_2.webm","size":1071316},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_2.mp3","size":1776998}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_3.webm","size":522867},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_3.mp3","size":870758}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_4.webm","size":814410},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_4.mp3","size":1367503}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_5.webm","size":918586},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_5.mp3","size":1523611}}]}', 3448751)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_1.webm","size":121572},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_1.mp3","size":203173}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_2.webm","size":1071316},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_2.mp3","size":1776998}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_3.webm","size":522867},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_3.mp3","size":870758}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_4.webm","size":814410},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_4.mp3","size":1367503}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_001_5.webm","size":918586},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_001_5.mp3","size":1523611}}]}', ContentSize = 3448751, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 2:1-12', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_001.json"}', 39673)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_001.json"}', ContentSize = 39673, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002001 AND EndVerseId = 1042002012
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002001, 1042002012)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:13-17' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:13-17', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:13-17' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:13-17', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 2:13-17', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_1.webm","size":115081},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_1.mp3","size":197531}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_2.webm","size":895583},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_2.mp3","size":1528940}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_3.webm","size":387015},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_3.mp3","size":655614}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_4.webm","size":613832},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_4.mp3","size":1027493}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_5.webm","size":777674},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_5.mp3","size":1306063}}]}', 2789185)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_1.webm","size":115081},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_1.mp3","size":197531}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_2.webm","size":895583},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_2.mp3","size":1528940}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_3.webm","size":387015},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_3.mp3","size":655614}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_4.webm","size":613832},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_4.mp3","size":1027493}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_013_5.webm","size":777674},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_013_5.mp3","size":1306063}}]}', ContentSize = 2789185, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 2:13-17', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_013.json"}', 32297)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_013.json"}', ContentSize = 32297, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002013 AND EndVerseId = 1042002017
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002013, 1042002017)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageId = NULL;
SELECT @AudioResourceId = NULL;
SELECT @TextResourceId = NULL;
SELECT @AudioResourceContentId = NULL;
SELECT @TextResourceContentId = NULL;
SELECT @PassageResourceId = NULL;

SELECT @AudioResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:18-22' AND MediaType = 2
IF @AudioResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:18-22', 2)
SELECT @AudioResourceId = @@IDENTITY
END


SELECT @TextResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:18-22' AND MediaType = 1
IF @TextResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:18-22', 1)
SELECT @TextResourceId = @@IDENTITY
END


SELECT @AudioResourceContentId = Id FROM ResourceContents WHERE ResourceId = @AudioResourceId AND LanguageId = @LanguageId
IF @AudioResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@AudioResourceId, @LanguageId, N'मरकुस 2:18-22', 1, 1, 1, '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_1.webm","size":115016},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_1.mp3","size":190632}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_2.webm","size":774258},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_2.mp3","size":1295405}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_3.webm","size":400574},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_3.mp3","size":669929}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_4.webm","size":477339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_4.mp3","size":800646}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_5.webm","size":525742},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_5.mp3","size":875252}}]}', 2292929)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:18-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"steps":[{"step":1,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_1.webm","size":115016},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_1.mp3","size":190632}},{"step":2,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_2.webm","size":774258},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_2.mp3","size":1295405}},{"step":3,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_3.webm","size":400574},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_3.mp3","size":669929}},{"step":4,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_4.webm","size":477339},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_4.mp3","size":800646}},{"step":5,"webm":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/webm/HIN_CBBTER_042_MRK_002_018_5.webm","size":525742},"mp3":{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/audio/mp3/HIN_CBBTER_042_MRK_002_018_5.mp3","size":875252}}]}', ContentSize = 2292929, Updated = GETUTCDATE() WHERE Id = @AudioResourceContentId
SELECT @TextResourceContentId = Id FROM ResourceContents WHERE ResourceId = @TextResourceId AND LanguageId = @LanguageId


IF @TextResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@TextResourceId, @LanguageId, N'मरकुस 2:18-22', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_018.json"}', 26998)
ELSE
UPDATE ResourceContents SET DisplayName = N'मरकुस 2:18-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/HIN/text/HIN_CBBTER_042_MRK_002_018.json"}', ContentSize = 26998, Updated = GETUTCDATE() WHERE Id = @TextResourceContentId


SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002018 AND EndVerseId = 1042002022
IF @PassageId IS NULL
BEGIN
INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002018, 1042002022)
SELECT @PassageId = @@IDENTITY
END


SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @AudioResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @AudioResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


SELECT @PassageResourceId = NULL;
SELECT @PassageResourceId = PassageId FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @TextResourceId
IF @PassageResourceId IS NULL
BEGIN
INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @TextResourceId)
SELECT @PassageResourceId = @@IDENTITY
END


COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION;
THROW;
END CATCH
