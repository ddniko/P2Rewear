using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContactButtonScript : MonoBehaviour
{
    private MArticle clothingItem;


    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI conditionText;
    [SerializeField] private TextMeshProUGUI lastMessageText;
    [SerializeField] private Image image;

    public MArticle Article;
    

    public void SetupClothingItem(MArticle clothingItem)
    {
        this.clothingItem = clothingItem;
        //name.text = clothingItem.paren;
        priceText.text = clothingItem.Prize.ToString();
        //Distance
        sizeText.text = clothingItem.Size.ToString();
        scoreText.text = clothingItem.LifeTime.ToString();
        conditionText.text = clothingItem.Condition.ToString();
        //Last message
    }

    public void ActivateOverlay()
    {
        ChatManager.instance.SetupChat(Article);

    }
    
}
