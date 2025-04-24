using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public static class TestDataGenerator
{
    private static System.Random rand = new System.Random();
    private static string[] parentNames = { "Anna", "Mark", "Lisa", "John", "Sara", "Peter", "Nina", "Tom", "Eva", "Lars" };
    private static string[] childNames = { "Emma", "Oscar", "Ida", "Noah", "Freja", "Lucas", "Maja", "William", "Sofie", "Oliver" };
    private static string[] clothingCategories = { "Shirt", "Pants", "Jacket", "Dress", "Sweater" };
    private static string[] memoryTitles = { "Birthday", "School Start", "Picnic", "Winter Trip", "Visit Grandma" };
    private static string[] tags = { "Shoes", "Shirt", "Sweater", "Dress", "Gloves", "Underwear", "Blue", "Red", "Pink", "Yellow", "Green" };
    static string[] predefinedSizeRanges = { "50/56", "57/63", "64/70", "71/77", "78/84", "85/91", "92/98", "99/105", "106/112", "113/119", "120/128" };
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
                Distance = (float)(rand.NextDouble() * 50),
                SustainabilityScore = rand.Next(1, 11),
                ReliabilityScore = rand.Next(1, 11)
            };
            DBManager.AddParent(parent);
            int parentId = parent.Id;

            int childCount = rand.Next(1, 4);
            for (int j = 0; j < childCount; j++)
            {

                var child = new MChild
                {
                    ParentId = parentId,
                    Name = childNames[rand.Next(childNames.Length)],
                    Age = rand.Next(0, 10).ToString(),
                    Size = rand.Next(50, 128).ToString(),
                    Gender = (GENDER)rand.Next(0, 2),
                    Tags = $"{tags[rand.Next(tags.Length)]},{tags[rand.Next(tags.Length)]}"
                };
                DBManager.AddChild(child);
                int childId = child.Id;

                int articleCount = rand.Next(2, 6);
                for (int k = 0; k < articleCount; k++)
                {
                    var range = FindSizeRangeForValue(int.Parse(child.Size));
                    var article = new MArticle
                    {
                        ChildId = childId,
                        Name = $"{clothingCategories[rand.Next(clothingCategories.Length)]} {rand.Next(1000)}",
                        Size = rand.Next(range.Value.min, range.Value.max + 1).ToString(),
                        Condition = rand.Next(1, 6),
                        LifeTime = rand.Next(1, 6),
                        Prize = (float)(rand.NextDouble() * 100),
                        Description = "Randomly generated clothing article.",
                        Tags = $"{tags[rand.Next(tags.Length)]},{tags[rand.Next(tags.Length)]}",
                        ImageData = new byte[0]
                    };
                    DBManager.AddArticle(article);
                    int articleId = article.Id;


                    var memory = new MMemory
                    {
                        ArticleID = articleId,
                        Title = memoryTitles[rand.Next(memoryTitles.Length)],
                        DateAdded = DateTime.Now.ToString("yyyy-MM-dd"),
                        Description = "du fik engang en klodset hånder i den her",
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
