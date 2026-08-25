namespace TrainingApi.Models;

public class SessionResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ParticipantName { get; set; } = "";
    public int Score { get; set; }
    public string Feedback { get; set; } = "";
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}