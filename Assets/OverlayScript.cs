using UnityEngine;

public class OverlayScript : MonoBehaviour
{

    [SerializeField] private GameObject sellerArticleOverlay;
    [SerializeField] private GameObject ownArticleOverlay;
    [SerializeField] private GameObject notificationOverlay;
    private SetupClothingArticle setup;
    
    public void OpenSellerArticleOverlay(ClothingItem clothingItem)
    {
        sellerArticleOverlay.SetActive(true);
        setup = sellerArticleOverlay.GetComponent<SetupClothingArticle>();
        setup.SetupClothing(clothingItem);
    }
    
    public void CloseSellerArticleOverlay()
    {
        sellerArticleOverlay.SetActive(false);
    }

    public void OpenOwnArticleOverlay(ClothingItem clothingItem)
    {
        ownArticleOverlay.SetActive(true);
        setup = ownArticleOverlay.GetComponent<SetupClothingArticle>();
        setup.SetupClothing(clothingItem);
    }
    
    public void CloseOwnArticleOverlay()
    {
        ownArticleOverlay.SetActive(false);
    }

    public void OpenNotificationOverlay()
    {
        notificationOverlay.SetActive(true);
    }
    
    public void CloseNotificationOverlay()
    {
        notificationOverlay.SetActive(false);
    }
}
