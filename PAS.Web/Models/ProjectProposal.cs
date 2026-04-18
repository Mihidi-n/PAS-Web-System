using System.ComponentModel.DataAnnotations;

namespace PAS.Web.Models;

public class ProjectProposal
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string Abstract { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string TechnicalStack { get; set; } = string.Empty;

    [Required]
    public int ResearchAreaId { get; set; }
    public ResearchArea? ResearchArea { get; set; }

    [Required]
    public string StudentId { get; set; } = string.Empty;
    public ApplicationUser? Student { get; set; }

    [Required]
    public ProposalStatus Status { get; set; } = ProposalStatus.Pending;

    public bool IdentityRevealed { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}