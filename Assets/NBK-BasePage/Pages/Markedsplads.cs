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


    private void OnEnable()
    {
        //DBManager.Init();
        AllClothes = DBManager.GetAllArticles();
        List<MArticle> AllOtherClothes = new List<MArticle>();

        for (int i = 0; i < AllClothes.Count; i++)
        {
            if (DBManager.GetParentByArticleId(AllClothes[i].Id).Id != LogIn.LoggedIn.Id)
            {
                AllOtherClothes.Add(AllClothes[i]);
                //AllClothes.RemoveAt(i);
                //Debug.Log("remove");
            }
        }


        SortScript = new SortScrollScript();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;

        SortScript.InstantiateAllArticles(AllOtherClothes);
    }

    private void OnDisable()
    {
        SortScript.DestroyItems();
    }
}
