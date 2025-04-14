//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class SortingAlgorithm : MonoBehaviour
//{
//    public GameObject[] itemsVisible = new GameObject[5];
//    public GameObject[] itemsSpots = new GameObject[5];
//    public List<GameObject> CurrentArticles;
//    public GameObject content;
//    public GameObject ClothingPrefab;
//    public Transform ParentObject;
//    [Header("ItemPositions")]
//    public int maxItemsPerRow = 5;
//    public float horizontalSpacing = 2f;// de her bestemmer hvor meget rum der skal v�re imellem dem. kunne g�res til public fields s� man kunne �ndre dem i inspectoren
//    public float verticalSpacing = 2f;
//    public int currentRow = 0;
//    public int currentColumn = 0;
//    private Vector3 startPosition = new Vector3(0, 0, 0);
    
//    public TextField textField;
    
    
//    private List<MArticle> allArticles;
//    private List<MArticle> sortedArticles;
    
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        allArticles = DBManager.GetAllArticles();
//    }
    
    
//    private void FilterArticles(ChildDemands demands)
//    {
//        foreach (MArticle article in allArticles)
//        {
//            if (demands.SizeCategory.Contains(article.SizeCategory) &&
//                demands.Category.Contains(article.Category) &&
//                demands.maxPrize >= article.Prize && 
//                demands.minCondition <= article.Condition /*&&
//                demands.maxDistance >= article.distance*/)
//            {
//                sortedArticles.Add(article);
//            }
//            else
//            {
//                Debug.Log("næ");
//            }
//        }
//        InstantiateAllArticles();
//    }
//    public void InstantiateAllArticles()
//    {
//        CurrentArticles = new List<GameObject>();

//        foreach (MArticle article in sortedArticles)
//        {
//            CurrentArticles.Add(CreateArticle(article));
//        }
//        SortArticles();

//    }

//    public void InstantiateArticlesParent(int parentID)
//    {
//        CurrentArticles = new List<GameObject>();

//        foreach (MArticle article in DBManager.GetArticlesByParentId(parentID))
//        {
//            CurrentArticles.Add(CreateArticle(article));
//        }
//        SortArticles();
//    }
    
//    public void InstantiateArticlesChild(int childID)
//    {
//        CurrentArticles = new List<GameObject>();

//        foreach (MArticle article in DBManager.GetArticlesByParentId(childID))
//        {
//            CurrentArticles.Add(CreateArticle(article));
//        }
//        SortArticles();
//    }
    
    
//    public void SortArticles()
//    {
//        for (int i = 0; i < CurrentArticles.Count ; i++)  //index for r�kken
//        {
//            CurrentArticles[i].gameObject.transform.position = startPosition + new Vector3(currentColumn * horizontalSpacing, -currentRow * verticalSpacing, 0);
            
//            currentColumn++;

//            // hvis den her row er fyldt, s� g� et hak ned
//            if (currentColumn >= maxItemsPerRow)
//            {
//                currentColumn = 0;
//                currentRow++;
//            }
//        }
//    }
    
    
    
//}
