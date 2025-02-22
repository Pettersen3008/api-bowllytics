using System.ComponentModel.DataAnnotations;

namespace Bowllytics.Models;

public class Score
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EndId { get; set; }
    public End End { get; set; }
    public Guid ParticipantId { get; set; }
    public User Participant { get; set; }
    public int ScoreValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
