using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAS.Web.Data;
using PAS.Web.Models;
using PAS.Web.ViewModels;

namespace PAS.Web.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Dashboard()
    {
        var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var proposals = await _context.ProjectProposals
            .Include(p => p.ResearchArea)
            .Where(p => p.StudentId == studentId)
            .ToListAsync();

        return View(proposals);
    }﻿
