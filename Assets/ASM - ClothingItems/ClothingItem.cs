using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ClothingItem : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI conditionText;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private TextMeshProUGUI sustainabilityText;
    [SerializeField] private RawImage clothingImageContainer;
    
    private enum availabletags { Clothing, ClothingItem, ClothingItemClothing };
    private List<availabletags> tags = new List<availabletags>();
    private string clothingItemName;
    private string clothingDescription;
    private int price = 0;
    private float distance = 0;
    private float sizeSmallChildren = 0;
    private int sustainabilityScore = 0;
    private int condition = 0;
    private RawImage clothingImage; 

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
        clothingImageContainer = clothingImage;
        priceText.text = price.ToString();
        sizeText.text = sizeSmallChildren.ToString();
        sustainabilityText.text = sustainabilityScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpClothingItem()
    {
        
    }
    
    
    //getters
    public int GetPrice()
    {
        return price;
    }

    public float GetDistance()
    {
        return distance;
    }

    public float GetSizeSmallChildren()
    {
        return sizeSmallChildren;
    }

    public string GetClothingItemName()
    {
        return clothingItemName;
    }
    
    
    // Setters

    public void SetClothingItemName(string newName)
    {
        clothingItemName = newName;
    }

    public void SetClothingDescription(string newDescription)
    {
        clothingDescription = newDescription;
    }

    public void SetPrice(int newPrice)
    {
        price = newPrice;
    }
    
    
    
    
}
