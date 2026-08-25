namespace TrainingApi.Models;

public class SubmitRequest
{
    public string ParticipantName { get; set; } = "";
    public List<int> SelectedChoiceIndexes { get; set; } = new(); // one per question, in order
}