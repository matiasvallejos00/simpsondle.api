namespace SimpsonsDle.Api.DTOs;

public class GuessResult
{
    public Dictionary<string, string> Results { get; set; } = new();
    public bool IsCorrect { get; set; }
}


public class GuessHistoryItem
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Gender { get; set; }
    public string AgeGroup { get; set; }
    public string Hair { get; set; }
    public string Job { get; set; }
    public int FirstSeason { get; set; }
    public string Status { get; set; }
    public Dictionary<string, string> Results { get; set; }
}