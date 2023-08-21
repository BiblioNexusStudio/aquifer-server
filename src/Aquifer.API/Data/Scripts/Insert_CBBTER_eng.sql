DECLARE @ResourceId INT
DECLARE @PassageId INT
DECLARE @PassageResourceCount INT
DECLARE @ResourceContentId INT


SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045007001 AND EndVerseId = 1045007008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045007001, 1045007008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 7:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 7:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 7:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_007_001.json"}', 22040)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 7:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_007_001.json"}', ContentSize = 22040, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045007009 AND EndVerseId = 1045007019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045007009, 1045007019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 7:9-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 7:9-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 7:9-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_007_009.json"}', 16083)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 7:9-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_007_009.json"}', ContentSize = 16083, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045007020 AND EndVerseId = 1045007034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045007020, 1045007034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 7:20-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 7:20-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 7:20-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_007_020.json"}', 19195)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 7:20-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_007_020.json"}', ContentSize = 19195, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045008004 AND EndVerseId = 1045008025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045008004, 1045008025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 8:4-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 8:4-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 8:4-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_008_004.json"}', 27802)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 8:4-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_008_004.json"}', ContentSize = 27802, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045008026 AND EndVerseId = 1045008040
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045008026, 1045008040)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 8:26-40' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 8:26-40', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 8:26-40', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_008_026.json"}', 28926)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 8:26-40', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_008_026.json"}', ContentSize = 28926, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045009001 AND EndVerseId = 1045009019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045009001, 1045009019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 9:1-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 9:1-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 9:1-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_001.json"}', 21482)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 9:1-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_001.json"}', ContentSize = 21482, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045009019 AND EndVerseId = 1045009031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045009019, 1045009031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 9:19-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 9:19-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 9:19-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_019.json"}', 19242)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 9:19-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_019.json"}', ContentSize = 19242, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045009032 AND EndVerseId = 1045009035
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045009032, 1045009035)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 9:32-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 9:32-35', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 9:32-35', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_032.json"}', 10447)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 9:32-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_032.json"}', ContentSize = 10447, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045009036 AND EndVerseId = 1045009043
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045009036, 1045009043)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 9:36-43' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 9:36-43', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 9:36-43', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_036.json"}', 15841)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 9:36-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_009_036.json"}', ContentSize = 15841, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045010001 AND EndVerseId = 1045010008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045010001, 1045010008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 10:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 10:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 10:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_010_001.json"}', 15091)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 10:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_010_001.json"}', ContentSize = 15091, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045010009 AND EndVerseId = 1045010023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045010009, 1045010023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 10:9-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 10:9-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 10:9-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_010_009.json"}', 17005)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 10:9-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_010_009.json"}', ContentSize = 17005, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045011001 AND EndVerseId = 1045011018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045011001, 1045011018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 11:1-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 11:1-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 11:1-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_011_001.json"}', 25489)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 11:1-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_011_001.json"}', ContentSize = 25489, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045011019 AND EndVerseId = 1045011026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045011019, 1045011026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 11:19-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 11:19-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 11:19-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_011_019.json"}', 19293)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 11:19-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_011_019.json"}', ContentSize = 19293, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045011027 AND EndVerseId = 1045011030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045011027, 1045011030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 11:27-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 11:27-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 11:27-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_011_027.json"}', 13842)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 11:27-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_011_027.json"}', ContentSize = 13842, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045012001 AND EndVerseId = 1045012005
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045012001, 1045012005)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 12:1-5' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 12:1-5', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 12:1-5', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_001.json"}', 13452)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 12:1-5', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_001.json"}', ContentSize = 13452, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045012006 AND EndVerseId = 1045012019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045012006, 1045012019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 12:6-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 12:6-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 12:6-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_006.json"}', 25835)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 12:6-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_006.json"}', ContentSize = 25835, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045012020 AND EndVerseId = 1045012024
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045012020, 1045012024)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 12:20-24' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 12:20-24', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 12:20-24', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_020.json"}', 17369)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 12:20-24', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_020.json"}', ContentSize = 17369, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045012025 AND EndVerseId = 1045013003
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045012025, 1045013003)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 12:25-13:3' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 12:25-13:3', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 12:25-13:3', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_025.json"}', 16397)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 12:25-13:3', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_012_025.json"}', ContentSize = 16397, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045013004 AND EndVerseId = 1045013012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045013004, 1045013012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 13:4-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 13:4-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 13:4-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_013_004.json"}', 21852)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 13:4-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_013_004.json"}', ContentSize = 21852, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045013013 AND EndVerseId = 1045013022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045013013, 1045013022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 13:13-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 13:13-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 13:13-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_013_013.json"}', 20880)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 13:13-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_013_013.json"}', ContentSize = 20880, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045014008 AND EndVerseId = 1045014020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045014008, 1045014020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 14:8-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 14:8-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 14:8-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_014_008.json"}', 14433)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 14:8-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_014_008.json"}', ContentSize = 14433, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045017001 AND EndVerseId = 1045017009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045017001, 1045017009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 17:1-9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 17:1-9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 17:1-9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_001.json"}', 20005)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 17:1-9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_001.json"}', ContentSize = 20005, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045017010 AND EndVerseId = 1045017015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045017010, 1045017015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 17:10-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 17:10-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 17:10-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_010.json"}', 18410)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 17:10-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_010.json"}', ContentSize = 18410, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045017016 AND EndVerseId = 1045017021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045017016, 1045017021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 17:16-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 17:16-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 17:16-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_016.json"}', 15137)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 17:16-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_016.json"}', ContentSize = 15137, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045017022 AND EndVerseId = 1045017034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045017022, 1045017034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 17:22-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 17:22-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 17:22-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_022.json"}', 17917)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 17:22-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_017_022.json"}', ContentSize = 17917, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045018018 AND EndVerseId = 1045018023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045018018, 1045018023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 18:18-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 18:18-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 18:18-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_018_018.json"}', 16098)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 18:18-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_018_018.json"}', ContentSize = 16098, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045018024 AND EndVerseId = 1045018028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045018024, 1045018028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 18:24-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 18:24-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 18:24-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_018_024.json"}', 17028)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 18:24-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_018_024.json"}', ContentSize = 17028, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045019001 AND EndVerseId = 1045019007
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045019001, 1045019007)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 19:1-7' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 19:1-7', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 19:1-7', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_001.json"}', 17079)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 19:1-7', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_001.json"}', ContentSize = 17079, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045019008 AND EndVerseId = 1045019010
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045019008, 1045019010)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 19:8-10' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 19:8-10', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 19:8-10', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_008.json"}', 16049)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 19:8-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_008.json"}', ContentSize = 16049, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045019011 AND EndVerseId = 1045019020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045019011, 1045019020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 19:11-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 19:11-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 19:11-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_011.json"}', 23489)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 19:11-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_011.json"}', ContentSize = 23489, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045019021 AND EndVerseId = 1045019041
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045019021, 1045019041)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 19:21-41' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 19:21-41', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 19:21-41', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_021.json"}', 33366)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 19:21-41', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_019_021.json"}', ContentSize = 33366, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045020001 AND EndVerseId = 1045020006
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045020001, 1045020006)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 20:1-6' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 20:1-6', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 20:1-6', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_001.json"}', 20717)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 20:1-6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_001.json"}', ContentSize = 20717, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045020007 AND EndVerseId = 1045020012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045020007, 1045020012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 20:7-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 20:7-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 20:7-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_007.json"}', 23997)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 20:7-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_007.json"}', ContentSize = 23997, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045020013 AND EndVerseId = 1045020017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045020013, 1045020017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 20:13-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 20:13-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 20:13-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_013.json"}', 24384)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 20:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_013.json"}', ContentSize = 24384, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045020018 AND EndVerseId = 1045020038
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045020018, 1045020038)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 20:18-38' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 20:18-38', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 20:18-38', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_018.json"}', 36814)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 20:18-38', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_020_018.json"}', ContentSize = 36814, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045021001 AND EndVerseId = 1045021009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045021001, 1045021009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 21:1-9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 21:1-9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 21:1-9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_001.json"}', 11055)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 21:1-9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_001.json"}', ContentSize = 11055, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045021010 AND EndVerseId = 1045021014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045021010, 1045021014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 21:10-14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 21:10-14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 21:10-14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_010.json"}', 9155)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 21:10-14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_010.json"}', ContentSize = 9155, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045021015 AND EndVerseId = 1045021026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045021015, 1045021026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 21:15-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 21:15-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 21:15-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_015.json"}', 13738)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 21:15-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_015.json"}', ContentSize = 13738, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1045021027 AND EndVerseId = 1045021036
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1045021027, 1045021036)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Acts 21:27-36' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Acts 21:27-36', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Acts 21:27-36', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_027.json"}', 13333)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Acts 21:27-36', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_45_021_027.json"}', ContentSize = 13333, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001001001 AND EndVerseId = 1001002003
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001001001, 1001002003)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 1:1-2:3' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 1:1-2:3', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 1:1-2:3', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_001_001.json"}', 26276)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 1:1-2:3', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_001_001.json"}', ContentSize = 26276, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001008020 AND EndVerseId = 1001009017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001008020, 1001009017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 8:20-9:17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 8:20-9:17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 8:20-9:17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_008_020.json"}', 24933)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 8:20-9:17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_008_020.json"}', ContentSize = 24933, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001009018 AND EndVerseId = 1001009029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001009018, 1001009029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 9:18-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 9:18-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 9:18-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_009_018.json"}', 18677)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 9:18-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_009_018.json"}', ContentSize = 18677, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001010001 AND EndVerseId = 1001010032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001010001, 1001010032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 10:1-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 10:1-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 10:1-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_010_001.json"}', 15402)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 10:1-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_010_001.json"}', ContentSize = 15402, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001011001 AND EndVerseId = 1001011009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001011001, 1001011009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 11:1-9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 11:1-9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 11:1-9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_011_001.json"}', 17733)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 11:1-9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_011_001.json"}', ContentSize = 17733, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001011010 AND EndVerseId = 1001011026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001011010, 1001011026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 11:10-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 11:10-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 11:10-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_011_010.json"}', 13110)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 11:10-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_011_010.json"}', ContentSize = 13110, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001011027 AND EndVerseId = 1001011032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001011027, 1001011032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 11:27-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 11:27-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 11:27-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_011_027.json"}', 13864)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 11:27-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_011_027.json"}', ContentSize = 13864, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001012001 AND EndVerseId = 1001012009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001012001, 1001012009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 12:1-9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 12:1-9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 12:1-9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_012_001.json"}', 23017)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 12:1-9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_012_001.json"}', ContentSize = 23017, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001012010 AND EndVerseId = 1001012020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001012010, 1001012020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 12:10-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 12:10-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 12:10-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_012_010.json"}', 21355)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 12:10-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_012_010.json"}', ContentSize = 21355, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001013001 AND EndVerseId = 1001013018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001013001, 1001013018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 13:1-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 13:1-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 13:1-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_013_001.json"}', 23967)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 13:1-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_013_001.json"}', ContentSize = 23967, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001014001 AND EndVerseId = 1001014016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001014001, 1001014016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 14:1-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 14:1-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 14:1-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_014_001.json"}', 23908)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 14:1-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_014_001.json"}', ContentSize = 23908, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001002004 AND EndVerseId = 1001002025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001002004, 1001002025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 2:4-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 2:4-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 2:4-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_002_004.json"}', 26694)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 2:4-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_002_004.json"}', ContentSize = 26694, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001014017 AND EndVerseId = 1001014024
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001014017, 1001014024)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 14:17-24' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 14:17-24', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 14:17-24', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_014_017.json"}', 19385)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 14:17-24', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_014_017.json"}', ContentSize = 19385, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001015001 AND EndVerseId = 1001015021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001015001, 1001015021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 15:1-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 15:1-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 15:1-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_015_001.json"}', 35350)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 15:1-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_015_001.json"}', ContentSize = 35350, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001016001 AND EndVerseId = 1001016016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001016001, 1001016016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 16:1-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 16:1-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 16:1-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_016_001.json"}', 23463)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 16:1-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_016_001.json"}', ContentSize = 23463, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001017001 AND EndVerseId = 1001017027
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001017001, 1001017027)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 17:1-27' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 17:1-27', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 17:1-27', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_017_001.json"}', 34680)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 17:1-27', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_017_001.json"}', ContentSize = 34680, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001018001 AND EndVerseId = 1001018015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001018001, 1001018015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 18:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 18:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 18:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_018_001.json"}', 24208)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 18:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_018_001.json"}', ContentSize = 24208, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001018016 AND EndVerseId = 1001018033
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001018016, 1001018033)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 18:16-33' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 18:16-33', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 18:16-33', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_018_016.json"}', 25774)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 18:16-33', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_018_016.json"}', ContentSize = 25774, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001019001 AND EndVerseId = 1001019029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001019001, 1001019029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 19:1-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 19:1-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 19:1-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_019_001.json"}', 32215)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 19:1-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_019_001.json"}', ContentSize = 32215, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001019030 AND EndVerseId = 1001019038
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001019030, 1001019038)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 19:30-38' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 19:30-38', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 19:30-38', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_019_030.json"}', 16073)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 19:30-38', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_019_030.json"}', ContentSize = 16073, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001020001 AND EndVerseId = 1001020018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001020001, 1001020018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 20:1-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 20:1-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 20:1-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_020_001.json"}', 25876)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 20:1-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_020_001.json"}', ContentSize = 25876, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001021001 AND EndVerseId = 1001021021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001021001, 1001021021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 21:1-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 21:1-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 21:1-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_021_001.json"}', 26323)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 21:1-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_021_001.json"}', ContentSize = 26323, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001003001 AND EndVerseId = 1001003024
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001003001, 1001003024)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 3:1-24' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 3:1-24', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 3:1-24', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_003_001.json"}', 38580)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 3:1-24', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_003_001.json"}', ContentSize = 38580, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001021022 AND EndVerseId = 1001021034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001021022, 1001021034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 21:22-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 21:22-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 21:22-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_021_022.json"}', 20232)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 21:22-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_021_022.json"}', ContentSize = 20232, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001022001 AND EndVerseId = 1001022019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001022001, 1001022019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 22:1-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 22:1-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 22:1-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_022_001.json"}', 29197)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 22:1-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_022_001.json"}', ContentSize = 29197, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001022020 AND EndVerseId = 1001022024
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001022020, 1001022024)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 22:20-24' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 22:20-24', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 22:20-24', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_022_020.json"}', 9447)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 22:20-24', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_022_020.json"}', ContentSize = 9447, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001023001 AND EndVerseId = 1001023020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001023001, 1001023020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 23:1-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 23:1-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 23:1-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_023_001.json"}', 19934)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 23:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_023_001.json"}', ContentSize = 19934, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001024001 AND EndVerseId = 1001024014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001024001, 1001024014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 24:1-14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 24:1-14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 24:1-14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_024_001.json"}', 19843)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 24:1-14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_024_001.json"}', ContentSize = 19843, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001024050 AND EndVerseId = 1001024061
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001024050, 1001024061)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 24:50-61' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 24:50-61', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 24:50-61', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_024_050.json"}', 16550)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 24:50-61', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_024_050.json"}', ContentSize = 16550, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001024062 AND EndVerseId = 1001024067
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001024062, 1001024067)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 24:62-67' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 24:62-67', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 24:62-67', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_024_062.json"}', 9583)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 24:62-67', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_024_062.json"}', ContentSize = 9583, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001025001 AND EndVerseId = 1001025011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001025001, 1001025011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 25:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 25:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 25:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_025_001.json"}', 15294)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 25:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_025_001.json"}', ContentSize = 15294, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001025012 AND EndVerseId = 1001025018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001025012, 1001025018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 25:12-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 25:12-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 25:12-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_025_012.json"}', 13678)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 25:12-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_025_012.json"}', ContentSize = 13678, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001025019 AND EndVerseId = 1001025034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001025019, 1001025034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 25:19-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 25:19-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 25:19-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_025_019.json"}', 26443)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 25:19-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_025_019.json"}', ContentSize = 26443, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001026001 AND EndVerseId = 1001026033
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001026001, 1001026033)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 26:1-33' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 26:1-33', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 26:1-33', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_026_001.json"}', 27762)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 26:1-33', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_026_001.json"}', ContentSize = 27762, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001026034 AND EndVerseId = 1001027017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001026034, 1001027017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 26:34-27:17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 26:34-27:17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 26:34-27:17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_026_034.json"}', 16901)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 26:34-27:17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_026_034.json"}', ContentSize = 16901, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001027018 AND EndVerseId = 1001027029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001027018, 1001027029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 27:18-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 27:18-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 27:18-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_027_018.json"}', 15783)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 27:18-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_027_018.json"}', ContentSize = 15783, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001027030 AND EndVerseId = 1001027040
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001027030, 1001027040)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 27:30-40' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 27:30-40', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 27:30-40', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_027_030.json"}', 17503)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 27:30-40', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_027_030.json"}', ContentSize = 17503, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001027041 AND EndVerseId = 1001028009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001027041, 1001028009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 27:41-28:9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 27:41-28:9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 27:41-28:9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_027_041.json"}', 23111)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 27:41-28:9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_027_041.json"}', ContentSize = 23111, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001028010 AND EndVerseId = 1001028022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001028010, 1001028022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 28:10-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 28:10-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 28:10-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_028_010.json"}', 18892)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 28:10-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_028_010.json"}', ContentSize = 18892, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001029001 AND EndVerseId = 1001029014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001029001, 1001029014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 29:1-14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 29:1-14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 29:1-14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_029_001.json"}', 19121)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 29:1-14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_029_001.json"}', ContentSize = 19121, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001029015 AND EndVerseId = 1001029030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001029015, 1001029030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 29:15-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 29:15-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 29:15-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_029_015.json"}', 21915)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 29:15-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_029_015.json"}', ContentSize = 21915, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001029031 AND EndVerseId = 1001030024
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001029031, 1001030024)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 29:31-30:24' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 29:31-30:24', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 29:31-30:24', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_029_031.json"}', 20228)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 29:31-30:24', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_029_031.json"}', ContentSize = 20228, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001030025 AND EndVerseId = 1001030043
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001030025, 1001030043)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 30:25-43' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 30:25-43', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 30:25-43', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_030_025.json"}', 21916)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 30:25-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_030_025.json"}', ContentSize = 21916, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001031001 AND EndVerseId = 1001031021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001031001, 1001031021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 31:1-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 31:1-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 31:1-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_031_001.json"}', 20720)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 31:1-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_031_001.json"}', ContentSize = 20720, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001031022 AND EndVerseId = 1001031035
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001031022, 1001031035)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 31:22-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 31:22-35', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 31:22-35', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_031_022.json"}', 21675)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 31:22-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_031_022.json"}', ContentSize = 21675, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001031036 AND EndVerseId = 1001031055
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001031036, 1001031055)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 31:36-55' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 31:36-55', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 31:36-55', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_031_036.json"}', 24725)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 31:36-55', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_031_036.json"}', ContentSize = 24725, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001032001 AND EndVerseId = 1001032021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001032001, 1001032021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 32:1-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 32:1-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 32:1-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_032_001.json"}', 18145)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 32:1-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_032_001.json"}', ContentSize = 18145, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001032022 AND EndVerseId = 1001032032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001032022, 1001032032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 32:22-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 32:22-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 32:22-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_032_022.json"}', 17879)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 32:22-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_032_022.json"}', ContentSize = 17879, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001033001 AND EndVerseId = 1001033020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001033001, 1001033020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 33:1-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 33:1-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 33:1-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_033_001.json"}', 21321)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 33:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_033_001.json"}', ContentSize = 21321, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001004001 AND EndVerseId = 1001004016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001004001, 1001004016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 4:1-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 4:1-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 4:1-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_004_001.json"}', 27600)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 4:1-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_004_001.json"}', ContentSize = 27600, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001004017 AND EndVerseId = 1001004026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001004017, 1001004026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 4:17-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 4:17-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 4:17-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_004_017.json"}', 17576)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 4:17-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_004_017.json"}', ContentSize = 17576, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001005001 AND EndVerseId = 1001005032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001005001, 1001005032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 5:1-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 5:1-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 5:1-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_005_001.json"}', 11353)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 5:1-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_005_001.json"}', ContentSize = 11353, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001034001 AND EndVerseId = 1001034017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001034001, 1001034017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 34:1-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 34:1-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 34:1-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_034_001.json"}', 18762)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 34:1-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_034_001.json"}', ContentSize = 18762, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001034018 AND EndVerseId = 1001034031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001034018, 1001034031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 34:18-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 34:18-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 34:18-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_034_018.json"}', 18261)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 34:18-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_034_018.json"}', ContentSize = 18261, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001035001 AND EndVerseId = 1001035015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001035001, 1001035015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 35:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 35:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 35:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_035_001.json"}', 22866)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 35:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_035_001.json"}', ContentSize = 22866, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001035016 AND EndVerseId = 1001035020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001035016, 1001035020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 35:16-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 35:16-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 35:16-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_035_016.json"}', 12680)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 35:16-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_035_016.json"}', ContentSize = 12680, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001035021 AND EndVerseId = 1001035029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001035021, 1001035029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 35:21-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 35:21-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 35:21-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_035_021.json"}', 13849)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 35:21-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_035_021.json"}', ContentSize = 13849, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001036001 AND EndVerseId = 1001036019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001036001, 1001036019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 36:1-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 36:1-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 36:1-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_036_001.json"}', 14894)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 36:1-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_036_001.json"}', ContentSize = 14894, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001036020 AND EndVerseId = 1001036030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001036020, 1001036030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 36:20-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 36:20-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 36:20-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_036_020.json"}', 10521)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 36:20-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_036_020.json"}', ContentSize = 10521, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001036031 AND EndVerseId = 1001036043
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001036031, 1001036043)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 36:31-43' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 36:31-43', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 36:31-43', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_036_031.json"}', 11389)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 36:31-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_036_031.json"}', ContentSize = 11389, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001037001 AND EndVerseId = 1001037011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001037001, 1001037011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 37:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 37:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 37:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_037_001.json"}', 20903)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 37:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_037_001.json"}', ContentSize = 20903, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001037012 AND EndVerseId = 1001037036
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001037012, 1001037036)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 37:12-36' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 37:12-36', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 37:12-36', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_037_012.json"}', 23390)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 37:12-36', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_037_012.json"}', ContentSize = 23390, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001038001 AND EndVerseId = 1001038030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001038001, 1001038030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 38:1-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 38:1-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 38:1-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_038_001.json"}', 23581)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 38:1-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_038_001.json"}', ContentSize = 23581, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001039001 AND EndVerseId = 1001039023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001039001, 1001039023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 39:1-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 39:1-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 39:1-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_039_001.json"}', 23629)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 39:1-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_039_001.json"}', ContentSize = 23629, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001040001 AND EndVerseId = 1001040023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001040001, 1001040023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 40:1-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 40:1-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 40:1-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_040_001.json"}', 21889)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 40:1-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_040_001.json"}', ContentSize = 21889, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001006001 AND EndVerseId = 1001006008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001006001, 1001006008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 6:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 6:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 6:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_006_001.json"}', 15858)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 6:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_006_001.json"}', ContentSize = 15858, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001041001 AND EndVerseId = 1001041036
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001041001, 1001041036)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 41:1-36' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 41:1-36', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 41:1-36', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_041_001.json"}', 22276)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 41:1-36', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_041_001.json"}', ContentSize = 22276, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001041037 AND EndVerseId = 1001041057
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001041037, 1001041057)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 41:37-57' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 41:37-57', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 41:37-57', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_041_037.json"}', 13761)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 41:37-57', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_041_037.json"}', ContentSize = 13761, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001042001 AND EndVerseId = 1001042026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001042001, 1001042026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 42:1-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 42:1-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 42:1-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_042_001.json"}', 21644)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 42:1-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_042_001.json"}', ContentSize = 21644, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001042027 AND EndVerseId = 1001042038
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001042027, 1001042038)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 42:27-38' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 42:27-38', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 42:27-38', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_042_027.json"}', 15601)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 42:27-38', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_042_027.json"}', ContentSize = 15601, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001043001 AND EndVerseId = 1001043034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001043001, 1001043034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 43:1-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 43:1-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 43:1-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_043_001.json"}', 24617)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 43:1-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_043_001.json"}', ContentSize = 24617, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001044001 AND EndVerseId = 1001044013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001044001, 1001044013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 44:1-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 44:1-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 44:1-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_044_001.json"}', 17678)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 44:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_044_001.json"}', ContentSize = 17678, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001044014 AND EndVerseId = 1001044034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001044014, 1001044034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 44:14-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 44:14-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 44:14-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_044_014.json"}', 16573)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 44:14-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_044_014.json"}', ContentSize = 16573, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001045001 AND EndVerseId = 1001045028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001045001, 1001045028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 45:1-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 45:1-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 45:1-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_045_001.json"}', 22381)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 45:1-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_045_001.json"}', ContentSize = 22381, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001046001 AND EndVerseId = 1001046027
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001046001, 1001046027)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 46:1-27' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 46:1-27', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 46:1-27', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_046_001.json"}', 19230)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 46:1-27', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_046_001.json"}', ContentSize = 19230, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001046028 AND EndVerseId = 1001047012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001046028, 1001047012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 46:28-47:12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 46:28-47:12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 46:28-47:12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_046_028.json"}', 17764)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 46:28-47:12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_046_028.json"}', ContentSize = 17764, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001047013 AND EndVerseId = 1001047026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001047013, 1001047026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 47:13-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 47:13-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 47:13-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_047_013.json"}', 16300)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 47:13-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_047_013.json"}', ContentSize = 16300, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001047027 AND EndVerseId = 1001047031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001047027, 1001047031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 47:27-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 47:27-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 47:27-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_047_027.json"}', 12682)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 47:27-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_047_027.json"}', ContentSize = 12682, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001006009 AND EndVerseId = 1001006022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001006009, 1001006022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 6:9-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 6:9-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 6:9-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_006_009.json"}', 23822)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 6:9-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_006_009.json"}', ContentSize = 23822, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001048001 AND EndVerseId = 1001048022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001048001, 1001048022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 48:1-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 48:1-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 48:1-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_048_001.json"}', 21968)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 48:1-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_048_001.json"}', ContentSize = 21968, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001049001 AND EndVerseId = 1001049028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001049001, 1001049028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 49:1-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 49:1-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 49:1-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_049_001.json"}', 19835)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 49:1-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_049_001.json"}', ContentSize = 19835, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001049029 AND EndVerseId = 1001050014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001049029, 1001050014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 49:29-50:14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 49:29-50:14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 49:29-50:14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_049_029.json"}', 18511)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 49:29-50:14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_049_029.json"}', ContentSize = 18511, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001050015 AND EndVerseId = 1001050021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001050015, 1001050021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 50:15-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 50:15-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 50:15-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_050_015.json"}', 19526)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 50:15-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_050_015.json"}', ContentSize = 19526, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001050022 AND EndVerseId = 1001050026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001050022, 1001050026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 50:22-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 50:22-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 50:22-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_050_022.json"}', 14211)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 50:22-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_050_022.json"}', ContentSize = 14211, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001007001 AND EndVerseId = 1001007024
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001007001, 1001007024)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 7:1-24' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 7:1-24', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 7:1-24', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_007_001.json"}', 22037)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 7:1-24', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_007_001.json"}', ContentSize = 22037, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1001008001 AND EndVerseId = 1001008019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1001008001, 1001008019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Genesis 8:1-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Genesis 8:1-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Genesis 8:1-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_008_001.json"}', 20006)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Genesis 8:1-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_1_008_001.json"}', ContentSize = 20006, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1044004001 AND EndVerseId = 1044004015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1044004001, 1044004015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER John 4:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER John 4:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'John 4:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_44_004_001.json"}', 26383)
ELSE
    UPDATE ResourceContents SET DisplayName = 'John 4:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_44_004_001.json"}', ContentSize = 26383, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1044004016 AND EndVerseId = 1044004026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1044004016, 1044004026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER John 4:16-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER John 4:16-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'John 4:16-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_44_004_016.json"}', 29666)
ELSE
    UPDATE ResourceContents SET DisplayName = 'John 4:16-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_44_004_016.json"}', ContentSize = 29666, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1018001001 AND EndVerseId = 1018001005
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1018001001, 1018001005)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Job 1:1-5' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Job 1:1-5', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Job 1:1-5', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_001_001.json"}', 12264)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Job 1:1-5', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_001_001.json"}', ContentSize = 12264, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1018001006 AND EndVerseId = 1018001012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1018001006, 1018001012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Job 1:6-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Job 1:6-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Job 1:6-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_001_006.json"}', 13985)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Job 1:6-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_001_006.json"}', ContentSize = 13985, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1018001013 AND EndVerseId = 1018001022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1018001013, 1018001022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Job 1:13-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Job 1:13-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Job 1:13-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_001_013.json"}', 14645)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Job 1:13-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_001_013.json"}', ContentSize = 14645, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1018002001 AND EndVerseId = 1018002006
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1018002001, 1018002006)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Job 2:1-6' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Job 2:1-6', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Job 2:1-6', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_002_001.json"}', 13830)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Job 2:1-6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_002_001.json"}', ContentSize = 13830, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1018002007 AND EndVerseId = 1018002013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1018002007, 1018002013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Job 2:7-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Job 2:7-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Job 2:7-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_002_007.json"}', 15073)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Job 2:7-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_18_002_007.json"}', ContentSize = 15073, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043001001 AND EndVerseId = 1043001004
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043001001, 1043001004)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 1:1-4' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 1:1-4', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 1:1-4', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_001.json"}', 7872)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 1:1-4', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_001.json"}', ContentSize = 7872, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043003015 AND EndVerseId = 1043003022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043003015, 1043003022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 3:15-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 3:15-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 3:15-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_003_015.json"}', 15032)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 3:15-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_003_015.json"}', ContentSize = 15032, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043003023 AND EndVerseId = 1043003038
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043003023, 1043003038)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 3:23-38' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 3:23-38', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 3:23-38', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_003_023.json"}', 9340)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 3:23-38', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_003_023.json"}', ContentSize = 9340, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043004001 AND EndVerseId = 1043004013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043004001, 1043004013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 4:1-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 4:1-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 4:1-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_004_001.json"}', 18456)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 4:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_004_001.json"}', ContentSize = 18456, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043004014 AND EndVerseId = 1043004030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043004014, 1043004030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 4:14-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 4:14-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 4:14-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_004_014.json"}', 20648)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 4:14-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_004_014.json"}', ContentSize = 20648, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043004031 AND EndVerseId = 1043004044
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043004031, 1043004044)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 4:31-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 4:31-44', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 4:31-44', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_004_031.json"}', 17414)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 4:31-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_004_031.json"}', ContentSize = 17414, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043005001 AND EndVerseId = 1043005011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043005001, 1043005011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 5:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 5:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 5:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_001.json"}', 15404)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 5:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_001.json"}', ContentSize = 15404, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043005012 AND EndVerseId = 1043005016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043005012, 1043005016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 5:12-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 5:12-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 5:12-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_012.json"}', 10688)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 5:12-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_012.json"}', ContentSize = 10688, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043005017 AND EndVerseId = 1043005026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043005017, 1043005026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 5:17-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 5:17-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 5:17-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_017.json"}', 20215)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 5:17-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_017.json"}', ContentSize = 20215, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043005027 AND EndVerseId = 1043005039
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043005027, 1043005039)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 5:27-39' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 5:27-39', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 5:27-39', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_027.json"}', 24263)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 5:27-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_005_027.json"}', ContentSize = 24263, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006001 AND EndVerseId = 1043006011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006001, 1043006011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_001.json"}', 19284)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_001.json"}', ContentSize = 19284, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043001005 AND EndVerseId = 1043001025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043001005, 1043001025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 1:5-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 1:5-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 1:5-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_005.json"}', 26571)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 1:5-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_005.json"}', ContentSize = 26571, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006012 AND EndVerseId = 1043006016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006012, 1043006016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:12-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:12-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:12-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_012.json"}', 10274)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:12-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_012.json"}', ContentSize = 10274, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006017 AND EndVerseId = 1043006019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006017, 1043006019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:17-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:17-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:17-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_017.json"}', 9435)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:17-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_017.json"}', ContentSize = 9435, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006020 AND EndVerseId = 1043006026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006020, 1043006026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:20-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:20-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:20-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_020.json"}', 14220)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:20-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_020.json"}', ContentSize = 14220, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006027 AND EndVerseId = 1043006036
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006027, 1043006036)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:27-36' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:27-36', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:27-36', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_027.json"}', 11525)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:27-36', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_027.json"}', ContentSize = 11525, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006037 AND EndVerseId = 1043006042
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006037, 1043006042)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:37-42' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:37-42', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:37-42', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_037.json"}', 11570)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:37-42', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_037.json"}', ContentSize = 11570, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043006043 AND EndVerseId = 1043006049
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043006043, 1043006049)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 6:43-49' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 6:43-49', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 6:43-49', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_043.json"}', 11066)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 6:43-49', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_006_043.json"}', ContentSize = 11066, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043007001 AND EndVerseId = 1043007010
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043007001, 1043007010)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 7:1-10' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 7:1-10', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 7:1-10', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_001.json"}', 16166)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 7:1-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_001.json"}', ContentSize = 16166, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043007011 AND EndVerseId = 1043007017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043007011, 1043007017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 7:11-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 7:11-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 7:11-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_011.json"}', 13437)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 7:11-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_011.json"}', ContentSize = 13437, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043007018 AND EndVerseId = 1043007035
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043007018, 1043007035)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 7:18-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 7:18-35', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 7:18-35', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_018.json"}', 24961)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 7:18-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_018.json"}', ContentSize = 24961, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043007036 AND EndVerseId = 1043008003
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043007036, 1043008003)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 7:36-8:3' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 7:36-8:3', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 7:36-8:3', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_036.json"}', 23430)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 7:36-8:3', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_007_036.json"}', ContentSize = 23430, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043008004 AND EndVerseId = 1043008015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043008004, 1043008015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 8:4-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 8:4-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 8:4-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_004.json"}', 19913)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 8:4-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_004.json"}', ContentSize = 19913, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043008016 AND EndVerseId = 1043008018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043008016, 1043008018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 8:16-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 8:16-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 8:16-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_016.json"}', 9683)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 8:16-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_016.json"}', ContentSize = 9683, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043008019 AND EndVerseId = 1043008021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043008019, 1043008021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 8:19-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 8:19-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 8:19-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_019.json"}', 8562)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 8:19-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_019.json"}', ContentSize = 8562, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043008022 AND EndVerseId = 1043008025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043008022, 1043008025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 8:22-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 8:22-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 8:22-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_022.json"}', 11971)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 8:22-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_008_022.json"}', ContentSize = 11971, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043001026 AND EndVerseId = 1043001038
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043001026, 1043001038)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 1:26-38' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 1:26-38', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 1:26-38', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_026.json"}', 16702)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 1:26-38', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_026.json"}', ContentSize = 16702, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043001039 AND EndVerseId = 1043001056
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043001039, 1043001056)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 1:39-56' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 1:39-56', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 1:39-56', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_039.json"}', 14660)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 1:39-56', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_039.json"}', ContentSize = 14660, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043001057 AND EndVerseId = 1043001080
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043001057, 1043001080)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 1:57-80' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 1:57-80', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 1:57-80', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_057.json"}', 25084)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 1:57-80', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_001_057.json"}', ContentSize = 25084, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043016001 AND EndVerseId = 1043016015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043016001, 1043016015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 16:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 16:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 16:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_016_001.json"}', 17103)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 16:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_016_001.json"}', ContentSize = 17103, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043016016 AND EndVerseId = 1043016018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043016016, 1043016018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 16:16-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 16:16-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 16:16-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_016_016.json"}', 10630)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 16:16-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_016_016.json"}', ContentSize = 10630, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043016019 AND EndVerseId = 1043016031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043016019, 1043016031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 16:19-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 16:19-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 16:19-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_016_019.json"}', 19434)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 16:19-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_016_019.json"}', ContentSize = 19434, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043017001 AND EndVerseId = 1043017010
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043017001, 1043017010)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 17:1-10' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 17:1-10', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 17:1-10', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_017_001.json"}', 16372)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 17:1-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_017_001.json"}', ContentSize = 16372, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043017011 AND EndVerseId = 1043017019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043017011, 1043017019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 17:11-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 17:11-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 17:11-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_017_011.json"}', 13569)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 17:11-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_017_011.json"}', ContentSize = 13569, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043017020 AND EndVerseId = 1043017037
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043017020, 1043017037)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 17:20-37' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 17:20-37', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 17:20-37', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_017_020.json"}', 20402)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 17:20-37', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_017_020.json"}', ContentSize = 20402, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043002001 AND EndVerseId = 1043002021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043002001, 1043002021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 2:1-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 2:1-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 2:1-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_002_001.json"}', 21870)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 2:1-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_002_001.json"}', ContentSize = 21870, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043018001 AND EndVerseId = 1043018017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043018001, 1043018017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 18:1-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 18:1-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 18:1-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_018_001.json"}', 24020)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 18:1-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_018_001.json"}', ContentSize = 24020, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043018018 AND EndVerseId = 1043018030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043018018, 1043018030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 18:18-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 18:18-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 18:18-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_018_018.json"}', 14778)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 18:18-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_018_018.json"}', ContentSize = 14778, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043018031 AND EndVerseId = 1043018034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043018031, 1043018034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 18:31-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 18:31-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 18:31-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_018_031.json"}', 10560)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 18:31-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_018_031.json"}', ContentSize = 10560, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043002022 AND EndVerseId = 1043002040
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043002022, 1043002040)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 2:22-40' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 2:22-40', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 2:22-40', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_002_022.json"}', 24557)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 2:22-40', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_002_022.json"}', ContentSize = 24557, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043002041 AND EndVerseId = 1043002052
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043002041, 1043002052)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 2:41-52' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 2:41-52', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 2:41-52', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_002_041.json"}', 16137)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 2:41-52', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_002_041.json"}', ContentSize = 16137, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1043003001 AND EndVerseId = 1043003014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1043003001, 1043003014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Luke 3:1-14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Luke 3:1-14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Luke 3:1-14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_003_001.json"}', 22775)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Luke 3:1-14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_43_003_001.json"}', ContentSize = 22775, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041001001 AND EndVerseId = 1041001017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041001001, 1041001017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 1:1-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 1:1-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 1:1-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_001_001.json"}', 28323)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 1:1-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_001_001.json"}', ContentSize = 28323, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041027032 AND EndVerseId = 1041027044
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041027032, 1041027044)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 27:32-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 27:32-44', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 27:32-44', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_032.json"}', 17737)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 27:32-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_032.json"}', ContentSize = 17737, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041027045 AND EndVerseId = 1041027056
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041027045, 1041027056)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 27:45-56' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 27:45-56', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 27:45-56', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_045.json"}', 20086)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 27:45-56', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_045.json"}', ContentSize = 20086, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041027057 AND EndVerseId = 1041027066
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041027057, 1041027066)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 27:57-66' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 27:57-66', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 27:57-66', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_057.json"}', 13829)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 27:57-66', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_057.json"}', ContentSize = 13829, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041028001 AND EndVerseId = 1041028015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041028001, 1041028015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 28:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 28:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 28:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_028_001.json"}', 17184)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 28:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_028_001.json"}', ContentSize = 17184, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041028016 AND EndVerseId = 1041028020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041028016, 1041028020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 28:16-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 28:16-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 28:16-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_028_016.json"}', 13236)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 28:16-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_028_016.json"}', ContentSize = 13236, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041005027 AND EndVerseId = 1041005032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041005027, 1041005032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 5:27-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 5:27-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 5:27-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_027.json"}', 17822)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 5:27-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_027.json"}', ContentSize = 17822, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041005033 AND EndVerseId = 1041005042
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041005033, 1041005042)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 5:33-42' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 5:33-42', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 5:33-42', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_033.json"}', 19954)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 5:33-42', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_033.json"}', ContentSize = 19954, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041005043 AND EndVerseId = 1041005048
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041005043, 1041005048)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 5:43-48' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 5:43-48', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 5:43-48', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_043.json"}', 19068)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 5:43-48', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_043.json"}', ContentSize = 19068, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041006001 AND EndVerseId = 1041006008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041006001, 1041006008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 6:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 6:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 6:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_006_001.json"}', 20696)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 6:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_006_001.json"}', ContentSize = 20696, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041006009 AND EndVerseId = 1041006018
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041006009, 1041006018)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 6:9-18' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 6:9-18', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 6:9-18', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_006_009.json"}', 22415)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 6:9-18', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_006_009.json"}', ContentSize = 22415, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041006019 AND EndVerseId = 1041006034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041006019, 1041006034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 6:19-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 6:19-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 6:19-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_006_019.json"}', 24972)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 6:19-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_006_019.json"}', ContentSize = 24972, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041007001 AND EndVerseId = 1041007012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041007001, 1041007012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 7:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 7:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 7:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_007_001.json"}', 25555)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 7:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_007_001.json"}', ContentSize = 25555, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041007013 AND EndVerseId = 1041007029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041007013, 1041007029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 7:13-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 7:13-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 7:13-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_007_013.json"}', 27065)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 7:13-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_007_013.json"}', ContentSize = 27065, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041001018 AND EndVerseId = 1041001025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041001018, 1041001025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 1:18-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 1:18-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 1:18-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_001_018.json"}', 17203)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 1:18-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_001_018.json"}', ContentSize = 17203, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041010001 AND EndVerseId = 1041010015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041010001, 1041010015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 10:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 10:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 10:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_010_001.json"}', 20024)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 10:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_010_001.json"}', ContentSize = 20024, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013001 AND EndVerseId = 1041013009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013001, 1041013009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:1-9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:1-9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:1-9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_001.json"}', 12651)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:1-9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_001.json"}', ContentSize = 12651, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013010 AND EndVerseId = 1041013017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013010, 1041013017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:10-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:10-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:10-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_010.json"}', 14307)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:10-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_010.json"}', ContentSize = 14307, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013018 AND EndVerseId = 1041013023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013018, 1041013023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:18-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:18-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:18-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_018.json"}', 12367)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:18-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_018.json"}', ContentSize = 12367, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013024 AND EndVerseId = 1041013035
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013024, 1041013035)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:24-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:24-35', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:24-35', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_024.json"}', 15119)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:24-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_024.json"}', ContentSize = 15119, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013036 AND EndVerseId = 1041013043
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013036, 1041013043)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:36-43' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:36-43', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:36-43', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_036.json"}', 12913)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:36-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_036.json"}', ContentSize = 12913, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013044 AND EndVerseId = 1041013053
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013044, 1041013053)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:44-53' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:44-53', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:44-53', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_044.json"}', 13632)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:44-53', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_044.json"}', ContentSize = 13632, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041013054 AND EndVerseId = 1041013058
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041013054, 1041013058)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 13:54-58' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 13:54-58', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 13:54-58', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_054.json"}', 9690)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 13:54-58', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_013_054.json"}', ContentSize = 9690, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041014001 AND EndVerseId = 1041014012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041014001, 1041014012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 14:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 14:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 14:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_014_001.json"}', 16124)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 14:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_014_001.json"}', ContentSize = 16124, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041014013 AND EndVerseId = 1041014021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041014013, 1041014021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 14:13-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 14:13-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 14:13-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_014_013.json"}', 13409)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 14:13-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_014_013.json"}', ContentSize = 13409, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041015021 AND EndVerseId = 1041015028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041015021, 1041015028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 15:21-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 15:21-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 15:21-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_015_021.json"}', 17900)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 15:21-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_015_021.json"}', ContentSize = 17900, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041015029 AND EndVerseId = 1041015039
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041015029, 1041015039)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 15:29-39' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 15:29-39', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 15:29-39', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_015_029.json"}', 18808)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 15:29-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_015_029.json"}', ContentSize = 18808, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041016001 AND EndVerseId = 1041016012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041016001, 1041016012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 16:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 16:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 16:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_016_001.json"}', 20011)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 16:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_016_001.json"}', ContentSize = 20011, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041016013 AND EndVerseId = 1041016020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041016013, 1041016020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 16:13-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 16:13-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 16:13-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_016_013.json"}', 17605)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 16:13-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_016_013.json"}', ContentSize = 17605, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041017001 AND EndVerseId = 1041017013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041017001, 1041017013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 17:1-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 17:1-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 17:1-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_017_001.json"}', 17445)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 17:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_017_001.json"}', ContentSize = 17445, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041017014 AND EndVerseId = 1041017021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041017014, 1041017021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 17:14-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 17:14-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 17:14-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_017_014.json"}', 17496)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 17:14-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_017_014.json"}', ContentSize = 17496, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041017022 AND EndVerseId = 1041017027
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041017022, 1041017027)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 17:22-27' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 17:22-27', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 17:22-27', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_017_022.json"}', 14248)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 17:22-27', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_017_022.json"}', ContentSize = 14248, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041004001 AND EndVerseId = 1041004011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041004001, 1041004011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 4:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 4:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 4:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_004_001.json"}', 18389)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 4:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_004_001.json"}', ContentSize = 18389, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041018001 AND EndVerseId = 1041018009
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041018001, 1041018009)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 18:1-9' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 18:1-9', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 18:1-9', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_001.json"}', 15945)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 18:1-9', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_001.json"}', ContentSize = 15945, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041018010 AND EndVerseId = 1041018014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041018010, 1041018014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 18:10-14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 18:10-14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 18:10-14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_010.json"}', 13011)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 18:10-14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_010.json"}', ContentSize = 13011, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041018015 AND EndVerseId = 1041018020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041018015, 1041018020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 18:15-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 18:15-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 18:15-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_015.json"}', 18653)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 18:15-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_015.json"}', ContentSize = 18653, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041018021 AND EndVerseId = 1041018035
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041018021, 1041018035)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 18:21-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 18:21-35', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 18:21-35', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_021.json"}', 24000)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 18:21-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_018_021.json"}', ContentSize = 24000, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041019001 AND EndVerseId = 1041019012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041019001, 1041019012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 19:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 19:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 19:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_019_001.json"}', 18528)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 19:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_019_001.json"}', ContentSize = 18528, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041019013 AND EndVerseId = 1041019030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041019013, 1041019030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 19:13-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 19:13-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 19:13-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_019_013.json"}', 19422)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 19:13-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_019_013.json"}', ContentSize = 19422, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041020001 AND EndVerseId = 1041020016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041020001, 1041020016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 20:1-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 20:1-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 20:1-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_020_001.json"}', 16236)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 20:1-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_020_001.json"}', ContentSize = 16236, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041020017 AND EndVerseId = 1041020028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041020017, 1041020028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 20:17-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 20:17-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 20:17-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_020_017.json"}', 17933)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 20:17-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_020_017.json"}', ContentSize = 17933, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041020029 AND EndVerseId = 1041020034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041020029, 1041020034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 20:29-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 20:29-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 20:29-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_020_029.json"}', 11374)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 20:29-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_020_029.json"}', ContentSize = 11374, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041004012 AND EndVerseId = 1041004025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041004012, 1041004025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 4:12-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 4:12-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 4:12-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_004_012.json"}', 21591)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 4:12-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_004_012.json"}', ContentSize = 21591, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041021012 AND EndVerseId = 1041021022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041021012, 1041021022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 21:12-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 21:12-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 21:12-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_021_012.json"}', 27435)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 21:12-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_021_012.json"}', ContentSize = 27435, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041021023 AND EndVerseId = 1041021032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041021023, 1041021032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 21:23-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 21:23-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 21:23-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_021_023.json"}', 23587)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 21:23-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_021_023.json"}', ContentSize = 23587, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041023023 AND EndVerseId = 1041023028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041023023, 1041023028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 23:23-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 23:23-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 23:23-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_023_023.json"}', 19240)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 23:23-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_023_023.json"}', ContentSize = 19240, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041005001 AND EndVerseId = 1041005012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041005001, 1041005012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 5:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 5:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 5:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_001.json"}', 19967)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 5:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_001.json"}', ContentSize = 19967, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041023029 AND EndVerseId = 1041023036
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041023029, 1041023036)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 23:29-36' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 23:29-36', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 23:29-36', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_023_029.json"}', 20147)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 23:29-36', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_023_029.json"}', ContentSize = 20147, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041023037 AND EndVerseId = 1041024002
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041023037, 1041024002)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 23:37-24:2' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 23:37-24:2', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 23:37-24:2', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_023_037.json"}', 16479)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 23:37-24:2', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_023_037.json"}', ContentSize = 16479, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041024003 AND EndVerseId = 1041024014
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041024003, 1041024014)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 24:3-14' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 24:3-14', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 24:3-14', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_003.json"}', 21517)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 24:3-14', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_003.json"}', ContentSize = 21517, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041024015 AND EndVerseId = 1041024028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041024015, 1041024028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 24:15-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 24:15-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 24:15-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_015.json"}', 30940)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 24:15-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_015.json"}', ContentSize = 30940, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041024029 AND EndVerseId = 1041024036
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041024029, 1041024036)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 24:29-36' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 24:29-36', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 24:29-36', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_029.json"}', 20025)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 24:29-36', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_029.json"}', ContentSize = 20025, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041024037 AND EndVerseId = 1041024044
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041024037, 1041024044)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 24:37-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 24:37-44', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 24:37-44', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_037.json"}', 18427)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 24:37-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_037.json"}', ContentSize = 18427, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041024045 AND EndVerseId = 1041024051
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041024045, 1041024051)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 24:45-51' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 24:45-51', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 24:45-51', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_045.json"}', 18652)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 24:45-51', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_024_045.json"}', ContentSize = 18652, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041005013 AND EndVerseId = 1041005016
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041005013, 1041005016)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 5:13-16' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 5:13-16', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 5:13-16', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_013.json"}', 16034)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 5:13-16', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_005_013.json"}', ContentSize = 16034, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041026057 AND EndVerseId = 1041026068
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041026057, 1041026068)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 26:57-68' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 26:57-68', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 26:57-68', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_026_057.json"}', 13482)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 26:57-68', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_026_057.json"}', ContentSize = 13482, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041027001 AND EndVerseId = 1041027010
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041027001, 1041027010)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 27:1-10' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 27:1-10', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 27:1-10', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_001.json"}', 14483)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 27:1-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_001.json"}', ContentSize = 14483, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041027011 AND EndVerseId = 1041027026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041027011, 1041027026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 27:11-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 27:11-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 27:11-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_011.json"}', 20794)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 27:11-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_011.json"}', ContentSize = 20794, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1041027027 AND EndVerseId = 1041027031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1041027027, 1041027031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Matthew 27:27-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Matthew 27:27-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Matthew 27:27-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_027.json"}', 10183)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Matthew 27:27-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_41_027_027.json"}', ContentSize = 10183, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001001 AND EndVerseId = 1042001013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001001, 1042001013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:1-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:1-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 1:1-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_001.json"}', 17455)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 1:1-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_001.json"}', ContentSize = 17455, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002023 AND EndVerseId = 1042003006
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002023, 1042003006)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:23-3:6' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:23-3:6', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 2:23-3:6', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_023.json"}', 18565)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 2:23-3:6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_023.json"}', ContentSize = 18565, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042003007 AND EndVerseId = 1042003012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042003007, 1042003012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:7-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:7-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 3:7-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_003_007.json"}', 11444)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 3:7-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_003_007.json"}', ContentSize = 11444, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042003013 AND EndVerseId = 1042003019
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042003013, 1042003019)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:13-19' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:13-19', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 3:13-19', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_003_013.json"}', 8801)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 3:13-19', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_003_013.json"}', ContentSize = 8801, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042003020 AND EndVerseId = 1042003035
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042003020, 1042003035)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 3:20-35' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 3:20-35', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 3:20-35', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_003_020.json"}', 15627)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 3:20-35', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_003_020.json"}', ContentSize = 15627, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004001 AND EndVerseId = 1042004020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004001, 1042004020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:1-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:1-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 4:1-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_001.json"}', 24114)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 4:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_001.json"}', ContentSize = 24114, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004021 AND EndVerseId = 1042004025
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004021, 1042004025)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:21-25' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:21-25', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 4:21-25', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_021.json"}', 10387)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 4:21-25', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_021.json"}', ContentSize = 10387, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004026 AND EndVerseId = 1042004034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004026, 1042004034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:26-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:26-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 4:26-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_026.json"}', 11327)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 4:26-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_026.json"}', ContentSize = 11327, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042004035 AND EndVerseId = 1042004041
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042004035, 1042004041)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 4:35-41' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 4:35-41', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 4:35-41', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_035.json"}', 14176)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 4:35-41', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_004_035.json"}', ContentSize = 14176, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042005001 AND EndVerseId = 1042005020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042005001, 1042005020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:1-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:1-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 5:1-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_005_001.json"}', 19499)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 5:1-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_005_001.json"}', ContentSize = 19499, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042005021 AND EndVerseId = 1042005034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042005021, 1042005034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:21-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:21-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 5:21-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_005_021.json"}', 17926)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 5:21-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_005_021.json"}', ContentSize = 17926, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042005035 AND EndVerseId = 1042005043
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042005035, 1042005043)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 5:35-43' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 5:35-43', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 5:35-43', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_005_035.json"}', 15840)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 5:35-43', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_005_035.json"}', ContentSize = 15840, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001014 AND EndVerseId = 1042001020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001014, 1042001020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:14-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:14-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 1:14-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_014.json"}', 13333)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 1:14-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_014.json"}', ContentSize = 13333, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006001 AND EndVerseId = 1042006006
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006001, 1042006006)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:1-6' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:1-6', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 6:1-6', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_001.json"}', 10109)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 6:1-6', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_001.json"}', ContentSize = 10109, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006006 AND EndVerseId = 1042006013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006006, 1042006013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:6-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:6-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 6:6-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_006.json"}', 11266)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 6:6-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_006.json"}', ContentSize = 11266, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006014 AND EndVerseId = 1042006029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006014, 1042006029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:14-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:14-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 6:14-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_014.json"}', 17334)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 6:14-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_014.json"}', ContentSize = 17334, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006030 AND EndVerseId = 1042006044
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006030, 1042006044)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:30-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:30-44', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 6:30-44', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_030.json"}', 15120)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 6:30-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_030.json"}', ContentSize = 15120, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042006045 AND EndVerseId = 1042006056
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042006045, 1042006056)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 6:45-56' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 6:45-56', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 6:45-56', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_045.json"}', 14420)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 6:45-56', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_006_045.json"}', ContentSize = 14420, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007001 AND EndVerseId = 1042007008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007001, 1042007008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 7:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_001.json"}', 13954)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 7:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_001.json"}', ContentSize = 13954, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007009 AND EndVerseId = 1042007013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007009, 1042007013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:9-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:9-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 7:9-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_009.json"}', 7350)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 7:9-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_009.json"}', ContentSize = 7350, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007014 AND EndVerseId = 1042007023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007014, 1042007023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:14-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:14-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 7:14-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_014.json"}', 11675)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 7:14-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_014.json"}', ContentSize = 11675, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007024 AND EndVerseId = 1042007030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007024, 1042007030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:24-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:24-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 7:24-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_024.json"}', 12808)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 7:24-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_024.json"}', ContentSize = 12808, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042007031 AND EndVerseId = 1042007037
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042007031, 1042007037)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 7:31-37' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 7:31-37', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 7:31-37', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_031.json"}', 11337)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 7:31-37', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_007_031.json"}', ContentSize = 11337, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008001 AND EndVerseId = 1042008010
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008001, 1042008010)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:1-10' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:1-10', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 8:1-10', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_001.json"}', 10066)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 8:1-10', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_001.json"}', ContentSize = 10066, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008011 AND EndVerseId = 1042008021
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008011, 1042008021)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:11-21' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:11-21', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 8:11-21', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_011.json"}', 14759)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 8:11-21', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_011.json"}', ContentSize = 14759, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008022 AND EndVerseId = 1042008026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008022, 1042008026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:22-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:22-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 8:22-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_022.json"}', 9709)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 8:22-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_022.json"}', ContentSize = 9709, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008027 AND EndVerseId = 1042008030
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008027, 1042008030)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:27-30' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:27-30', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 8:27-30', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_027.json"}', 10100)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 8:27-30', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_027.json"}', ContentSize = 10100, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042008031 AND EndVerseId = 1042009001
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042008031, 1042009001)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 8:31-9:1' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 8:31-9:1', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 8:31-9:1', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_031.json"}', 21955)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 8:31-9:1', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_008_031.json"}', ContentSize = 21955, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001021 AND EndVerseId = 1042001028
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001021, 1042001028)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:21-28' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:21-28', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 1:21-28', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_021.json"}', 14275)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 1:21-28', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_021.json"}', ContentSize = 14275, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042009002 AND EndVerseId = 1042009013
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042009002, 1042009013)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:2-13' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:2-13', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 9:2-13', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_009_002.json"}', 16486)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 9:2-13', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_009_002.json"}', ContentSize = 16486, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042009014 AND EndVerseId = 1042009029
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042009014, 1042009029)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:14-29' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:14-29', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 9:14-29', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_009_014.json"}', 17058)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 9:14-29', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_009_014.json"}', ContentSize = 17058, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042009030 AND EndVerseId = 1042009050
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042009030, 1042009050)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 9:30-50' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 9:30-50', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 9:30-50', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_009_030.json"}', 20699)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 9:30-50', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_009_030.json"}', ContentSize = 20699, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010001 AND EndVerseId = 1042010012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010001, 1042010012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 10:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_001.json"}', 15622)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 10:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_001.json"}', ContentSize = 15622, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010013 AND EndVerseId = 1042010031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010013, 1042010031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:13-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:13-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 10:13-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_013.json"}', 19661)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 10:13-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_013.json"}', ContentSize = 19661, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010032 AND EndVerseId = 1042010045
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010032, 1042010045)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:32-45' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:32-45', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 10:32-45', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_032.json"}', 18930)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 10:32-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_032.json"}', ContentSize = 18930, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042010046 AND EndVerseId = 1042010052
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042010046, 1042010052)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 10:46-52' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 10:46-52', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 10:46-52', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_046.json"}', 13029)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 10:46-52', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_010_046.json"}', ContentSize = 13029, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042011001 AND EndVerseId = 1042011011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042011001, 1042011011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 11:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_011_001.json"}', 20133)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 11:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_011_001.json"}', ContentSize = 20133, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042011012 AND EndVerseId = 1042011026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042011012, 1042011026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:12-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:12-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 11:12-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_011_012.json"}', 20124)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 11:12-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_011_012.json"}', ContentSize = 20124, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042011027 AND EndVerseId = 1042011033
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042011027, 1042011033)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 11:27-33' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 11:27-33', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 11:27-33', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_011_027.json"}', 14166)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 11:27-33', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_011_027.json"}', ContentSize = 14166, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012001 AND EndVerseId = 1042012012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012001, 1042012012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 12:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_001.json"}', 18703)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 12:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_001.json"}', ContentSize = 18703, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001029 AND EndVerseId = 1042001034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001029, 1042001034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:29-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:29-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 1:29-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_029.json"}', 10208)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 1:29-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_029.json"}', ContentSize = 10208, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012013 AND EndVerseId = 1042012017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012013, 1042012017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:13-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:13-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 12:13-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_013.json"}', 14277)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 12:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_013.json"}', ContentSize = 14277, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012018 AND EndVerseId = 1042012027
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012018, 1042012027)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:18-27' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:18-27', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 12:18-27', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_018.json"}', 15414)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 12:18-27', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_018.json"}', ContentSize = 15414, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012028 AND EndVerseId = 1042012034
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012028, 1042012034)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:28-34' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:28-34', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 12:28-34', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_028.json"}', 13819)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 12:28-34', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_028.json"}', ContentSize = 13819, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012035 AND EndVerseId = 1042012037
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012035, 1042012037)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:35-37' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:35-37', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 12:35-37', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_035.json"}', 10681)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 12:35-37', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_035.json"}', ContentSize = 10681, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042012038 AND EndVerseId = 1042012044
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042012038, 1042012044)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 12:38-44' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 12:38-44', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 12:38-44', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_038.json"}', 10635)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 12:38-44', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_012_038.json"}', ContentSize = 10635, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042013001 AND EndVerseId = 1042013008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042013001, 1042013008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 13:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 13:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 13:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_001.json"}', 11824)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 13:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_001.json"}', ContentSize = 11824, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042013009 AND EndVerseId = 1042013023
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042013009, 1042013023)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 13:9-23' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 13:9-23', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 13:9-23', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_009.json"}', 20011)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 13:9-23', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_009.json"}', ContentSize = 20011, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042013024 AND EndVerseId = 1042013031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042013024, 1042013031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 13:24-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 13:24-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 13:24-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_024.json"}', 15462)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 13:24-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_024.json"}', ContentSize = 15462, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042013032 AND EndVerseId = 1042013037
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042013032, 1042013037)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 13:32-37' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 13:32-37', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 13:32-37', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_032.json"}', 11586)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 13:32-37', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_013_032.json"}', ContentSize = 11586, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014001 AND EndVerseId = 1042014011
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014001, 1042014011)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:1-11' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:1-11', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:1-11', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_001.json"}', 19028)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:1-11', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_001.json"}', ContentSize = 19028, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014012 AND EndVerseId = 1042014026
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014012, 1042014026)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:12-26' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:12-26', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:12-26', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_012.json"}', 24410)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:12-26', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_012.json"}', ContentSize = 24410, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014027 AND EndVerseId = 1042014031
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014027, 1042014031)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:27-31' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:27-31', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:27-31', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_027.json"}', 10499)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:27-31', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_027.json"}', ContentSize = 10499, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014032 AND EndVerseId = 1042014042
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014032, 1042014042)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:32-42' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:32-42', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:32-42', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_032.json"}', 20412)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:32-42', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_032.json"}', ContentSize = 20412, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014043 AND EndVerseId = 1042014052
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014043, 1042014052)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:43-52' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:43-52', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:43-52', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_043.json"}', 17350)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:43-52', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_043.json"}', ContentSize = 17350, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001035 AND EndVerseId = 1042001039
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001035, 1042001039)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:35-39' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:35-39', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 1:35-39', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_035.json"}', 10097)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 1:35-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_035.json"}', ContentSize = 10097, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014053 AND EndVerseId = 1042014065
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014053, 1042014065)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:53-65' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:53-65', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:53-65', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_053.json"}', 18750)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:53-65', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_053.json"}', ContentSize = 18750, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042014066 AND EndVerseId = 1042014072
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042014066, 1042014072)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 14:66-72' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 14:66-72', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 14:66-72', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_066.json"}', 15723)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 14:66-72', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_014_066.json"}', ContentSize = 15723, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042015001 AND EndVerseId = 1042015015
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042015001, 1042015015)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 15:1-15' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 15:1-15', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 15:1-15', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_001.json"}', 19052)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 15:1-15', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_001.json"}', ContentSize = 19052, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042015016 AND EndVerseId = 1042015032
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042015016, 1042015032)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 15:16-32' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 15:16-32', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 15:16-32', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_016.json"}', 23887)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 15:16-32', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_016.json"}', ContentSize = 23887, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042015033 AND EndVerseId = 1042015039
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042015033, 1042015039)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 15:33-39' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 15:33-39', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 15:33-39', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_033.json"}', 15634)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 15:33-39', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_033.json"}', ContentSize = 15634, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042015040 AND EndVerseId = 1042015047
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042015040, 1042015047)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 15:40-47' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 15:40-47', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 15:40-47', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_040.json"}', 15011)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 15:40-47', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_015_040.json"}', ContentSize = 15011, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042016001 AND EndVerseId = 1042016008
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042016001, 1042016008)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 16:1-8' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 16:1-8', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 16:1-8', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_016_001.json"}', 12529)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 16:1-8', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_016_001.json"}', ContentSize = 12529, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042016009 AND EndVerseId = 1042016020
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042016009, 1042016020)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 16:9-20' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 16:9-20', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 16:9-20', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_016_009.json"}', 17787)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 16:9-20', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_016_009.json"}', ContentSize = 17787, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042001040 AND EndVerseId = 1042001045
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042001040, 1042001045)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 1:40-45' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 1:40-45', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 1:40-45', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_040.json"}', 11522)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 1:40-45', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_001_040.json"}', ContentSize = 11522, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002001 AND EndVerseId = 1042002012
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002001, 1042002012)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:1-12' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:1-12', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 2:1-12', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_001.json"}', 23175)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 2:1-12', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_001.json"}', ContentSize = 23175, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002013 AND EndVerseId = 1042002017
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002013, 1042002017)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:13-17' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:13-17', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 2:13-17', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_013.json"}', 13289)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 2:13-17', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_013.json"}', ContentSize = 13289, Updated = GETUTCDATE() WHERE Id = @ResourceContentId

SELECT @PassageId = NULL
SELECT @ResourceId = NULL
SELECT @ResourceContentId = NULL

SELECT @PassageId = Id FROM Passages WHERE StartVerseId = 1042002018 AND EndVerseId = 1042002022
IF @PassageId IS NULL
BEGIN
    INSERT INTO Passages(StartVerseId, EndVerseId) VALUES (1042002018, 1042002022)
    SELECT @PassageId = @@IDENTITY
END

SELECT @ResourceId  = Id FROM Resources WHERE EnglishLabel = 'CBBT-ER Mark 2:18-22' AND MediaType = 1
IF @ResourceId IS NULL
BEGIN
    INSERT INTO Resources(Type, EnglishLabel, MediaType) VALUES (1, 'CBBT-ER Mark 2:18-22', 1)
    SELECT @ResourceId = @@IDENTITY
END

SELECT @PassageResourceCount = COUNT(*) FROM PassageResources WHERE PassageId = @PassageId AND ResourceId = @ResourceId
IF @PassageResourceCount = 0
    INSERT INTO PassageResources(PassageId, ResourceId) VALUES (@PassageId, @ResourceId)

SELECT @ResourceContentId = Id FROM ResourceContents WHERE ResourceId = @ResourceId AND LanguageId = 1
IF @ResourceContentId IS NULL
    INSERT INTO ResourceContents(ResourceId, LanguageId, DisplayName, [Version], Completed, Trusted, Content, ContentSize) VALUES (@ResourceId, 1, 'Mark 2:18-22', 1, 1, 1, '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_018.json"}', 10924)
ELSE
    UPDATE ResourceContents SET DisplayName = 'Mark 2:18-22', [Version] = 1, Completed = 1, Trusted = 1, Content = '{"url": "https://cdn.aquifer.bible/aquifer-content/resources/CBBTER/ENG/text/ENG_CBBTER_42_002_018.json"}', ContentSize = 10924, Updated = GETUTCDATE() WHERE Id = @ResourceContentId
