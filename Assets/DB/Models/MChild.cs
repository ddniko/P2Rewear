using SQLite;
// 'MChild' klassen svarer til tabellen 'Children' i databasen
public class MChild
{
    [PrimaryKey, AutoIncrement] // Sætter 'Id' som primær nøgle og auto-inkrementerende
    public int Id { get; set; }
    public int ParentId { get; set; } // Forælderens Id (refererer til MParent tabellen)
    public string Name { get; set; } // Navn på barnet
    public int Gender { get; set; } // gender, 0 for pige, 1 for dreng
    public string Tags { get; set; } // comma separated values
    public int? Age { get; set; } // Alder på barnet 
    public string Size { get; set; } // Størrelse på barnet (kan være null)

}

