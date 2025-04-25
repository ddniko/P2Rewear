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

    string[] predefinedSizeRanges = { "50/56", "57/63", "64/70", "71/77", "78/84", "85/91", "92/98", "99/105", "106/112", "113/119", "120/128" };
    public int currentArticlesPage;
    int articlePages;
    private bool scrollEventTriggered;
    public ScrollRect scrollRect;
    public float scrollTriggerThreshold;

    public event Action OnScrollThresholdReached;


    void Update()
    {
        if (!scrollEventTriggered && scrollRect.verticalNormalizedPosition <= scrollTriggerThreshold)
        {
            scrollEventTriggered = true;
            OnScrollThresholdReached?.Invoke();
        }
    }
    public void ResetScrollTrigger()
    {
        scrollEventTriggered = false;
    }
    private void OnEnable()
    {
        filter = new Filter();
        SortScript = GetComponent<SortScrollScript>();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;
        SortScript.ChildParentObject = BarnSortMarketOverlay.transform;
        DisplayMarketArticles();
        OnScrollThresholdReached += HandleScrollThreshold;
    }

    private void OnDisable()
    {
        SortScript.DestroyItems();
        OnScrollThresholdReached -= HandleScrollThreshold;
    }
    private void HandleScrollThreshold()
    {
        DisplayMarketArticles(null, filter);
    }
    public Filter CreateFilterFromChild(MChild child)
    {
        var filter = new Filter
        {

            Category = new List<string>(),
            tags = new List<string>()
        };

        if (!string.IsNullOrEmpty(child.Size) && int.TryParse(child.Size, out int numericSize))
        {
            var sizeRange = FindSizeRangeForValue(numericSize);
            if (sizeRange != null)
            {
                filter.MinSize = sizeRange.Value.min;
                filter.MaxSize = sizeRange.Value.max;
            }
        }

        if (!string.IsNullOrEmpty(child.Tags))
        {
            string[] tagArray = child.Tags.Split(',');
            foreach (var tag in tagArray)
            {
                var trimmedTag = tag.Trim();
                if (!string.IsNullOrEmpty(trimmedTag))
                {
                    filter.tags.Add(trimmedTag);
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

        int totalPages = DBManager.GetTotalPagesForArticles(20);
        AllOtherClothes = DBManager.GetAllArticlesExceptParent(UserInformation.Instance.User.Id, currentArticlesPage, totalPages);

        // if (LogIn.LoggedIn != null)
        //   AllOtherClothes = DBManager.GetAllArticlesExceptParent(LogIn.LoggedIn.Id);
        //else
        //  AllOtherClothes = DBManager.GetAllArticlesExceptParent(1);


        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(AllOtherClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(AllOtherClothes, setMaxDistance, setMaxPrice, st, filter);
    }
    public void FilterByTag()
    {
        if (TO.tagValues.Count == 0)
            return;
        filter = new Filter();
        filter.tags = new List<string>();
        filter.tags.AddRange(TO.tagValues);
        DisplayMarketArticles(null, filter);
        TO.ClearTags();
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
    private (int min, int max)? FindSizeRangeForValue(int size)
    {
        foreach (var range in predefinedSizeRanges)
        {
            var parts = range.Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out int min) && int.TryParse(parts[1], out int max))
            {
                if (size >= min && size <= max)
                    return (min, max);
            }
        }

        return null;
    }



}
