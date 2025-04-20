using TMPro;
using UnityEngine;

public class OverlayMessage : MonoBehaviour
{
    // start is called once before the first execution of update after the monobehaviour is created
    //[serializefield] private canvas clothingoverlay;
    // [serializefield] private clothingoverlay overlayscript;
    // void start()
    // {
    //     clothingoverlay = gameobject.find("overlay").getcomponent<canvas>();
    //     overlayscript = gameobject.find("overlay").getcomponent<clothingoverlay>();
    //     // clothingoverlay.enabled = false;
    // }


    // public void sendmessage(clothingitem clothingitem)
    // {
    //     overlayscript.openoverlay(clothingitem);
    //     clothingoverlay.gameobject.setactive(true);
    // }

    public GameObject OverlayMarket;
    public GameObject OverlayProfile;
    public GameObject canvas;

    private ClothingItem CurrentArticle;


    public void OpenOverlay()
    {
        GameObject newOverlay;
        canvas = GameObject.Find("CanvasOverlays");
        CurrentArticle = gameObject.GetComponent<ClothingItem>();

        if (LogIn.LoggedIn.Id == DBManager.GetParentByArticleId(CurrentArticle.primaryKey).Id)
        {
            newOverlay = Instantiate(OverlayProfile, canvas.transform);
        }
        else
        {
            newOverlay = Instantiate(OverlayMarket, canvas.transform);
        }

        newOverlay.GetComponent<SetupOverlay>().setupOverlay(CurrentArticle.primaryKey);
    }
}
