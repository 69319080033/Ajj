using TeamLinkHub.Models;

namespace TeamLinkHub.ViewModels;

public class ToolLinkIndexViewModel
{
    public IReadOnlyList<ToolLink> Items { get; init; } = [];
    public IReadOnlyList<string> Categories { get; init; } = [];
    public string? Search { get; init; }
    public string? Category { get; init; }
    public ToolStatus? Status { get; init; }
}
