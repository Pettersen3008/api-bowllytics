using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bowllytics.Models;

public class End
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MatchId { get; set; }
    [JsonIgnore]
    public Match Match { get; set; }
    public int EndNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int Player1Score { get; set; } = 0;
    public int Player2Score { get; set; } = 0;
}
