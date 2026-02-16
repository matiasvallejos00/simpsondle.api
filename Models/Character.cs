namespace SimpsonsDle.Api.Models;

public class Character
{
    public int Id { get; set; }
    public string Slug { get; set; } = "";
    public string Name { get; set; } = "";
    public string Gender { get; set; } = "";
    public string AgeGroup { get; set; } = "";
    public string Hair { get; set; } = "";
    public string Job { get; set; } = "";
    public int FirstSeason { get; set; }
    public string Status { get; set; } = "";
    public string Extra { get; set; } = "";
    public string Image { get; set; } = "";
}
