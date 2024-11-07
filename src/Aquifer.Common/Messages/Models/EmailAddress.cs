namespace Aquifer.Common.Messages.Models;

public sealed record EmailAddress(
    string Email,
    string? Name = null);