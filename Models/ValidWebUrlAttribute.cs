using System.ComponentModel.DataAnnotations;

namespace TeamLinkHub.Models;

/// <summary>Accepts only an absolute HTTP or HTTPS URL with a host. No network request is made.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ValidWebUrlAttribute : ValidationAttribute
{
    public ValidWebUrlAttribute()
        : base("กรุณาระบุ URL แบบเต็มที่ขึ้นต้นด้วย http:// หรือ https:// และมีชื่อโฮสต์") { }

    public override bool IsValid(object? value)
    {
        if (value is not string url || string.IsNullOrWhiteSpace(url)) return false;

        return Uri.TryCreate(url.Trim(), UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            && !string.IsNullOrWhiteSpace(uri.Host);
    }
}
