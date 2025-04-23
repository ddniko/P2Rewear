using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profil : BasePage
{
    public override Enum MyPage() => PAGENAMES.PROFIL;
    public TMP_Text TrustScore;
    public TMP_Text SustainabilityScore;
    public TMP_Text Name;

    public GameObject ViewPort;
    public GameObject ChildPrefab;
    
    public GameObject ClothingPrefab;
    public GameObject ProfileSortMarketOverlay;
    private List<MArticle> OwnClothes;
    private SortScrollScript SortScript;

    private int UserId;

    private void OnEnable()
    {
        UserId = LogIn.LoggedIn.Id;
        TrustScore.text = DBManager.GetParentById(UserId).ReliabilityScore.ToString();
        SustainabilityScore.text = DBManager.GetParentById(UserId).SustainabilityScore.ToString();
        Name.text = DBManager.GetParentById(UserId).Name.ToString();

        //DBManager.Init();
        if (LogIn.LoggedIn != null)
            OwnClothes = DBManager.GetAllArticlesExceptParent(LogIn.LoggedIn.Id);
        else
            OwnClothes = DBManager.GetAllArticlesExceptParent(1);

        SortScript = GetComponent<SortScrollScript>();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;

        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(OwnClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(OwnClothes);
    }
    public void DisplayChildren()
    {
        if (SortScript.CurrentChildren.Count <= 0)
        {
            SortScript.CreateChildren();
            foreach (var child in SortScript.CurrentChildren)
            {
                var button = child.GetComponent<Button>();
                var childComponent = child.GetComponent<Child>();
                MChild mChild = childComponent.GetChild;



                button.onClick.AddListener(() => FilterByChild(mChild.Id));
            }
        }
        ProfileSortMarketOverlay.SetActive(true);
    }
    public void FilterByChild(int i)
    {
        List<MArticle> articles = DBManager.GetArticlesByChildId(i);
        SortScript.InstantiateArticles(articles);
    }
    private void OnDisable()
    {
        SortScript.DestroyItems();
    }

}
