using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class ClothingItem : MonoBehaviour
{
    //[Header("Text Elements")]
    //[SerializeField] private TextMeshProUGUI priceText;
    //[SerializeField] private TextMeshProUGUI distanceText;
    //[SerializeField] private TextMeshProUGUI conditionText;
    //[SerializeField] private TextMeshProUGUI sizeText;
    //[SerializeField] private TextMeshProUGUI sustainabilityText;
    //[SerializeField] private Image clothingImageContainer;

    //private enum availabletags { Clothing, ClothingItem, ClothingItemClothing };
    //private List<availabletags> tags = new List<availabletags>();
    //private string clothingItemName;
    //private string clothingDescription;
    //private int price = 0;
    //private float distance = 0;
    //private float sizeSmallChildren = 0;
    //private int sustainabilityScore = 0;
    //private int condition = 0;
    //private int primaryKey;
    //private int childId;
    //private Image clothingImage;
    //
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI describtionText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI conditionText;
    public TextMeshProUGUI sustainabilityScore;
    public TextMeshProUGUI distanceText;

    public string name;
    public string describtion;

    public UnityEngine.UI.Image clothingImage;
    public Sprite placeholderSprite;

    public int primaryKey;
    public string ClothingName;
    public int childId;
    public int sizeCategory;
    public string category;
    public float condition;
    public int? lifeTime;
    public float? prize;
    public string description;
    public byte[] imageData;

    public GameObject distanceObject;
    private enum sizeOlderChildren
    {
        small, medium, large, xl, xxl
    }

    private enum clothingType
    {
        Pants, TShirt
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //clothingImageContainer = clothingImage;
        //priceText.text = price.ToString();
       // sizeText.text = sizeSmallChildren.ToString();
       // sustainabilityText.text = sustainabilityScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpClothingItem(int primaryKey, string name, int childId, int sizeCategory, float condition, int? lifeTime, float? prize, string description, byte[] imageData, float? distance)
    {
        this.primaryKey = primaryKey;
        this.childId = childId;
        this.ClothingName = name;

        this.sizeCategory = sizeCategory;
        this.distanceText.text = distance.ToString() + " km";
        this.condition = condition;
        this.lifeTime = lifeTime;
        this.prize = prize;
        this.description = description;
        this.imageData = imageData;
        OpenOverlay();

        if (LogIn.LoggedIn.Id == DBManager.GetParentByArticleId(primaryKey).Id)
        {
            distanceObject.SetActive(false);
        }
        else
        {
            distanceObject.SetActive(true);
        }
    }
    public void SetUpClothingItem(MArticle art)
    {
        this.primaryKey = art.Id;
        this.childId = art.ChildId;
        this.ClothingName = art.Name;

        this.sizeCategory = art.Size;

        this.condition = art.Condition;
        this.lifeTime = art.LifeTime;
        this.prize = art.Prize;
        this.description = art.Description;
        this.imageData = art.ImageData;
        OpenOverlay();
    }

    public void OpenOverlay()
    {
        if (prize == null)
            priceText.text = "Gratis";
        else
            priceText.text = $"{prize:F2} kr.";
        //nameText.text = this.name;
        //describtionText.text = description;
        sizeText.text = sizeCategory + " cm";
        conditionText.text = $"{condition:F0}/5";
        if (sustainabilityScore != null)
        {
            if (lifeTime == null)
            {
                sustainabilityScore.text = "0";
            }
            else
            {
                sustainabilityScore.text = lifeTime.ToString();
            }
        }



        // Load image fra bytes stored. skal implementer en placeholder
        Sprite itemSprite = CreateImage(imageData);
        clothingImage.sprite = itemSprite != null ? itemSprite : placeholderSprite;
    }
    public Sprite CreateImage(byte[] imageBytes)
    {

        if (imageBytes != null && imageBytes.Length > 0)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return newSprite;
        }
        else
            return null;
    }
    //getters
    public int GetPrimaryKey()
    {
        return primaryKey;
    }

    public string GetClothingItemName()
    {
        return name;
    }

    public int GetChildId()
    {
        return childId;
    }

    public int GetSizeCategory()
    {
        return sizeCategory;
    }

    public string GetCategory()
    {
        return category;
    }

    public float GetCondition()
    {
        return condition;
    }

    public int? GetLifeTime()
    {
        return lifeTime;
    }

    public float? GetPrize()
    {
        return prize;
    }

    public string GetDescription()
    {
        return description;
    }

    public byte[] GetImageData()
    {
        return imageData;
    }


    // Setters

    public void SetPrimaryKey(int newPrimaryKey)
    {
        primaryKey = newPrimaryKey;
    }

    public void SetClothingItemName(string newName)
    {
        name = newName;
    }

    public void SetChildId(int newChildId)
    {
        childId = newChildId;
    }

    public void SetSizeCategory(int newSizeCategory)
    {
        sizeCategory = newSizeCategory;
    }

    public void SetCategory(string newCategory)
    {
        category = newCategory;
    }

    public void SetCondition(float newCondition)
    {
        condition = newCondition;
    }

    public void SetLifeTime(int? newLifeTime)
    {
        lifeTime = newLifeTime;
    }

    public void SetPrize(float newPrize)
    {
        prize = newPrize;
    }

    public void SetDescription(string newDescription)
    {
        description = newDescription;
    }

    public void SetImageData(byte[] newImageData)
    {
        imageData = newImageData;
    }




}