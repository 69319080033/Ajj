using Microsoft.EntityFrameworkCore;
using TeamLinkHub.Models;

namespace TeamLinkHub.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.ToolLinks.AnyAsync()) return;

        db.ToolLinks.AddRange(
            new ToolLink
            {
                Name = "Figma",
                Url = "https://www.figma.com/",
                Category = "ออกแบบ",
                Description = "เครื่องมือออกแบบและทำงานร่วมกันของทีม",
                Status = ToolStatus.Active
            },
            new ToolLink
            {
                Name = "คู่มือทีม",
                Url = "https://docs.example.com/team-guide",
                Category = "เอกสาร",
                Description = "คู่มือ ขั้นตอน และมาตรฐานการทำงาน",
                Status = ToolStatus.Active
            },
            new ToolLink
            {
                Name = "ระบบติดตามงาน",
                Url = "https://tasks.example.com/",
                Category = "ติดตามงาน",
                Description = "ติดตามสถานะและมอบหมายงานของทีม",
                Status = ToolStatus.Active
            });

        await db.SaveChangesAsync();
    }
}
