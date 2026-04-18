namespace PAS.Web.Models;

public class SupervisorExpertise
{
    public int Id { get; set; }

    public string SupervisorId { get; set; } = string.Empty;
    public ApplicationUser? Supervisor { get; set; }

    public int ResearchAreaId { get; set; }
    public ResearchArea? ResearchArea { get; set; }
}