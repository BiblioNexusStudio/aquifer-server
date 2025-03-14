﻿namespace Aquifer.API.Endpoints.Projects.List;

public class Response
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Language { get; set; }
    public required string ProjectLead { get; set; }
    public required string? Manager { get; set; }
    public required string Resource { get; set; }
    public required int ItemCount { get; set; }
    public required int WordCount { get; set; }
    public required string Company { get; set; }
    public required string ProjectPlatform { get; set; }
    public required int? Days { get; set; }
    public required bool IsStarted { get; set; }
    public required bool IsCompleted { get; set; }

    public ProjectResourceStatusCounts Counts { get; set; } = null!;
}