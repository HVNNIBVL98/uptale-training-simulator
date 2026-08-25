using TrainingApi.Models;

namespace TrainingApi.Data;

public static class ScenarioData
{
    public static List<Question> Questions => new()
    {
        new Question
        {
            Id = 1,
            Text = "You receive an email from 'IT Support' asking you to urgently reset your password via a link. What do you do?",
            Choices = new()
            {
                new Choice { Text = "Click the link immediately, it says urgent", Quality = "risky" },
                new Choice { Text = "Check the sender's real email address and report it to security", Quality = "good" },
                new Choice { Text = "Ignore it and delete without reporting", Quality = "neutral" }
            }
        },
        new Question
        {
            Id = 2,
            Text = "Your monitoring dashboard shows unusual outbound traffic from a workstation at 2 AM. What's your first move?",
            Choices = new()
            {
                new Choice { Text = "Isolate the workstation from the network and start investigating", Quality = "good" },
                new Choice { Text = "Wait until morning to check, it's probably nothing", Quality = "risky" },
                new Choice { Text = "Restart the workstation and see if it happens again", Quality = "neutral" }
            }
        },
        new Question
        {
            Id = 3,
            Text = "A colleague asks you to share your admin credentials so they can finish a task while you're on leave. What do you do?",
            Choices = new()
            {
                new Choice { Text = "Share it, they're trustworthy and it's urgent", Quality = "risky" },
                new Choice { Text = "Politely refuse and suggest they request temporary access through IT", Quality = "good" },
                new Choice { Text = "Change your password after sharing it, just in case", Quality = "neutral" }
            }
        }
    };
}