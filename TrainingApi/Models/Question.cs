namespace TrainingApi.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public List<Choice> Choices { get; set; } = new();
}