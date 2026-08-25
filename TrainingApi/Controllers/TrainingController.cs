using Microsoft.AspNetCore.Mvc;
using TrainingApi.Data;
using TrainingApi.Models;
using TrainingApi.Services;

namespace TrainingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingController : ControllerBase
{
    private readonly GroqService _groqService;
    private static readonly List<SessionResult> _results = new(); // in-memory storage for now

    public TrainingController(GroqService groqService)
    {
        _groqService = groqService;
    }

    [HttpGet("questions")]
    public ActionResult<List<Question>> GetQuestions()
    {
        return Ok(ScenarioData.Questions);
    }

    [HttpPost("submit")]
    public async Task<ActionResult<SessionResult>> Submit([FromBody] SubmitRequest request)
    {
        var questions = ScenarioData.Questions;
        int score = 0;
        var qualities = new List<string>();

        for (int i = 0; i < request.SelectedChoiceIndexes.Count && i < questions.Count; i++)
        {
            var chosenIndex = request.SelectedChoiceIndexes[i];
            var choice = questions[i].Choices[chosenIndex];
            qualities.Add(choice.Quality);

            if (choice.Quality == "good") score += 2;
            else if (choice.Quality == "neutral") score += 1;
            // risky = 0 points
        }

        var feedback = await _groqService.GetFeedbackAsync(score, questions.Count * 2, qualities);

        var result = new SessionResult
        {
            ParticipantName = request.ParticipantName,
            Score = score,
            Feedback = feedback
        };

        _results.Add(result);
        return Ok(result);
    }

    [HttpGet("results")]
    public ActionResult<List<SessionResult>> GetResults()
    {
        return Ok(_results.OrderByDescending(r => r.CompletedAt).ToList());
    }
}