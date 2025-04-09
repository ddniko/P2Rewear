using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using UnityEngine;

public static class DBManager
{
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

    // Denne metode opretter tabellerne i databasen, hvis de ikke allerede findes
    private static void CreateTables()
    {
        // Opretter tabellen 'Parents' hvis den ikke eksisterer
        connection.CreateTable<Parent>();

        // Opretter tabellen 'Children' hvis den ikke eksisterer
        connection.CreateTable<Child>();

        // Opretter tabellen 'Clothing' hvis den ikke eksisterer
        connection.CreateTable<Article>();
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
        var parent = new Parent
        {
            Name = name,  // Navn på forælder
            SustainabilityScore = sustainabilityScore, // Bæredygtighedsscore (kan være null)
            ReliabilityScore = reliabilityScore // Pålidelighedsscore (kan være null)
        };
        GetConnection().Insert(parent);  // Indsætter den nye forælder i databasen
    }

    // Henter en forælder ud fra dens ID
    public static Parent GetParentById(int id)
    {
        return GetConnection().Table<Parent>().FirstOrDefault(p => p.Id == id);  // Finder den første forælder med dette ID
    }

    // Henter alle forældre fra databasen
    public static List<Parent> GetAllParents()
    {
        return GetConnection().Table<Parent>().ToList();  // Henter alle forældre i en liste
    }

    // Opdaterer en eksisterende forælder i databasen
    public static void UpdateParent(Parent parent)
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
        var child = new Child
        {
            ParentId = parentId,  // Forælderens ID (tilknytter barnet til en forælder)
            Name = name,          // Barnets navn
            Age = age,            // Barnets alder (kan være null)
            Size = size           // Barnets størrelse
        };
        GetConnection().Insert(child);  // Indsætter barnet i databasen
    }

    // Henter et barn ud fra dens ID
    public static Child GetChildById(int id)
    {
        return GetConnection().Table<Child>().FirstOrDefault(c => c.Id == id);  // Finder barnet med dette ID
    }

    // Henter alle børn fra databasen
    public static List<Child> GetAllChildren()
    {
        return GetConnection().Table<Child>().ToList();  // Henter alle børn i en liste
    }

    // Henter alle børn, som hører til en bestemt forælder
    public static List<Child> GetChildrenByParentId(int parentId)
    {
        return GetConnection().Table<Child>().Where(c => c.ParentId == parentId).ToList();  // Henter børn med et specifikt ParentId
    }

    // Opdaterer et barns oplysninger i databasen
    public static void UpdateChild(Child child)
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
    public static void AddArticle(string name, int childId, string sizeCategory, string category, float condition, int? lifeTime)
    {
        var article = new Article
        {
            ChildId = childId,        // Hvilket barn tøjet hører til
            SizeCategory = sizeCategory,  // Kategori af størrelsen (f.eks. lille, medium, stor)
            Category = category,      // Kategori af tøj (f.eks. skjorte, bukser)
            Condition = condition,    // Tøjets tilstand (f.eks. nyt, brugt)
            LifeTime = lifeTime       // Forventet levetid (kan være null)
        };
        GetConnection().Insert(article);  // Indsætter tøjet i databasen
    }

    // Henter et stykke tøj ud fra dets ID
    public static Article GetArticleById(int id)
    {
        return GetConnection().Table<Article>().FirstOrDefault(a => a.Id == id);  // Finder tøjet med dette ID
    }

    // Henter alle stykker tøj fra databasen
    public static List<Article> GetAllArticles()
    {
        return GetConnection().Table<Article>().ToList();  // Henter alle stykker tøj i en liste
    }

    // Henter alle stykker tøj, der hører til et specifikt barn
    public static List<Article> GetArticlesByChildId(int childId)
    {
        return GetConnection().Table<Article>().Where(a => a.ChildId == childId).ToList();  // Henter tøj baseret på ChildId
    }

    // Henter alt tøj, der hører til alle børn af en bestemt forælder
    public static List<Article> GetArticlesByParentId(int parentId)
    {
        // Henter alle børn for forælderen
        var children = GetChildrenByParentId(parentId);

        var allArticles = new List<Article>();

        // Gennemgår hvert barn og samler deres tøj
        foreach (var child in children)
        {
            var childArticles = GetArticlesByChildId(child.Id);
            allArticles.AddRange(childArticles); //add range tilføjer det der står på listen til vores nye liste
        }

        return allArticles;
    }

    // Opdaterer et stykke tøj i databasen
    public static void UpdateArticle(Article article)
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
}
    #endregion
    #region Models/Tables
    // Modellerne (tabellerne) i databasen defineres som C# klasser, fordi SQLite-Net er meget federe end SQLite

    // 'Parent' klassen svarer til tabellen 'Parents' i databasen


#endregion


