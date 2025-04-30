using UnityEngine;
using UnityEngine.UI;

public class Child : MonoBehaviour
{
    public MChild ThisChild;
    public Filter childFilter;
    public void SetupChild(MChild child)
    {
        ThisChild = child;
        if (child.Image != null)
            GetComponent<Image>().sprite = CreateImage(child.Image);
    }
    public MChild GetChild { get { return ThisChild; } }
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

    void OnDisable()
    {
        Destroy(gameObject);
        
    }
}
