using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 'MArticle' klassen svarer til tabellen 'MArticle' i databasen
public class MArticle
{
    [PrimaryKey, AutoIncrement] // Sætter 'Id' som primær nøgle og auto-inkrementerende
    public int Id { get; set; }

    public int ChildId { get; set; } // Barnets Id (refererer til MChild tabellen)

    public string Name { get; set; }
    public string Size{ get; set; } // Kategori for størrelsen (f.eks. lille, medium, stor)
    public string Tags { get; set; } // Tags, comma separated value ideally
    public float Condition { get; set; } // Tøjets tilstand (f.eks. nyt, brugt)
    public int? LifeTime { get; set; } // Forventet levetid for tøjet i år (kan være null)
    public float? Prize {  get; set; } // prisen artiklen er sat 
    public string Description { get; set; } //Beskrivelse af tøjet
    public byte[] ImageData { get; set; }



}

