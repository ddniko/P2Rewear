using SQLite;
// 'Child' klassen svarer til tabellen 'Children' i databasen
public class Child
{
    [PrimaryKey, AutoIncrement] // Sætter 'Id' som primær nøgle og auto-inkrementerende
    public int Id { get; set; }

    public int ParentId { get; set; } // Forælderens Id (refererer til Parent tabellen)
    public string Name { get; set; } // Navn på barnet
    public int? Age { get; set; } // Alder på barnet (kan være null)
    public string Size { get; set; } // Størrelse på barnet (kan være null)

 
}

