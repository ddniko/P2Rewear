using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothingOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI describtionText;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI sustainabilityText;
    [SerializeField] private TextMeshProUGUI conditionText;
    
    [SerializeField] private Image clothingImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OpenOverlay(ClothingItem clothingItem)
    //{
    //    priceText.text = clothingItem.GetPrice().ToString();
    //    nameText.text = clothingItem.GetClothingItemName();
    //    describtionText.text = clothingItem.GetDescription();
    //    sizeText.text = clothingItem.GetSizeSmallChildren().ToString();
    //    distanceText.text = clothingItem.GetDistance().ToString();
    //    sustainabilityText.text = clothingItem.GetSustainabilityScore().ToString();
    //    conditionText.text = clothingItem.GetCondition().ToString();
        
    //    clothingImage.sprite = clothingItem.GetClothingImage().sprite;
        
    //}
    
    
}
