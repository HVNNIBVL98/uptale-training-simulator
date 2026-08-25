namespace TrainingApi.Models;

public class Choice
{
    public string Text { get; set; } = "";
    public string Quality { get; set; } = ""; // "good", "neutral", "risky"
}