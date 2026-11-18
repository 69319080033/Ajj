using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLinkHub.Data;
using TeamLinkHub.Models;
using TeamLinkHub.ViewModels;

namespace TeamLinkHub.Controllers;

public class ToolLinksController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search, string? category, ToolStatus? status)
    {
        var query = db.ToolLinks.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(x => x.Name.Contains(term) || x.Description.Contains(term) || x.Url.Contains(term));
        }
        if (!string.IsNullOrWhiteSpace(category)) query = query.Where(x => x.Category == category);
        if (status.HasValue) query = query.Where(x => x.Status == status);

        var categories = await db.ToolLinks.AsNoTracking()
            .Select(x => x.Category).Distinct().OrderBy(x => x).ToListAsync();

        return View(new ToolLinkIndexViewModel
        {
            Items = await query.OrderBy(x => x.Name).ToListAsync(),
            Categories = categories,
            Search = search,
            Category = category,
            Status = status
        });
    }

    public IActionResult Create() => View(new ToolLinkInputModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ToolLinkInputModel input)
    {
        if (!ModelState.IsValid) return View(input);

        db.ToolLinks.Add(new ToolLink
        {
            Name = input.Name.Trim(), Url = input.Url.Trim(), Category = input.Category.Trim(),
            Description = input.Description.Trim(), Status = input.Status
        });
        await db.SaveChangesAsync();
        TempData["Success"] = "เพิ่มลิงก์เครื่องมือเรียบร้อยแล้ว";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await db.ToolLinks.FindAsync(id);
        if (item is null) return NotFound();
        return View(new ToolLinkInputModel
        {
            Name = item.Name, Url = item.Url, Category = item.Category,
            Description = item.Description, Status = item.Status
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ToolLinkInputModel input)
    {
        var item = await db.ToolLinks.FindAsync(id);
        if (item is null) return NotFound();
        if (!ModelState.IsValid) return View(input);

        item.Name = input.Name.Trim();
        item.Url = input.Url.Trim();
        item.Category = input.Category.Trim();
        item.Description = input.Description.Trim();
        item.Status = input.Status;
        item.UpdatedAtUtc = DateTime.UtcNow;
        await db.SaveChangesAsync();
        TempData["Success"] = "บันทึกการแก้ไขเรียบร้อยแล้ว";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.ToolLinks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await db.ToolLinks.FindAsync(id);
        if (item is not null)
        {
            db.ToolLinks.Remove(item);
            await db.SaveChangesAsync();
            TempData["Success"] = "ลบลิงก์เครื่องมือเรียบร้อยแล้ว";
        }
        return RedirectToAction(nameof(Index));
    }
}
