namespace PAS.Web.Models;

public class Match
{
    public int Id { get; set; }

    public int ProjectProposalId { get; set; }
    public ProjectProposal? ProjectProposal { get; set; }

    public string SupervisorId { get; set; } = string.Empty;
    public ApplicationUser? Supervisor { get; set; }

    public MatchStatus Status { get; set; } = MatchStatus.Interested;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ConfirmedAt { get; set; }
}