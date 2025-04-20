using UnityEngine;
using System;
using System.Collections.Generic;

public class Markedsplads : BasePage
{
    public override Enum MyPage() => PAGENAMES.MARKEDSPLADS;

    public GameObject ViewPort;

    public GameObject ClothingPrefab;

    private List<MArticle> AllClothes;

    private SortScrollScript SortScript;

    public static List<MArticle> AllOtherClothes;


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

        SortScript.InstantiateAllArticles(AllOtherClothes);
    }

    private void OnDisable()
    {
        SortScript.DestroyItems();
    }
}
