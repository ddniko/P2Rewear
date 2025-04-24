using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

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

        SortScript.InstantiateArticles(childClothes);
    }


    private void OnDisable()
    {
        
    }
}
