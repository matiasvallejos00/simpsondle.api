using Microsoft.AspNetCore.Mvc;
using SimpsonsDle.Api.Services;

namespace SimpsonsDle.Api.Controllers;

[ApiController]
[Route("api/characters")]
public class CharactersController : ControllerBase
{
    private readonly CharacterService _service;

    public CharactersController(CharacterService service)
    {
        _service = service;
    }

    // GET: api/characters
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET: api/characters/today
    [HttpGet("today")]
    public IActionResult GetToday()
    {
        var character = _service.GetTodayCharacter();
        return Ok(character);
    }

    // GET: api/characters/yesterday
    [HttpGet("yesterday")]
    public IActionResult GetYesterday()
    {
        var character = _service.GetYesterdayCharacter();
        return Ok(character);
    }

    [HttpPost("guess")]
    public IActionResult Guess([FromBody] GuessRequest request)
    {
        var all = _service.GetAll();
        var guess = all.FirstOrDefault(c => c.Slug == request.Slug);
        var target = _service.GetTodayCharacter();

        if (guess == null) return NotFound();

        var results = _service.Compare(guess, target);

        return Ok(new
        {
            isCorrect = guess.Slug == target.Slug,
            results = results
        });
    }

}
public class GuessRequest
{
    public string Slug { get; set; }
}