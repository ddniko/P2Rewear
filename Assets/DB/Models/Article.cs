using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 'Article' klassen svarer til tabellen 'Article' i databasen
public class Article
{
    [PrimaryKey, AutoIncrement] // Sætter 'Id' som primær nøgle og auto-inkrementerende
    public int Id { get; set; }

    public int ChildId { get; set; } // Barnets Id (refererer til Child tabellen)
    public string SizeCategory { get; set; } // Kategori for størrelsen (f.eks. lille, medium, stor)
    public string Category { get; set; } // Kategori af tøj (f.eks. skjorte, bukser)
    public float Condition { get; set; } // Tøjets tilstand (f.eks. nyt, brugt)
    public int? LifeTime { get; set; } // Forventet levetid for tøjet i år (kan være null)


}

