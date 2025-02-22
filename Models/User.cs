using Microsoft.AspNetCore.Identity;

namespace Bowllytics.Models;

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}