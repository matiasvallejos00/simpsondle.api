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

    [HttpGet]
    public IActionResult GetAll()
    {
        // Devolvemos la lista completa sin recortes para que el 
        // frontend tenga todos los atributos (Género, Pelo, etc.)
        var characters = _service.GetAll();
        return Ok(characters);
    }
}