namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.List;

public class Response
{
    public required int? ResourceId { get; set; }
    public required string ResourceType { get; set; }
    public required string Title { get; set; }
    public required MarketingLicenseInfo? LicenseInfo { get; set; }
    public required ParentResourceStatus Status { get; set; }

    // this is a merger of the BibleLicenseInfo and ResourceLicenseInfo types
    public sealed class MarketingLicenseInfo
    {
        public required string Title { get; init; }
        public required LicenseCopyright Copyright { get; init; }
        public required IReadOnlyList<LicenseDetails> Licenses { get; init; }

        // not present in Bibles
        public bool? ShowAdaptationNoticeForEnglish { get; set; }
        public bool? ShowAdaptationNoticeForNonEnglish { get; set; }

        public sealed class LicenseCopyright
        {
            public string? Dates { get; set; }
            public required LicenseCopyrightHolder Holder { get; init; }
        }

        public sealed class LicenseCopyrightHolder
        {
            public required string Name { get; init; }

            // missing for some Bibles
            public string? Url { get; set; }
        }

        public sealed class LicenseDetails
        {
            public required LicenseData Eng { get; init; }
        }

        public sealed class LicenseData
        {
            public required string Name { get; init; }
            public required string Url { get; init; }
        }
    }
}