
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TestDataGenerator
{
    private static System.Random rand = new System.Random();
    private static string[] parentNames = { "Anna", "Mark", "Lisa", "John", "Sara", "Peter", "Nina", "Tom", "Eva", "Lars" };
    private static string[] childNames = { "Emma", "Oscar", "Ida", "Noah", "Freja", "Lucas", "Maja", "William", "Sofie", "Oliver" };
    private static string[] clothingCategories = { "Shirt", "Pants", "Jacket", "Dress", "Sweater" };
    private static string[] memoryTitles = { "Birthday", "School Start", "Picnic", "Winter Trip", "Visit Grandma" };
    private static string[] tags = { "Trøje", "T-shirts", "Hættetrøje", "Kjole", "Bukser", "Shorts", "Overtøj", "Undertøj", "strømper", "Badetøj", "Nattøj", "Overalls", "Sæt", "Sko", "Heldragt", "Tøj til babyer" };
    static string[] predefinedSizeRanges = { "50/56", "57/63", "64/70", "71/77", "78/84", "85/91", "92/98", "99/105", "106/112", "113/119", "120/128", "129/1000" };
    public static List<byte[]> images;
    public static void GenerateRandomTestData(int TestAmount)
    {
        DBManager.Init();

        for (int i = 0; i < TestAmount; i++)
        {
            // Create Parent
            var parent = new MParent
            {
                Name = parentNames[i % parentNames.Length],
                Email = $"parent{i}@example.com",
                Password = "test123",
                Distance = rand.Next(1,10),
                SustainabilityScore = rand.Next(1, 150),
                ReliabilityScore = rand.Next(1, 100)
            };
            DBManager.AddParent(parent);
            int parentId = parent.Id;

            int childCount = rand.Next(1, 3);
            for (int j = 0; j < childCount; j++)
            {

                var child = new MChild
                {
                    ParentId = parentId,
                    Name = childNames[rand.Next(childNames.Length)],
                    Age = rand.Next(0, 10).ToString(),
                    Size = rand.Next(50, 200).ToString(),
                    Gender = (GENDER)rand.Next(0, 2),
                    Tags = $"{tags[rand.Next(tags.Length)]},{tags[rand.Next(tags.Length)]}"
                };
                DBManager.AddChild(child);
                int childId = child.Id;

                int articleCount = rand.Next(2, 4);
                for (int k = 0; k < articleCount; k++)
                {
                    var range = FindSizeRangeForValue(int.Parse(child.Size));
                    var article = new MArticle
                    {
                        ChildId = childId,
                        ParentId = parentId,
                        //Name = $"{clothingCategories[rand.Next(clothingCategories.Length)]} {rand.Next(1000)}",
                        Name = $"Tøj titel",
                        Size = rand.Next(range.Value.min, 200 + 1),
                        Condition = rand.Next(1, 6),
                        LifeTime = rand.Next(1, 12),
                        Prize = rand.Next(10,300),
                        Description = "I dag tog vi en tur til stranden for at bade. Solen bagte, og det var så varmt, at far næsten fik hedeslag. Så snart vi satte foden i sandet, spurtede Mathilde direkte ned til vandet – fuldt påklædt – og blev gennemblødt. Heldigvis havde vi badetøjet med, så vi kunne hurtigt skifte, mens hendes tøj tørrede i solen. Det viste sig at være held i uheld, for da vi senere spiste is, fik Mathilde spildt over det hele. Det våde tøj var altså blevet reddet i sidste øjeblik.",
                        Tags = $"{tags[rand.Next(tags.Length)]}",
                        //ImageData = new byte[0]
                        ImageData = images[rand.Next(images.Count)]
                    };
                    DBManager.AddArticle(article);
                    int articleId = article.Id;


                    var memory = new MMemory
                    {
                        ArticleID = articleId,
                        Title = "Varm sommerdag på stranden",
                        DateAdded = DateTime.Now.ToString("yyyy-MM-dd"),
                        Description = "Idag var vi ud og besøge bedstemor og bedstefair",
                        ImageData = new byte[0]
                    };
                    DBManager.AddMemory(memory);
                }
            }
        }

        Debug.Log("Random test data generated successfully!");
    }
    private static (int min, int max)? FindSizeRangeForValue(int size)
    {
        foreach (var range in predefinedSizeRanges)
        {
            var parts = range.Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out int min) && int.TryParse(parts[1], out int max))
            {
                if (size >= min && size <= max)
                    return (min, max);
            }
        }

        return null;
    }
}
