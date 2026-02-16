using Microsoft.AspNetCore.Mvc;
using SimpsonsDle.Api.Services;
using SimpsonsDle.Api.DTOs;

namespace SimpsonsDle.Api.Controllers;

[ApiController]
[Route("api/guess")]
public class GuessController : ControllerBase
{
    private readonly CharacterService _service;

    public GuessController(CharacterService service)
    {
        _service = service;
    }

    // GET api/guess/today
    [HttpGet("today")]
    public IActionResult GetToday()
    {
        var character = _service.GetTodayCharacter();

        return Ok(new
        {
            character.Id,
            character.Image
        });
    }

    // POST api/guess
    [HttpPost]
    public IActionResult Guess([FromBody] GuessRequest request)
    {
        var guess = _service.GetAll()
            .FirstOrDefault(c => c.Slug == request.Slug);

        if (guess == null)
            return BadRequest("Character not found");

        var target = _service.GetTodayCharacter();
        var results = _service.Compare(guess, target);

        return Ok(new GuessResult
        {
            Results = results,
            IsCorrect = guess.Id == target.Id
        });
    }

[HttpGet("history")]
public IActionResult GetHistory([FromQuery] string slugs)
{
    if (string.IsNullOrEmpty(slugs)) return Ok(new List<GuessHistoryItem>());

    var target = _service.GetTodayCharacter();
    var slugList = slugs.Split(',');
    
    var history = slugList.Select(slug => {
        var guess = _service.GetAll().FirstOrDefault(c => c.Slug == slug);
        if (guess == null) return null;

        // ESTO ES LO IMPORTANTE: pasar todas las propiedades
        return new GuessHistoryItem {
            Name = guess.Name,
            Image = guess.Image,
            Gender = guess.Gender,
            AgeGroup = guess.AgeGroup,
            Hair = guess.Hair,
            Job = guess.Job,
            FirstSeason = guess.FirstSeason,
            Status = guess.Status,
            Results = _service.Compare(guess, target)
        };
    }).Where(x => x != null).ToList();

    return Ok(history);
}
}
