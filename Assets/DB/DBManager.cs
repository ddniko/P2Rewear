using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using UnityEngine;

public static class DBManager
{
    #region CreateConnection
    // Sti (filepath) til SQLite-databasen, som gemmes i "persistentDataPath" (s� det den samme sti uanset hva platform vi p�)
    private static string dbPath = $"{Application.persistentDataPath}/clothing.db";

    // Variabel til at opbevare forbindelsen til databasen. de static, s� vi ikke laver flere forbindelser og laver rod.
    private static SQLiteConnection connection;

    // Denne metode initialiserer databasen og opretter de n�dvendige tabeller
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

    // Denne metode returnerer forbindelsen til databasen, s� den kan bruges andre steder i koden
    public static SQLiteConnection GetConnection() => connection;

    // Denne metode lukker forbindelsen til databasen, n�r vi er f�rdige med at bruge den
    public static void Close() => connection?.Close();
    #endregion
    #region CRUD for Parent (For�lder)
    // Tilf�jer en ny for�lder til databasen
    public static void AddParent(string name, int? sustainabilityScore, int? reliabilityScore)
    {
        var parent = new Parent
        {
            Name = name,  // Navn p� for�lder
            SustainabilityScore = sustainabilityScore, // B�redygtighedsscore (kan v�re null)
            ReliabilityScore = reliabilityScore // P�lidelighedsscore (kan v�re null)
        };
        GetConnection().Insert(parent);  // Inds�tter den nye for�lder i databasen
    }

    // Henter en for�lder ud fra dens ID
    public static Parent GetParentById(int id)
    {
        return GetConnection().Table<Parent>().FirstOrDefault(p => p.Id == id);  // Finder den f�rste for�lder med dette ID
    }

    // Henter alle for�ldre fra databasen
    public static List<Parent> GetAllParents()
    {
        return GetConnection().Table<Parent>().ToList();  // Henter alle for�ldre i en liste
    }

    // Opdaterer en eksisterende for�lder i databasen
    public static void UpdateParent(Parent parent)
    {
        GetConnection().Update(parent);  // Opdaterer for�lderen i databasen
    }

    // Sletter en for�lder fra databasen baseret p� ID
    public static void DeleteParent(int id)
    {
        var parent = GetParentById(id);  // Henter for�lderen ud fra ID
        if (parent != null)
        {
            GetConnection().Delete(parent);  // Sletter for�lderen fra databasen
        }
    }
    #endregion
    #region CRUD for Child (Barn)
    // Tilf�jer et nyt barn til databasen
    public static void AddChild(string name, int parentId, int? age, string size)
    {
        var child = new Child
        {
            ParentId = parentId,  // For�lderens ID (tilknytter barnet til en for�lder)
            Name = name,          // Barnets navn
            Age = age,            // Barnets alder (kan v�re null)
            Size = size           // Barnets st�rrelse
        };
        GetConnection().Insert(child);  // Inds�tter barnet i databasen
    }

    // Henter et barn ud fra dens ID
    public static Child GetChildById(int id)
    {
        return GetConnection().Table<Child>().FirstOrDefault(c => c.Id == id);  // Finder barnet med dette ID
    }

    // Henter alle b�rn fra databasen
    public static List<Child> GetAllChildren()
    {
        return GetConnection().Table<Child>().ToList();  // Henter alle b�rn i en liste
    }

    // Henter alle b�rn, som h�rer til en bestemt for�lder
    public static List<Child> GetChildrenByParentId(int parentId)
    {
        return GetConnection().Table<Child>().Where(c => c.ParentId == parentId).ToList();  // Henter b�rn med et specifikt ParentId
    }

    // Opdaterer et barns oplysninger i databasen
    public static void UpdateChild(Child child)
    {
        GetConnection().Update(child);  // Opdaterer barnet i databasen
    }

    // Sletter et barn fra databasen baseret p� ID
    public static void DeleteChild(int id)
    {
        var child = GetChildById(id);  // Henter barnet ud fra ID
        if (child != null)
        {
            GetConnection().Delete(child);  // Sletter barnet fra databasen
        }
    }
    #endregion
    #region CRUD for Article (T�j)
    // Tilf�jer et nyt stykke t�j til databasen
    public static void AddArticle(string name, int childId, string sizeCategory, string category, float condition, int? lifeTime)
    {
        var article = new Article
        {
            ChildId = childId,        // Hvilket barn t�jet h�rer til
            SizeCategory = sizeCategory,  // Kategori af st�rrelsen (f.eks. lille, medium, stor)
            Category = category,      // Kategori af t�j (f.eks. skjorte, bukser)
            Condition = condition,    // T�jets tilstand (f.eks. nyt, brugt)
            LifeTime = lifeTime       // Forventet levetid (kan v�re null)
        };
        GetConnection().Insert(article);  // Inds�tter t�jet i databasen
    }

    // Henter et stykke t�j ud fra dets ID
    public static Article GetArticleById(int id)
    {
        return GetConnection().Table<Article>().FirstOrDefault(a => a.Id == id);  // Finder t�jet med dette ID
    }

    // Henter alle stykker t�j fra databasen
    public static List<Article> GetAllArticles()
    {
        return GetConnection().Table<Article>().ToList();  // Henter alle stykker t�j i en liste
    }

    // Henter alle stykker t�j, der h�rer til et specifikt barn
    public static List<Article> GetArticlesByChildId(int childId)
    {
        return GetConnection().Table<Article>().Where(a => a.ChildId == childId).ToList();  // Henter t�j baseret p� ChildId
    }

    // Henter alt t�j, der h�rer til alle b�rn af en bestemt for�lder
    public static List<Article> GetArticlesByParentId(int parentId)
    {
        // Henter alle b�rn for for�lderen
        var children = GetChildrenByParentId(parentId);

        var allArticles = new List<Article>();

        // Gennemg�r hvert barn og samler deres t�j
        foreach (var child in children)
        {
            var childArticles = GetArticlesByChildId(child.Id);
            allArticles.AddRange(childArticles); //add range tilf�jer det der st�r p� listen til vores nye liste
        }

        return allArticles;
    }

    // Opdaterer et stykke t�j i databasen
    public static void UpdateArticle(Article article)
    {
        GetConnection().Update(article);  // Opdaterer t�jet i databasen
    }

    // Sletter et stykke t�j fra databasen baseret p� ID
    public static void DeleteArticle(int id)
    {
        var article = GetArticleById(id);  // Henter t�jet ud fra ID
        if (article != null)
        {
            GetConnection().Delete(article);  // Sletter t�jet fra databasen
        }
    }
}
    #endregion
    #region Models/Tables
    // Modellerne (tabellerne) i databasen defineres som C# klasser, fordi SQLite-Net er meget federe end SQLite

    // 'Parent' klassen svarer til tabellen 'Parents' i databasen


#endregion


