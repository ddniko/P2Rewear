using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Profil : BasePage
{
    public override Enum MyPage() => PAGENAMES.PROFIL;
    public TMP_Text TrustScore;
    public TMP_Text SustainabilityScore;
    public TMP_Text Name;

    public GameObject ViewPort;

    public GameObject ClothingPrefab;

    private List<MArticle> OwnClothes;

    private SortScrollScript SortScript;

    private int UserId;

    //private void Start() //sets scores and name on profile page to the current parent's scores and name
    //{
    //}




    private void OnEnable()
    {
        UserId = LogIn.LoggedIn.Id;
        TrustScore.text = DBManager.GetParentById(UserId).ReliabilityScore.ToString();
        SustainabilityScore.text = DBManager.GetParentById(UserId).SustainabilityScore.ToString();
        Name.text = DBManager.GetParentById(UserId).Name.ToString();

        //DBManager.Init();
        OwnClothes = DBManager.GetArticlesByParentId(UserId);

        SortScript = new SortScrollScript();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;

        SortScript.InstantiateAllArticles(OwnClothes);
    }

    private void OnDisable()
    {
        SortScript.DestroyItems();
    }

}
