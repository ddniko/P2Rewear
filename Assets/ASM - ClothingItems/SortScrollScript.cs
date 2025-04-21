using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SORTTYPE { PRICEASC, PRICEDESC, CONDITIONASC, CONDITIONDESC }
public class SortScrollScript : MonoBehaviour
{
    public List<GameObject> CurrentArticles;
    public GameObject content;
    public GameObject ClothingPrefab;
    public Transform ParentObject;
    [Header("ItemPositions")]
    public int maxItemsPerRow = 2;
    public float horizontalSpacing = 140f;// de her bestemmer hvor meget rum der skal være imellem dem. kunne gøres til public fields så man kunne ændre dem i inspectoren
    public float verticalSpacing = 100f;
    public int currentRow = 0;
    public int currentColumn = 0;
    private Vector3 startPosition = Vector3.zero;
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

        startPosition = StartObjects[0].transform.position;
        horizontalSpacing = Mathf.Abs(StartObjects[0].transform.position.x) - Mathf.Abs(StartObjects[1].transform.position.x);
        verticalSpacing = Mathf.Abs(StartObjects[0].transform.position.y) - Mathf.Abs(StartObjects[2].transform.position.y);
        currentColumn = 0;
        currentRow = 0;
        //Mathf.abs gør at det er i positive tal, altså ikke -13, men bare 13 eks.

    }
    private void OnDisable()
    {
        DestroyItems();
    }
    public void DestroyItems()
    {
        if (CurrentArticles != null)
        {
            for (int i = 0; i < CurrentArticles.Count; i++)
            {
                Destroy(CurrentArticles[i].gameObject);
            }
            if (StartObjects != null)
            {
                startPosition = StartObjects[0].transform.position;
                horizontalSpacing = Mathf.Abs(StartObjects[0].transform.position.x) - Mathf.Abs(StartObjects[1].transform.position.x);
                verticalSpacing = Mathf.Abs(StartObjects[0].transform.position.y) - Mathf.Abs(StartObjects[2].transform.position.y);
                currentColumn = 0;
                currentRow = 0;
            }
        }
    }
    private List<MArticle> FilterArticles(ChildDemands demands, List<MArticle> articles)
    {
        DestroyItems();

        List<MArticle> filteredArticles = new List<MArticle>();

        foreach (MArticle article in articles)
        {
            bool matches = true;
            if (demands.SizeCategory != null && demands.SizeCategory.Count > 0)
            {
                if (!demands.SizeCategory.Contains(article.SizeCategory))
                    matches = false;
            }
            if (demands.Category != null && demands.Category.Count > 0)
            {
                if (!demands.Category.Contains(article.Category))
                    matches = false;
            }
            if (demands.maxPrize != -1)
            {
                if (!article.Prize.HasValue || article.Prize.Value > demands.maxPrize)
                    matches = false;
            }
            if (demands.minCondition.HasValue)
            {
                if (article.Condition < demands.minCondition.Value)
                    matches = false;
            }
            if (matches)
                filteredArticles.Add(article);
        }

        return filteredArticles;

    }
    private bool ArticleValidByFilter(ChildDemands demands, MArticle art)
    {
        DestroyItems();

        List<MArticle> filteredArticles = new List<MArticle>();



        bool matches = true;
        if (demands.SizeCategory != null && demands.SizeCategory.Count > 0)
        {
            if (!demands.SizeCategory.Contains(art.SizeCategory))
                matches = false;
        }
        if (demands.Category != null && demands.Category.Count > 0)
        {
            if (!demands.Category.Contains(art.Category))
                matches = false;
        }
        if (demands.maxPrize != -1)
        {
            if (!art.Prize.HasValue || art.Prize.Value > demands.maxPrize)
                matches = false;
        }
        if (demands.minCondition.HasValue)
        {
            if (art.Condition < demands.minCondition.Value)
                matches = false;
        }
        if (matches)
            filteredArticles.Add(art);


        return matches;

    }

    public void InstantiateAllArticles()
    {
        DestroyItems();
        CurrentArticles = new List<GameObject>();
        //praktisk talt at resette listen.
        List<MArticle> articles = DBManager.GetAllArticles();

        foreach (MArticle article in articles)
        {
            CurrentArticles.Add(CreateArticle(article));
        }
        OrderArticles();
    }

    public void InstantiateArticles(List<MArticle> art, SORTTYPE? sortType = null, ChildDemands childDemands = null)
    {
        DestroyItems();
        CurrentArticles = new List<GameObject>();
        //praktisk talt at resette listen.
        List<MArticle> articles = art;
        
        foreach (MArticle article in articles)
        {
            if (childDemands != null)
            {
                if (ArticleValidByFilter(childDemands, article))
                {
                    CurrentArticles.Add(CreateArticle(article));
                }
                else continue;
            }
            else
                CurrentArticles.Add(CreateArticle(article));
        }
        if (sortType != null)
            SortAndDisplayArticles(sortType);
        else
            OrderArticles();
    }
    public void InstantiateArticlesParent(int parentID, SORTTYPE? sortType = null, ChildDemands childDemands = null)
    {
        List<MArticle> parentArticles = DBManager.GetArticlesByParentId(parentID);
        InstantiateArticles(parentArticles, sortType, childDemands);
    }
    public void InstantiateArticlesOtherParent(int parentID, SORTTYPE? sortType = null, ChildDemands childDemands = null)
    {
        List<MArticle> parentArticles = DBManager.GetAllArticlesExceptParent(LogIn.LoggedIn.Id);
        InstantiateArticles(parentArticles, sortType, childDemands);
    }
    public void InstantiateArticlesChild(int childID, SORTTYPE? sortType = null, ChildDemands childDemands = null)
    {
        List<MArticle> childArticles = DBManager.GetArticlesByChildId(childID);
        InstantiateArticles(childArticles, sortType, childDemands);
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
            CurrentArticles[i].gameObject.transform.localPosition = startPosition + new Vector3(currentColumn * horizontalSpacing + 50, -currentRow * verticalSpacing - 45, 0);

            currentColumn++;

            // hvis den her row er fyldt, så gå et hak ned
            if (currentColumn >= maxItemsPerRow)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
    }


    public void SortAndDisplayArticles(SORTTYPE? sortType = null)
    {

        List<GameObject> newOrder = new List<GameObject>();
        if (sortType.HasValue)
            switch (sortType)
            {
                case SORTTYPE.PRICEASC:
                    newOrder = CurrentArticles.OrderBy(a => a.GetComponent<ClothingItem>().prize ?? float.MaxValue).ToList();
                    break;
                case SORTTYPE.PRICEDESC:
                    newOrder = CurrentArticles.OrderByDescending(a => a.GetComponent<ClothingItem>().prize ?? float.MinValue).ToList();
                    break;
                case SORTTYPE.CONDITIONASC:
                    newOrder = CurrentArticles.OrderBy(a => a.GetComponent<ClothingItem>().condition).ToList();
                    break;
                case SORTTYPE.CONDITIONDESC:
                    newOrder = CurrentArticles.OrderByDescending(a => a.GetComponent<ClothingItem>().condition).ToList();
                    break;
            }

        DestroyItems();
        CurrentArticles = new List<GameObject>();
        CurrentArticles.AddRange(newOrder);
        OrderArticles();


    }

}