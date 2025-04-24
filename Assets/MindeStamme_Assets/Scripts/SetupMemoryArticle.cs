using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SetupMemoryArticle : MonoBehaviour
{

    public void SetupMemory(MMemory m)

    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI dateText;
    public Image image;
    private Sprite memorySprite;
    public void SetupMemory(MMemory memory)
    {
        titleText.text = memory.Title;
        descriptionText.text = memory.Description;
        dateText.text = memory.DateAdded;
        memorySprite = CreateImage(memory.ImageData);
        image.sprite  = memorySprite;
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
