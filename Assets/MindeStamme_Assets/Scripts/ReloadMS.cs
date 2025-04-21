using UnityEngine;

public class ReloadMS : MonoBehaviour
{
    public GameObject MSPageContent;

    public void ReloadMSPage()
    {
        MSPageContent.SetActive(false);
        MSPageContent.SetActive(true);
    }
}
