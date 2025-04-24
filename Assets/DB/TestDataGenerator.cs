using System;
using System.Collections.Generic;
using UnityEngine;

public static class TestDataGenerator
{
    private static System.Random rand = new System.Random();
    private static string[] parentNames = { "Anna", "Mark", "Lisa", "John", "Sara", "Peter", "Nina", "Tom", "Eva", "Lars" };
    private static string[] childNames = { "Emma", "Oscar", "Ida", "Noah", "Freja", "Lucas", "Maja", "William", "Sofie", "Oliver" };
    private static string[] sizes = { "small", "medium", "large", "98", "104", "110", "116", "122", "128" };
    private static string[] clothingCategories = { "Shirt", "Pants", "Jacket", "Dress", "Sweater" };
    private static string[] memoryTitles = { "Birthday", "School Start", "Picnic", "Winter Trip", "Visit Grandma" };
    private static string[] tags = { "cute", "warm", "soft", "bright", "used" };

    public static void GenerateRandomTestData()
    {
        DBManager.Init();

        for (int i = 0; i < 10; i++)
        {
            // Create Parent
            var parent = new MParent
            {
                Name = parentNames[i],
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
                // Create Child
                var child = new MChild
                {
                    ParentId = parentId,
                    Name = childNames[rand.Next(childNames.Length)],
                    Age = rand.Next(2, 10).ToString(),
                    Size = sizes[rand.Next(sizes.Length)],
                    Gender = (GENDER)rand.Next(0, 2),
                    Tags = $"{tags[rand.Next(tags.Length)]},{tags[rand.Next(tags.Length)]}"
                };
                DBManager.AddChild(child);
                int childId = child.Id;

                int articleCount = rand.Next(2, 6);
                for (int k = 0; k < articleCount; k++)
                {
                    // Create Article
                    var article = new MArticle
                    {
                        ChildId = childId,
                        Name = $"{clothingCategories[rand.Next(clothingCategories.Length)]} {rand.Next(1000)}",
                        SizeCategory = sizes[rand.Next(sizes.Length)],
                        Category = clothingCategories[rand.Next(clothingCategories.Length)],
                        Condition = rand.Next(1, 6),
                        LifeTime = rand.Next(1, 6),
                        Prize = (float)(rand.NextDouble() * 100),
                        Description = "Randomly generated clothing article.",
                        Tags = $"{tags[rand.Next(tags.Length)]},{tags[rand.Next(tags.Length)]}",
                        ImageData = new byte[0]
                    };
                    DBManager.AddArticle(article);
                    int articleId = article.Id;

                    // Create Memory
                    var memory = new MMemory
                    {
                        ArticleID = articleId,
                        Title = memoryTitles[rand.Next(memoryTitles.Length)],
                        DateAdded = DateTime.Now.ToString("yyyy-MM-dd"),
                        Description = "A special moment with this article.",
                        ImageData = new byte[0]
                    };
                    DBManager.AddMemory(memory);
                }
            }
        }

        Debug.Log("Random test data generated successfully!");
    }
}
