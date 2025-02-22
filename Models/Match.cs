using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bowllytics.Models;

public enum MatchStatus
{
    Scheduled,
    InProgress,
    Completed,
    Cancelled
}

public class Match
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Location { get; set; }
    public string Status { get; set; } = MatchStatus.Scheduled.ToString();
    public Guid CreatedBy { get; set; }
    [JsonIgnore]
    public User CreatedByUser { get; set; }
    public string OpponentName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int TotalEnds { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<End> Ends { get; set; } = new List<End>();
}