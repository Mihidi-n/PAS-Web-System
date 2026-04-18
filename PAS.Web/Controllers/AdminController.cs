using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAS.Web.Data;
using PAS.Web.Models;

namespace PAS.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }﻿
    public async Task<IActionResult> Dashboard()
{
    var matches = await _context.Matches
        .Include(m => m.ProjectProposal)
        .ThenInclude(p => p.Student)
        .Include(m => m.Supervisor)
        .ToListAsync();

    return View(matches);
}
[HttpPost]
public async Task<IActionResult> Reassign(int id, string supervisorId)
{
    var match = await _context.Matches.FindAsync(id);

    match.SupervisorId = supervisorId;

    await _context.SaveChangesAsync();

    TempData["Success"] = "Supervisor reassigned successfully!";
    return RedirectToAction("Dashboard");
}
