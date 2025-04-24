using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverlayMessage : MonoBehaviour
{

    public GameObject OverlayMarket;
    public GameObject OverlayProfile;
    public GameObject canvas;
    public int ChildId;

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

    public void OpenMemoryTree()
    {
        MindeskovOverlay.instance.SortScript.DestroyItems();
        MindeskovOverlay.instance.mindeskovOverlay.SetActive(false);
        StammeManager.instance.StammeStartup(gameObject.GetComponent<ClothingItem>().GetPrimaryKey());
    }
}
