using UnityEngine;

public class MSBttnOverlayManager : MonoBehaviour
{
    
    [SerializeField] private GameObject mindestammeOverlayEditOrCreateOwnMem;
    [SerializeField] private GameObject mindestammeOverlayUserMems;
    //[SerializeField] private GameObject mindestammePAGE;
    [SerializeField] private GameObject mindeskovClothingArticleOverlay;
    private SetupMemoryArticle setup; // we set-up / format the overlays with information when the overlay is being activated

    public void OpenUserMemOverlay(MMemory memory) // this is the on click enter method for the memory overlay
    {
        mindestammeOverlayUserMems.SetActive(true);
        setup = mindestammeOverlayUserMems.GetComponent<SetupMemoryArticle>();
        setup.SetupMemory(memory);
    }

    public void CloseUserMemOverlay()
    {
        mindestammeOverlayUserMems.SetActive(false);
    }


    public void OpenOwnEditOrCreateMemOverlay(MMemory memory)
    {
        mindestammeOverlayEditOrCreateOwnMem.SetActive(true);
        setup = mindestammeOverlayEditOrCreateOwnMem.GetComponent<SetupMemoryArticle>();
        setup.SetupMemory(memory);
    }

    public void CloseOwnEditOrCreateMemOverlay()
    {
        mindestammeOverlayEditOrCreateOwnMem.SetActive(false);
    }

    public void OpenMindeskovClothingArticleOverlay(MMemory memory) // this is the on click enter method for the memory overlay
    {
        mindeskovClothingArticleOverlay.SetActive(true);
        setup = mindeskovClothingArticleOverlay.GetComponent<SetupMemoryArticle>();
        setup.SetupMemory(memory);
    }

    public void CloseMindeskovClothingArticleOverlay()
    {
        mindestammeOverlayUserMems.SetActive(false);
    }
}
