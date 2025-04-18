using UnityEngine;
using System;
using System.Collections.Generic;

public class Markedsplads : BasePage
{
    public override Enum MyPage() => PAGENAMES.MARKEDSPLADS;

    public GameObject ViewPort;

    public GameObject ClothingPrefab;

    private List<MArticle> AllClothes;

    private GameObject[] Prefabs;

    private void Start()
    {
        DBManager.Init();
        AllClothes = DBManager.GetAllArticles();


        foreach (MArticle Current in AllClothes)
        {
            GameObject Spawn = Instantiate(ClothingPrefab, ViewPort.transform);
            if (Current.Id % 2 == 0)
            {
                Spawn.transform.position = new Vector2(1, -Mathf.Floor((Current.Id - 1) / 2) * 4 - 1);
            }
            else
            {
                Spawn.transform.position = new Vector2(-1, -Mathf.Floor((Current.Id - 1) / 2) * 4 - 1);
            }

            ClothingItem ThisItem = Spawn.GetComponent<ClothingItem>();
            ThisItem.SetUpClothingItem(Current.Id, Current.Name, Current.ChildId, Current.SizeCategory, Current.Category,
                Current.Condition, Current.LifeTime, Current.Prize, Current.Description, Current.ImageData);
        }
    }
}
