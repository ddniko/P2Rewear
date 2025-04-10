using System.Collections.Generic;
using UnityEngine;

public class SortingAlgorithm : MonoBehaviour
{
    
    private List<MArticle> articles;
    private List<MArticle> sortedArticles;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        articles = DBManager.GetAllArticles();
    }

    private void SortArticles(ChildDemands demands)
    {
        foreach (MArticle article in articles)
        {
            if (demands.SizeCategory.Contains(article.SizeCategory) && demands.SizeCategory.Contains(article.SizeCategory) )
            {
                sortedArticles.Add(article);
            }
            
            if (article.SizeCategory == "Small")
            {
                sortedArticles.Add(article);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
