using System.Text.Json;
using SimpsonsDle.Api.Models;

namespace SimpsonsDle.Api.Services;

public class CharacterService
{
    private readonly List<Character> _characters;

    public CharacterService()
    {
        // Ruta dinámica para encontrar el JSON en la carpeta Data
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "characters.json");

        try
        {
            if (!File.Exists(jsonPath))
            {
                _characters = new List<Character>();
                return;
            }

            var json = File.ReadAllText(jsonPath);

            // Usamos PropertyNameCaseInsensitive para que coincida con tu JSON aunque varíen las mayúsculas
            _characters = JsonSerializer.Deserialize<List<Character>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Character>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar personajes: {ex.Message}");
            _characters = new List<Character>();
        }
    }

    // Retorna todos los personajes (usado por el buscador)
    public List<Character> GetAll()
    {
        return _characters;
    }

    // Busca un personaje específico por su slug
    public Character? GetBySlug(string slug)
    {
        return _characters.FirstOrDefault(c => c.Slug == slug);
    }

    public Character GetYesterdayCharacter()
    {
    var all = _characters;
    // Usamos el día anterior para el índice
    int dayOfYear = DateTime.Now.AddDays(-1).DayOfYear;
    int year = DateTime.Now.Year;
    int index = (dayOfYear + year) % all.Count;
    return all[index];
    }

    // Lógica para determinar el personaje del día basado en la fecha actual
    public Character GetTodayCharacter()
    {
        var all = _characters; // Tu lista de 60
        // Usamos la fecha como "semilla" para que el índice sea el mismo para todos hoy
        int dayOfYear = DateTime.Now.DayOfYear;
        int year = DateTime.Now.Year;
        int index = (dayOfYear + year) % all.Count;
        return all[index];
    }

    // Compara el intento del usuario contra el objetivo del día
    public Dictionary<string, string> Compare(Character guess, Character target)
{
    // Definimos el diccionario que el compilador no encontraba
    var results = new Dictionary<string, string>();

    results["gender"] = guess.Gender == target.Gender ? "correct" : "wrong";
    results["ageGroup"] = guess.AgeGroup == target.AgeGroup ? "correct" : "wrong";
    results["hair"] = guess.Hair == target.Hair ? "correct" : "wrong";
    results["job"] = guess.Job == target.Job ? "correct" : "wrong";
    results["status"] = guess.Status == target.Status ? "correct" : "wrong";
    results["extra"] = guess.Extra == target.Extra ? "correct" : "wrong";

    // Lógica de Temporada con flechas
    if (guess.FirstSeason == target.FirstSeason)
    {
        results["season"] = "correct";
    }
    else
    {
        results["season"] = guess.FirstSeason < target.FirstSeason ? "wrong higher" : "wrong lower";
    }

    return results;
}
}