using PAS.Web.Models;

namespace PAS.Web.ViewModels;

public class BlindProjectVm
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
    public string TechnicalStack { get; set; } = string.Empty;
    public string ResearchAreaName { get; set; } = string.Empty;
    public ProposalStatus Status { get; set; }
}
