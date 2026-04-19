using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PAS.Web.Data;
using PAS.Web.Models;
using PAS.Web.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PAS.Web.Controllers;

[Authorize(Roles = "Supervisor")]
public class SupervisorController : Controller
{
    private readonly IMatchingService _matchingService;
    private readonly ApplicationDbContext _context;

    public SupervisorController(IMatchingService matchingService, ApplicationDbContext context)
    {
        _matchingService = matchingService;
        _context = context;
    }

    public async Task<IActionResult> Dashboard()
    {
        var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var projects = await _matchingService.GetAnonymousProjectsForSupervisorAsync(supervisorId);
        return View(projects);
    }
[HttpGet]
public async Task<IActionResult> Expertise()
{
    var researchAreas = await _context.ResearchAreas.OrderBy(r => r.Name).ToListAsync();
    ViewBag.ResearchAreas = new MultiSelectList(researchAreas, "Id", "Name");
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Expertise(List<int> selectedAreas)
{
    var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    var oldItems = _context.SupervisorExpertise.Where(x => x.SupervisorId == supervisorId);
    _context.SupervisorExpertise.RemoveRange(oldItems);

    foreach (var areaId in selectedAreas)
    {
        _context.SupervisorExpertise.Add(new SupervisorExpertise
        {
            SupervisorId = supervisorId,
            ResearchAreaId = areaId
        });
    }

    await _context.SaveChangesAsync();
    TempData["Success"] = "Expertise areas saved successfully.";
    return RedirectToAction(nameof(Dashboard));
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ExpressInterest(int proposalId)
{
    var supervisorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    var result = await _matchingService.ExpressInterestAsync(proposalId, supervisorId);

    if (!result)
    {
        TempData["Success"] = "Express Interest failed or already exists.";
        return RedirectToAction(nameof(Dashboard));
    }

    TempData["Success"] = "Interest expressed successfully!";
    return RedirectToAction(nameof(Dashboard));
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ConfirmMatch(int proposalId)
{
    var supervisorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    var result = await _matchingService.ConfirmMatchAsync(proposalId, supervisorId);

    if (!result)
    {
        TempData["Success"] = "Match confirmation failed.";
        return RedirectToAction(nameof(Dashboard));
    }

    TempData["Success"] = "Match confirmed successfully!";
    return RedirectToAction("MatchedDetails", new { proposalId });
}
public async Task<IActionResult> MatchedDetails(int proposalId)
{
    var supervisorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    var match = await _context.Matches
        .Include(m => m.ProjectProposal)
        .ThenInclude(p => p.Student)
        .FirstOrDefaultAsync(m => m.ProjectProposalId == proposalId && m.SupervisorId == supervisorId);

    if (match == null)
    {
        TempData["Success"] = "Match not found!";
        return RedirectToAction("Dashboard");
    }

    return View(match);
}
