using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChildButton : MonoBehaviour
{
    public MChild thisChild;
    public GameObject mindeskovOverlay;

    //set image af child

    private void Start()
    {

        //Set image of child
        //gameObject.GetComponent<Image>();
        //Sprite itemSprite = CreateImage(thisChild.ImageData);
        //clothingImage.sprite = itemSprite != null ? itemSprite : placeholderSprite;
    }

    public void ClickChild()
    {
        mindeskovOverlay.SetActive(true);
        //mindeskovOverlay.GetComponent<MindeskovOverlay>().OpenOverlay(thisChild);
        mindeskovOverlay.GetComponent<MindeskovOverlay>().OpenOverlay(DBManager.GetChildById(1));

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

}
