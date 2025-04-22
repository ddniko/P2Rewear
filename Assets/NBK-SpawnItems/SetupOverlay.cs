using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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

    public GameObject MindestammeObj;


    public GameObject Chat;
    public GameObject BottomBar;

    public UnityEngine.UI.Image clothingImage;
    public Sprite placeholderSprite;

    private MArticle open;

    private ChatManager chatManager;


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
        if (DBManager.GetParentByArticleId(open.Id).Id == LogIn.LoggedIn.Id)
        {
            List<MArticle> Display = DBManager.GetArticlesByParentId(LogIn.LoggedIn.Id);

            int Open = Display.IndexOf(Display.FirstOrDefault(z => z.Id == open.Id));

            if (Open + 1 < Display.Count)
            {
                setupOverlay(Display[Open + 1].Id);
            }
        }
        else
        {
            List<MArticle> Display = Markedsplads.AllOtherClothes;

            int Open = Display.IndexOf(Display.FirstOrDefault(z => z.Id == open.Id));

            if (Open + 1 < Display.Count)
            {
                setupOverlay(Display[Open + 1].Id);
            }
        }
    }
    public void Previous()
    {
        if (DBManager.GetParentByArticleId(open.Id).Id == LogIn.LoggedIn.Id)
        {
            List<MArticle> OwnClothes = DBManager.GetArticlesByParentId(LogIn.LoggedIn.Id);

            int Open = OwnClothes.IndexOf(OwnClothes.FirstOrDefault(z => z.Id == open.Id));

            if (Open - 1 >= 0)
            {
                setupOverlay(OwnClothes[Open - 1].Id);
            }
        }
        else
        {
            List<MArticle> Display = Markedsplads.AllOtherClothes;

            int Open = Display.IndexOf(Display.FirstOrDefault(z => z.Id == open.Id));

            if (Open - 1 >= 0)
            {
                setupOverlay(Display[Open - 1].Id);
            }
        }
    }

    public void StartChat()
    {
        ContactPageManager.AddSellerItem(open);
        ChatManager.instance.SetupChat(open);
        //Chat.SetActive(true);

        gameObject.SetActive(false);

    }

    public void MemStamme()
    {
        //MindestammeObj.SetActive(true);
        //StammeManager.instance.gameObject.SetActive(true);
        StammeManager.instance.StammeStartup(open.Id);
        //StammeManager.instance.clothingArticleID = open.Id;
        gameObject.SetActive(false);
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
