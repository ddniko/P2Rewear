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

    public void CreateMemory(int articleID, string title, string description, byte[] imageData)
    {
        this.ArticleID = articleID;
        this.Title = title;
        this.Description = description;
        this.ImageData = imageData;
        this.DateAdded = DateTime.Now.ToString();
        DBManager.AddMemory(this);

    }

    //public override string ToString()
    //{
    //    return $" This is information of memory ({Id}) connected to this article ({ArticleID}) : \n{Title} ({DateAdded})\n{Description}\nImage: {ImageData}\n";
    //}
}

