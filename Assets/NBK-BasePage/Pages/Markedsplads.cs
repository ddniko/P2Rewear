using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Markedsplads : BasePage
{
    public override Enum MyPage() => PAGENAMES.MARKEDSPLADS;

    public GameObject ViewPort;

    public GameObject ClothingPrefab;
    public GameObject BarnSortMarketOverlay;

    private List<MArticle> AllClothes;

    private SortScrollScript SortScript;

    public static List<MArticle> AllOtherClothes;

    private float setMaxDistance = 10000f;
    public TagOrganizer TO;
    private float setMaxPrice = 100000f;
    private Filter filter;

    public TextMeshProUGUI SetMaxPriceText;
    public TextMeshProUGUI maxPriceText;
    public TextMeshProUGUI maxPriceTextMarket;
    public int maxPrice;
    string[] predefinedSizeRanges = { "50/56", "57/63", "64/70", "71/77", "78/84", "85/91", "92/98", "99/105", "106/112", "113/119", "120/128", "129/200" };
    public int currentArticlesPage;
    int articlePages;
    private bool scrollEventTriggered;
    public ScrollRect scrollRect;
    public float scrollTriggerThreshold;
    public GameObject nothingFoundObj;

    public event Action OnScrollThresholdReached;

    private int numericSize;
    private int totalPages;
    private bool displayingChild = false;
    private bool maxprice = false;
    private bool maxdistance = false;
    private bool filterTag = false;

    void Update()
    {
        if (!scrollEventTriggered && scrollRect.verticalNormalizedPosition <= scrollTriggerThreshold && totalPages != currentArticlesPage)
        {
            scrollEventTriggered = true;
            OnScrollThresholdReached?.Invoke();
        }
    }
    public void ResetScrollTrigger()
    {
        maxprice = false;
        displayingChild = false;
        maxdistance = false;
        filterTag = false;
        scrollEventTriggered = false;
    }
    private void OnEnable()
    {
        maxprice = false;
        displayingChild = false;
        maxdistance = false;
        filterTag = false;
        filter = new Filter();
        filter.tags = new List<string>();
        SortScript = GetComponent<SortScrollScript>();
        SortScript.ParentObject = ViewPort.transform;
        SortScript.ClothingPrefab = ClothingPrefab;
        SortScript.ChildParentObject = BarnSortMarketOverlay.transform;
        AllOtherClothes = new List<MArticle>();
        currentArticlesPage = 0;
        DisplayMarketArticles();
        OnScrollThresholdReached += HandleScrollThreshold;
        //DisplayMarketArticles(null, null);
    }

    private void OnDisable()
    {
        SortScript.DestroyItems();
        OnScrollThresholdReached -= HandleScrollThreshold;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();
        currentArticlesPage = 0;

    }
    private void HandleScrollThreshold()
    {
        currentArticlesPage++;
        DisplayMarketArticles(null, filter);
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();
        scrollEventTriggered = false;
    }
    public Filter CreateFilterFromChild(MChild child)
    {
        var filter = new Filter
        {

            Category = new List<string>(),
            tags = new List<string>()
        };

        if (!string.IsNullOrEmpty(child.Size) && int.TryParse(child.Size, out numericSize))
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
            SortScript.CreateChildren(false);
            foreach (var child in SortScript.CurrentChildren)
            {
                var button = child.GetComponentInChildren<Button>();
                var childComponent = child.GetComponent<Child>();
                MChild mChild = childComponent.GetChild;

                var filter = CreateFilterFromChild(mChild);
                childComponent.childFilter = filter;

                button.onClick.AddListener(() => currentArticlesPage = 0);
                button.onClick.AddListener(() => displayingChild = true);
                button.onClick.AddListener(() => maxprice = false);
                button.onClick.AddListener(() => maxdistance = false);
                button.onClick.AddListener(() => filterTag = false);
                button.onClick.AddListener(() => DisplayMarketArticles(null, filter));
                
            }
        }
        BarnSortMarketOverlay.SetActive(true);
    }
    public void DisplayMarketArticles(SORTTYPE? st = null, Filter filter = null)
    {
        scrollRect.verticalNormalizedPosition = 1f;

        //AllOtherClothes = DBManager.GetAllArticlesExceptParent(UserInformation.Instance.User.Id, currentArticlesPage, 20);
        if (displayingChild)
        {
            AllOtherClothes = DBManager.GetFilteredArticles(UserInformation.Instance.User.Id, filter, currentArticlesPage, 20);
            totalPages = (int)Mathf.Ceil(AllOtherClothes.Count / 20f);
        }
        else if (maxprice)
        {
            AllOtherClothes = DBManager.GetArticlesUnderPrice(UserInformation.Instance.User.Id, (int)setMaxPrice, currentArticlesPage, 20);
            totalPages = DBManager.GetArticlesUnderPriceCount(UserInformation.Instance.User.Id, (int)setMaxPrice, 20);
        }
        else if (maxdistance)
        {
            AllOtherClothes = DBManager.GetArticlesUnderDistance(UserInformation.Instance.User.Id, setMaxDistance, currentArticlesPage, 20);
            totalPages = DBManager.GetArticlesUnderDistanceCount(UserInformation.Instance.User.Id, setMaxDistance, 20);
        }
        else if (filterTag)
        {
            AllOtherClothes = DBManager.GetArticlesOfTag(UserInformation.Instance.User.Id, filter.tags, currentArticlesPage, 20);
            totalPages = DBManager.GetArticlesOfTagCount(UserInformation.Instance.User.Id, filter.tags, 20);
        }
        else
        {
            totalPages = DBManager.GetTotalPagesForArticles(20);
            AllOtherClothes = DBManager.GetAllArticlesExceptParent(UserInformation.Instance.User.Id, currentArticlesPage, 20);
            Debug.Log("everything is false");
            if (filter != null)
            {
                Debug.Log("Filter max price = " + filter.maxPrize);
            }

        }

        if (totalPages == 0)
        {
            nothingFoundObj.SetActive(true);
            
        }
        else
        {
            nothingFoundObj.SetActive(false);
        }
        
        Debug.Log("total pages = " + totalPages);


        RectTransform rt = ViewPort.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, MathF.Round(AllOtherClothes.Count / 2f + 0.4f) * 105);

        SortScript.InstantiateArticles(AllOtherClothes);
    }
    public void FilterByTag()
    {
        displayingChild = false;
        maxdistance = false;
        maxprice = false;
        filterTag = true;
        currentArticlesPage = 0;
        if (TO.tagValues.Count == 0)
            return;
        filter = new Filter();
        filter.tags = new List<string>();
        filter.tags.AddRange(TO.tagValues);
        DisplayMarketArticles(null, filter);
        TO.ClearTags();
    }

    public float SetMaxDistance(int i)
    {
        currentArticlesPage = 0;
        setMaxDistance = i;
        return setMaxDistance;
    }
    public void FilterByDistance()
    {
        displayingChild = false;
        maxprice = false;
        filterTag = false;
        maxdistance = true;
        currentArticlesPage = 0;
        DisplayMarketArticles(null, filter); 
    }
    public void SetMaxPrice()
    {
        currentArticlesPage = 0;
        string inputText = SetMaxPriceText.text.Trim().Replace(",", ".");

        // Remove all characters except digits, dot and minus sign
        inputText = Regex.Replace(inputText, @"[^0-9\.\-]", "");

        if (float.TryParse(inputText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float price))
        {
            displayingChild = false;
            maxdistance = false;
            filterTag = false;
            maxprice = true;
            setMaxPrice = price;
            DisplayMarketArticles(null, filter);
            maxPriceText.text = price.ToString("0.##") + " kr"; // format nicely, max 2 decimals
            maxPriceTextMarket.text = price.ToString("0.##") + " kr";;
        }
        else
        {
            Debug.LogWarning("Invalid input for max price: " + inputText);
            maxPriceText.text = "0 kr";
            setMaxPrice = 0f;
        }

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

    public void ResetFilter()
    {
        currentArticlesPage = 0;
        maxprice = false;
        displayingChild = false;
        filterTag = false;
        maxdistance = false;
        filter = new Filter();
        filter.tags = new List<string>();
        DisplayMarketArticles(null, filter);
    }


}
