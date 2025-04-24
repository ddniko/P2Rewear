using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Markedsplads : BasePage
{
    public override Enum MyPage() => PAGENAMES.MARKEDSPLADS;

    public GameObject ViewPort;

    public GameObject ClothingPrefab;
    public GameObject BarnSortMarketOverlay;

    private List<MArticle> AllClothes;

    private SortScrollScript SortScript;

    public static List<MArticle> AllOtherClothes;

    private float setMaxDistance;
    public TagOrganizer TO;
    private float setMaxPrice;
    private Filter filter;

    public TextMeshProUGUI SetMaxPriceText;
    public TextMeshProUGUI maxPriceText;


    private void OnEnable()
    {
        filter = new Filter();
        SortScript = GetComponent<SortScrollScript>();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;
        SortScript.ChildParentObject = BarnSortMarketOverlay.transform;
        DisplayMarketArticles();
    }
    public Filter CreateFilterFromChild(MChild child)
    {
        var filter = new Filter
        {
            SizeCategory = new List<string>(),
            Category = new List<string>(), 
            tags = new List<ClothingTags>() 
        };
        
        if (!string.IsNullOrEmpty(child.Size))
            filter.SizeCategory.Add(child.Size);

        if (!string.IsNullOrEmpty(child.Tags))
        {
            string[] tagArray = child.Tags.Split(',');
            foreach (var tag in tagArray)
            {
                if (Enum.TryParse(tag.Trim(), true, out ClothingTags parsedTag))
                {
                    filter.tags.Add(parsedTag);
                }
            }
        }

        return filter;
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

                var filter = CreateFilterFromChild(mChild);
                childComponent.childFilter = filter;

                button.onClick.AddListener(() => DisplayMarketArticles(null, filter));
            }
        }
        BarnSortMarketOverlay.SetActive(true);
    }
    public void DisplayMarketArticles(SORTTYPE? st = null, Filter filter = null)
    {
        
        if (LogIn.LoggedIn != null)
            AllOtherClothes = DBManager.GetAllArticlesExceptParent(LogIn.LoggedIn.Id);
        else
            AllOtherClothes = DBManager.GetAllArticlesExceptParent(1);

        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(AllOtherClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(AllOtherClothes, setMaxDistance, setMaxPrice, st, filter);
    }
    public void FilterByTag()
    {
        if (TO.tagValues.Count == 0)
            return;
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
