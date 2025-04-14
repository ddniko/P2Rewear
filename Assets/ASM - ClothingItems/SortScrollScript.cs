using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;
using static UnityEngine.Rendering.DebugUI.Table;


public class SortScrollScript : MonoBehaviour
{

    public GameObject[] itemsVisible = new GameObject[5];
    public GameObject[] itemsSpots = new GameObject[5];
    public List<GameObject> CurrentArticles;
    public GameObject content;
    public GameObject ClothingPrefab;
    public Transform ParentObject;
    [Header("ItemPositions")]
    public int maxItemsPerRow = 5;
    public float horizontalSpacing = 2f;// de her bestemmer hvor meget rum der skal v�re imellem dem. kunne g�res til public fields s� man kunne �ndre dem i inspectoren
    public float verticalSpacing = 2f;
    public int currentRow = 0;
    public int currentColumn = 0;
    private Vector3 startPosition = new Vector3(0, 0, 0);

    void Start()
    {
        sortItems();
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
        //Mathf.abs g�r at det er i positive tal, alts� ikke -13, men bare 13 eks.
        InstantiateAllArticles();
    }
    private void OnDisable()
    {
        for (int i = 0; i < CurrentArticles.Count; i++)
        {
            Destroy(CurrentArticles[i].gameObject);
        }
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

    }
    public GameObject CreateArticle(MArticle article)
    {
        GameObject newArticle = Instantiate(ClothingPrefab, ParentObject);

        ClothingItem articleItem = newArticle.GetComponent<ClothingItem>();
        articleItem.SetUpClothingItem(article.SizeCategory);
        return newArticle;
    }
    public void SortArticles()
    {
        for (int i = 0; i < CurrentArticles.Count ; i++)  //index for r�kken
        {
            CurrentArticles[i].gameObject.transform.position = startPosition + new Vector3(currentColumn * horizontalSpacing, -currentRow * verticalSpacing, 0);
            
            currentColumn++;

            // hvis den her row er fyldt, s� g� et hak ned
            if (currentColumn >= maxItemsPerRow)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
    }
    public void sortItems()
    {
        for (int i = 0; i < itemsVisible.Length; i++)
        {
            Instantiate(itemsVisible[i], itemsSpots[i].transform.position, Quaternion.identity, content.transform);


        }


    }
}
