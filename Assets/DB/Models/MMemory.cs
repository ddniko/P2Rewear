using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 'MMemory' klassen svarer til tabellen 'MMemory' i databasen
public class MMemory
{
    [PrimaryKey, AutoIncrement] // Sætter 'Id' som primær nøgle og auto-inkrementerende
    public int Id { get; set; }
    public int ArticleID { get; set; } //Tøjet det hører til, det's id
    public string Title { get; set; }
    public string DateAdded { get; set; }
    public string Description { get; set; } //Beskrivelse af mindet
    public byte[] ImageData { get; set; }

}

