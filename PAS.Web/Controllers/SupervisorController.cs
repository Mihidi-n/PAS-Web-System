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
