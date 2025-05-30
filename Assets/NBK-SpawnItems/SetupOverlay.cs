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
    public TextMeshProUGUI TrustScore;
    public GameObject MindestammeObj;
    public TextMeshProUGUI Tag;


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
        sizeText.text = open.Size.ToString();
        //category something here
        conditionText.text = open.Condition.ToString() + "/5";
        Tag.text = "Tag: " + open.Tags.ToString();
        if (sustainabilityScore != null)
        {
            if (open.LifeTime == null)
            {
                sustainabilityScore.text = "0";
            }
            else
            {
                sustainabilityScore.text = open.LifeTime.ToString();
            }
        }

        if (SellerId != null || Distance != null)
        {
            Distance.text = DBManager.GetParentByArticleId(open.Id).Distance.ToString("F2");
            Distance.text += " KM";
            SellerId.text = DBManager.GetParentByArticleId(open.Id).Name.ToString();
            TrustScore.text = DBManager.GetParentByArticleId(open.Id).ReliabilityScore.ToString();
        }

        if (DBManager.GetArticleById(id).ImageData != null)
        {
            clothingImage.sprite = CreateImage(DBManager.GetArticleById(id).ImageData);
        }
        else
        {
            clothingImage.sprite = placeholderSprite;
        }


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
        gameObject.SetActive(false);

    }

    public void MemStamme()
    {
        StammeManager.instance.StammeStartup(open.Id);
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
