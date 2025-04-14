using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using UnityEngine;

public static class DBManager
{
    static System.Random rand = new System.Random();

    static string[] names = { "Emma", "Oliver", "Liam", "Ava", "Sophia", "Noah", "Lucas", "Mia", "Elijah", "Amelia" };
    static string[] tags = { "Winter", "Summer", "Casual", "Hardcore", "Formal", "Outdoor", "Sports", "School", "Party", "Ball-gag", "9mm Glock" };
    static string[] sizes = { "XS", "S", "M", "L", "XL" };
    static string[] categories = { "Shirt", "Pants", "Dress", "Jacket", "Shoes", "Hat" };
    #region CreateConnection
    // Sti (filepath) til SQLite-databasen, som gemmes i "persistentDataPath" (så det den samme sti uanset hva platform vi på)
    private static string dbPath = $"{Application.persistentDataPath}/clothing.db";

    // Variabel til at opbevare forbindelsen til databasen. de static, så vi ikke laver flere forbindelser og laver rod.
    private static SQLiteConnection connection;

    // Denne metode initialiserer databasen og opretter de nødvendige tabeller
    public static void Init()
    {
        // Opretter forbindelsen til SQLite-databasen, som vi gemmer i den angivne sti
        connection = new SQLiteConnection(dbPath);

        // Opretter tabellerne, hvis de ikke allerede findes
        CreateTables();
        Debug.Log($"Database stored at: {dbPath}");

    }
    public static void ForceConnection(SQLiteConnection conn)
    {
        connection = conn;
    }
    // Denne metode opretter tabellerne i databasen, hvis de ikke allerede findes
    private static void CreateTables()
    {
        // Opretter tabellen 'Parents' hvis den ikke eksisterer
        connection.CreateTable<MParent>();

        // Opretter tabellen 'Children' hvis den ikke eksisterer
        connection.CreateTable<MChild>();

        // Opretter tabellen 'Clothing' hvis den ikke eksisterer
        connection.CreateTable<MArticle>();
    }

    // Denne metode returnerer forbindelsen til databasen, så den kan bruges andre steder i koden
    public static SQLiteConnection GetConnection() => connection;

    // Denne metode lukker forbindelsen til databasen, når vi er færdige med at bruge den
    public static void Close() => connection?.Close();
    #endregion
    #region CRUD for Parent (Forælder)
    // Tilføjer en ny forælder til databasen
    public static void AddParent(string name, int? sustainabilityScore, int? reliabilityScore)
    {
        var parent = new MParent
        {
            Name = name,  // Navn på forælder
            SustainabilityScore = sustainabilityScore, // Bæredygtighedsscore (kan være null)
            ReliabilityScore = reliabilityScore // Pålidelighedsscore (kan være null)
        };
        GetConnection().Insert(parent);  // Indsætter den nye forælder i databasen
    }

    // Henter en forælder ud fra dens ID
    public static MParent GetParentById(int id)
    {
        return GetConnection().Table<MParent>().FirstOrDefault(p => p.Id == id);  // Finder den første forælder med dette ID
    }

    // Henter alle forældre fra databasen
    public static List<MParent> GetAllParents()
    {
        return GetConnection().Table<MParent>().ToList();  // Henter alle forældre i en liste
    }

    // Opdaterer en eksisterende forælder i databasen
    public static void UpdateParent(MParent parent)
    {
        GetConnection().Update(parent);  // Opdaterer forælderen i databasen
    }

    // Sletter en forælder fra databasen baseret på ID
    public static void DeleteParent(int id)
    {
        var parent = GetParentById(id);  // Henter forælderen ud fra ID
        if (parent != null)
        {
            GetConnection().Delete(parent);  // Sletter forælderen fra databasen
        }
    }
    #endregion
    #region CRUD for Child (Barn)
    // Tilføjer et nyt barn til databasen
    public static void AddChild(string name, int parentId, int? age, string size)
    {
        var child = new MChild
        {
            ParentId = parentId,  // Forælderens ID (tilknytter barnet til en forælder)
            Name = name,          // Barnets navn
            Age = age,            // Barnets alder (kan være null)
            Size = size           // Barnets størrelse
        };
        GetConnection().Insert(child);  // Indsætter barnet i databasen
    }

    // Henter et barn ud fra dens ID
    public static MChild GetChildById(int id)
    {
        return GetConnection().Table<MChild>().FirstOrDefault(c => c.Id == id);  // Finder barnet med dette ID
    }

    // Henter alle børn fra databasen
    public static List<MChild> GetAllChildren()
    {
        return GetConnection().Table<MChild>().ToList();  // Henter alle børn i en liste
    }

    // Henter alle børn, som hører til en bestemt forælder
    public static List<MChild> GetChildrenByParentId(int parentId)
    {
        return GetConnection().Table<MChild>().Where(c => c.ParentId == parentId).ToList();  // Henter børn med et specifikt ParentId
    }

    // Opdaterer et barns oplysninger i databasen
    public static void UpdateChild(MChild child)
    {
        GetConnection().Update(child);  // Opdaterer barnet i databasen
    }

    // Sletter et barn fra databasen baseret på ID
    public static void DeleteChild(int id)
    {
        var child = GetChildById(id);  // Henter barnet ud fra ID
        if (child != null)
        {
            GetConnection().Delete(child);  // Sletter barnet fra databasen
        }
    }
    #endregion
    #region CRUD for Article (Tøj)
    // Tilføjer et nyt stykke tøj til databasen
    public static void AddArticle(string name, int childId, string sizeCategory, string category, float condition, int? lifeTime, float prize, string description, byte[] imageData)
    {
        var article = new MArticle
        {
            ChildId = childId,        // Hvilket barn tøjet hører til
            Name = name,
            Prize = prize,
            Description = description,
            ImageData = imageData,
            SizeCategory = sizeCategory,  // Kategori af størrelsen (f.eks. lille, medium, stor)
            Category = category,      // Kategori af tøj (f.eks. skjorte, bukser)
            Condition = condition,    // Tøjets tilstand (f.eks. nyt, brugt)
            LifeTime = lifeTime       // Forventet levetid (kan være null)
        };
        GetConnection().Insert(article);  // Indsætter tøjet i databasen
    }
    //Overload til hvis man laver articlen udefra
    public static void AddArticle(MArticle art)
    {
        GetConnection().Insert(art);  // Indsætter tøjet i databasen
    }

    // Henter et stykke tøj ud fra dets ID
    public static MArticle GetArticleById(int id)
    {
        return GetConnection().Table<MArticle>().FirstOrDefault(a => a.Id == id);  // Finder tøjet med dette ID
    }

    public static byte[] TextureToBytes(Texture2D texture)
    {
        return texture.EncodeToPNG(); // or EncodeToJPG() if you prefer
    }

    public static Texture2D BytesToTexture(byte[] bytes)
    {
        Texture2D tex = new Texture2D(2, 2); // size will auto-adjust
        tex.LoadImage(bytes);
        return tex;
    }

    // Henter alle stykker tøj fra databasen
    public static List<MArticle> GetAllArticles()
    {
        return GetConnection().Table<MArticle>().ToList();  // Henter alle stykker tøj i en liste
    }

    // Henter alle stykker tøj, der hører til et specifikt barn
    public static List<MArticle> GetArticlesByChildId(int childId)
    {
        return GetConnection().Table<MArticle>().Where(a => a.ChildId == childId).ToList();  // Henter tøj baseret på ChildId
    }

    // Henter alt tøj, der hører til alle børn af en bestemt forælder
    public static List<MArticle> GetArticlesByParentId(int parentId)
    {
        // Henter alle børn for forælderen
        var children = GetChildrenByParentId(parentId);

        var allArticles = new List<MArticle>();

        // Gennemgår hvert barn og samler deres tøj
        foreach (var child in children)
        {
            var childArticles = GetArticlesByChildId(child.Id);
            allArticles.AddRange(childArticles); //add range tilføjer det der står på listen til vores nye liste
        }

        return allArticles;
    }

    // Opdaterer et stykke tøj i databasen
    public static void UpdateArticle(MArticle article)
    {
        GetConnection().Update(article);  // Opdaterer tøjet i databasen
    }

    // Sletter et stykke tøj fra databasen baseret på ID
    public static void DeleteArticle(int id)
    {
        var article = GetArticleById(id);  // Henter tøjet ud fra ID
        if (article != null)
        {
            GetConnection().Delete(article);  // Sletter tøjet fra databasen
        }
    }
    #endregion



    public static void GenerateTestData(int numberOfParents = 5)
    {
        GetConnection().CreateTable<MParent>();
        GetConnection().CreateTable<MChild>();
        GetConnection().CreateTable<MArticle>();

        for (int i = 0; i < numberOfParents; i++)
        {
            var parent = new MParent
            {
                Name = GetRandomName(),
                SustainabilityScore = rand.Next(1, 11),
                ReliabilityScore = rand.Next(1, 11)
            };
            GetConnection().Insert(parent);

            int numChildren = rand.Next(1, 4); // 1–3 children
            for (int j = 0; j < numChildren; j++)
            {
                var child = new MChild
                {
                    ParentId = parent.Id,
                    Name = GetRandomName(),
                    Gender = (GENDER)rand.Next(0, 3),
                    Tags = GetRandomTags(),
                    Age = rand.Next(1, 13),
                    Size = sizes[rand.Next(sizes.Length)]
                };
                GetConnection().Insert(child);

                int numArticles = rand.Next(2, 6); // 2–5 clothing items
                for (int k = 0; k < numArticles; k++)
                {
                    var article = new MArticle
                    {
                        ChildId = child.Id,
                        Name = categories[rand.Next(categories.Length)] + " " + rand.Next(100),
                        SizeCategory = sizes[rand.Next(sizes.Length)],
                        Tags = GetRandomTags(),
                        Category = categories[rand.Next(categories.Length)],
                        Condition = (float)Math.Round(rand.NextDouble() * 5, 1),
                        LifeTime = rand.Next(1, 6),
                        Prize = (float)Math.Round(rand.NextDouble() * 500, 2),
                        Description = "Description for " + categories[rand.Next(categories.Length)],
                        ImageData = new byte[0] // tomt billede ligenu
                    };
                    GetConnection().Insert(article);
                }
            }
        }
    }

    private static string GetRandomName()
    {
        return names[rand.Next(names.Length)];
    }

    private static string GetRandomTags()
    {
        var selectedTags = tags.OrderBy(x => rand.Next()).Take(rand.Next(1, 4));
        return string.Join(",", selectedTags);
    }
}



