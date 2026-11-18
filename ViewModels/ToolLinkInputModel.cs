using System.ComponentModel.DataAnnotations;
using TeamLinkHub.Models;

namespace TeamLinkHub.ViewModels;

public class ToolLinkInputModel
{
    [Required(ErrorMessage = "กรุณาระบุชื่อเครื่องมือ")]
    [StringLength(120, ErrorMessage = "ชื่อเครื่องมือต้องไม่เกิน 120 ตัวอักษร")]
    [Display(Name = "ชื่อเครื่องมือ")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณาระบุ URL")]
    [StringLength(2048)]
    [ValidWebUrl]
    [Display(Name = "URL")]
    public string Url { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณาระบุหมวดหมู่")]
    [StringLength(80)]
    [Display(Name = "หมวดหมู่")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณาระบุคำอธิบาย")]
    [StringLength(500, ErrorMessage = "คำอธิบายต้องไม่เกิน 500 ตัวอักษร")]
    [Display(Name = "คำอธิบาย")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "สถานะใช้งาน")]
    public ToolStatus Status { get; set; } = ToolStatus.Active;
}
