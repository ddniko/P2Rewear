using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class Markedsplads : BasePage
{
    public override Enum MyPage() => PAGENAMES.MARKEDSPLADS;

    public GameObject ViewPort;

    public GameObject ClothingPrefab;

    private List<MArticle> AllClothes;

    private SortScrollScript SortScript;

    public static List<MArticle> AllOtherClothes;

    private float setMaxDistance;

    private float setMaxPrice;
    private Filter filter;

    public TextMeshProUGUI SetMaxPriceText;
    public TextMeshProUGUI maxPriceText;

    private void OnEnable()
    {
        //  TestDataGenerator.GenerateRandomTestData();
        filter = new Filter();
        if (LogIn.LoggedIn != null)
            AllOtherClothes = DBManager.GetAllArticlesExceptParent(LogIn.LoggedIn.Id);
        else
            AllOtherClothes = DBManager.GetAllArticlesExceptParent(1);

        SortScript = GetComponent<SortScrollScript>();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;

        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(AllOtherClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(AllOtherClothes, setMaxDistance, setMaxPrice);
    }

    public void SortViaChild(MChild child)
    {
        filter = new Filter();
        filter.SizeCategory[0] = child.Size;
        filter.

    }
    public float SetMaxDistance(TextMeshProUGUI inputText)
    {
        setMaxDistance = float.Parse(inputText.text);
        return setMaxDistance;
    }

    public void SetMaxPrice()
    {
        maxPriceText.text = SetMaxPriceText.text + " kr";
        setMaxPrice = float.Parse(SetMaxPriceText.text);
    }



    private void OnDisable()
    {
        SortScript.DestroyItems();
    }
}
