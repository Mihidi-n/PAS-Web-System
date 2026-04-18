using PAS.Web.ViewModels;

namespace PAS.Web.Services.Interfaces;

public interface IMatchingService
{
    Task<List<BlindProjectVm>> GetAnonymousProjectsForSupervisorAsync(string supervisorId);
    Task<bool> ExpressInterestAsync(int proposalId, string supervisorId);
    Task<bool> ConfirmMatchAsync(int proposalId, string supervisorId);
}