
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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:1-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:1-13', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:1-13', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_001.json"}', 19481)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_001.json"}', ContentSize = 19481, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:23-3:6' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:23-3:6', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:23-3:6', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_023.json"}', 22189)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:23-3:6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_023.json"}', ContentSize = 22189, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:7-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:7-12', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 3:7-12', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_003_007.json"}', 14300)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 3:7-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_003_007.json"}', ContentSize = 14300, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:13-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:13-19', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 3:13-19', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_003_013.json"}', 10950)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 3:13-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_003_013.json"}', ContentSize = 10950, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:20-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:20-35', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 3:20-35', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_003_020.json"}', 19569)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 3:20-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_003_020.json"}', ContentSize = 19569, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:1-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:1-20', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:1-20', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_001.json"}', 30801)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_001.json"}', ContentSize = 30801, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:21-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:21-25', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:21-25', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_021.json"}', 13093)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:21-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_021.json"}', ContentSize = 13093, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:26-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:26-34', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:26-34', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_026.json"}', 14915)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:26-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_026.json"}', ContentSize = 14915, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:35-41' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:35-41', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 4:35-41', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_035.json"}', 17797)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 4:35-41', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_004_035.json"}', ContentSize = 17797, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:1-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:1-20', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 5:1-20', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_005_001.json"}', 23256)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 5:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_005_001.json"}', ContentSize = 23256, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:21-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:21-34', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 5:21-34', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_005_021.json"}', 22354)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 5:21-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_005_021.json"}', ContentSize = 22354, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:35-43' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:35-43', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 5:35-43', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_005_035.json"}', 20330)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 5:35-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_005_035.json"}', ContentSize = 20330, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:14-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:14-20', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:14-20', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_014.json"}', 15258)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:14-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_014.json"}', ContentSize = 15258, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:1-6a' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:1-6a', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:1-6a', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_001.json"}', 12710)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:1-6a', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_001.json"}', ContentSize = 12710, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:6b-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:6b-13', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:6b-13', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_006.json"}', 14873)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:6b-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_006.json"}', ContentSize = 14873, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:14-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:14-29', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:14-29', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_014.json"}', 22097)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:14-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_014.json"}', ContentSize = 22097, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:30-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:30-44', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:30-44', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_030.json"}', 18913)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:30-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_030.json"}', ContentSize = 18913, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:45-56' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:45-56', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 6:45-56', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_045.json"}', 18016)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 6:45-56', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_006_045.json"}', ContentSize = 18016, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:1-8', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:1-8', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_001.json"}', 18043)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_001.json"}', ContentSize = 18043, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:9-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:9-13', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:9-13', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_009.json"}', 9411)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:9-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_009.json"}', ContentSize = 9411, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:14-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:14-23', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:14-23', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_014.json"}', 15159)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:14-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_014.json"}', ContentSize = 15159, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:24-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:24-30', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 7:24-30', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_024.json"}', 16824)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 7:24-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_007_024.json"}', ContentSize = 16824, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:1-10' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:1-10', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:1-10', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_001.json"}', 12889)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:1-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_001.json"}', ContentSize = 12889, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:11-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:11-21', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:11-21', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_011.json"}', 17207)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:11-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_011.json"}', ContentSize = 17207, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:22-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:22-26', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:22-26', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_022.json"}', 12460)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:22-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_022.json"}', ContentSize = 12460, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:27-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:27-30', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:27-30', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_027.json"}', 12561)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:27-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_027.json"}', ContentSize = 12561, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:31-9:1' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:31-9:1', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 8:31-9:1', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_031.json"}', 27056)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 8:31-9:1', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_008_031.json"}', ContentSize = 27056, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:21-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:21-28', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:21-28', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_021.json"}', 16179)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:21-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_021.json"}', ContentSize = 16179, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:2-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:2-13', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 9:2-13', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_009_002.json"}', 20436)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 9:2-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_009_002.json"}', ContentSize = 20436, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:14-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:14-29', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 9:14-29', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_009_014.json"}', 22477)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 9:14-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_009_014.json"}', ContentSize = 22477, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:30-50' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:30-50', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 9:30-50', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_009_030.json"}', 25897)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 9:30-50', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_009_030.json"}', ContentSize = 25897, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:1-12', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:1-12', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_001.json"}', 20336)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_001.json"}', ContentSize = 20336, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:13-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:13-31', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:13-31', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_013.json"}', 25476)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:13-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_013.json"}', ContentSize = 25476, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:32-45' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:32-45', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:32-45', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_032.json"}', 24445)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:32-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_032.json"}', ContentSize = 24445, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:46-52' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:46-52', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 10:46-52', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_046.json"}', 16655)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 10:46-52', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_010_046.json"}', ContentSize = 16655, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:1-11', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 11:1-11', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_011_001.json"}', 25508)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 11:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_011_001.json"}', ContentSize = 25508, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:12-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:12-26', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 11:12-26', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_011_012.json"}', 25382)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 11:12-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_011_012.json"}', ContentSize = 25382, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:27-33' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:27-33', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 11:27-33', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_011_027.json"}', 17423)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 11:27-33', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_011_027.json"}', ContentSize = 17423, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:1-12', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:1-12', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_001.json"}', 24157)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_001.json"}', ContentSize = 24157, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:29-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:29-34', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:29-34', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_029.json"}', 11139)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:29-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_029.json"}', ContentSize = 11139, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:13-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:13-17', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:13-17', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_013.json"}', 17886)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_013.json"}', ContentSize = 17886, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:18-27' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:18-27', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:18-27', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_018.json"}', 19028)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:18-27', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_018.json"}', ContentSize = 19028, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:28-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:28-34', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:28-34', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_028.json"}', 16775)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:28-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_028.json"}', ContentSize = 16775, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:35-37' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:35-37', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:35-37', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_035.json"}', 13129)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:35-37', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_035.json"}', ContentSize = 13129, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:38-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:38-44', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 12:38-44', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_038.json"}', 13893)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 12:38-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_012_038.json"}', ContentSize = 13893, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:35-39' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:35-39', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:35-39', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_035.json"}', 11090)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:35-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_035.json"}', ContentSize = 11090, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:40-45' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:40-45', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 1:40-45', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_040.json"}', 14491)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 1:40-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_001_040.json"}', ContentSize = 14491, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:1-12', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:1-12', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_001.json"}', 20931)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_001.json"}', ContentSize = 20931, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:13-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:13-17', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:13-17', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_013.json"}', 17448)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_013.json"}', ContentSize = 17448, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

SELECT @ResourceId = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:18-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:18-22', 1)
SELECT @ResourceId = @@IDENTITY
END


SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = @LanguageId
IF @ResourceContentId IS NULL
INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize)
VALUES (@ResourceId, @LanguageId, 'Mak 2:18-22', 1, 1, 1, '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_018.json"}', 14396)
ELSE
UPDATE ResourceContents SET DisplayName = 'Mak 2:18-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url":"https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/TPI/text/TPI_CBBTER_42_002_018.json"}', ContentSize = 14396, Updated = GETUTCDATE() WHERE Id = @ResourceContentId


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

