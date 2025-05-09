using Aquifer.Jobs.Services.TranslationProcessors;

namespace Aquifer.Jobs.UnitTests.Services.TranslationProcessors;

public class PortugueseTranslationProcessingServiceTests
{
    private readonly PortugueseTranslationProcessingService _service = new();

    [Theory]
    [InlineData(
        """A maioria dos especialistas acredita que os israelitas aprenderam a cultivar com os cananeus, pois estavam em contato com eles quando se estabeleceram na Terra Prometida. Embora se saiba que o cultivo de grãos existia antes disso, com Caim sendo um agricultor ou "lavrador da terra" (Gn. 4.2), não está claro o que exatamente ele cultivava. Arqueólogos encontraram evidências de cultivo de grãos datando de cerca de 6800 a.C. no Oriente Próximo. Isaque semeou grãos em Gerar (Gên 26.12), e José sonhou com feixes de grãos (Ruth 37.6–7). José provavelmente aprendeu mais sobre o cultivo de grãos com os egípcios, que os cultivavam nos solos ricos do Nilo. (2 Sam 2.10) O trigo era uma 2sam das culturas mais importantes. Salomão enviou grandes quantidades de trigo, junto com cevada e óleo, para Hirão (1Sam 2.10), e continuou a ser uma grande exportação (Eze 27.17). A cevada era a segunda cultura mais importante. Foi o principal ingrediente do pão no início (jud 7.13). Mais tarde, tornou-se um alimento significativo para pessoas mais pobres (Jo. 6.9,13). Também era usada como ração para gado. Por exemplo some example. See cap 2 and caps 3 and 4. O rei Hazael de Damasco capturou Aroer, assegurando o controle sírio de Transjordânia (2 Rs 10.33). (<span data-bnType="bibleReference" data-verses="[[1012010033,1012010033]]">2&nbsp;Rs 10.33</span>)""",
        """A maioria dos especialistas acredita que os israelitas aprenderam a cultivar com os cananeus, pois estavam em contato com eles quando se estabeleceram na Terra Prometida. Embora se saiba que o cultivo de grãos existia antes disso, com Caim sendo um agricultor ou "lavrador da terra" (Gn 4.2), não está claro o que exatamente ele cultivava. Arqueólogos encontraram evidências de cultivo de grãos datando de cerca de 6800 a.C. no Oriente Próximo. Isaque semeou grãos em Gerar (Gn 26.12), e José sonhou com feixes de grãos (Rt 37.6–7). José provavelmente aprendeu mais sobre o cultivo de grãos com os egípcios, que os cultivavam nos solos ricos do Nilo. (2Sm 2.10) O trigo era uma 2sam das culturas mais importantes. Salomão enviou grandes quantidades de trigo, junto com cevada e óleo, para Hirão (1Sm 2.10), e continuou a ser uma grande exportação (Ez 27.17). A cevada era a segunda cultura mais importante. Foi o principal ingrediente do pão no início (Jz 7.13). Mais tarde, tornou-se um alimento significativo para pessoas mais pobres (Jó 6.9,13). Também era usada como ração para gado. E.g. some example. See cap. 2 and caps. 3 and 4. O rei Hazael de Damasco capturou Aroer, assegurando o controle sírio de Transjordânia (2Rs 10.33). (<span data-bntype="bibleReference" data-verses="[[1012010033,1012010033]]">2Rs 10.33</span>)""")]
    public async Task PostProcessHtmlAsync_WithBadAbbreviations_ShouldReturnCorrectAbbreviations(string text, string expectedText)
    {
        var response = await _service.PostProcessHtmlAsync(text, TestContext.Current.CancellationToken);
        Assert.Equal(expectedText, response);
    }
}