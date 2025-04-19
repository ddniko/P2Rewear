using TMPro;
using UnityEngine;

public class SetupOverlay : MonoBehaviour
{

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI describtionText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI conditionText;
    public TextMeshProUGUI sustainabilityScore;
    public TextMeshProUGUI Distance;
    public TextMeshProUGUI SellerId;

    public UnityEngine.UI.Image clothingImage;
    public Sprite placeholderSprite;

    private MArticle open;

    //public void setupOverlay(int primaryKey, string name, int childId, string sizeCategory, string category, float condition, int? lifeTime, float? prize, string description, byte[] imageData)
    //{
    //    priceText.text = prize.ToString();
    //    nameText.text = name;
    //    describtionText.text = description;
    //    sizeText.text = sizeCategory;
    //    //category something here
    //    conditionText.text = condition.ToString() + "/5";
    //    sustainabilityScore.text = lifeTime.ToString();
    //    //Distance.text = Distance.ToString(); Mangler udregning til distance
    //    SellerId.text = DBManager.GetParentByArticleId(primaryKey).Name.ToString();

    //    Sprite itemSprite = CreateImage(imageData);
    //    clothingImage.sprite = itemSprite != null ? itemSprite : placeholderSprite;

    //    //something to set parent profile picture down here :)

    //}

    public void setupOverlay(int id)
    {
        open = DBManager.GetArticleById(id);

        priceText.text = open.Prize.ToString();
        nameText.text = open.Name.ToString();
        describtionText.text = open.Description.ToString();
        sizeText.text = open.SizeCategory.ToString();
        //category something here
        conditionText.text = open.Condition.ToString() + "/5";
        sustainabilityScore.text = open.LifeTime.ToString();

        if (SellerId != null || Distance != null)
        {
            //Distance.text = Distance.ToString(); Mangler udregning til distance
            SellerId.text = DBManager.GetParentByArticleId(open.Id).Name.ToString();
        }

        Sprite itemSprite = CreateImage(open.ImageData);
        clothingImage.sprite = itemSprite != null ? itemSprite : placeholderSprite;

        //something to set parent profile picture down here :)

    }

    public void Next()
    {
        if (DBManager.GetAllArticles().Count > open.Id)
        {
            setupOverlay(open.Id + 1);
        }
    }
    public void Previous()
    {
        if (open.Id != 1)
        {
            setupOverlay(open.Id - 1);
        }
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

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
