using UnityEngine;

public class TuturialManagerAVersion : MonoBehaviour
{
    private bool beenInMarketPlace = false;

    private bool beenInProfile = false;
    
    private bool beenInMemoryForest = false;
    
    private bool beenInMemoryTree = false;
    
    public GameObject tutorialMarketPlaceObject;
    public GameObject tutorialProfileObject;
    public GameObject tutorialTreeObject;
    public GameObject tutorialForestObject;


    public void StartTuturialMarketplace()
    {
        if (!beenInMarketPlace)
        {
            tutorialMarketPlaceObject.SetActive(true);
            beenInMarketPlace = true;
        }
        
    }

    public void StartTuturialProfile()
    {
        if (!beenInProfile)
        {
            tutorialProfileObject.SetActive(true);
            beenInProfile = true;
        }
        
    }

    public void StartTuturialTree()
    {
        if (!beenInMemoryTree)
        {
            tutorialTreeObject.SetActive(true);
            beenInMemoryTree = true;
        }
    }

    public void StartTuturialForest()
    {
        if (!beenInMemoryForest)
        {
            tutorialForestObject.SetActive(true);
            beenInMemoryForest = true;
        }
    }
    
}
