using System.ComponentModel.DataAnnotations;

namespace MvcYummy.Models;

// ID, Name, Time, Difficulty, Number of likes, Ingredients, Process, Tips and Tricks
public class Recipe
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [DataType(DataType.Time)]
    public TimeSpan Time { get; set; }
    public string? Difficulty { get; set; }
    public int Likes { get; set; }
    public string? Ingredients { get; set; }
    public string? Process { get; set; }
    public string? Tips { get; set; }
}