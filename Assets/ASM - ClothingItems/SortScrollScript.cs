using System.Collections.Generic;
using UnityEngine;

public class SortScrollScript : MonoBehaviour
{
    public List<GameObject> CurrentArticles;
    public GameObject content;
    public GameObject ClothingPrefab;
    public Transform ParentObject;
    [Header("ItemPositions")]
    public int maxItemsPerRow = 5;
    public float horizontalSpacing = 2f;// de her bestemmer hvor meget rum der skal være imellem dem. kunne gøres til public fields så man kunne ændre dem i inspectoren
    public float verticalSpacing = 2f;
    public int currentRow = 0;
    public int currentColumn = 0;
    private Vector3 startPosition = new Vector3(0, 0, 0);
    public GameObject[] StartObjects;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        /*startPosition = StartObjects[0].transform.position;
        horizontalSpacing = Mathf.Abs(StartObjects[0].transform.position.x) - Mathf.Abs(StartObjects[1].transform.position.x);
        verticalSpacing = Mathf.Abs(StartObjects[0].transform.position.y) - Mathf.Abs(StartObjects[2].transform.position.y);
        currentColumn = 0;
        currentRow = 0;
        //Mathf.abs gør at det er i positive tal, altså ikke -13, men bare 13 eks.
        InstantiateAllArticles();
        //Mathf.abs g�r at det er i positive tal, alts� ikke -13, men bare 13 eks.
        InstantiateAllArticles();*/
    }
    private void OnDisable()
    {
        for (int i = 0; i < CurrentArticles.Count; i++)
        {
            Destroy(CurrentArticles[i].gameObject);
        }
    }

    private void FilterArticles(ChildDemands demands)
    {
        List<MArticle> articles = DBManager.GetAllArticles();
        List<MArticle> sortedArticles = new List<MArticle>();
        foreach (MArticle article in articles)
        {
            if (demands.SizeCategory.Contains(article.SizeCategory) &&
                demands.Category.Contains(article.Category) &&
                demands.maxPrize >= article.Prize &&
                demands.minCondition <= article.Condition /*&&
                demands.maxDistance >= article.distance*/)
            {
                sortedArticles.Add(article);
            }
            else
            {
                Debug.Log("næ");
            }
        }
        InstantiateAllArticles();
    }

    public void InstantiateAllArticles()
    {
        CurrentArticles = new List<GameObject>();
        //praktisk talt at resette listen.
        List<MArticle> articles = DBManager.GetAllArticles();

        foreach (MArticle article in articles)
        {
            CurrentArticles.Add(CreateArticle(article));
        }
        OrderArticles();
    }

    public void InstantiateAllArticles(List<MArticle> art)
    {
        CurrentArticles = new List<GameObject>();
        //praktisk talt at resette listen.
        List<MArticle> articles = art;

        foreach (MArticle article in articles)
        {
            CurrentArticles.Add(CreateArticle(article));
        }
        OrderArticles();
    }


    public void InstantiateArticlesParent(int parentID)
    {
        CurrentArticles = new List<GameObject>();

        foreach (MArticle article in DBManager.GetArticlesByParentId(parentID))
        {
            CurrentArticles.Add(CreateArticle(article));
        }
        OrderArticles();
    }

    public void InstantiateArticlesChild(int childID)
    {
        CurrentArticles = new List<GameObject>();

        foreach (MArticle article in DBManager.GetArticlesByParentId(childID))
        {
            CurrentArticles.Add(CreateArticle(article));
        }
        OrderArticles();
    }
    public GameObject CreateArticle(MArticle article)
    {
        GameObject newArticle = Instantiate(ClothingPrefab, ParentObject);

        ClothingItem articleItem = newArticle.GetComponent<ClothingItem>();
        articleItem.SetUpClothingItem(article.Id, article.Name, article.ChildId, article.SizeCategory, article.Category,
            article.Condition, article.LifeTime, article.Prize, article.Description, article.ImageData);
        return newArticle;
    }


    public void OrderArticles()
    {
        for (int i = 0; i < CurrentArticles.Count; i++)  //index for rækken
        {
            CurrentArticles[i].gameObject.transform.position = startPosition + new Vector3(currentColumn * horizontalSpacing, -currentRow * verticalSpacing, 0);

            currentColumn++;

            // hvis den her row er fyldt, så gå et hak ned
            if (currentColumn >= maxItemsPerRow)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
    }

}