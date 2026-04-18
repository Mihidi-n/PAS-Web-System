using System.ComponentModel.DataAnnotations;

namespace PAS.Web.ViewModels;

public class ProjectProposalCreateVm
{
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
}