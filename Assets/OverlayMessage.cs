using UnityEngine;

public class OverlayMessage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Canvas clothingOverlay;
    [SerializeField] private ClothingOverlay overlayScript;
    void Start()
    {
        clothingOverlay = GameObject.Find("Overlay").GetComponent<Canvas>();
        overlayScript = GameObject.Find("Overlay").GetComponent<ClothingOverlay>();
    }
    

    public void SendMessage(ClothingItem clothingItem)
    {
        overlayScript.OpenOverlay(clothingItem);
        clothingOverlay.gameObject.SetActive(true);
    }
}
