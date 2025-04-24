using SQLite;

public class MParent
{
    [PrimaryKey, AutoIncrement] // Sætter 'Id' som primær nøgle og auto-inkrementerende (øges automatisk)
    public int Id { get; set; }
    public string Name { get; set; } // Navn på forælder
    public string Password { get; set; }
    public string Email { get; set; }
    public int? SustainabilityScore { get; set; } // Bæredygtighedsscore (kan være null)
    public int? ReliabilityScore { get; set; } // Pålidelighedsscore (kan være null)
    public float Distance { get; set; }
    public byte[] ProfilePic { get; set; }
}

