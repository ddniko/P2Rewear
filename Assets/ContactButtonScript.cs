using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class ContactButtonScript : MonoBehaviour
{
    private ClothingItem clothingItem;
    public ChatManager chatManager;


    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI conditionText;
    [SerializeField] private TextMeshProUGUI lastMessageText;
    [SerializeField] private Image image;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupClothingItem(ClothingItem clothingItem)
    {
        this.clothingItem = clothingItem;
        //name.text = clothingItem.paren;
        priceText.text = clothingItem.prize.ToString();
        //Distance
        sizeText.text = clothingItem.sizeCategory;
        scoreText.text = clothingItem.sustainabilityScore.ToString();
        conditionText.text = clothingItem.condition.ToString();
        //Last message
    }

    public void ActivateOverlay()
    {
        chatManager.SetupChat(clothingItem);
    }
    
}
