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
    
    public TextMeshProUGUI SetMaxPriceText;
    public TextMeshProUGUI maxPriceText;

    private void OnEnable()
    {
        //DBManager.Init();
        AllClothes = DBManager.GetAllArticles();
        AllOtherClothes = new List<MArticle>();

        for (int i = 0; i < AllClothes.Count; i++)
        {
            if (DBManager.GetParentByArticleId(AllClothes[i].Id).Id != LogIn.LoggedIn.Id)
            {
                AllOtherClothes.Add(AllClothes[i]);
            }
        }
        

        SortScript = new SortScrollScript();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;

        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(AllOtherClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(AllOtherClothes, setMaxDistance, setMaxPrice);
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
