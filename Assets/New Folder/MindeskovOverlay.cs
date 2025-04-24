using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

public class MindeskovOverlay : MonoBehaviour
{
    public SortScrollScript SortScript;
    public GameObject ViewPort;
    public GameObject ClothingPrefab;
    public GameObject mindeskovOverlay;

    public static MindeskovOverlay instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    public void OpenOverlay(MChild child)
    {
        mindeskovOverlay.SetActive(true);
        List<MArticle> childClothes = DBManager.GetArticlesByChildId(child.Id);

        SortScript = new SortScrollScript();
        SortScript.ParentObject = ViewPort.transform;
        ClothingPrefab.GetComponent<OverlayMessage>().ChildId = child.Id;
        SortScript.ClothingPrefab = ClothingPrefab;
        SortScript.horizontalSpacing = 120;

        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(childClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(childClothes);
    }
}
